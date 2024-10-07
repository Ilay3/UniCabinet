using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    public class PracticalConfiguration : IEntityTypeConfiguration<Practical>
    {
        public void Configure(EntityTypeBuilder<Practical> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.DisciplineDetails)
                .WithMany(dd => dd.Practicals)
                .HasForeignKey(p => p.DisciplineDetailId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
