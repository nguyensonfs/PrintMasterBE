using PrintMaster.Domain.Entities.BaseEntity;

namespace PrintMaster.Domain.Entities
{
    public class ResourceType : BaseEntitySoftDeletable
    {
        public string NameOfResourceType { get; set; } = string.Empty;
        public virtual ICollection<Resource>? Resources { get; set; }
    }
}
