using OnlineShop.Db.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Db
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }

        public IdentityContext(DbContextOptions<IdentityContext> options) 
            : base(options)
        {
            Database.Migrate();
        }
    }
}
