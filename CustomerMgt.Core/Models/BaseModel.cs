namespace CustomerMgt.Core.Models
{
    public class BaseModel<TPrimaryKey>
    {
        public BaseModel()
        {
            IsActive = true;
            IsDeleted = false;
            DateCreated = DateTime.Now;
        }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateModified { get; set; }= DateTime.Now;
        public TPrimaryKey Id { get; set; }
    }
}