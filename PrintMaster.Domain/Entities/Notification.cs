using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("Notifications")]
    public class Notification : BaseEntitySoftDeletable
    {
        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }

        public string Content { get; set; } = string.Empty;

        public string Link { get; set; } = string.Empty;

        public bool IsSeen { get; set; } = false;
    }
}
