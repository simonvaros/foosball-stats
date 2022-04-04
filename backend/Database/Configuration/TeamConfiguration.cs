using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasMany(t => t.Players)
                .WithMany(p => p.Teams);

            builder.HasMany(t => t.EventTeams)
                .WithOne(et => et.Team)
                .HasForeignKey(et => et.TeamId);
        }
    }
}