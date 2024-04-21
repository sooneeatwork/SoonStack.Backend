using Core.Domain.DomainModel.OrderModel;

namespace OrderSlices.Repository.OrderItemTableMapper
{
    public interface IOrderItemTableMap
    {
        public IEnumerable<Dictionary<string, object>> CreateMap(IReadOnlyList<OrderItem> orderItem);
    }

   
}
