using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet для остальных сущностей
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


            // Переименовываем таблицы
            builder.Entity<User>(b => b.ToTable("Users"));
            builder.Entity<IdentityRole>(b => b.ToTable("Roles"));
            builder.Entity<IdentityUserRole<string>>(b => b.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(b => b.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(b => b.ToTable("UserLogins"));
            builder.Entity<IdentityRoleClaim<string>>(b => b.ToTable("RoleClaims"));
            builder.Entity<IdentityUserToken<string>>(b => b.ToTable("UserTokens"));


            // Конфигурация для LectureVisits
            builder.Entity<LectureVisit>(entity =>
            {
                entity.HasKey(e => e.Id); // Явное определение первичного ключа

                entity.HasOne(e => e.Student)
                    .WithMany(u => u.LectureVisits)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Lecture)
                    .WithMany(l => l.LectureVisits)
                    .HasForeignKey(e => e.LectureId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<PracticalResult>(entity =>
            {
                entity.HasKey(e => e.Id); // Первичный ключ

                entity.HasOne(e => e.Student)
                    .WithMany(u => u.PracticalResults)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Practical)
                    .WithMany(p => p.PracticalResults)
                    .HasForeignKey(e => e.PracticalId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ExamResult>(entity =>
            {
                entity.HasKey(e => e.Id); // Первичный ключ

                entity.HasOne(e => e.Student)
                    .WithMany(u => u.ExamResults)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Exam)
                    .WithMany(e => e.ExamResults)
                    .HasForeignKey(e => e.ExamId)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            // Конфигурация для Group → Course (многие-ко-одному)
            builder.Entity<Group>()
                .HasOne(g => g.Course)
                .WithMany(c => c.Groups)
                .HasForeignKey(g => g.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Конфигурация для Group → Semester (многие-ко-одному)
            builder.Entity<Group>()
                .HasOne(g => g.Semester)
                .WithMany(s => s.Groups)
                .HasForeignKey(g => g.SemesterId)
                .OnDelete(DeleteBehavior.Restrict);

            // Конфигурация для DisciplineDetails → Teacher (многие-ко-одному)
            builder.Entity<DisciplineDetail>()
                .HasOne(d => d.Teacher)
                .WithMany()
                .HasForeignKey(d => d.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // Конфигурация для User → Group (многие-ко-одному)
            builder.Entity<User>()
                .HasOne(u => u.Group)
                .WithMany(g => g.Users)
                .HasForeignKey(u => u.GroupId)
                .OnDelete(DeleteBehavior.SetNull);

            // Конфигурация для DisciplineDetail → Discipline (многие-ко-одному)
            builder.Entity<DisciplineDetail>()
                .HasOne(d => d.Discipline)
                .WithMany(d => d.DisciplineDetails)
                .HasForeignKey(d => d.DisciplineId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для DisciplineDetails → Group (многие-ко-одному)
            builder.Entity<DisciplineDetail>()
                .HasOne(d => d.Group)
                .WithMany(g => g.DisciplineDetails)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для DisciplineDetails → Semester (многие-ко-одному)
            builder.Entity<DisciplineDetail>()
                .HasOne(d => d.Semester)
                .WithMany(s => s.DisciplineDetials)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для Lecture → DisciplineDetails (многие-ко-одному)
            builder.Entity<Lecture>()
                .HasOne(l => l.DisciplineDetails)
                .WithMany(d => d.Lectures)
                .HasForeignKey(l => l.DisciplineDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для LectureAttendance → Lecture (многие-ко-одному)
            builder.Entity<LectureVisit>()
                .HasOne(a => a.Lecture)
                .WithMany(l => l.LectureVisits)
                .HasForeignKey(a => a.LectureId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для LectureAttendance → User (многие-ко-одному)
            builder.Entity<LectureVisit>()
                .HasOne(a => a.Student)
                .WithMany(u => u.LectureVisits)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для Practical → DisciplineDetails (многие-ко-одному)
            builder.Entity<Practical>()
                .HasOne(p => p.DisciplineDetails)
                .WithMany(d => d.Practicals)
                .HasForeignKey(p => p.DisciplineDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для PracticalResult → Practical (многие-ко-одному)
            builder.Entity<PracticalResult>()
                .HasOne(r => r.Practical)
                .WithMany(p => p.PracticalResults)
                .HasForeignKey(r => r.PracticalId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для PracticalResult → User (многие-ко-одному)
            builder.Entity<PracticalResult>()
                .HasOne(r => r.Student)
                .WithMany(u => u.PracticalResults)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для Exam → DisciplineDetails (многие-ко-одному)
            builder.Entity<Exam>()
                .HasOne(e => e.DisciplineDetails)
                .WithMany(d => d.Exams)
                .HasForeignKey(e => e.DisciplineDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для ExamResult → Exam (многие-ко-одному)
            builder.Entity<ExamResult>()
                .HasOne(r => r.Exam)
                .WithMany(e => e.ExamResults)
                .HasForeignKey(r => r.ExamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для ExamResult → User (многие-ко-одному)
            builder.Entity<ExamResult>()
                .HasOne(r => r.Student)
                .WithMany(u => u.ExamResults)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для StudentProgress → DisciplineDetails (многие-ко-одному)
            builder.Entity<StudentProgress>()
                .HasOne(sp => sp.DisciplineDetails)
                .WithMany(d => d.StudentProgresses)
                .HasForeignKey(sp => sp.DisciplineDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для StudentProgress → User (многие-ко-одному)
            builder.Entity<StudentProgress>()
                .HasOne(sp => sp.Student)
                .WithMany(u => u.StudentProgresses)
                .HasForeignKey(sp => sp.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для Discipline → DisciplineDetails (один-ко-многим)
            builder.Entity<Discipline>()
                .HasMany(d => d.DisciplineDetails)
                .WithOne(o => o.Discipline)
                .HasForeignKey(o => o.DisciplineId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для Course → Semesters (один-ко-многим)
            builder.Entity<Course>()
                .HasMany(c => c.Semesters)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для Course → Groups (один-ко-многим)
            builder.Entity<Course>()
                .HasMany(c => c.Groups)
                .WithOne(g => g.Course)
                .HasForeignKey(g => g.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Конфигурация для Semester → Groups (один-ко-многим)
            builder.Entity<Semester>()
                .HasMany(s => s.Groups)
                .WithOne(g => g.Semester)
                .HasForeignKey(g => g.SemesterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
