using Core.UseCases.ProductSlices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.ModuleServices
{
    public class ProductQueryService : IProductQueryServices
    {
        private IProductRepository _productRepository;
        public ProductQueryService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<int> GetProductStockCount(int productId)
        {
            var result = await _productRepository.GetStockCountByIdAsync(productId);

            return result;
        }
    }
}
