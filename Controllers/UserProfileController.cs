using Iu_InstaShare_Api.Configurations;
using Iu_InstaShare_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Iu_InstaShare_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            var userProfileById = _context.UserProfiles
                .FirstOrDefault(i => i.Id == id);

            if (userProfileById == null)
                return BadRequest();

            return Ok(userProfileById);
        }

        //TODO: Auch Sonderzeichen erlauben
        [HttpPost("create")]
        public ActionResult<UserProfileModel> create(UserProfileModel entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            _context.UserProfiles.Add(entity);
            _context.SaveChanges();

            return Ok(entity);
        }

        //TODO: Auch Sonderzeichen erlauben
        [HttpPost("update")]
        public ActionResult<UserProfileModel> update(UserProfileModel entity)
        {
            var userProfileToChange = _context.UserProfiles.Find(entity.Id);

            if (userProfileToChange == null)
            {
                return BadRequest();
            }

            userProfileToChange.FirstName = entity.FirstName;
            userProfileToChange.LastName = entity.LastName;
            userProfileToChange.Email = entity.Email;
            userProfileToChange.Password = entity.Password;
            userProfileToChange.Street = entity.Street;
            userProfileToChange.Zip = entity.Zip;
            userProfileToChange.City = entity.City;
            userProfileToChange.UpdatedAt = DateTime.Now;

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

            _context.UserProfiles.Remove(userProfileToDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}
