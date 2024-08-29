using BaseArchiCsharp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BaseArchiCsharp.Infrastructure.Common.Behavior;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    // Add all tables entities here
    public DbSet<User> Users { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
        base.OnModelCreating(modelBuilder);
    }
}