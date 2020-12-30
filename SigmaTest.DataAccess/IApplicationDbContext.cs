using Microsoft.EntityFrameworkCore;
using SigmaTest.Domain.Entities;
using System.Threading.Tasks;

namespace SigmaTest.DataAccess
{
    public interface IApplicationDbContext
    {
        DbSet<Shortner> Shortner { get; set; }       

        Task<int> SaveChangesAsync();
    }
}
