using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    public class SemesterConfiguration : IEntityTypeConfiguration<SemesterEntity>
    {
        public void Configure(EntityTypeBuilder<SemesterEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder.HasMany(s => s.Groups)
                .WithOne(g => g.Semester)
                .HasForeignKey(g => g.SemesterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
