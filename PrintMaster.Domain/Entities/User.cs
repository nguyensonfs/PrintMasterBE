using PrintMaster.Commons.Enumerates;
using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("Users")]
    public class User : BaseEntitySoftDeletable
    {
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Avatar { get; set; } = string.Empty;

        public GenderEnum Gender { get; set; } = GenderEnum.UnKnown;

        public UserStatus Status { get; set; } = UserStatus.UnActivated;

        public DateTime DateOfBirth { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public Guid? TeamId { get; set; }
        public virtual Team? Team { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public virtual ICollection<Permission>? Permissions { get; set; }

        public virtual ICollection<Notification>? Notifications { get; set; }
    }
}
