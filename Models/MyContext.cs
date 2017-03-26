using Microsoft.EntityFrameworkCore;
 
namespace network.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Invite> invites { get; set; }
    }
}