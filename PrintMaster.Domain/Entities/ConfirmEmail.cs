using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("ConfirmEmails")]
    public class ConfirmEmail : BaseEntitySoftDeletable
    {

        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }

        public string ConfirmCode { get; set; } = string.Empty;

        public DateTime ExpiryTime { get; set; }

        public bool IsConfirm { get; set; } = false;
    }
}
