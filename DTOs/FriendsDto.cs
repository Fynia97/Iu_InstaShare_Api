using Iu_InstaShare_Api.Models;

namespace Iu_InstaShare_Api.DTO
{
    public class FriendsDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FriendId { get; set; }
        public FriendsStatusEnum Status { get; set; }
    }
}
