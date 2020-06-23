using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MybookAPI.Data;
using MybookAPI.Entities;
using MybookAPI.Interface;

namespace MybookAPI.Services
{
    public class BookService : IBook
    {
        private MybookAPIDataContext _context;
        public BookService(MybookAPIDataContext context)
        {
            _context = context;
        }
        public void Add(Book book)
        {
            book.DateCreated = DateTime.Now;
            _context.Add(book);
            _context.SaveChanges();
        }

        public async Task<bool> AddAsync(Book book)
        {
            try
            {
                await _context.AddAsync(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> Delete(int Id)
        {
            var book = await _context.Books.FindAsync(Id);

            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book> GetById(int Id)
        {
            var book = await _context.Books.FindAsync(Id);

            return book;
        }

        public async Task<bool> Update(Book book)
        {
            var aut = await _context.Books.FindAsync(book.Id);
            if (aut != null)
            {
               
                aut.Title = book.Title;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
