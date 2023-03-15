using System.ComponentModel.DataAnnotations.Schema;

namespace Users.Models
{
    public class PhoneNumbers
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
    }
}


