namespace ProductMgmtSlices.Repository.ProductTableMapper
{
    public class ProductTableMappers : IProductTableMappers
    {

        public Dictionary<string, object> CreateMapForUpdate(Product product)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add(nameof(ProductTable.id), product.Id);
            return result;
        }

        public Dictionary<string, object> CreateMapForInsert(Product product)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            result.Add(nameof(ProductTable.name), product.Name);
            result.Add(nameof(ProductTable.price), product.Price);
            result.Add(nameof(ProductTable.description), product.Description);
            result.Add(nameof(ProductTable.stock_quantity), product.StockQuantity);

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

            whereClause.Add(nameof(ProductTable.id), originalProduct.id);

            if (modifiedProduct.Name != originalProduct.name)
                dataFields.Add(nameof(ProductTable.name), modifiedProduct.Name);

            if (modifiedProduct.Price != originalProduct.price)
                dataFields.Add(nameof(ProductTable.price), modifiedProduct.Price);

            if (modifiedProduct.StockQuantity != originalProduct.stock_quantity)
                dataFields.Add(nameof(ProductTable.stock_quantity), modifiedProduct.StockQuantity);

            return (dataFields, whereClause);
        }

        public Product MapToDomain(ProductTable productData)
        {
            throw new NotImplementedException();
        }
    }
}
