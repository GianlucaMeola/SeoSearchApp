using Microsoft.EntityFrameworkCore;
using WebScapperAPI.Models;

namespace WebScapperAPI.Data
{
    public class EngineResultDbContext : DbContext
    {
        public EngineResultDbContext(DbContextOptions<EngineResultDbContext> options) : base(options)
        {
        }

        public DbSet<EngineResult> EngineResults { get; set; }
    }
}
