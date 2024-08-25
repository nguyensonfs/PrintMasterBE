using PrintMaster.Commons.Enumerates;
using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("PrintJobs")]
    public class PrintJob : BaseEntitySoftDeletable
    {
        public Guid? DesignId { get; set; }
        public virtual Design? Design { get; set; }

        public PrintJobStatus? PrintJobStatus { get; set; }

        public virtual ICollection<ResourceForPrintJob>? ResourceForPrints { get; set; }
    }
}
