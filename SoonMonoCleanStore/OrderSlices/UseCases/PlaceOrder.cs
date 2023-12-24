using DapperPersistence;
using SharedKernel.Domain.RepoInterface;
using System.Data;

namespace OrderSlices.UseCases
{
    public record PlaceOrderCommand(int CustomerId, List<ProductOrderDto> ProductOrders) : IRequest<Result<long>>;

    // DTO for product order details
    public record ProductOrderDto(int ProductId, int Quantity, decimal Price);

    public class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand, Result<long>>
    {
      
        private readonly IGenericRepository _genericRepository;
        private readonly IOrderTableMap _orderTableMap;
        private readonly IOrderItemTableMap _orderItemsTableMap;
        private readonly IProductQueryServices _productQueryServices;
        private readonly IProductCommandServices _productCommandServices;
        private readonly IUnitOfWork _unitOfWork;

        public PlaceOrderHandler(IGenericRepository genericRepository,
                                 IOrderTableMap orderTableMap,
                                 IOrderItemTableMap orderItemsTableMap,
                                 IProductQueryServices productQueryServices,
                                 IProductCommandServices productCommandServices,
                                 IUnitOfWork unitOfWork)
        {
           
            _genericRepository = genericRepository;
            _orderTableMap = orderTableMap;
            _orderItemsTableMap = orderItemsTableMap;
            _productQueryServices = productQueryServices;
            _productCommandServices = productCommandServices;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<long>> Handle(PlaceOrderCommand command, CancellationToken cancellationToken)
        {
            // Validate the customer ID
            if (command.CustomerId <= 0)
                return Result<long>.Failure("Invalid customer ID.");

            using (var dbTrans = _unitOfWork.BeginTransaction())
            {
                try
                {
                    // Create a new order
                    var result = await ProcessPlaceOrder(command, dbTrans);

                    if(result.IsSuccess)
                        _unitOfWork.CommitTransaction(dbTrans);

                    return result;
                }
                catch (Exception ex)
                {
                    _unitOfWork.RollbackTransaction(dbTrans);
                    // Log the exception if necessary
                    return Result<long>.Failure($"Error placing order: {ex.Message}");
                }
            }
        }

        private async Task<Result<long>> ProcessPlaceOrder(PlaceOrderCommand command, IDbTransaction dbTrans)
        {
            var order = Order.CreateOrder(command.CustomerId);
            var orderTableData = _orderTableMap.CreateMap(order);
            long newOrderId = await _genericRepository.InsertOneGetIdAsync<OrderTable>(orderTableData, dbTrans);
            Dictionary<long, int> productQuantityDict = new Dictionary<long, int>();
            // Process each product order
            foreach (var productOrder in command.ProductOrders)
            {
                int stockCount = await _productQueryServices.GetProductStockCount(productOrder.ProductId);
                var (canAddOrder,message) = order.CanProceed(stockCount, productOrder.Quantity);

                if(!canAddOrder)
                    return Result<long>.Failure(message);

                order.TryAddOrderItem(newOrderId,
                                       productOrder.ProductId, 
                                       productOrder.Quantity,
                                       productOrder.Price);

                productQuantityDict.Add(productOrder.ProductId, productOrder.Quantity);
            }

            var orderItemsTableData = _orderItemsTableMap.CreateMap(order.GetOrderItems());
            await _genericRepository.InsertManyAsync<OrderItemsTable>(orderItemsTableData, dbTrans);
            await _productCommandServices.UpdateProductStockCount(productQuantityDict,dbTrans);
            return Result<long>.Success(newOrderId);
        }
    }
}
