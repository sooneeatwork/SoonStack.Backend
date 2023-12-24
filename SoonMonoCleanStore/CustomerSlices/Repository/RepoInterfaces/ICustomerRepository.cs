using CustomerSlices.Repository.DatabaseModel;
using SharedKernel.Domain.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerSlices.Repository.RepoInterfaces
{
    public interface ICustomerRepository : IGenericRepository
    {
        Task<CustomersTable?> GetByEmailAsync(string newEmail);
        Task<int> GetCountByEmailAsync(string newEmail);
    }
}
