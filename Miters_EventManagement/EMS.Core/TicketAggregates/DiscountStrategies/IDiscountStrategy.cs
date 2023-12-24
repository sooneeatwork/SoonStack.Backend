namespace EMS.Core.TicketMgmt.DiscountStrategies
{
    public interface IDiscountStrategy
    {
        bool CanApplyDiscount(DiscountCriteria discountCriteria);
        decimal ApplyDiscount(DiscountCriteria discountCriteria);
    }



}