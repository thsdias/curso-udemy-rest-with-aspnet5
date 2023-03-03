using Microsoft.EntityFrameworkCore;

namespace RestWithAspNetUdemy.Model.Context
{
    public class PostgreSqlContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public DbSet<Books> Books { get; set; }
        
        public DbSet<User> Users { get; set; }

        public PostgreSqlContext()
        {
        }

        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) : base(options) 
        {    
        }
    }
}
