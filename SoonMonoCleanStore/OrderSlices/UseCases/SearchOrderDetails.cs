using Infrastructure.Logging;
using SharedKernel.UseCases.Wrapper;

namespace OrderSlices.UseCases
{
    public record GetOrderDetailsQuery(long OrderId) : IRequest<Result<OrderDetailsDto>>;
    public class OrderDetailsDto
    {
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItemDto>? Items { get; set; }
        public OrderDetailsDto()
        {
            Items = new List<OrderItemDto>(); // Initialize the Items list in the constructor
        }
    }

    public record OrderItemDto(long ProductId, int Quantity, decimal Price);

    public class GetOrderDetailsHandler : IRequestHandler<GetOrderDetailsQuery, Result<OrderDetailsDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetOrderDetailsHandler(IOrderRepository orderRepository, IMapper mapper, ILogger logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<OrderDetailsDto>> Handle(GetOrderDetailsQuery query, CancellationToken cancellationToken)
        {
            Result<OrderDetailsDto> result;

            try
            {
                var orderDetails = await _orderRepository.GetOrderDetailsByIdAsync(query.OrderId);

                if (orderDetails == null || !orderDetails.Any())
                    return Result<OrderDetailsDto>.Failure("Order not found.");

                var orderInfo = orderDetails.FirstOrDefault();
                var orderDetailsDto = _mapper.Map<OrderDetailsView?, OrderDetailsDto>(orderInfo);

                var items = orderDetails.Select(od => new OrderItemDto(od.ProductId, od.Quantity, od.Price));
                orderDetailsDto.Items?.AddRange(items);

                result = Result<OrderDetailsDto>.Success(orderDetailsDto);
            }
            catch (Exception ex)
            {
                // Log the exception details if necessary
                _logger.LogError(nameof(GetOrderDetailsQuery), ex);
                result = Result<OrderDetailsDto>.Failure($"Failed to get order detail");
            }

            return result;
        }




    }
}
