using PrintMaster.Commons.Enumerates;
using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("KeyPerformanceIndicators")]
    public class KeyPerformanceIndicator : BaseEntitySoftDeletable
    {
        public Guid? EmployeeId { get; set; }
        public virtual User? Employee { get; set; }

        public string IndicatorName { get; set; } = string.Empty;

        public int Target { get; set; }

        public int ActuallyAchieved { get; set; } = 0;

        public KPIEnum Period { get; set; } = KPIEnum.Month;

        public bool AchieveKPI { get; set; } = false;
    }
}
