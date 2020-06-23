using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MybookAPI.Entities;

namespace MybookAPI.Data
{
    public class MybookAPIDataContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public MybookAPIDataContext(DbContextOptions<MybookAPIDataContext> options) : base(options)
        {
        }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<User> BookUsers { get; set; }
    }
}
