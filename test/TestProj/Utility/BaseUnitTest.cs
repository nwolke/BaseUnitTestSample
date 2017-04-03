using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TestLibrary.Models;
using TestLibrary.Services;

namespace TestProj.Utility
{
    /// <summary>
    /// The core of this project
    /// </summary>
    public class BaseUnitTest
    {
        protected TestModelService _testmodelservice;

        /// <summary>
        /// This sets up the Sqlite in-memory connection, creates the database based on the dbcontext, seeds any necessary data
        /// and creates the TestModelService to be used
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
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

        /// <summary>
        /// This re-establishes the connection to the in-memory database for assertion requirements
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        protected virtual TestDbContext DbContextSetupForAssertion(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<TestDbContext>().UseSqlite(connection).Options;
            var dbcontext = new TestDbContext(options);
            return dbcontext;
        }

        /// <summary>
        /// Takes a pair of Func delegates that are Async in nature to run 
        /// </summary>
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

        /// <summary>
        /// This takes a Func delegate for running the test and an Action delegate for assertion methods
        /// </summary>
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

        /// <summary>
        /// This takes an Action delegate for test running and a Func delegate for Async assertions. Probably not going
        /// to be used very much at all, but figured better have it in place, just in case.
        /// </summary>
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

        /// <summary>
        /// This takes Action delegates for both testing and assertion
        /// </summary>
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
