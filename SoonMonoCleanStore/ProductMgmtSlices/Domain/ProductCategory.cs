namespace ProductMgmtSlices.Domain
{
    public class ProductCategory : BaseEntity
    {
        public int Id { get; set; }
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

        public static (bool isValid, string errorMessage) Validate(ProductCategory productCategory)
        {
            
            var errorMessages = new List<string>();

            if (string.IsNullOrWhiteSpace(productCategory.CategoryName))
            {
                errorMessages.Add("CategoryName is required.");
            }

            if (string.IsNullOrWhiteSpace(productCategory.CategoryDescription))
            {
                errorMessages.Add("CategoryDescription is required.");
            }
          
            if (productCategory.SubCategories.Any(sub => sub.Id != productCategory.Id))
            {
                errorMessages.Add("All subcategories must have a correct reference to the parent category.");
            }

          
            bool isValid = !errorMessages.Any();
            string errorMessage = isValid ? string.Empty : string.Join("; ", errorMessages);

            return (isValid, errorMessage);
        }

        public void UpdateCategoryDetails(string categoryName, string categoryDescription, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(categoryName) || string.IsNullOrWhiteSpace(categoryDescription))
            {
                throw new ArgumentException("CategoryName and CategoryDescription cannot be null or whitespace.");
            }

            CategoryName = categoryName;
            CategoryDescription = categoryDescription;
            IsActive = isActive;
        }

        public void AddSubCategory(ProductCategory childCategory)
        {
            throw new NotImplementedException();
        }
    }
}
