using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MybookAPI.Entities;

namespace MybookAPI.Interface
{
    public interface IPublisher
    {
        void Add(Publisher pub);
        Task<bool> AddAsync(Publisher pub);
        Task<bool> Update(Publisher pub);
        Task<IEnumerable<Publisher>> GetAll();
        Task<Publisher> GetById(int Id);
        Task<bool> Delete(int Id);
    }
}
