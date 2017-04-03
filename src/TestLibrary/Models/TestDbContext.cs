using Microsoft.EntityFrameworkCore;

namespace TestLibrary.Models
{
    /// <summary>
    /// simple dbcontext for the purposes of showing the test
    /// </summary>
    public class TestDbContext : DbContext, ITestDbContext
    {
        /// <summary>
        /// need the following constructor for setting up the unit test
        /// </summary>
        /// <param name="options"></param>
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
        
        /// <summary>
        /// simple model
        /// </summary>
        public DbSet<TestModel> TestModels { get; set; }
    }
}
