using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TestLibrary.Models;

namespace TestLibrary.Services
{
    /// <summary>
    /// simplified service
    /// </summary>
    public class TestModelService
    {
        private readonly ITestDbContext _dbcontext;

        /// <summary>
        /// constructor with dbcontext injected
        /// </summary>
        /// <param name="dbcontext"></param>
        public TestModelService(ITestDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        /// <summary>
        /// simplified add service method. 
        /// bool would not be the best option for a return value but this works for the sake of the sample code
        /// </summary>
        /// <param name="model">model to be added</param>
        /// <returns>True if success, False if failed in any way.</returns>
        public async Task<bool> AddTestModel(TestModel model)
        {
            var existingTestModels = await _dbcontext.TestModels.ToListAsync();
            if (existingTestModels.Where(tm => tm.Name == model.Name).Any()) return false;

            _dbcontext.TestModels.Add(model);
            var result = await _dbcontext.SaveChangesAsync();
            if (result == 1) return true;
            return false;
        }
    }
}
