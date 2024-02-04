namespace ProductMgmtSlices.Domain
{
    public class CategoryHierarchy : BaseEntity
    {
        public long ParentCategoryId { get; set; }
        public ProductCategory? ParentCategory { get; set; }

        public Guid ChildCategoryId { get; set; }
        public ProductCategory? ChildCategory { get; set; }

        public int HierarchyDepth { get; set; }
    }

}
