using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    public class ExamResultConfiguration : IEntityTypeConfiguration<ExamResult>
    {
        public void Configure(EntityTypeBuilder<ExamResult> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FinalPoint)
                .HasColumnType("decimal(18, 2)");

            builder.Property(e => e.PointAvarage)
                .HasColumnType("decimal(18, 2)");

            builder.HasOne(e => e.Student)
                .WithMany(u => u.ExamResults)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Exam)
                .WithMany(e => e.ExamResults)
                .HasForeignKey(e => e.ExamId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
