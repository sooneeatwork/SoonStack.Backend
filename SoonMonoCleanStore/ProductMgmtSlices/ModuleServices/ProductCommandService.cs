using Core.Domain.DomainModel.ProductModel;
using Core.PersistanceShared;
using Core.UseCases.ProductSlices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.ModuleServices
{
    public class ProductCommandService : IProductCommandServices
    {
        private IProductRepository _productRepository;
        private IProductTableMappers _productTableMapper;
        public ProductCommandService(IProductRepository productRepository, IProductTableMappers productTableMappers)
        {
            _productRepository = productRepository;
            _productTableMapper = productTableMappers;
        }
        public async Task<int> GetProductStockCount(int productId)
        {
            var result = await _productRepository.GetStockCountByIdAsync(productId);

            return result;
        }

        public async Task<int> UpdateProductStockCount(Dictionary<long, int> productPurchasedQtyDict, IDbTransaction? dbTrans = null)
        {
            int result = 0;
            try
            {
                var allProductIds = productPurchasedQtyDict.Keys.ToList();
                var productList = await _productRepository.GetByIdListAsync<Product>(allProductIds);
                List<Product> modifiedProductList = new List<Product>();

                if (productList == null)
                    throw new Exception($@"Products not found");

                foreach (Product product in productList)
                {
                    productPurchasedQtyDict.TryGetValue(product.Id, out int purchasedQty);
                    product.RemoveStock(purchasedQty);
                    modifiedProductList.Add(product);
                }

                // Assuming _productTableMapper.CreateMapForUpdateStockCount creates a list of objects suitable for your stored procedure.
                var updateData = _productTableMapper.CreateMapForUpdateStockCount(modifiedProductList);

                // Convert updateData to JSON or the required format for your stored procedure
                var jsonUpdateData = JsonConvert.SerializeObject(updateData);

                // Assuming "UpdateProductStock" is the name of your stored procedure
                result = await _productRepository.ExecuteStoredProcedureAsync<Product>(
                    DatabaseStoreProdName.SP_BulkUpdateProduct,
                    new { jsonData = jsonUpdateData },
                    dbTrans);
            }
            catch
            {
                throw;
            }

            return result;
        }

    }
}
