using Iu_InstaShare_Api.Configurations;
using Iu_InstaShare_Api.DTOs;
using Iu_InstaShare_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using static System.Reflection.Metadata.BlobBuilder;


namespace Iu_InstaShare_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LendController : ControllerBase
    {
        private readonly DataDbContext _context;

        public LendController(DataDbContext context)
        {
            _context = context;
        }


        [HttpGet("getAllByUserId")]
        public ActionResult<List<LendModel>> getAllByUserId(int userId)
        {
            var lends = _context.Lends
                .Include(x => x.Borrower)
                .Include(x => x.Book)
                .Where(x => x.Book.User.Id == userId)
                .OrderBy(x => x.LendTo)
                .ToList();

            return Ok(lends);
        }

        [HttpGet("getAllLendsOfUserByUserId")]
        public ActionResult<List<LendModel>> getAllLendsOfUserByUserId(int userId)
        {
            var lends = _context.Lends
                .Include(x => x.Borrower)
                .Include(x => x.Book)
                .Where(x => x.Borrower.Id == userId)
                .OrderBy(x => x.LendTo)
                .ToList();

            var lendDtos = new List<LendDto>();
            foreach (var element in lends)
            {
                var lendDto = new LendDto
                {
                    Id = element.Id,
                    LendFrom = element.LendFrom,
                    LendTo = element.LendTo,
                    BorrowerId = element.BorrowerId,
                    BookId = element.BookId,
                    Note = element.Note,
                    CreatedAt = element.CreatedAt,
                    UpdatedAt = element.UpdatedAt,
                    LendStatus = element.LendStatus.ToString(),
                    Borrower = element.Borrower,
                    Book = element.Book
                };
                lendDtos.Add(lendDto);
            }

            return Ok(lendDtos);
        }

        [HttpGet("getAllLendsFromUserByUserId")]
        public ActionResult<List<LendModel>> getAllLendsFromUserByUserId(int userId)
        {
            var lends = _context.Lends
                .Include(x => x.Borrower)
                .Include(x => x.Book)
                .Where(x => x.Book.UserId == userId)
                .OrderBy(x => x.LendTo)
                .ToList();

            var lendDtos = new List<LendDto>();
            foreach (var element in lends)
            {
                var lendDto = new LendDto
                {
                    Id = element.Id,
                    LendFrom = element.LendFrom,
                    LendTo = element.LendTo,
                    BorrowerId = element.BorrowerId,
                    BookId = element.BookId,
                    Note = element.Note,
                    CreatedAt = element.CreatedAt,
                    UpdatedAt = element.UpdatedAt,
                    LendStatus = element.LendStatus.ToString(),
                    Borrower = element.Borrower,
                    Book = element.Book
                };
                lendDtos.Add(lendDto);
            }

            return Ok(lendDtos);
        }

        [HttpGet("getByIdAndUserId")]
        public ActionResult<LendModel> getByIdAndUserId(int id, int userId)
        {
            var lendById = _context.Lends
                .Include(x => x.Borrower)
                .Include(x => x.Book)
                .Where(x => x.Book.UserId == userId || x.Borrower.Id == userId)
                .FirstOrDefault(i => i.Id == id);

            if (lendById == null)
                return BadRequest();

            return Ok(lendById);
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
        public ActionResult<LendModel> update(LendDto entity)
        {
            var lendToChange = _context.Lends
                .Include(x => x.Borrower)
                .Include(x => x.Book)
                .FirstOrDefault(i => i.Id == entity.Id);

            if (lendToChange == null)
            {
                return BadRequest();
            }

            LendStatusEnum lendStatus = (LendStatusEnum)Enum.Parse(typeof(LendStatusEnum), entity.LendStatus);

            lendToChange.LendFrom = entity.LendFrom;
            lendToChange.LendTo = entity.LendTo;
            lendToChange.Note = entity.Note;
            lendToChange.UpdatedAt = DateTime.Now;
            lendToChange.LendStatus = lendStatus;

            _context.Lends.Update(lendToChange);
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

        [HttpGet("getNextLendFrom")]
        public ActionResult<LendModel> getNextLendFrom(int userId)
        {
            var nextLendFrom = _context.Lends
            .Include(x => x.Borrower)
            .Where(x => x.LendFrom >= DateTime.Now && x.Borrower.Id == userId)
            .OrderBy(y => y.LendFrom)
            .FirstOrDefault();

            if (nextLendFrom == null)
                return Ok();

            return Ok(nextLendFrom);
        }

        [HttpGet("countLendsWithStatus")]
        public ActionResult<int> countLendsWithStatus(int status, int userId)
        {
            var countLendsWithStatus = _context.Lends
                .Include(x => x.Book)
                .Where(x => x.Book.User.Id == userId || x.BorrowerId == userId)
                .Count(x => x.LendStatus == (LendStatusEnum)status);

            return Ok(countLendsWithStatus);
        }
    }
}