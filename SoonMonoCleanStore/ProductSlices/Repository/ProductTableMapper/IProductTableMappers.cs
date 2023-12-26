namespace ProductSlices.Repository.ProductTableMapper
{
    public interface IProductTableMappers
    {
        Dictionary<string, object> CreateMapForUpdate(Product product);
        Dictionary<string, object> CreateMapForInsert(Product product);
        IEnumerable<Dictionary<string, object>> CreateMapForUpdateStockCount(List<Product> productList);

        (Dictionary<string, object> dataFields, 
         Dictionary<string, object> whereClause) CreateMapForUpdate(Product modifiedProduct, 
                                                                    ProductTable originalProduct);
    }
}
