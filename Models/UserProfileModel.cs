using System.ComponentModel.DataAnnotations;

namespace Iu_InstaShare_Api.Models
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Password { get; set; } = "";

        public bool Active { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Today;
        public DateTime? UpdatedAt { get; set; } = null;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
    }
}
