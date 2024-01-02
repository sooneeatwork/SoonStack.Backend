using System;

namespace ProductMgmtSlices.Repository.DatabaseModel
{
    public class ProductTable
    {
        // Corresponding to the database fields
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

     

        // Constructor for initializing a new instance
        public ProductTable(string name, string description, decimal price, int stockQuantity, int? createdBy = null, int? modifiedBy = null, DateTime? createdDate = null, DateTime? modifiedDate = null)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            CreatedBy = createdBy;
            ModifiedBy = modifiedBy;
            CreatedDate = createdDate ?? DateTime.UtcNow;
            ModifiedDate = modifiedDate ?? DateTime.UtcNow;
        }

        // Additional methods if needed for database-related operations
        // ...
    }
}
