using OrderSlices.Domain;
using OrderSlices.Repository.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSlices.Repository.OrderItemTableMapper
{
    public class OrderItemTableMapper : IOrderItemTableMap
    {
        public Dictionary<string, object> CreateMap(OrderItem order)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add(nameof(OrderItem.OrderId), order.Id);
            return result;
        }

        public IEnumerable<Dictionary<string, object>> CreateMap(IReadOnlyList<OrderItem> orderItems)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            foreach (var item in orderItems)
            {
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>
                {
                    { nameof(OrderItem.OrderId), item.OrderId },
                    { nameof(OrderItem.ProductId), item.ProductId },
                    { nameof(OrderItem.Quantity), item.Quantity },
                    { nameof(OrderItem.Price), item.Price }
                };

                result.Add(keyValuePairs);
            }

            return result;
        }

        
    }
}
