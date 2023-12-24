using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSlices.Repository.OrderItemTableMapper
{
    public interface IOrderItemTableMap
    {
        public IEnumerable<Dictionary<string, object>> CreateMap(IReadOnlyList<OrderItem> orderItem);
    }

   
}
