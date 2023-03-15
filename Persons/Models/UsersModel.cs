using System.ComponentModel.DataAnnotations.Schema;

namespace Users.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? IsMarried { get; set; }
        public int? SpouseId { get; set; }
        [ForeignKey("SpouseId")]
        public User Spouse { get; set; }
        public virtual ICollection<Addresses>? Addresses { get; set; }
        public virtual ICollection<PhoneNumbers>? PhoneNumbers { get; set; }
    }


}
