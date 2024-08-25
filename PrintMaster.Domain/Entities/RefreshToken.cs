using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("RefreshTokens")]
    public class RefreshToken : BaseEntitySoftDeletable
    {
        public Guid? UserId { get; set; }
        public virtual User? User { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiryTime { get; set; }
    }
}
