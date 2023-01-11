using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RPS.Core.Entities;
using RPS.Core.Enums;

namespace RPS.DataAccess.Persistence;

public class DatabaseContext : DbContext
{

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<SystemTask> Tasks { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<SystemTask>()
       .HasData(
           new SystemTask
           {
               Title = "C1",
               Importance = Importance.Critical
           },
         new SystemTask
         {
             Title = "C2",
             Importance = Importance.Critical
         },
         new SystemTask
         {
             Title = "C3",
             Importance = Importance.Critical
         },
         new SystemTask
         {
             Title = "E1",
             Importance = Importance.Essential
         },
          new SystemTask
          {
              Title = "E2",
              Importance = Importance.Essential
          },
          new SystemTask
          {
              Title = "E3",
              Importance = Importance.Essential
          },
           new SystemTask
           {
               Title = "E4",
               Importance = Importance.Essential
           },
          new SystemTask
          {
              Title = "E5",
              Importance = Importance.Essential
          },
          new SystemTask
          {
              Title = "E6",
              Importance = Importance.Essential
          },
          new SystemTask
          {
              Title = "E7",
              Importance = Importance.Essential
          },
          new SystemTask
          {
              Title = "I1",
              Importance = Importance.Important
          },
           new SystemTask
           {
               Title = "I2",
               Importance = Importance.Important
           },
           new SystemTask
           {
               Title = "I3",
               Importance = Importance.Important
           },
           new SystemTask
           {
               Title = "I4",
               Importance = Importance.Important
           },
           new SystemTask
           {
               Title = "I5",
               Importance = Importance.Important
           }
       );
        base.OnModelCreating(builder);
    }

}
