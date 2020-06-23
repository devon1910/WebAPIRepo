using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MybookAPI.Dtos;
using MybookAPI.Entities;
using MybookAPI.Interface;

namespace MybookAPI.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _userService;
        private IConfiguration _config;

        public UserController(IUser userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }
        // GET: /<controller>/

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            // map dto to entity
            // var userdto = _mapper.Map<User>(userDto);

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Username = userDto.Username,
                PhoneNo = userDto.PhoneNo
            };
            try
            {
                // save 
                var userCreated = _userService.Create(user, userDto.Password);
                return Ok();
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });



            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Secret").Value);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //var claims = new List<Claim>
            //        {
            //        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
            //        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //        };

            //var tokenDescriptor = new JwtSecurityToken(
            //        issuer: "http://cyberinterns.slack.com",
            //        audience: "http://api.cyberinterns.com",
            //        expires: DateTime.UtcNow.AddDays(7),
            //        cliams: claims,
            //        signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            //        );






            var token = tokenHandler.CreateToken(tokenDescriptor);

            var Expires = tokenDescriptor.Expires.ToString();





            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                access_token = tokenHandler.WriteToken(token),
                expires = Expires
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var uid = await _userService.GetById(id);
            return Ok(uid);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] User u)
        {
            u.Id = id;
            var updateuser = await _userService.Update(u);

            if (updateuser)
            {
                return Ok("User Updated");
            }
            else
            {
                return BadRequest(new { message = "Unable to update User details" });
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteUser =  await _userService.Delete(id);
            if (deleteUser)
            {
                return Ok("User Deleted");
            }
            else
            {
                return BadRequest(new { message = "Unable to delete User details" });
            }
        }
    }
}
