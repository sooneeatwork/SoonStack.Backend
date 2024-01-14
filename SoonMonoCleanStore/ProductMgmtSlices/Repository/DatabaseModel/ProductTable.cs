using System;

namespace ProductMgmtSlices.Repository.DatabaseModel
{
    public class ProductTable
    {
        // Corresponding to the database fields
        public const string TableName = "products.products";
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public int stock_quantity { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }

     

     

        

        // Additional methods if needed for database-related operations
        // ...
    }
}
