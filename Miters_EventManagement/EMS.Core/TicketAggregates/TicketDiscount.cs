using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Core.TicketMgmt
{
    public class TicketDiscount :  BaseEntity
    {
        public long Id { get; set; }

        public long TicketId { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal BasePrice { get; set; }

        public decimal FinalAmount { get; set; }
        private TicketDiscount() { }

        private TicketDiscount(long ticketId, decimal basePrice, decimal discountAmt)
        {
            if(ticketId == 0)
                throw new ArgumentException(nameof(ticketId));

            TicketId = ticketId;
            BasePrice = basePrice;
            DiscountAmount = discountAmt;
            decimal result = basePrice - discountAmt;
            FinalAmount = result <= 0 ? 0 : result;
        }

        // Static factory method to create a new TicketDiscount instance
        public static TicketDiscount ApplyDiscount(long ticketId, 
                                                   decimal baseAmt, 
                                                   decimal discountAmt)
        {
            return new TicketDiscount(ticketId, baseAmt, discountAmt);
        }
    }
}
