namespace OrderManagement.Core.Entities
{
    public interface IEntity
    {
        int Id { get; set; }
        bool IsDeleted { get; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
