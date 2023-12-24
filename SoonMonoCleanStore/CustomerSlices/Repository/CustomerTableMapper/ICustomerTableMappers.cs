using CustomerSlices.Repository.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSlices.Repository.CustomerTableMapper
{
    public interface ICustomerTableMappers
    {
        (Dictionary<string, object>,
        Dictionary<string, object>) CreateMapForUpdate(Customer newCustomer,CustomersTable originalCustomer);

        Dictionary<string, object> CreateMapForInsert(Customer customer);
    }
}
