namespace EMS.Core.TicketMgmt
{
    public class DiscountCriteria
    {

        public int CustomerPurchasedTicketCount { get; set; }
        public int TicketQuantity { get; set; }
        public decimal BasePrice { get; set; }

        public DiscountCriteria() { }

        public static DiscountCriteria Create(int customerPurchasedTicketCount,
                                              int ticketQuantity,
                                              decimal ticketBasePrice)
        {
            DiscountCriteria criteria = new DiscountCriteria();

            criteria.CustomerPurchasedTicketCount = customerPurchasedTicketCount;
            criteria.TicketQuantity = ticketQuantity;
            criteria.BasePrice = ticketBasePrice;

            return criteria;
        }
    }
}
