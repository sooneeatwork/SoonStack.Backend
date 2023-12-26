namespace ProductSlices.Repository.ProductTableMapper
{
    public class ProductTableMappers : IProductTableMappers
    {

        public Dictionary<string, object> CreateMapForUpdate(Product product)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add(nameof(ProductTable.Id), product.Id);
            return result;
        }

        public Dictionary<string, object> CreateMapForInsert(Product product)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add(nameof(ProductTable.Name), product.Name);
            result.Add(nameof(ProductTable.Price), product.Price);
            result.Add(nameof(ProductTable.Description), product.Description);
            result.Add(nameof(ProductTable.StockQuantity), product.StockQuantity);

            return result;
        }

        public IEnumerable<Dictionary<string, object>> CreateMap(IReadOnlyList<Product> productList)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            foreach (var product in productList)
            {
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>
                {
                    { nameof(product.StockQuantity), product.StockQuantity }
                };

                result.Add(keyValuePairs);
            }

            return result;
        }

        public IEnumerable<Dictionary<string, object>>
            CreateMapForUpdateStockCount(List<Product> productList)
        {
            List<Dictionary<string, object>> dataFields = new List<Dictionary<string, object>>();

            foreach (var product in productList)
            {
                Dictionary<string, object> keyValuePairs = new Dictionary<string, object>
                {
                    { nameof(product.Id), product.Id },
                    { nameof(product.StockQuantity), product.StockQuantity }
                };

                dataFields.Add(keyValuePairs);
            }

            return dataFields;
        }


        public (Dictionary<string, object> dataFields,
                Dictionary<string, object> whereClause) 
         CreateMapForUpdate(Product modifiedProduct, ProductTable originalProduct)
        {
            Dictionary<string, object> dataFields = new Dictionary<string, object>();
            Dictionary<string, object> whereClause = new Dictionary<string, object>();

            ArgumentNullException.ThrowIfNull(modifiedProduct, nameof(modifiedProduct));
            ArgumentNullException.ThrowIfNull(originalProduct, nameof(originalProduct));

            whereClause.Add(nameof(ProductTable.Id), originalProduct.Id);

            if (modifiedProduct.Name != originalProduct.Name)
                dataFields.Add(nameof(ProductTable.Name), modifiedProduct.Name);

            if (modifiedProduct.Price != originalProduct.Price)
                dataFields.Add(nameof(ProductTable.Price), modifiedProduct.Price);

            if (modifiedProduct.StockQuantity != originalProduct.StockQuantity)
                dataFields.Add(nameof(ProductTable.StockQuantity), modifiedProduct.StockQuantity);

            return (dataFields, whereClause);
        }
    }
}
