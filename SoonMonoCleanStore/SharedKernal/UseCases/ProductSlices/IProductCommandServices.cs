using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.UseCases.ProductSlices
{
    public interface IProductCommandServices
    {
        Task<int> UpdateProductStockCount(Dictionary<long, int> productPurchasedQtyDict, 
                                          IDbTransaction? dbTrans = null);


    }

}
