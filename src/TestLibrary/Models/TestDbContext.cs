using Microsoft.EntityFrameworkCore;

namespace TestLibrary.Models
{
    public class TestDbContext : DbContext, ITestDbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
        
        public DbSet<TestModel> TestModels { get; set; }
    }
}
