using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Domain.Entities;
using UniCabinet.Infrastructure.Data.EntityConfigurations;

namespace UniCabinet.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet для сущностей
        public DbSet<Course> Courses { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<DisciplineDetail> DisciplineDetails { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Practical> Practicals { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<LectureVisit> LectureVisits { get; set; }
        public DbSet<PracticalResult> PracticalResults { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<StudentProgress> StudentProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Переименовываем таблицы Identity
            builder.Entity<User>(b => b.ToTable("Users"));
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

            builder.Entity<Course>().HasData(
                new Course {Id = 1, Number = 1 },
                new Course {Id = 2, Number = 2 },
                new Course {Id = 3, Number = 3 },
                new Course {Id = 4, Number = 4 },
                new Course {Id = 5, Number = 5 });
            
            builder.Entity<Semester>().HasData(
                new Semester { Id = 1, Number = 1, DayStart = 1, MounthStart = 9, DayEnd = 25, MounthEnd = 1 },
                new Semester { Id = 2, Number = 2, DayStart = 7, MounthStart = 2, DayEnd = 30, MounthEnd = 6 });
        }
    }
}
