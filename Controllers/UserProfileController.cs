using Iu_InstaShare_Api.Configurations;
using Iu_InstaShare_Api.DTOs;
using Iu_InstaShare_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Iu_InstaShare_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfileController : ControllerBase
    {
        private readonly DataDbContext _context;

        public UserProfileController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAll")]
        public ActionResult<UserProfileModel> getAll()
        {
            var userProfiles = _context.UserProfiles.ToList();

            if (userProfiles == null)
                return BadRequest();

            return Ok(userProfiles);
        }

        [HttpGet("getById")]
        public ActionResult<UserProfileModel> getById(int id)
        {
            var userProfile = _context.UserProfiles.FirstOrDefault(i => i.Id == id);

            if (userProfile == null)
            {
                return NotFound();
            }

            return Ok(userProfile);
        }

        [HttpGet("getByEmail")]
        public async Task<ActionResult<UserProfileModel>> getByEmail(string email)
        {
            var userProfileByEmail = await _context.UserProfiles
                .FirstOrDefaultAsync(i => i.Email == email);

            if (userProfileByEmail == null)
                return BadRequest();

            return Ok(userProfileByEmail);
        }

        [HttpPost("update")]
        public ActionResult<UserProfileModel> update(UserProfileChangeDto entity)
        {
            var userProfileToChange = _context.UserProfiles.Find(entity.Id);

            if (userProfileToChange == null)
            {
                return BadRequest();
            }

            using var hmac = new HMACSHA512();

            userProfileToChange.FirstName = entity.FirstName;
            userProfileToChange.LastName = entity.LastName;
            userProfileToChange.Email = entity.Email;
            userProfileToChange.Street = entity.Street;
            userProfileToChange.Zip = entity.Zip;
            userProfileToChange.City = entity.City;
            userProfileToChange.PhoneNumber = entity.PhoneNumber;
            userProfileToChange.UpdatedAt = DateTime.Now;
            userProfileToChange.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(entity.Password));
            userProfileToChange.PasswordSalt = hmac.Key;

            _context.UserProfiles.Update(userProfileToChange);
            _context.SaveChanges();

            return Ok(entity);
        }

        [HttpDelete("deleteById")]
        public ActionResult<UserProfileModel> deleteById(int id)
        {
            var userProfileToDelete = _context.UserProfiles
                .FirstOrDefault(i => i.Id == id);

            if (userProfileToDelete == null)
            {
                return BadRequest();
            }

            var booksOfUser = _context.Books
                .Include(x => x.User)
                .Where(x => x.UserId == id)
                .ToList();

            var lendsOfBook = new List<LendModel>();

            foreach (BookModel element in booksOfUser)
            {
                var lend = _context.Lends.Where(x => x.Book.Id == element.Id).FirstOrDefault();
                if (lend != null)
                {
                    lendsOfBook.Add(lend);
                }
            }

            foreach (LendModel element in lendsOfBook)
            {
                _context.Lends.Remove(element);
                _context.SaveChanges();
            }

            var lendsOfUser = _context.Lends
                .Where(x => x.BorrowerId == id)
                .ToList();

            foreach (LendModel element in lendsOfUser)
            {
                _context.Lends.Remove(element);
                _context.SaveChanges();
            }

            foreach (BookModel element in booksOfUser)
            {
                _context.Books.Remove(element);
                _context.SaveChanges();
            }

            var friendsOfUser = _context.Friends
                .Include(x => x.User)
                .Include(x => x.Friend)
                .Where(x => x.UserId == id || x.FriendId == id)
                .ToList();

            foreach (FriendsModel element in friendsOfUser)
            {
                _context.Friends.Remove(element);
                _context.SaveChanges();
            }

            _context.UserProfiles.Remove(userProfileToDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}
