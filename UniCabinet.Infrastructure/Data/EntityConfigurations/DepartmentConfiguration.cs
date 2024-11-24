using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniCabinet.Domain.Entities;
using UniCabinet.Domain.Models;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    class DepartmentConfiguration : IEntityTypeConfiguration<DepartmentEntity>
    {
        public void Configure(EntityTypeBuilder<DepartmentEntity> builder)
        {

            builder.HasOne(d => d.User)
                .WithOne(user => user.DepartmentEntity)
                .HasForeignKey<DepartmentEntity>(department => department.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Discipline)
                .WithOne(discipline => discipline.Department)
                .HasForeignKey(discipline => discipline.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}