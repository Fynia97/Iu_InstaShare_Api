using Iu_InstaShare_Api.Configurations;
using Iu_InstaShare_Api.DTO;
using Iu_InstaShare_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Iu_InstaShare_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly DataDbContext _context;

        public FriendsController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAll")]
        public ActionResult<IEnumerable<FriendsDto>> getAll()
        {
            var friendsList = _context.Friends
            .Include(x => x.Friend)
            .Include(x => x.User)
            .ToList();

            var friendsDtoList = friendsList.Select(f => FriendsModel.ToDto(f)).ToList();

            return Ok(friendsDtoList);
        }

        /*
        [HttpGet("getAll")]
        public ActionResult<FriendsDto> getAll()
        {
            return Ok(_context.Friends.Include(x => x.Friend)
                .Include(x => x.User).ToList());
            var friendsByUserId = _context.UserProfiles
                .Include(x => x.Friends)
                .FirstOrDefault(i => i.Id == id);

            if (friendsByUserId == null)
                return BadRequest();

            return Ok(friendsByUserId);
            
        }
        */

        [HttpGet("getById")]
        public ActionResult<FriendsModel> getById(int id)
        {
            var friendsById = _context.Friends
                .Include(x => x.Friend)
                .Include(x => x.User)
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
        public ActionResult<FriendsModel> deleteById(int id)
        {
            var friendsToDelete = _context.Friends
                .Include(x => x.User)
                .Include(x => x.Friend)
                .FirstOrDefault(i => i.Id == id);

            if (friendsToDelete == null)
            {
                return BadRequest();
            }

            _context.Friends.Remove(friendsToDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}
