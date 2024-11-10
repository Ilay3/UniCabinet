using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    public class ExamConfiguration : IEntityTypeConfiguration<ExamEntity>
    {
        public void Configure(EntityTypeBuilder<ExamEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.DisciplineDetails)
                .WithMany(dd => dd.Exams)
                .HasForeignKey(e => e.DisciplineDetailId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
