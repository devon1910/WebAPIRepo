using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MybookAPI.Entities;
using MybookAPI.Interface;

namespace MybookAPI.Controllers
{
    [Route("api/Publisher")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private IPublisher _pub;
        public PublisherController(IPublisher p)
        {
            _pub = p;
        }

        [HttpPost]
        public void Post([FromBody] Publisher p)
        {
            _pub.Add(p);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPublisher([FromBody] Publisher p)
        {
            var NewPub = await _pub.AddAsync(p);

            if (NewPub)
            {
                return Ok("New Publisher Added");
            }
            else
            {
                return BadRequest(new { message = "Unable to create Publisher details" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pubs = await _pub.GetAll();
            return Ok(pubs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pub = await _pub.GetById(id);
            return Ok(pub);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Publisher p)
        {
            p.Id = id;
            var updatePub = await _pub.Update(p);

            if (updatePub)
            {
                return Ok("Publisher Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Publisher details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletePub = await _pub.Delete(id);
            if (deletePub)
            {
                return Ok("Publisher Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Publisher details" });
            }
        }
    }
}
