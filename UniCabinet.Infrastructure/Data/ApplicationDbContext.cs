using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Domain.Entities;
using UniCabinet.Domain.Models;
using UniCabinet.Infrastructure.Data.EntityConfigurations;

namespace UniCabinet.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserEntity, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet для сущностей
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<SemesterEntity> Semesters { get; set; }
        public DbSet<GroupEntity> Groups { get; set; }
        public DbSet<DisciplineEntity> Disciplines { get; set; }
        public DbSet<DisciplineDetailEntity> DisciplineDetails { get; set; }
        public DbSet<LectureEntity> Lectures { get; set; }
        public DbSet<PracticalEntity> Practicals { get; set; }
        public DbSet<ExamEntity> Exams { get; set; }
        public DbSet<LectureVisitEntity> LectureVisits { get; set; }
        public DbSet<PracticalResultEntity> PracticalResults { get; set; }
        public DbSet<ExamResultEntity> ExamResults { get; set; }
        public DbSet<StudentProgressEntity> StudentProgresses { get; set; }
        public DbSet<SpecialtyEntity> Specialties { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Переименовываем таблицы Identity
            builder.Entity<UserEntity>(b => b.ToTable("Users"));
            builder.Entity<IdentityRole>(b => b.ToTable("Roles"));
            builder.Entity<IdentityUserRole<string>>(b => b.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(b => b.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(b => b.ToTable("UserLogins"));
            builder.Entity<IdentityRoleClaim<string>>(b => b.ToTable("RoleClaims"));
            builder.Entity<IdentityUserToken<string>>(b => b.ToTable("UserTokens"));

            // Применяем конфигурации для сущностей
            builder.ApplyConfiguration(new ExamResultConfiguration());
            builder.ApplyConfiguration(new LectureVisitConfiguration());
            builder.ApplyConfiguration(new PracticalResultConfiguration());
            builder.ApplyConfiguration(new GroupConfiguration());
            builder.ApplyConfiguration(new DisciplineDetailConfiguration());
            builder.ApplyConfiguration(new DisciplineConfiguration());
            builder.ApplyConfiguration(new CourseConfiguration());
            builder.ApplyConfiguration(new SemesterConfiguration());
            builder.ApplyConfiguration(new LectureConfiguration());
            builder.ApplyConfiguration(new PracticalConfiguration());
            builder.ApplyConfiguration(new ExamConfiguration());
            builder.ApplyConfiguration(new StudentProgressConfiguration());
            builder.ApplyConfiguration(new DepartmentConfiguration());

            builder.Entity<CourseEntity>().HasData(
                new CourseEntity {Id = 1, Number = 1 },
                new CourseEntity {Id = 2, Number = 2 },
                new CourseEntity {Id = 3, Number = 3 },
                new CourseEntity {Id = 4, Number = 4 },
                new CourseEntity {Id = 5, Number = 5 });
            
            builder.Entity<SemesterEntity>().HasData(
                new SemesterEntity { Id = 1, Number = 1, DayStart = 1, MounthStart = 9, DayEnd = 25, MounthEnd = 1 },
                new SemesterEntity { Id = 2, Number = 2, DayStart = 7, MounthStart = 2, DayEnd = 30, MounthEnd = 6 });
        }
    }
}
