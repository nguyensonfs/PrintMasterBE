using PrintMaster.Commons.Enumerates;
using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("Bills")]
    public class Bill : BaseEntitySoftDeletable
    {
        public string BillName { get; set; } = string.Empty;

        public BillStatus BillStatus { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalMoney { get; set; }

        public Guid? ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public Guid? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public string TradingCode { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public Guid? EmployeeId { get; set; }

        public virtual User? Employee { get; set; }
    }
}
