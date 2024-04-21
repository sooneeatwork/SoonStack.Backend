using SharedKernel.Domain.DomainModel.OrderModel;

namespace OrderSlices.Repository.OrderTableMapper
{
    public class OrderTableMap : IOrderTableMap
    {

        public Dictionary<string, object> CreateMap(Order order) 
        {
            Dictionary<string,object> result = new Dictionary<string,object>();

            result.Add(nameof(OrderTable.CustomerId), order.CustomerId);
            result.Add(nameof(OrderTable.OrderDate), DateTime.Now);
            return result;
        }
    }
}
