using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MybookAPI.Entities;
using MybookAPI.Interface;

namespace MybookAPI.Controllers
{
    [Route("api/Genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private IGenre _genre;
        public GenreController(IGenre g)
        {
            _genre = g;
        }

        [HttpPost]
        public void Post([FromBody] Genre g)
        {
            _genre.Add(g);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddGenre([FromBody] Genre g)
        {
            var createGenre = await _genre.AddAsync(g);

            if (createGenre)
            {
                return Ok("Genre Created");
            }
            else
            {
                return BadRequest(new { message = "Unable to create Genre details" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var genre = await _genre.GetAll();
            return Ok(genre);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var genre = await _genre.GetById(id);
            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Genre g)
        {
            g.Id = id;
            var updategenre = await _genre.Update(g);

            if (updategenre)
            {
                return Ok("Genre Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Genre details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletegenre = await _genre.Delete(id);
            if (deletegenre)
            {
                return Ok("Genre Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Genre details" });
            }
        }
    }
}
