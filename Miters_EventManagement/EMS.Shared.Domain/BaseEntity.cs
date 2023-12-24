namespace EMS.Shared.Domain
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; }    // Date when the entity was created
        public DateTime? ModifiedDate { get; set; }  // Date when the entity was last modified
        public long ModifiedBy { get; set; }
        public long CreatedBy { get; set; }


    }
}