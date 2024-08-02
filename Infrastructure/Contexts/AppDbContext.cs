using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        DotNetEnv.Env.TraversePath().Load();
    }

    // configuring connection
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connectionstring = Environment.GetEnvironmentVariable("MYDIARY_POSTGRES_CONNECTIONSTRING")
         ?? throw new("<MYDIARY_POSTGRES_CONNECTIONSTRING> environment is required!");

        options.UseNpgsql(connectionstring);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>(entity =>
        {
            entity.HasIndex(property => property.Email).IsUnique();
        });
    }

    public DbSet<Note> Notes { get; set; }
    public DbSet<User> Users { get; set; }
}