using System.Reflection;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using RPS.Core.Entities;

namespace RPS.DataAccess.Persistence;

public class DatabaseContext : DbContext
{

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Game> Games { get; set; }

    public DbSet<Round> Rounds { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<Game>().HasMany(s => s.Rounds).WithOne(s => s.Game);
        builder.Entity<Game>().Navigation(e => e.Rounds).AutoInclude();

        builder.Entity<Round>().HasMany(s => s.Players).WithOne(s => s.Round);
        builder.Entity<Round>().Navigation(e => e.Players).AutoInclude();

        base.OnModelCreating(builder);
    }

}
