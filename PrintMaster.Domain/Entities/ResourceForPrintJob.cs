using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("ResourceForPrintJobs")]
    public class ResourceForPrintJob : BaseEntitySoftDeletable
    {
        public Guid? ResourcePropertyDetailId { get; set; }
        public virtual ResourcePropertyDetail? Resource { get; set; }

        public int Quantity { get; set; }

        public Guid? PrintJobId { get; set; }
        public virtual PrintJob? PrintJob { get; set; }
    }
}
