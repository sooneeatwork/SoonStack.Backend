namespace OrderSlices.Domain
{
    public class OrderItem : BaseEntity
    {
      
        public long OrderId { get; private set; }
        public long ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

       

        public static OrderItem CreateOrderItem(long orderId, long productId, int quantity, decimal price)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("Order ID must be greater than zero", nameof(orderId));
            }

            if (productId <= 0)
            {
                throw new ArgumentException("Product ID must be greater than zero", nameof(productId));
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero", nameof(quantity));
            }

            if (price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero", nameof(price));
            }

            // You can add additional validation logic here

            return new OrderItem
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = quantity,
                Price = price
            };
        }

       
    }


}
