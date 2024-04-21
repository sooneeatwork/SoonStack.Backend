------------------------------------------------
## Log 009: Product Category Model : ProductCategory.cs
------------------------------------------------

```csharp
namespace ProductSlices.Domain
{
    public class ProductCategory : BaseEntity
    {
        public string CategoryName { get; set; } = string.Empty;
        public string CategoryDescription { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        // Navigation properties for hierarchical structure
        public ICollection<ProductCategory> SubCategories { get; set; } = new List<ProductCategory>();
        public ICollection<CategoryHierarchy> ChildHierarchies { get; set; } = new List<CategoryHierarchy>();
        public ICollection<CategoryHierarchy> ParentHierarchies { get; set; } = new List<CategoryHierarchy>();

        // Navigation property for products belonging to this category
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public static (List<string> validationErrors, ProductCategory productCategory) 
            CreateProductCategory(string categoryName,
                                  string categoryDescription,
                                  bool isActive = true)
        {
            var validationErrors = new List<string>();

            if (string.IsNullOrWhiteSpace(categoryName))
                validationErrors.Add("CategoryName is required.");

            if (string.IsNullOrWhiteSpace(categoryDescription))
                validationErrors.Add("CategoryDescription is required.");

            var productCategory = new ProductCategory
            {
                CategoryName = categoryName,
                CategoryDescription = categoryDescription,
                IsActive = isActive
            };

            return (validationErrors, productCategory);
        }
    }
}
```