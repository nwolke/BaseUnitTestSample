using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestLibrary.Models;
using TestLibrary.Services;

namespace TestProj.Utility
{
    public class BaseUnitTest
    {
        protected TestModelService _testmodelservice;

        protected virtual TestDbContext DbContextSetup(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<TestDbContext>().UseSqlite(connection).Options;
            var dbcontext = new TestDbContext(options);
            dbcontext.Database.EnsureCreated();
            // any default data seeding goes here
            dbcontext.TestModels.Add(new TestModel() { Name = "Some Developer", Company = "Developer Co." });
            dbcontext.SaveChanges();
            _testmodelservice = new TestModelService(dbcontext);
            return dbcontext;
        }

        protected virtual TestDbContext DbContextSetupForAssertion(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<TestDbContext>().UseSqlite(connection).Options;
            var dbcontext = new TestDbContext(options);
            return dbcontext;
        }

        protected async Task RunTest(Func<TestDbContext, Task> testFunc, Func<TestDbContext, Task> assertFunc)
        {

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                using (var context = DbContextSetup(connection))
                {
                    if (testFunc != null)
                    {
                        await testFunc.Invoke(context);
                    }
                }

                using (var context = DbContextSetupForAssertion(connection))
                {
                    if (assertFunc != null)
                    {
                        await assertFunc.Invoke(context);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        protected async Task RunTest(Func<TestDbContext, Task> testFunc, Action<TestDbContext> assertFunc)
        {

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                using (var context = DbContextSetup(connection))
                {
                    if (testFunc != null)
                    {
                        await testFunc.Invoke(context);
                    }
                }

                using (var context = DbContextSetupForAssertion(connection))
                {
                    if (assertFunc != null)
                    {
                        assertFunc.Invoke(context);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        protected async Task RunTest(Action<TestDbContext> testFunc, Func<TestDbContext, Task> assertFunc)
        {

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                using (var context = DbContextSetup(connection))
                {
                    if (testFunc != null)
                    {
                        testFunc.Invoke(context);
                    }
                }

                using (var context = DbContextSetupForAssertion(connection))
                {
                    if (assertFunc != null)
                    {
                        await assertFunc.Invoke(context);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }

        protected void RunTest(Action<TestDbContext> testFunc, Action<TestDbContext> assertFunc)
        {

            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            try
            {
                using (var context = DbContextSetup(connection))
                {
                    if (testFunc != null)
                    {
                        testFunc.Invoke(context);
                    }
                }

                using (var context = DbContextSetupForAssertion(connection))
                {
                    if (assertFunc != null)
                    {
                        assertFunc.Invoke(context);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
