using System.Threading.Tasks;
using TestLibrary.Models;

namespace TestLibrary.Services
{
    public interface ITestModelService
    {
        Task<bool> AddTestModel(TestModel model);
    }
}
