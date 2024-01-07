using System;

namespace ProductMgmtSlices.Repository.DatabaseModel
{
    public class ProductTable : IProductTableMappers
    {
        // Corresponding to the database fields
        public const string TableName = "Products.Products";
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

        // Additional methods if needed for database-related operations
        // ...
    }
}
