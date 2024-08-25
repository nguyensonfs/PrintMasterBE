using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("Teams")]
    public class Team : BaseEntitySoftDeletable
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int NumberOfMember { get; set; } = 0;
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid ManagerId { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
