using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("ShippingMethods")]
    public class ShippingMethod : BaseEntitySoftDeletable
    {
        public string ShippingMethodName { get; set; } = string.Empty;
        public virtual ICollection<Delivery>? Deliveries { get; set; }
    }
}
