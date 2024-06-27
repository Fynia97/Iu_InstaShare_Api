using Iu_InstaShare_Api.DTO;
using System.ComponentModel.DataAnnotations.Schema;

namespace Iu_InstaShare_Api.Models
{
    public class FriendsModel
    {
        public int Id { get; set; }

        [Column(Order = 0)]
        public int UserId { get; set; }

        [Column(Order = 1)]
        public int FriendId { get; set; }

        [ForeignKey("UserId")]
        public virtual UserProfileModel? User { get; set; }

        [ForeignKey("FriendId")]
        public virtual UserProfileModel? Friend { get; set; }
        public FriendsStatusEnum Status { get; set; }


        public static FriendsDto ToDto(FriendsModel model)
        {
            return new FriendsDto
            {
                Id = model.Id,
                UserId = model.UserId,
                FriendId = model.FriendId,
                Status = model.Status.ToString()
            };
        }
    }
}
