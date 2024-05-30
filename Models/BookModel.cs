namespace Iu_InstaShare_Api.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = "";
        public string Title { get; set; } = "";
        public string? Author { get; set; }
        public string? Publisher { get; set; }
        public DateTime? PublishingYear { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Today;
        public DateTime? UpdatedAt { get; set; } = null;
        public bool LendOut { get; set; } = false;
    }
}
