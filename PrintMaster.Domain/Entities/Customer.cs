using PrintMaster.Domain.Entities.BaseEntity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrintMaster.Domain.Entities
{
    [Table("Customers")]
    public class Customer : BaseEntitySoftDeletable
    {
        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public virtual ICollection<Project>? Projects { get; set; }

        public virtual ICollection<Bill>? Bills { get; set; }

        public virtual ICollection<Delivery>? Delivery { get; set; }
    }
}
