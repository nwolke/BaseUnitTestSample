using TestLibrary.Models;

namespace TestProj.Utility
{
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
