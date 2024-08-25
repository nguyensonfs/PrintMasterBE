using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("ResourcePropertyDetails")]
    public class ResourcePropertyDetail : BaseEntitySoftDeletable
    {
        public Guid? ResourcePropertyId { get; set; }
        public virtual ResourceProperty? ResourceProperty { get; set; }

        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string Image { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public virtual ICollection<ImportCoupon>? ImportCoupons { get; set; }

        public virtual ICollection<ResourceForPrintJob>? ResourceForPrintJobs { get; set; }
    }
}
