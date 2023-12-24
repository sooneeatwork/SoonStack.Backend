
namespace EMS.Core.TicketMgmt
{
    public class TicketPurchaseHistory : BaseEntity
    {
        public long Id { get; set; }

        public long CustomerId { get; set; }

        public long EventId { get; set; }

        public long TicketId { get; set; }

        public DateTime PurchaseDate { get; set; }

        public int Quantity { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public string PaymentMethod { get; set; } = string.Empty;
    }
}
