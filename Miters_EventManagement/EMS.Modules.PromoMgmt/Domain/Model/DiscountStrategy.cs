using EMS.Modules.PromoMgmt.Domain.DomainServices;
using EMS.Shared.Domain.PromoMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Modules.PromoMgmt.Domain.Model
{

    public class LoyaltyDiscountStrategy : IDiscountStrategy
    {
        public bool CanApplyDiscount(DiscountCriteria criteria)
        {
            // Logic to determine if the customer is loyal based on past events
            return criteria.CustomerPurchasedTicketCount > 1;
        }

        public decimal ApplyDiscount(DiscountCriteria criteria)
        {
            return criteria.BasePrice * 0.1m; // 10% discount
        }
    }

    public class BulkBookingDiscountStrategy : IDiscountStrategy
    {
        public bool CanApplyDiscount(DiscountCriteria criteria)
        {
            // Logic to determine if the purchase is a bulk booking
            return criteria.TicketQuantity > 10;
        }

        public decimal ApplyDiscount(DiscountCriteria criteria)
        {
            return criteria.BasePrice * 0.3m; // 10% discount
        }
    }

    











}
