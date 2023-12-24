using SharedKernel.Domain.RepoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSlices.Repository.Repository.RepoInterfaces
{
    public interface IOrderRepository: IGenericRepository
    {
        Task<IEnumerable<OrderDetailsView>> GetOrderDetailsByIdAsync(long orderId);
    }
}
