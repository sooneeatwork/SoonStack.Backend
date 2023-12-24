using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSlices.Repository.DatabaseModel
{
    public class OrderItemsTable
    {
        public const string TableName = "OrderItem";
        public int OrderId { get; set; }
        public long ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

    }
}
