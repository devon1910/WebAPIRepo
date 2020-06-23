using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MybookAPI.Dtos;
using MybookAPI.Entities;
using MybookAPI.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;



namespace MybookAPI.Services
{
    public class RoleService : IRole
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private IConfiguration _config;
        public RoleService(SignInManager<ApplicationUser> signInManager,
                                UserManager<ApplicationUser> userManager,
                                RoleManager<ApplicationRole> roleManager,
                                 IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        public async Task<bool> CreateRole(ApplicationRole role)
        {
            try
            {
                var checkrole = await _roleManager.CreateAsync(role);

                if (checkrole.Succeeded) {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> Delete(String Id)
        {
            var r = await _roleManager.FindByIdAsync(Id);

            if (r != null)
            {
                await _roleManager.DeleteAsync(r);
                // _userManager.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<List<ApplicationRole>> GetAll()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<ApplicationRole> GetById(String Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);

            return role;
        }

        public async Task<bool> Update(ApplicationRole ar)
        {
            var role = await _roleManager.FindByIdAsync(ar.Id);
            if (role != null)
            {
                role.RoleName = ar.RoleName;

                return true;
            }

            return false;
        }
    }

}

