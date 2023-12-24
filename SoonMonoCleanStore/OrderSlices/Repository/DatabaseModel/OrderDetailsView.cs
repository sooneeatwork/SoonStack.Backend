using System;

namespace OrderSlices.Repository.DatabaseModel
{
    public class OrderDetailsView
    {
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public long CustomerId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
