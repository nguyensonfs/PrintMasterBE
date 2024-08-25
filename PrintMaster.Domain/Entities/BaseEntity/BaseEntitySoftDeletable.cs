namespace PrintMaster.Domain.Entities.BaseEntity
{
    public class BaseEntitySoftDeletable : BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
    }
}
