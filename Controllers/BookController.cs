using Iu_InstaShare_Api.Configurations;
using Iu_InstaShare_Api.DTOs;
using Iu_InstaShare_Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("getAllByUserId")]
        public ActionResult<List<BookModel>> getAllByUserId(int userId)
        {
            var books = _context.Books
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .ToList();

            var bookDtos = new List<BookDto>();
            foreach (var element in books)
            {
                var bookDto = new BookDto
                {
                    Id = element.Id,
                    ISBN = element.ISBN,
                    Title = element.Title,
                    Author = element.Author,
                    Publisher = element.Publisher,
                    PublishingYear = element.PublishingYear,
                    CreatedAt = element.CreatedAt,
                    UpdatedAt = element.UpdatedAt,
                    LendOut = element.LendOut,
                    UserId = element.UserId,
                    Category = element.Category.ToString()
                };
                bookDtos.Add(bookDto);
            }
            return Ok(bookDtos);
        }

        [HttpGet("getByIdAndUserId")]
        public ActionResult<BookModel> getByIdAndUserId(int id, int userId)
        {
            var bookById = _context.Books
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .FirstOrDefault(i => i.Id == id);

            if (bookById == null)
                return BadRequest();

            var bookToGet = new BookDto
            {
                Id = bookById.Id,
                ISBN = bookById.ISBN,
                Title = bookById.Title,
                Author = bookById.Author,
                Publisher = bookById.Publisher,
                PublishingYear = bookById.PublishingYear,
                CreatedAt = bookById.CreatedAt,
                UpdatedAt = bookById.UpdatedAt,
                LendOut = bookById.LendOut,
                UserId = bookById.UserId,
                Category = bookById.Category.ToString()
            };

            return Ok(bookToGet);
        }

        [HttpPost("create")]
        public ActionResult<BookModel> create(BookDto entity)
        {
            if (entity == null)
            {
                return BadRequest();
            }

            BookCategoryEnum category = (BookCategoryEnum)Enum.Parse(typeof(BookCategoryEnum), entity.Category);

            BookModel newBook = new BookModel()
            {
                ISBN = entity.ISBN,
                Title = entity.Title,
                Author = entity.Author,
                Publisher = entity.Publisher,
                PublishingYear = entity.PublishingYear,
                LendOut = entity.LendOut,
                UpdatedAt = DateTime.Now,
                UserId = entity.UserId,
                Category = category
            };

            _context.Books.Add(newBook);
            _context.SaveChanges();

            return Ok(newBook);
        }

        [HttpPost("update")]
        public ActionResult<BookModel> update(BookDto entity)
        {
            var bookToChange = _context.Books
                .Include(x => x.User)
                .FirstOrDefault(i => i.Id == entity.Id);

            if (bookToChange == null)
            {
                return BadRequest();
            }

            BookCategoryEnum category = (BookCategoryEnum)Enum.Parse(typeof(BookCategoryEnum), entity.Category);

            bookToChange.ISBN = entity.ISBN;
            bookToChange.Title = entity.Title;
            bookToChange.Author = entity.Author;
            bookToChange.Publisher = entity.Publisher;
            bookToChange.PublishingYear = entity.PublishingYear;
            bookToChange.LendOut = entity.LendOut;
            bookToChange.UpdatedAt = DateTime.Now;
            bookToChange.UserId = entity.UserId;
            bookToChange.Category = category;

            _context.Books.Update(bookToChange);
            _context.SaveChanges();

            return Ok(bookToChange);
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
