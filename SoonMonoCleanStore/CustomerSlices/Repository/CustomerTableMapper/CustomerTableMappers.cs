using Core.Domain.DomainModel.CustomerModel;
using Core.Domain.ValueObject;

namespace CustomerSlices.Repository.CustomerTableMapper
{
    public class CustomerTableMappers: ICustomerTableMappers
    {

        public (Dictionary<string, object>, 
                Dictionary<string, object>) 
        CreateMapForUpdate(Customer newCustomer, CustomersTable originalCustomer)
        {
            Dictionary<string, object> dataFields = new Dictionary<string, object>();
            Dictionary<string, object> whereClause = new Dictionary<string, object>();

            ArgumentNullException.ThrowIfNull(newCustomer, nameof(newCustomer));
            ArgumentNullException.ThrowIfNull(originalCustomer, nameof(originalCustomer));

            whereClause.Add(nameof(CustomersTable.Id), originalCustomer.Id);

            if (newCustomer.Name != originalCustomer.Name)
                dataFields.Add(nameof(CustomersTable.Name), newCustomer.Name);

            if (newCustomer.DateOfBirth != originalCustomer.DateOfBirth)
                dataFields.Add(nameof(CustomersTable.DateOfBirth), newCustomer.DateOfBirth);

            if (newCustomer.Email != originalCustomer.Email)
                dataFields.Add(nameof(CustomersTable.Email), newCustomer.Email);

            if (newCustomer.BillingAddress != null &&
                Address.AreAddressesDifferent(newCustomer.BillingAddress, originalCustomer.BillingAddress!))
            {
                var billingAddress = newCustomer.BillingAddress;
                dataFields.Add(nameof(billingAddress.Country), billingAddress.Country);
                dataFields.Add(nameof(billingAddress.State), billingAddress.State);
                dataFields.Add(nameof(billingAddress.City), billingAddress.City);
                dataFields.Add(nameof(billingAddress.PostalCode), billingAddress.PostalCode);
                dataFields.Add(nameof(billingAddress.Street), billingAddress.Street);
            }

            return (dataFields,whereClause);
        }


        public Dictionary<string, object> CreateMapForInsert(Customer customer)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add(nameof(CustomersTable.Name), customer.Name);
            result.Add(nameof(CustomersTable.DateOfBirth), customer.DateOfBirth);
            result.Add(nameof(CustomersTable.Email), customer.Email);
            if (customer.BillingAddress != null)
            {
                var billingAddress = customer.BillingAddress;
                result.Add(nameof(billingAddress.Country), billingAddress.Country);
                result.Add(nameof(billingAddress.State), billingAddress.State);
                result.Add(nameof(billingAddress.City), billingAddress.City);
                result.Add(nameof(billingAddress.PostalCode), billingAddress.PostalCode);
                result.Add(nameof(billingAddress.Street), billingAddress.Street);
            }
            return result;
        }
    }
}
