using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("Roles")]
    public class Role : BaseEntitySoftDeletable
    {
        public string RoleCode { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public virtual ICollection<Permission>? Permissions { get; set; }
    }
}
