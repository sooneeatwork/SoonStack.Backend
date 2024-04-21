[assembly: InternalsVisibleTo("CleanStoreTest")]
namespace SharedKernel.Domain.DomainModel.ProductModel
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

        public static Product CreateProduct(string name,
                                            string description,
                                            decimal price,
                                            int stockQuantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be empty", nameof(name));


            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Product description cannot be empty", nameof(description));


            if (price < 0)
                throw new ArgumentException("Price cannot be negative", nameof(price));


            if (stockQuantity < 0)
                throw new ArgumentException("Quantity cannot be negative", nameof(stockQuantity));


            return new Product
            {
                Name = name,
                Price = price,
                Description = description,
                StockQuantity = stockQuantity  // Default stock quantity is set to zero
            };
        }



        public void RemoveStock(int purchasedQty)
        {
            throw new NotImplementedException();
        }



        public void UpdateProductInfo(string newName,
                                             string newDescription,
                                             decimal newPrice,
                                             int newStockQuantity)
        {
            Product product = new Product();
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
