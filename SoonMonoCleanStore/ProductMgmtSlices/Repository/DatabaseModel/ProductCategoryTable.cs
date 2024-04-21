using ProductMgmtSlices.Domain;

namespace ProductMgmtSlices.Repository.DatabaseModel
{
    public class ProductCategoryTable
    {
        public const string TableName = "Product.ProductCategory";
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string category_description { get; set; } = string.Empty;
        public decimal is_active { get; set; }
       
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }

    }
}
