using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace TestLibrary.Models
{
    public interface ITestDbContext
    {
        DbSet<TestModel> TestModels { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

    }
}
