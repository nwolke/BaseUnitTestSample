# BaseUnitTestSample

## Purpose

This project is to showcase my solution for using a SQLite in-memory database for unit tests and provide a means for streamlining the setup of the DbContext and any data seeding.

### Core Functionality

The BaseUnitTest class has a pair of methods for setting up the DbContext (`DbContextSetup` and `DbContextSetupForAssertion`) that is then used in the `RunTest` methods.

`DbContextSetup` will make the connection to the in-memory Sqlite database and then call `.EnsureCreated` to create the tables. Then, if needed, some test data seeding can be applied. It then returns the DbContext object.

`DbContextSetupForAssertion` will remake that connection, under the notion that you are connecting again, and simply return that DbContext for any data retrieval needs for test assertion.

There are **four** `RunTest` methods. Each take a combination of `Func<TestDbContext, Task>` for Async methods and `Action<TestDbContext>` for non-Async methods.
