using Iu_InstaShare_Api.Configurations;
using Iu_InstaShare_Api.DTO;
using Iu_InstaShare_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Iu_InstaShare_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FriendsController : ControllerBase
    {
        private readonly DataDbContext _context;

        public FriendsController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAllPossibleFriends")]
        public ActionResult<IEnumerable<FriendsDto>> getAllPossibleFriends(int userId)
        {
            var possibleFriendsList = _context.UserProfiles
                .Where(x => x.Id != userId)
                .ToList();

            var allMyFriendsId = _context.Friends
                .Where(x => x.UserId == userId || x.FriendId == userId)
                .Select(x => x.UserId == userId ? x.FriendId : x.UserId)
                .ToList();

            var notMyFriends = possibleFriendsList
                .Where(x => allMyFriendsId.Contains(x.Id) == false)
                .ToList();

            return Ok(notMyFriends);
        }

        [HttpGet("getAllFriendsByUserId")]
        public ActionResult<IEnumerable<FriendsDto>> getAllFriendsByUserId(int userId)
        {
            var friendsList = _context.Friends
            .Include(x => x.Friend)
            .Include(x => x.User)
            .Where(x => x.UserId == userId)
            .ToList();

            var friendsDtoList = friendsList.Select(f => FriendsModel.ToDto(f)).ToList();

            return Ok(friendsDtoList);
        }

        [HttpGet("getAllFriendsAskedForMe")]
        public ActionResult<IEnumerable<FriendsDto>> getAllFriendsAskedForMe(int friendId)
        {
            var friendsList = _context.Friends
            .Include(x => x.Friend)
            .Include(x => x.User)
            .Where(x => x.FriendId == friendId && x.Status == FriendsStatusEnum.ASKED)
            .ToList();

            var friendsDtoList = friendsList.Select(f => FriendsModel.ToDto(f)).ToList();

            return Ok(friendsDtoList);
        }

        [HttpPost("create")]
        public ActionResult<FriendsModel> create(FriendsModel entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            _context.Friends.Add(entity);
            _context.SaveChanges();

            return Ok(entity);
        }

        [HttpPost("update")]
        public ActionResult<FriendsModel> update(FriendsDto entity)
        {
            var friendToChange = _context.Friends
                .Include(x => x.User)
                .Include(x => x.Friend)
                .FirstOrDefault(i => i.Id == entity.Id);

            if (friendToChange == null)
            {
                return BadRequest();
            }

            friendToChange.Status = (FriendsStatusEnum)Enum.Parse(typeof(FriendsStatusEnum), entity.Status);

            _context.Friends.Update(friendToChange);
            _context.SaveChanges();

            return Ok(entity);
        }

        [HttpDelete("deleteByFriendIdAndUserId")]
        public ActionResult<FriendsModel> deleteByFriendIdAndUserId(int friendId, int userId)
        {
            var friendsToDelete = _context.Friends
                .Include(x => x.User)
                .Include(x => x.Friend)
                .Where(x => x.UserId == userId && x.FriendId == friendId || x.FriendId == userId && x.UserId == friendId)
                .ToList();

            foreach(FriendsModel element in friendsToDelete)
            {
                _context.Friends.Remove(element);
                _context.SaveChanges();
            }

            return Ok();
        }
    }
}
