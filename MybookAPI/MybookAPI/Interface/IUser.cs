using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MybookAPI.Entities;

namespace MybookAPI.Interface
{
    public interface IUser
    {
        User Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task <User> GetById(int id);
        User Create(User user, string password);
        Task <bool> Update(User user);
        Task <bool> Delete(int id);
    }
}
