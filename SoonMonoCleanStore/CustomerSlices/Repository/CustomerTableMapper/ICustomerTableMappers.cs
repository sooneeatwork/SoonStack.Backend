using Core.Domain.DomainModel.CustomerModel;

namespace CustomerSlices.Repository.CustomerTableMapper
{
    public interface ICustomerTableMappers
    {
        (Dictionary<string, object>,
        Dictionary<string, object>) CreateMapForUpdate(Customer newCustomer,CustomersTable originalCustomer);

        Dictionary<string, object> CreateMapForInsert(Customer customer);
    }
}
