using TestLibrary.Models;

namespace TestProj.Utility
{
    /// <summary>
    /// simple class for creating test objects
    /// </summary>
    public static class TestObjects
    {
        public static TestModel CreateTestModel()
        {
            return new TestModel()
            {
                Name = "Tester Testerson",
                Company = "Test LLC"
            };
        }

        public static TestModel CreateTestModelToFailAdd()
        {
            return new TestModel()
            {
                Name = "Some Developer",
                Company = "Test Co."
            };
        }
    }
}
