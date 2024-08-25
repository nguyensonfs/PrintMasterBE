using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("ResourceProperties")]
    public class ResourceProperty : BaseEntitySoftDeletable
    {
        public Guid? ResourceId { get; set; }
        public virtual Resource? Resource { get; set; }

        public string ResourcePropertyName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public virtual ICollection<ResourcePropertyDetail>? ResourcePropertyDetails { get; set; }
    }
}
