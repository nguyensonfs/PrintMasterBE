using PrintMaster.Commons.Enumerates;
using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("Designs")]
    public class Design : BaseEntitySoftDeletable
    {
        public Guid? ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public Guid? DesignerId { get; set; }
        public virtual User? Designer { get; set; }

        public string FilePath { get; set; } = string.Empty;

        public DateTime DesignTime { get; set; }

        public DesignStatus DesignStatus { get; set; }

        public Guid? ApproverId { get; set; }

        public virtual ICollection<PrintJob>? PrintJobs { get; set; } 
    }
}
