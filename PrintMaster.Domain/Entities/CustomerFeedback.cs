using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("CustomerFeedbacks")]
    public class CustomerFeedback : BaseEntitySoftDeletable
    {
        public Guid? ProjectId { get; set; }
        public virtual Project? Project { get; set; }

        public Guid? CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public string FeedbackContent { get; set; } = string.Empty;

        public string ResponseByCompany { get; set; } = string.Empty;

        public Guid? UserFeedbackId { get; set; }
        public virtual User? UserFeedback { get; set; }

        public DateTime FeedbackTime { get; set; }

        public DateTime? ResponseTime { get; set; }
    }
}
