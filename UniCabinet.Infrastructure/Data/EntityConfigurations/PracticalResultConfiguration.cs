using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    public class PracticalResultConfiguration : IEntityTypeConfiguration<PracticalResultEntity>
    {
        public void Configure(EntityTypeBuilder<PracticalResultEntity> builder)
        {
            builder.HasKey(pr => pr.Id);

            builder.HasOne(pr => pr.Student)
                .WithMany(u => u.PracticalResults)
                .HasForeignKey(pr => pr.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pr => pr.Practical)
                .WithMany(p => p.PracticalResults)
                .HasForeignKey(pr => pr.PracticalId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
