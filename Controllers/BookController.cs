using Iu_InstaShare_Api.Configurations;
using Iu_InstaShare_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iu_InstaShare_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly DataDbContext _context;

        public BookController(DataDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAll")]
        public ActionResult<List<BookModel>> getAll()
        {
            var books = _context.Books.ToList();

            if (books == null)
            {
                return BadRequest();
            }

            return Ok(books);
        }

        [HttpGet("getById")]
        public ActionResult<BookModel> getById(int id)
        {
            var bookById = _context.Books
                .FirstOrDefault(i => i.Id == id);

            if (bookById == null)
                return BadRequest();

            return Ok(bookById);
        }

        //TODO: Auch Sonderzeichen erlauben
        [HttpPost("create")]
        public ActionResult<BookModel> create(BookModel entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            _context.Books.Add(entity);
            _context.SaveChanges();

            return Ok(entity);
        }

        //TODO: Auch Sonderzeichen erlauben
        [HttpPost("update")]
        public ActionResult<BookModel> update(BookModel entity)
        {
            var bookToChange = _context.Books.Find(entity.Id);

            if (bookToChange == null)
            {
                return BadRequest();
            }

            bookToChange.ISBN = entity.ISBN;
            bookToChange.Title = entity.Title;
            bookToChange.Author = entity.Author;
            bookToChange.Publisher = entity.Publisher;
            bookToChange.PublishingYear = entity.PublishingYear;
            bookToChange.LendOut = entity.LendOut;
            bookToChange.UpdatedAt = DateTime.Now;

            _context.Books.Update(bookToChange);
            _context.SaveChanges();

            return Ok(entity);
        }

        [HttpDelete("deleteById")]
        public ActionResult<BookModel> deleteById(int id)
        {
            var bookToDelete = _context.Books
                .FirstOrDefault(i => i.Id == id);

            if (bookToDelete == null)
            {
                return BadRequest();
            }

            var lendsOfBook = _context.Lends.Where(x => x.Book.Id == bookToDelete.Id).ToList();

            if (lendsOfBook != null)
            {
                foreach (LendModel element in lendsOfBook)
                {
                    _context.Lends.Remove(element);
                    _context.SaveChanges();
                }
            }

            _context.Books.Remove(bookToDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}
