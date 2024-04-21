[assembly: InternalsVisibleTo("CleanStoreTest")]
namespace SharedKernel.Domain.DomainModel.ProductModel
{
    public class Product : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public int StockQuantity { get; private set; }

        // Private constructor to enforce the use of the factory method


        // Static factory method for creating a new product instance
        public static Product CreateProduct(string name, decimal price, string description, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name cannot be empty", nameof(name));
            }

            if (price < 0)
            {
                throw new ArgumentException("Price cannot be negative", nameof(price));
            }

            if (quantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative", nameof(price));
            }

            return new Product
            {
                Name = name,
                Price = price,
                Description = description,
                StockQuantity = quantity  // Default stock quantity is set to zero
            };
        }

        // Methods to manipulate the product
        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
            {
                throw new ArgumentException("Price cannot be negative", nameof(newPrice));
            }

            Price = newPrice;
        }

        public void UpdateDescription(string newDescription)
        {
            if (string.IsNullOrWhiteSpace(newDescription))
            {
                throw new ArgumentException("Description cannot be empty", nameof(newDescription));
            }

            Description = newDescription;
        }

        public void AddStock(int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative", nameof(quantity));
            }

            StockQuantity += quantity;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative", nameof(quantity));
            }

            if (StockQuantity < quantity)
            {
                throw new InvalidOperationException("Insufficient stock to remove");
            }

            StockQuantity -= quantity;
        }

        public static bool IsProductExists(int productCount)
        {
            return productCount > 0;
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
    }
}
