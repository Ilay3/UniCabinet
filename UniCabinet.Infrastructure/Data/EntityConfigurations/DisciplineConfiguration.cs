using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    public class DisciplineConfiguration : IEntityTypeConfiguration<Discipline>
    {
        public void Configure(EntityTypeBuilder<Discipline> builder)
        {
            builder.HasKey(d => d.Id);

            builder.HasMany(d => d.DisciplineDetails)
                .WithOne(dd => dd.Discipline)
                .HasForeignKey(dd => dd.DisciplineId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
