using System.ComponentModel.DataAnnotations.Schema;

namespace Users.Models
{
    public class Addresses
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Address { get; set; } = "";
        public bool IsPrimary { get; set; }

    }
}
