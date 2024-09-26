using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniCabinet.Domain.Entities;

namespace UniCabinet.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string,
        IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
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
        public DbSet<DisciplineOffering> DisciplineOfferings { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Practical> Practicals { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<LectureAttendance> LectureAttendances { get; set; }
        public DbSet<PracticalResult> PracticalResults { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<StudentProgress> StudentProgresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);




            // Конфигурация для UserRole (многие-ко-многим между User и Role)
            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            // Конфигурация для LectureAttendance
            builder.Entity<LectureAttendance>(entity =>
            {
                entity.HasKey(e => e.AttendanceId); // Явное определение первичного ключа

                entity.HasOne(e => e.Student)
                    .WithMany(u => u.LectureAttendances)
                    .HasForeignKey(e => e.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Lecture)
                    .WithMany(l => l.LectureAttendances)
                    .HasForeignKey(e => e.LectureId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<PracticalResult>(entity =>
            {
                entity.HasKey(e => e.ResultId); // Первичный ключ

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
                entity.HasKey(e => e.ExamResultId); // Первичный ключ

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
                .HasOne(g => g.CurrentCourse)
                .WithMany(c => c.Groups)
                .HasForeignKey(g => g.CurrentCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Конфигурация для Group → Semester (многие-ко-одному)
            builder.Entity<Group>()
                .HasOne(g => g.CurrentSemester)
                .WithMany(s => s.Groups)
                .HasForeignKey(g => g.CurrentSemesterId)
                .OnDelete(DeleteBehavior.Restrict);

            // Конфигурация для DisciplineOffering → Teacher (многие-ко-одному)
            builder.Entity<DisciplineOffering>()
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

            // Конфигурация для DisciplineOffering → Discipline (многие-ко-одному)
            builder.Entity<DisciplineOffering>()
                .HasOne(d => d.Discipline)
                .WithMany(d => d.DisciplineOfferings)
                .HasForeignKey(d => d.DisciplineId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для DisciplineOffering → Group (многие-ко-одному)
            builder.Entity<DisciplineOffering>()
                .HasOne(d => d.Group)
                .WithMany(g => g.DisciplineOfferings)
                .HasForeignKey(d => d.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для DisciplineOffering → Semester (многие-ко-одному)
            builder.Entity<DisciplineOffering>()
                .HasOne(d => d.Semester)
                .WithMany(s => s.DisciplineOfferings)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для Lecture → DisciplineOffering (многие-ко-одному)
            builder.Entity<Lecture>()
                .HasOne(l => l.DisciplineOffering)
                .WithMany(d => d.Lectures)
                .HasForeignKey(l => l.DisciplineOfferingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для LectureAttendance → Lecture (многие-ко-одному)
            builder.Entity<LectureAttendance>()
                .HasOne(a => a.Lecture)
                .WithMany(l => l.LectureAttendances)
                .HasForeignKey(a => a.LectureId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для LectureAttendance → User (многие-ко-одному)
            builder.Entity<LectureAttendance>()
                .HasOne(a => a.Student)
                .WithMany(u => u.LectureAttendances)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для Practical → DisciplineOffering (многие-ко-одному)
            builder.Entity<Practical>()
                .HasOne(p => p.DisciplineOffering)
                .WithMany(d => d.Practicals)
                .HasForeignKey(p => p.DisciplineOfferingId)
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

            // Конфигурация для Exam → DisciplineOffering (многие-ко-одному)
            builder.Entity<Exam>()
                .HasOne(e => e.DisciplineOffering)
                .WithMany(d => d.Exams)
                .HasForeignKey(e => e.DisciplineOfferingId)
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

            // Конфигурация для StudentProgress → DisciplineOffering (многие-ко-одному)
            builder.Entity<StudentProgress>()
                .HasOne(sp => sp.DisciplineOffering)
                .WithMany(d => d.StudentProgresses)
                .HasForeignKey(sp => sp.DisciplineOfferingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для StudentProgress → User (многие-ко-одному)
            builder.Entity<StudentProgress>()
                .HasOne(sp => sp.Student)
                .WithMany(u => u.StudentProgresses)
                .HasForeignKey(sp => sp.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Конфигурация для Discipline → DisciplineOfferings (один-ко-многим)
            builder.Entity<Discipline>()
                .HasMany(d => d.DisciplineOfferings)
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
                .WithOne(g => g.CurrentCourse)
                .HasForeignKey(g => g.CurrentCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Конфигурация для Semester → Groups (один-ко-многим)
            builder.Entity<Semester>()
                .HasMany(s => s.Groups)
                .WithOne(g => g.CurrentSemester)
                .HasForeignKey(g => g.CurrentSemesterId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
