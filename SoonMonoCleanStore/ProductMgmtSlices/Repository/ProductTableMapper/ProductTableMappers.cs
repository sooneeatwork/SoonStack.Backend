using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductMgmtSlices.Repository.ProductTableMapper
{
    public class ProductTableMappers : IProductTableMappers
    {
        public object CreateMapForUpdateStockCount(List<Product> modifiedProductList)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, object> MapToTableForInsert(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }

            product.CreatedDate = DateTime.Now;
            product.CreatedBy = 1;
            product.ModifiedDate = DateTime.Now;
            product.ModifiedBy = 1;

              var propertyMap = new Dictionary<string, object>
            {
                { nameof(ProductTable.name), product.Name },
                { nameof(ProductTable.description), product.Description },
                { nameof(ProductTable.price), product.Price },
                { nameof(ProductTable.stock_quantity), product.StockQuantity },
                { nameof(ProductTable.modified_by), product.ModifiedBy },
                { nameof(ProductTable.modified_date), product.ModifiedDate },
                { nameof(ProductTable.created_by), product.CreatedBy },
                { nameof(ProductTable.created_date), product.CreatedDate }
                // Map other properties if needed...
            };

            return propertyMap;
        }

        public (Dictionary<string, object> dataFields,
                 Dictionary<string, object> whereClause)
          CreateMapForUpdate(Product modifiedProduct, ProductTable originalProduct)
        {
            Dictionary<string, object> dataFields = new Dictionary<string, object>();
            Dictionary<string, object> whereClause = new Dictionary<string, object>();

            ArgumentNullException.ThrowIfNull(modifiedProduct, nameof(modifiedProduct));
            ArgumentNullException.ThrowIfNull(originalProduct, nameof(originalProduct));

            whereClause.Add(nameof(ProductTable.id), originalProduct.id);

            if (modifiedProduct.Name != originalProduct.name)
                dataFields.Add(nameof(ProductTable.name), modifiedProduct.Name);

            if (modifiedProduct.Price != originalProduct.price)
                dataFields.Add(nameof(ProductTable.price), modifiedProduct.Price);

            if (modifiedProduct.StockQuantity != originalProduct.stock_quantity)
                dataFields.Add(nameof(ProductTable.stock_quantity), modifiedProduct.StockQuantity);

            dataFields.Add(nameof(ProductTable.modified_by), 1);
            dataFields.Add(nameof(ProductTable.modified_date), DateTime.Now);

            return (dataFields, whereClause);
        }

        public Product MapToDomain(ProductTable productTable)
        {
            if (productTable == null)
            {
                throw new ArgumentNullException(nameof(productTable), "product Table cannot be null");
            }
            

            Product product = new Product();
            product.Price = productTable.price;
            product.Name = productTable.name;
            product.Id = productTable.id;
            product.CreatedBy = productTable.created_by.GetValueOrDefault();
            product.CreatedDate = productTable.created_date;
            product.ModifiedBy = productTable.modified_by.GetValueOrDefault();
            product.ModifiedDate = productTable.modified_date;
            product.Description = productTable.description;
            product.StockQuantity = productTable.stock_quantity;    

            return product;
        }
    }
}
