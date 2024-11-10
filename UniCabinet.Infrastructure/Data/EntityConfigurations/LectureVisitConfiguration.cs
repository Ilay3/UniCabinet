using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    public class LectureVisitConfiguration : IEntityTypeConfiguration<LectureVisitEntity>
    {
        public void Configure(EntityTypeBuilder<LectureVisitEntity> builder)
        {
            builder.HasKey(lv => lv.Id);

            builder.Property(lv => lv.PointsCount)
                .HasColumnType("decimal(18, 2)");

            builder.HasOne(lv => lv.Student)
                .WithMany(u => u.LectureVisits)
                .HasForeignKey(lv => lv.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(lv => lv.Lecture)
                .WithMany(l => l.LectureVisits)
                .HasForeignKey(lv => lv.LectureId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
