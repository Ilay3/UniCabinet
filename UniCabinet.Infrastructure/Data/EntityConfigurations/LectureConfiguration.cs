using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    public class LectureConfiguration : IEntityTypeConfiguration<Lecture>
    {
        public void Configure(EntityTypeBuilder<Lecture> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.LectureNumber)
                .HasColumnType("decimal(18, 2)");

            builder.HasOne(l => l.DisciplineDetails)
                .WithMany(dd => dd.Lectures)
                .HasForeignKey(l => l.DisciplineDetailId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
