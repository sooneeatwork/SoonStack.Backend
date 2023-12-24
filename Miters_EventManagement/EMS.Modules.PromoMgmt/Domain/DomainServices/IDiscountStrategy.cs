using EMS.Shared.Domain.PromoMgmt;

namespace EMS.Modules.PromoMgmt.Domain.DomainServices
{
    public interface IDiscountStrategy
    {
        bool CanApplyDiscount(DiscountCriteria discountCriteria);
        decimal ApplyDiscount(DiscountCriteria discountCriteria);
    }



}