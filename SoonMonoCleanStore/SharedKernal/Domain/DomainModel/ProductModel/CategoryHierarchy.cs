namespace SharedKernel.Domain.DomainModel.ProductModel
{
    public class CategoryHierarchy : BaseEntity
    {
        public long ParentCategoryId { get; set; }
        public ProductCategory? ParentCategory { get; set; }

        public long ChildCategoryId { get; set; }
        public ProductCategory? ChildCategory { get; set; }

        public int HierarchyDepth { get; set; }



        public static CategoryHierarchy Create(ProductCategory parentCategory, ProductCategory childCategory, int currentDepth)
        {
            // Assuming the depth is always 1 level deeper than the parent.
            // This might need adjustment based on your specific logic for depth calculation.
            int depth = currentDepth + 1;

            return new CategoryHierarchy
            {
                ParentCategory = parentCategory,
                ParentCategoryId = parentCategory.Id,
                ChildCategory = childCategory,
                ChildCategoryId = childCategory.Id,
                HierarchyDepth = depth
            };
        }



        public void UpdateParentAndDepth(ProductCategory? newParentCategory, int currentDepth)
        {

            ParentCategoryId = newParentCategory == null ? throw new ArgumentNullException() : newParentCategory.Id;
            ParentCategory = newParentCategory;
            HierarchyDepth = currentDepth + 1;
        }
    }

}
