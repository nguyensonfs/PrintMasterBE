using PrintMaster.Commons.Enumerates;
using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("Resources")]
    public class Resource : BaseEntitySoftDeletable
    {
        public Guid ResourceTypeId { get; set; }

        public virtual ResourceType? ResourceType { get; set; }

        public string ResourceName { get; set; } = string.Empty;

        public int AvailableQuantity { get; set; }

        public string Image { get; set; } = string.Empty;

        public ResourceStatus? Status { get; set; } = ResourceStatus.ReadyToUse;

        public virtual ICollection<ResourceProperty>? ResourceProperties { get; set; }
    }
}
