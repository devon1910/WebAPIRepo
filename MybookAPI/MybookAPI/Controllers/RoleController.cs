using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MybookAPI.Dtos;
using MybookAPI.Entities;
using MybookAPI.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace MybookAPI.Controllers
{
    [Route("api/Role")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize]
    public class RoleController : ControllerBase
    {
        private IRole _role;
        public RoleController(IRole role)
        {
            _role = role;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Post([FromBody] RoleDto registerRole)  
        {
            ApplicationRole r = new ApplicationRole();
            r.RoleName = registerRole.RoleName;


            var newRole = await _role.CreateRole(r);

            if (newRole)
                return Ok(new { message = "Role Created" });

            return BadRequest(new { message = "Role not created" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var r = await _role.GetAll();
            return Ok(r);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(String id)
        {
            var r = await _role.GetById(id);
            return Ok(r);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(String id, [FromBody] ApplicationRole r)
        {
            r.Id = id;
            var updateRole = await _role.Update(r);

            if (updateRole)
            {
                return Ok("Role Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update Role details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(String id)
        {
            var deleteRole = await _role.Delete(id);
            if (deleteRole)
            {
                return Ok("Role Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete Role details" });
            }
        }

    }
}
