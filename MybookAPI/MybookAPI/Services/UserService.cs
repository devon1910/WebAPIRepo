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
    public class UserService : IUser
    {
        private MybookAPIDataContext _context;

        public UserService(MybookAPIDataContext context)
        {
            _context = context;
        }

        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.BookUsers.SingleOrDefault(x => x.Username == username || x.Email == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public User Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");
            

            if (_context.BookUsers.Any(x => x.Username == user.Username || x.Email == user.Email))
                throw new Exception("Username \"" + user.Username + "\" or Email \"" + user.Email + " is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.BookUsers.Add(user);
            _context.SaveChanges();

            return user;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await _context.BookUsers.FindAsync(id);

            if (user != null)
            {
                _context.BookUsers.Remove(user);
                _context.SaveChanges();
                return true;
            }
            return false;
        }


        public async Task<User> GetById(int id)
        {
            var u =  await _context.BookUsers.FindAsync(id);

            return u;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.BookUsers.ToListAsync();
        }

        public  async Task<bool> Update(User user)
        {
            var u =   await _context.BookUsers.FindAsync(user.Id);//check later
            
            if (u!= null) {

                u.FirstName = user.FirstName;
                u.LastName = user.LastName;
                u.Username = user.Username;
                u.Email = user.Email;
                u.PhoneNo = user.PhoneNo;

                _context.SaveChanges();
                return true;

            }
            return false;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
