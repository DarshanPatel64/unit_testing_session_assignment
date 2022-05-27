using Microsoft.EntityFrameworkCore;

namespace unitTestAssignment.cs.Models
{
    public class AppDataDbContext : DbContext
    {
        public AppDataDbContext()
        {

        }
        public AppDataDbContext(DbContextOptions<AppDataDbContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
