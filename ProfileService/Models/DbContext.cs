using Microsoft.EntityFrameworkCore;
using ProfileService.DTOs;

namespace ProfileService.Models
{
    public class ProfileDbContext : DbContext
    {
        public ProfileDbContext(DbContextOptions<ProfileDbContext> options) : base(options) {}

        public DbSet<ProfileModel> Profiles { get; set; }
    }
}