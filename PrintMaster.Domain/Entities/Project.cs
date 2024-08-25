using PrintMaster.Commons.Enumerates;
using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("Projects")]
    public class Project : BaseEntitySoftDeletable
    {
        public string ProjectName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string RequestDescriptionFromCustomer { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public Guid? LeaderId { get; set; }
        public virtual User? Leader { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public string? ImageDescription { get; set; }
        public Guid EmployeeCreateId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CommissionPercentage { get; set; } = 0;
        [Column(TypeName = "decimal(18,2)")]
        public decimal StartingPrice { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public Guid? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }
        public double? Progress { get; set; } = 0;
        public ProjectStatus? Status { get; set; } = ProjectStatus.Initialization;
        public virtual ICollection<CustomerFeedback>? CustomerFeedbacks { get; set; }
        public virtual ICollection<Design>? Designs { get; set; }
    }
}
