namespace OrderManagement.Core.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        bool IsDeleted { get; }
        DateTime CreatedDate { get; set; }
        DateTime UpdatedDate { get; set; }
    }
}
