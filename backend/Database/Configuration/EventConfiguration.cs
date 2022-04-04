using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);
            
            builder.Property(e => e.Date)
                .HasColumnType("timestamp with time zone")
                .HasDefaultValueSql("now()");

            builder.HasMany(e => e.Teams)
                .WithOne(et => et.Event)
                .HasForeignKey(et => et.EventId);
        }
    }
}