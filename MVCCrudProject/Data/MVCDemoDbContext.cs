using Microsoft.EntityFrameworkCore;
using MVCCrudProject.Models.Domain;

namespace MVCCrudProject.Data
{
    public class MVCDemoDbContext : DbContext
    {
        public MVCDemoDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

    }
}
