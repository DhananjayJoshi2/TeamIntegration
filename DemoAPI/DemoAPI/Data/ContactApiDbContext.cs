using DemoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Data
{
    public class ContactApiDbContext : DbContext
    {
        public ContactApiDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }
    }
}
