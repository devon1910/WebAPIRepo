using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MybookAPI.Entities;
using MybookAPI.Interface;

namespace MybookAPI.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private IBook _book;
        public BookController(IBook book)
        {
            _book = book;
        }

        [HttpPost]
        public void Post([FromBody] Book book)
        {
            _book.Add(book);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            var createbook = await _book.AddAsync(book);

            if (createbook)
            {
                return Ok("Book Created");
            }
            else
            {
                return BadRequest(new { message = "Unable to create Book details" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _book.GetAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var book = await _book.GetById(id);
            return Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book b)
        {
            b.Id = id;
            var updateBook = await _book.Update(b);

            if (updateBook)
            {
                return Ok("Book Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Book details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteBook = await _book.Delete(id);
            if (deleteBook)
            {
                return Ok("Book Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Book details" });
            }
        }
    }
}
