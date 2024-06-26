namespace Iu_InstaShare_Api.DTOs
{
    public class UserProfileChangeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Street { get; set; } = "";
        public string Zip { get; set; } = "";
        public string City { get; set; } = "";
        public DateTime? UpdatedAt { get; set; }
    }
}
