using Iu_InstaShare_Api.Configurations;
using Iu_InstaShare_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;


namespace Iu_InstaShare_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LendController : ControllerBase
    {
        private readonly DataDbContext _context;

        public LendController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAll")]
        public ActionResult<List<LendModel>> getAll()
        {
            var lends = _context.Lends
                .Include(x => x.Borrower)
                .Include(x => x.Book)
                .ToList();

            return Ok(lends);
        }

        [HttpGet("getById")]
        public ActionResult<LendModel> getById(int id)
        {
            var lendById = _context.Lends
                .Include(x => x.Borrower)
                .Include(x => x.Book)
                .FirstOrDefault(i => i.Id == id);

            if (lendById == null)
                return BadRequest();

            return Ok(lendById);
        }

        [HttpGet("getNextLendFrom")]
        public ActionResult<LendModel> getNextLendFrom()
        {
            var nextLendFrom = _context.Lends
            .Where(x => x.LendFrom >= DateTime.Now)
            .OrderBy(y => y.LendFrom)
            .FirstOrDefault();

            if (nextLendFrom == null)
                return BadRequest();

            return Ok(nextLendFrom);
        }

        [HttpGet("countLendsWithStatus")]
        public ActionResult<int> countLendsWithStatus(int status)
        {
            var countLendsWithStatus = _context.Lends
            .Count(x => x.LendStatus == (LendStatusEnum)status);

            return Ok(countLendsWithStatus);
        }

        [HttpPost("create")]
        public ActionResult<LendModel> create(LendModel entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            _context.Lends.Add(entity);
            _context.SaveChanges();

            return Ok(entity);
        }

        [HttpPost("update")]
        public ActionResult<LendModel> update(LendModel entity)
        {
            var bookToChange = _context.Lends.Find(entity.Id);

            if (bookToChange == null)
            {
                return BadRequest();
            }

            bookToChange.LendFrom = entity.LendFrom;
            bookToChange.LendTo = entity.LendTo;
            bookToChange.Borrower = entity.Borrower;
            bookToChange.LendStatus = entity.LendStatus;
            bookToChange.Note = entity.Note;
            bookToChange.UpdatedAt = DateTime.Now;

            _context.Lends.Update(bookToChange);
            _context.SaveChanges();

            return Ok(entity);
        }

        [HttpDelete("deleteById")]
        public ActionResult<LendModel> deleteById(int id)
        {
            var bookToDelete = _context.Lends
                .FirstOrDefault(i => i.Id == id);

            if (bookToDelete == null)
            {
                return BadRequest();
            }

            _context.Lends.Remove(bookToDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}