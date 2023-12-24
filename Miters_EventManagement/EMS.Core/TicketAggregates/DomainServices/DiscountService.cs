
using EMS.Core.TicketMgmt.DiscountStrategies;


namespace EMS.Core.TicketMgmt.DomainServices
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
