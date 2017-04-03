using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestLibrary.Models;
using TestProj.Utility;
using Xunit;

namespace TestProj.UnitTests
{
    /// <summary>
    /// Unit tests for TestModelService
    /// </summary>
    public class TestModelService_UnitTests : BaseUnitTest
    {
        /// <summary>
        /// Assumed successful add of a TestModel object
        /// </summary>
        [Fact]
        public async void AddTestModel_Success()
        {
            bool result = false;
            Func<TestDbContext, Task> testaddmodel = async (testdbcontext) =>
            {
                var testmodel = TestObjects.CreateTestModel();
                result = await _testmodelservice.AddTestModel(testmodel);
            };
            Func<TestDbContext, Task> assertaddmodel = async (testdbcontext) =>
            {
                Assert.True(result, "Result actually: " + result.ToString());
                var addedtestmodellist = await testdbcontext.TestModels.ToListAsync();
                // data seeding would have inserted one record already. count should be 2.
                Assert.True(addedtestmodellist.Count == 2);
                var addedtestmodel = addedtestmodellist.Where(tm => tm.Name == "Tester Testerson").FirstOrDefault();                
                Assert.NotNull(addedtestmodel);                
            };
            await RunTest(testaddmodel, assertaddmodel);
        }

        /// <summary>
        /// Assumed failure as the AddTestModel method of the TestModelService requires a unique Name for all records inserted.
        /// Only applied this requirement to allow for a simple fail condition.
        /// </summary>
        [Fact]
        public async void AddTestModel_FailUniqueName()
        {
            bool result = false;
            Func<TestDbContext, Task> testaddmodel = async (testdbcontext) =>
            {
                var testmodel = TestObjects.CreateTestModelToFailAdd();
                result = await _testmodelservice.AddTestModel(testmodel);
            };
            Func<TestDbContext, Task> assertaddmodel = async (testdbcontext) =>
            {
                Assert.False(result, "Result actually: " + result.ToString());
                var addedtestmodellist = await testdbcontext.TestModels.ToListAsync();
                // data seeding would have inserted one record already. failed insert should keep record count at 1.
                Assert.True(addedtestmodellist.Count == 1);
            };
            await RunTest(testaddmodel, assertaddmodel);
        }
    }
}
