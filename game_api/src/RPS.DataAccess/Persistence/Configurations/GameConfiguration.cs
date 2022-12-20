using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RPS.Core.Entities;

namespace RPS.DataAccess.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasMany(tl => tl.Rounds)
            .WithOne(ti => ti.Game)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
