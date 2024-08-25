using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("Permissions")]
    public class Permission : BaseEntitySoftDeletable
    {
        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }

        public Guid? RoleId { get; set; }
        public virtual Role? Role { get; set; }
    }
}
