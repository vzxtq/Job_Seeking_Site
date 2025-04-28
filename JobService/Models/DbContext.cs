using Microsoft.EntityFrameworkCore;

namespace JobService.Models
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions<JobDbContext> options) : base(options) {}
        public DbSet<JobModel> Jobs { get; set; }
    }
}