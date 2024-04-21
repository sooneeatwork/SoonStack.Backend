namespace SharedKernel.Domain.DomainModel.OrderModel
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; private set; }
        public int CustomerId { get; private set; }

        private readonly List<OrderItem> _orderItems = new List<OrderItem>();

        // Private constructor to enforce the use of factory methods


        // Factory method to create a new order for a customer
        public static Order CreateOrder(int customerId)
        {
            if (customerId <= 0)
            {
                throw new ArgumentException("Customer ID must be greater than zero", nameof(customerId));
            }

            // You can add additional validation logic here

            return new Order
            {
                OrderDate = DateTime.UtcNow,
                CustomerId = customerId
            };
        }

        // Method to add order items to the order
        public void TryAddOrderItem(long orderId,
                                     long productId,
                                     int quantity,
                                     decimal price)
        {
            var orderItem = OrderItem.CreateOrderItem(orderId, productId, quantity, price);
            _orderItems.Add(orderItem);
        }

        public (bool, string) CanProceed(int stockCount, int quantity)
        {
            return quantity <= 0 ? (false, "Quantity must be greater than zero.") :
                   stockCount <= 0 ? (false, "Stock count must be greater than zero.") :
                   stockCount < quantity ? (false, "Insufficient stock.") :
                   (true, "Can proceed");
        }


        public IReadOnlyList<OrderItem> GetOrderItems() => _orderItems.AsReadOnly();

        internal void AddOrderItem(OrderItem orderItem)
        {
            if (orderItem != null)
            {
                OrderItem.CreateOrderItem(orderItem.OrderId,
                                        orderItem.ProductId,
                                        orderItem.Quantity,
                                        orderItem.Price);
                _orderItems.Add(orderItem);
            }


        }
    }
}
