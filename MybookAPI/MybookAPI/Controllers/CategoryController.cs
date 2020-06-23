using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MybookAPI.Entities;
using MybookAPI.Interface;

namespace MybookAPI.Controllers
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategory _category;
        public CategoryController(ICategory c)
        {
            _category = c;
        }

        [HttpPost]
        public void Post([FromBody] Category c)
        {
            _category.Add(c);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] Category c)
        {
            var createCategory = await _category.AddAsync(c);

            if (createCategory)
            {
                return Ok("Category Created");
            }
            else
            {
                return BadRequest(new { message = "Unable to create Category details" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var c = await _category.GetAll();
            return Ok(c);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var c = await _category.GetById(id);
            return Ok(c);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Category c)
        {
            c.Id = id;
            var updateCategory = await _category.Update(c);

            if (updateCategory)
            {
                return Ok("Category Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Category details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteCat = await _category.Delete(id);
            if (deleteCat)
            {
                return Ok("Category Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Category details" });
            }
        }
    }
}
