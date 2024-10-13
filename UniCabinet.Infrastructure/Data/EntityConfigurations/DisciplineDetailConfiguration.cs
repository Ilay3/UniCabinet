using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    public class DisciplineDetailConfiguration : IEntityTypeConfiguration<DisciplineDetail>
    {
        public void Configure(EntityTypeBuilder<DisciplineDetail> builder)
        {
            builder.HasKey(dd => dd.Id);

            builder.HasOne(dd => dd.Teacher)
                .WithMany()
                .HasForeignKey(dd => dd.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(dd => dd.Discipline)
                .WithMany(d => d.DisciplineDetails)
                .HasForeignKey(dd => dd.DisciplineId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dd => dd.Group)
                .WithMany(g => g.DisciplineDetails)
                .HasForeignKey(dd => dd.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dd => dd.Semester)
                .WithMany(s => s.DisciplineDetials)
                .HasForeignKey(dd => dd.SemesterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dd => dd.Course)
                .WithMany(s => s.DisciplineDetails)
                .HasForeignKey(dd => dd.CourseId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
