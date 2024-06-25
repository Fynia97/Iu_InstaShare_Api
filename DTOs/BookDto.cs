using Iu_InstaShare_Api.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iu_InstaShare_Api.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = "";
        public string Title { get; set; } = "";
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public DateTime? PublishingYear { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = null;
        public bool LendOut { get; set; } = false;
        public UserProfileModel? User { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; } = 0;
        public string Category { get; set; } = "";
    }
}
