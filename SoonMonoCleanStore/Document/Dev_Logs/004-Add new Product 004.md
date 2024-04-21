---

## Log 004: Development Domain Model: Product.cs

---

When creating a new module, I always start with the domain and application layers first, as they form the core logic of my API. Technical concerns such as database queries, logging libraries, and sending emails will be addressed later, once the core logic is functioning well. These aspects might require time to prepare and study to find the best tools for the job.

I've adopted a concept called the Rich Domain Model. The rationale behind using this approach is that significant business logic related to the product model is required in every use case/feature. If we encapsulate this logic within the domain model, it allows for a substantial amount of code reuse.

Take, for instance, the validation of the product model. When creating a product object, it's crucial to ensure that the object is in a valid state. One method is to scatter if-else conditions throughout your logic, but this leads to code duplication. What if new rules for creating a product are introduced in the future? We would need to scour the entire project to locate where the validations are and update them. By employing a Rich Model, I can centralize all validation logic and business logic related to the product model within the `Product.cs` class. This makes it reusable everywhere, establishing `Product.cs` as the single source of truth for all domain logic.

```csharp
[assembly: InternalsVisibleTo("CleanStoreTest")]
namespace ProductMgmtSlices.Domain
{
    public class Product : BaseEntity
    {
        // Properties specific to Product
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

        internal static bool IsProductExists(int productCount)
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
                StockQuantity = product.StockQuantity
            };
        }
    }
}
```