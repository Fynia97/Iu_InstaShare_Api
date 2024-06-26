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
                .Where(x => x.UserId == userId)
                .Select(x => x.FriendId)
                .ToList();

            var notMyFriends = possibleFriendsList
                .Where(x => allMyFriendsId.Contains(x.Id) == false)
                .ToList();

            return Ok(notMyFriends);
        }

        [HttpGet("getAllByUserId")]
        public ActionResult<IEnumerable<FriendsDto>> getAllByUserId(int userId)
        {
            var friendsList = _context.Friends
            .Include(x => x.Friend)
            .Include(x => x.User)
            .Where(x => x.UserId == userId)
            .ToList();

            var friendsDtoList = friendsList.Select(f => FriendsModel.ToDto(f)).ToList();

            return Ok(friendsDtoList);
        }

        [HttpGet("getByIdAndUserId")]
        public ActionResult<FriendsModel> getByIdAndUserId(int id, int userId)
        {
            var friendsById = _context.Friends
                .Include(x => x.Friend)
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .FirstOrDefault(i => i.Id == id);

            if (friendsById == null)
                return BadRequest();

            return Ok(friendsById);
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

        [HttpDelete("deleteById")]
        public ActionResult<FriendsModel> deleteById(int friendId, int userId)
        {
            var friendsToDelete = _context.Friends
                .Include(x => x.User)
                .Include(x => x.Friend)
                .Where(x => x.UserId == userId && x.FriendId == friendId)
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
