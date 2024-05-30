namespace Iu_InstaShare_Api.Models
{
    public class LendModel
    {
        public int Id { get; set; }
        public DateTime LendFrom { get; set; }
        public DateTime LendTo { get; set; }
        public UserProfileModel Borrower { get; set; }
        public BookModel Book { get; set; }
        public string Note { get; set; }
        public LendStatusEnum LendStatus { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
