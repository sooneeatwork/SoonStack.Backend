using SharedKernel.Domain.DomainModel.OrderModel;

namespace OrderSlices.Repository.OrderTableMapper
{
    public interface IOrderTableMap
    {
        public Dictionary<string, object> CreateMap(Order order);
    }
}