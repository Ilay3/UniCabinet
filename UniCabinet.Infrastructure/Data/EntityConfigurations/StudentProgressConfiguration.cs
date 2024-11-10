using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    public class StudentProgressConfiguration : IEntityTypeConfiguration<StudentProgressEntity>
    {
        public void Configure(EntityTypeBuilder<StudentProgressEntity> builder)
        {
            builder.HasKey(sp => sp.Id);

            builder.HasOne(sp => sp.DisciplineDetails)
                .WithMany(dd => dd.StudentProgresses)
                .HasForeignKey(sp => sp.DisciplineDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sp => sp.Student)
                .WithMany(u => u.StudentProgresses)
                .HasForeignKey(sp => sp.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
