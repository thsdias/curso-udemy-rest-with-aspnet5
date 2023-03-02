using Microsoft.EntityFrameworkCore;

namespace RestWithAspNetUdemy.Model.Context
{
    public class MySqlContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<Books> Books { get; set; }
        
        public DbSet<User> Users { get; set; }

        public MySqlContext()
        {
        }

        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) 
        {    
        }
    }
}
