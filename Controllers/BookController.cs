﻿using Iu_InstaShare_Api.Configurations;
using Iu_InstaShare_Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Iu_InstaShare_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            bookToChange.Name = entity.Name;

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

            _context.Books.Remove(bookToDelete);
            _context.SaveChanges();

            return Ok();
        }
    }
}
