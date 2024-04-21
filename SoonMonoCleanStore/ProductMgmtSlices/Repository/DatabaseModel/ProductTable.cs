using ProductMgmtSlices.UseCases.ProductUseCases;
using SharedKernel.Domain.DomainModel.ProductModel;
using System;

namespace ProductMgmtSlices.Repository.DatabaseModel
{
    public class ProductTable : IProductTableMappers
    {
        // Corresponding to the database fields
        public const string TableName = "Products.Products";
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int stock_quantity { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }

        internal static IEnumerable<ProductDto> ToProductDTO(IEnumerable<ProductTable> products)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> CreateMapForInsert(Product product)
        {
            throw new NotImplementedException();
        }

        public (Dictionary<string, object> dataFields, Dictionary<string, object> whereClause) CreateMapForUpdate(Product product, ProductTable productData)
        {
            throw new NotImplementedException();
        }

        public object CreateMapForUpdateStockCount(List<Product> modifiedProductList)
        {
            throw new NotImplementedException();
        }

        public object MapToDomain(ProductTable productData)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> MapToTableForInsert(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }

            var propertyMap = new Dictionary<string, object>
            {
                { nameof(product.Name), product.Name },
                { nameof(product.Description), product.Description },
                { nameof(product.Price), product.Price },
                { nameof(product.StockQuantity), product.StockQuantity },
                { nameof(product.ModifiedBy), product.ModifiedBy },
                { nameof(product.ModifiedDate), product.ModifiedDate },
                { nameof(product.CreatedBy), product.CreatedBy },
                { nameof(product.CreatedDate), product.CreatedDate }
                // Map other properties if needed...
            };

            return propertyMap;
        }

        Product IProductTableMappers.CreateMapForUpdateStockCount(List<Product> modifiedProductList)
        {
            throw new NotImplementedException();
        }

        Product IProductTableMappers.MapToDomain(ProductTable productData)
        {
            throw new NotImplementedException();
        }

        // Additional methods if needed for database-related operations
        // ...
    }
}
