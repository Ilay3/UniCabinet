using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    public class LectureConfiguration : IEntityTypeConfiguration<LectureEntity>
    {
        public void Configure(EntityTypeBuilder<LectureEntity> builder)
        {
            builder.HasKey(l => l.Id);


            builder.HasOne(l => l.DisciplineDetails)
                .WithMany(dd => dd.Lectures)
                .HasForeignKey(l => l.DisciplineDetailId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
