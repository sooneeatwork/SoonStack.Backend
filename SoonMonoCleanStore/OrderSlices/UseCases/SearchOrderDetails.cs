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

        public GetOrderDetailsHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Result<OrderDetailsDto>> Handle(GetOrderDetailsQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var orderDetails = await _orderRepository.GetOrderDetailsByIdAsync(query.OrderId);

                if (orderDetails == null || !orderDetails.Any())
                    return Result<OrderDetailsDto>.Failure("Order not found.");

                var orderInfo = orderDetails.FirstOrDefault();
                var orderDetailsDto = _mapper.Map<OrderDetailsView?, OrderDetailsDto>(orderInfo);

                var items = orderDetails.Select(od => new OrderItemDto(od.ProductId, od.Quantity, od.Price));
                orderDetailsDto.Items?.AddRange(items);

                return Result<OrderDetailsDto>.Success(orderDetailsDto);
            }
            catch (Exception ex)
            {
                // Log the exception details if necessary
                return Result<OrderDetailsDto>.Failure($"An error occurred: {ex.Message}");
            }
        }




    }
}
