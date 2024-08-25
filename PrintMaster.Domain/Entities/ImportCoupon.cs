using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("ImportCoupons")]
    public class ImportCoupon : BaseEntitySoftDeletable
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalMoney { get; set; }

        public Guid? ResourcePropertyDetailId { get; set; }
        public virtual ResourcePropertyDetail? ResourcePropertyDetail { get; set; }

        public Guid? EmployeeId { get; set; }
        public virtual User? Employee { get; set; }

        public int Quantity { get; set; }

        public string TradingCode { get; set; } = string.Empty;

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
