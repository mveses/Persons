namespace Users.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? IsMarried { get; set; }
        public int? SpouseId { get; set; }
        public int? Age { get; set; }
        public string? Spouse { get; set; }
        public List<Addresses>? AddressList { get; set; }
    }


}
