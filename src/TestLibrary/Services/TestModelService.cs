using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestLibrary.Models;

namespace TestLibrary.Services
{
    public class TestModelService : ITestModelService
    {
        private readonly ITestDbContext _dbcontext;

        public TestModelService(ITestDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // simplified add service method. 
        // bool would not be the best option for a return value but this works for the sake of the sample code
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
