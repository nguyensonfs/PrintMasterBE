using PrintMaster.Commons.Enumerates;
using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("Deliveries")]
    public class Delivery : BaseEntitySoftDeletable
    {
        public Guid? ShippingMethodId { get; set; }
        public virtual ShippingMethod? ShippingMethod { get; set; }

        public Guid? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public Guid? DeliverId { get; set; }
        public virtual User? Deliver { get; set; }

        public Guid? ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public string DeliveryAddress { get; set; } = string.Empty;

        public DateTime EstimateDeliveryTime { get; set; }

        public DateTime ActualDeliveryTime { get; set; }

        public DeliveryStatus DeliveryStatus { get; set; } = DeliveryStatus.Waiting;
    }
}
