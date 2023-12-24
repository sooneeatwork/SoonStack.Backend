using EMS.Shared.Domain.EventMgmt;
using EMS.Shared.Domain.PromoMgmt;
using EMS.Shared.Promotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Modules.PromoMgmt.Domain.DomainServices
{
    public class DiscountService : IDiscountService
    {
        private readonly IEnumerable<IDiscountStrategy> _discountStrategies;

        public DiscountService(IEnumerable<IDiscountStrategy> discountStrategies)
        {
            _discountStrategies = discountStrategies ?? 
                                  throw new ArgumentNullException(nameof(discountStrategies));
        }


        public decimal CalculateTotalDiscountAmt(DiscountCriteria discountCriteria)
        {
          
            decimal discountAmount = 0.00m;
            foreach (var strategy in _discountStrategies)
            {
                if (strategy.CanApplyDiscount(discountCriteria))
                     discountAmount += strategy.ApplyDiscount(discountCriteria);
            }

            return discountAmount; 
        }


    }

}
