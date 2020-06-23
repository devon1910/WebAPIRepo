using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MybookAPI.Entities;

namespace MybookAPI.Interface
{
    public interface IRole
    {
        Task<bool> CreateRole(ApplicationRole role);
       // Task<SignInModel> SignIn(LoginDto loginDetails);
        Task<bool> Update(ApplicationRole AppRole);
        Task<List<ApplicationRole>> GetAll();
        Task<ApplicationRole> GetById(String Id);
        Task<bool> Delete(String Id);
    }
}
