[assembly: InternalsVisibleTo("CleanStoreTest")]
namespace ProductMgmtSlices.Domain
{
    public class Product : BaseEntity
    {
        // Properties specific to Product
        public long Id { get; internal set; }
        public string Name { get; internal set; } = string.Empty;
        public string Description { get; internal set; } = string.Empty;
        public decimal Price { get; internal set; } 
        public int StockQuantity { get; internal set; }

        // Methods to update the entity
        public void UpdateStock(int quantity)
        {
            // Implement logic to update stock
            StockQuantity = quantity;
        }

        public static bool IsProductExists(int productCount)
        {
            return productCount > 0;
        }

        public static Product CreateProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("Product name cannot be empty", nameof(product.Name));


            if (string.IsNullOrWhiteSpace(product.Description))
                throw new ArgumentException("Product description cannot be empty", nameof(product.Description));


            if (product.Price < 0)
                throw new ArgumentException("Price cannot be negative", nameof(product.Price));


            if (product.StockQuantity < 0)
                throw new ArgumentException("Quantity cannot be negative", nameof(product.StockQuantity));


            return new Product
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                StockQuantity = product.StockQuantity  // Default stock quantity is set to zero
            };
        }



        internal void RemoveStock(int purchasedQty)
        {
            throw new NotImplementedException();
        }

       

        public void UpdateProductInfo(string newName,
                                      string newDescription,
                                      decimal newPrice,
                                      int newStockQuantity)
        {
            if (newStockQuantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative", nameof(newStockQuantity));
            }

            if (newPrice < 0)
            {
                throw new ArgumentException("Price cannot be negative", nameof(newPrice));
            }

            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Name cannot be empty", nameof(newName));
            }

            Name = newName;
            Price = newPrice;
            StockQuantity = newStockQuantity;
            Description = newDescription;
        }

        public static (bool IsValid, string ErrorMessage) Validate(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return (false, "Product name cannot be empty");
            if (string.IsNullOrWhiteSpace(product.Description))
                return (false, "Product description cannot be empty");
            if (product.Price < 0)
                return (false, "Price cannot be negative");
            if (product.StockQuantity < 0)
                return (false, "Quantity cannot be negative");

            return (true, string.Empty);
        }
    }
}
