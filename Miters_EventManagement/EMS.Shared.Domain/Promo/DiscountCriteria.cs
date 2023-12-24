using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Shared.Domain.Promo
{
    public class DiscountCriteria
    {
        public decimal BasePrice { get; set; }
        public int UserPastEventCount { get; set; }
        public int TicketQuantity { get; set; }
        public decimal DiscountPercentage { get; set; }

        public DiscountCriteria(decimal basePrice, int userPastEventCount, int ticketQuantity, decimal discountPercentage)
        {
            BasePrice = basePrice;
            UserPastEventCount = userPastEventCount;
            TicketQuantity = ticketQuantity;
            DiscountPercentage = discountPercentage;
        }
    }

}
