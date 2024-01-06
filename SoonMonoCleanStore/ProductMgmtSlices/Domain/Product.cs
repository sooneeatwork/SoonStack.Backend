using SharedKernal.Domain;
using System;

namespace ProductMgmtSlices.Domain
{
    public class Product : BaseEntity
    {
        // Properties specific to Product
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public int StockQuantity { get; private set; }
       

      

        // Public constructor
        public Product(string name, 
                       string description, 
                       decimal price, 
                       int stockQuantity, 
                       long? createdBy = null, 
                       long? modifiedBy = null)
        {
            Name = name;
            Description = description;
            Price = price;
            StockQuantity = stockQuantity;
            CreatedBy = createdBy.GetValueOrDefault();
            ModifiedBy = modifiedBy.GetValueOrDefault();
        }

        // Methods to update the entity
        public void UpdateStock(int quantity)
        {
            // Implement logic to update stock
            StockQuantity = quantity;
        }

        internal static bool IsProductExists(object productCount)
        {
            throw new NotImplementedException();
        }

        internal static object CreateProduct(string name, decimal price, string description, object quantity)
        {
            throw new NotImplementedException();
        }
    }
}
