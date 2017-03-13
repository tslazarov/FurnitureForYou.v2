namespace FFY.Models.Contracts
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
    }
}