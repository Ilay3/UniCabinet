﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniCabinet.Infrastructure.Persistence;

#nullable disable

namespace UniCabinet.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<int>("CourseNumber")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Discipline", b =>
                {
                    b.Property<int>("DisciplineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DisciplineId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DisciplineId");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.DisciplineOffering", b =>
                {
                    b.Property<int>("DisciplineOfferingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DisciplineOfferingId"));

                    b.Property<int>("AutoExamThreshold")
                        .HasColumnType("int");

                    b.Property<int>("CreditCount")
                        .HasColumnType("int");

                    b.Property<int>("DisciplineId")
                        .HasColumnType("int");

                    b.Property<int>("ExamCount")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<int>("LectureCount")
                        .HasColumnType("int");

                    b.Property<int>("MinLecturesRequired")
                        .HasColumnType("int");

                    b.Property<int>("MinPracticalsRequired")
                        .HasColumnType("int");

                    b.Property<int>("PassingScore")
                        .HasColumnType("int");

                    b.Property<int>("PracticalCount")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<string>("TeacherId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DisciplineOfferingId");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("GroupId");

                    b.HasIndex("SemesterId");

                    b.HasIndex("TeacherId");

                    b.ToTable("DisciplineOfferings");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Exam", b =>
                {
                    b.Property<int>("ExamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisciplineOfferingId")
                        .HasColumnType("int");

                    b.HasKey("ExamId");

                    b.HasIndex("DisciplineOfferingId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.ExamResult", b =>
                {
                    b.Property<int>("ExamResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamResultId"));

                    b.Property<decimal>("CalculatedGrade")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<int>("FinalGrade")
                        .HasColumnType("int");

                    b.Property<bool>("IsAutomatic")
                        .HasColumnType("bit");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ExamResultId");

                    b.HasIndex("ExamId");

                    b.HasIndex("StudentId");

                    b.ToTable("ExamResults");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"));

                    b.Property<int>("CurrentCourseId")
                        .HasColumnType("int");

                    b.Property<int>("CurrentSemesterId")
                        .HasColumnType("int");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StartYear")
                        .HasColumnType("int");

                    b.HasKey("GroupId");

                    b.HasIndex("CurrentCourseId");

                    b.HasIndex("CurrentSemesterId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Lecture", b =>
                {
                    b.Property<int>("LectureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LectureId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisciplineOfferingId")
                        .HasColumnType("int");

                    b.Property<int>("LectureNumber")
                        .HasColumnType("int");

                    b.HasKey("LectureId");

                    b.HasIndex("DisciplineOfferingId");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.LectureAttendance", b =>
                {
                    b.Property<int>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceId"));

                    b.Property<int>("LectureId")
                        .HasColumnType("int");

                    b.Property<int>("PointsAwarded")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("WasPresent")
                        .HasColumnType("bit");

                    b.HasKey("AttendanceId");

                    b.HasIndex("LectureId");

                    b.HasIndex("StudentId");

                    b.ToTable("LectureAttendances");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Practical", b =>
                {
                    b.Property<int>("PracticalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PracticalId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisciplineOfferingId")
                        .HasColumnType("int");

                    b.Property<int>("PracticalNumber")
                        .HasColumnType("int");

                    b.HasKey("PracticalId");

                    b.HasIndex("DisciplineOfferingId");

                    b.ToTable("Practicals");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.PracticalResult", b =>
                {
                    b.Property<int>("ResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResultId"));

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("PointsAwarded")
                        .HasColumnType("int");

                    b.Property<int>("PracticalId")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ResultId");

                    b.HasIndex("PracticalId");

                    b.HasIndex("StudentId");

                    b.ToTable("PracticalResults");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Semester", b =>
                {
                    b.Property<int>("SemesterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SemesterId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SemesterNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SemesterId");

                    b.HasIndex("CourseId");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.StudentProgress", b =>
                {
                    b.Property<int>("StudentProgressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentProgressId"));

                    b.Property<int>("DisciplineOfferingId")
                        .HasColumnType("int");

                    b.Property<int>("FinalGrade")
                        .HasColumnType("int");

                    b.Property<bool>("NeedsRetake")
                        .HasColumnType("bit");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TotalLecturePoints")
                        .HasColumnType("int");

                    b.Property<int>("TotalPoints")
                        .HasColumnType("int");

                    b.Property<int>("TotalPracticalPoints")
                        .HasColumnType("int");

                    b.HasKey("StudentProgressId");

                    b.HasIndex("DisciplineOfferingId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentProgresses");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.DisciplineOffering", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.Discipline", "Discipline")
                        .WithMany("DisciplineOfferings")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.Group", "Group")
                        .WithMany("DisciplineOfferings")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.Semester", "Semester")
                        .WithMany("DisciplineOfferings")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.User", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("Group");

                    b.Navigation("Semester");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Exam", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.DisciplineOffering", "DisciplineOffering")
                        .WithMany("Exams")
                        .HasForeignKey("DisciplineOfferingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DisciplineOffering");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.ExamResult", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.Exam", "Exam")
                        .WithMany("ExamResults")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.User", "Student")
                        .WithMany("ExamResults")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exam");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Group", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.Course", "CurrentCourse")
                        .WithMany("Groups")
                        .HasForeignKey("CurrentCourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.Semester", "CurrentSemester")
                        .WithMany("Groups")
                        .HasForeignKey("CurrentSemesterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CurrentCourse");

                    b.Navigation("CurrentSemester");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Lecture", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.DisciplineOffering", "DisciplineOffering")
                        .WithMany("Lectures")
                        .HasForeignKey("DisciplineOfferingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DisciplineOffering");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.LectureAttendance", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.Lecture", "Lecture")
                        .WithMany("LectureAttendances")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.User", "Student")
                        .WithMany("LectureAttendances")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecture");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Practical", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.DisciplineOffering", "DisciplineOffering")
                        .WithMany("Practicals")
                        .HasForeignKey("DisciplineOfferingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DisciplineOffering");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.PracticalResult", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.Practical", "Practical")
                        .WithMany("PracticalResults")
                        .HasForeignKey("PracticalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.User", "Student")
                        .WithMany("PracticalResults")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Practical");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Semester", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.Course", "Course")
                        .WithMany("Semesters")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.StudentProgress", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.DisciplineOffering", "DisciplineOffering")
                        .WithMany("StudentProgresses")
                        .HasForeignKey("DisciplineOfferingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.User", "Student")
                        .WithMany("StudentProgresses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DisciplineOffering");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.User", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Group");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Course", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Semesters");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Discipline", b =>
                {
                    b.Navigation("DisciplineOfferings");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.DisciplineOffering", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("Lectures");

                    b.Navigation("Practicals");

                    b.Navigation("StudentProgresses");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Exam", b =>
                {
                    b.Navigation("ExamResults");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Group", b =>
                {
                    b.Navigation("DisciplineOfferings");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Lecture", b =>
                {
                    b.Navigation("LectureAttendances");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Practical", b =>
                {
                    b.Navigation("PracticalResults");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.Semester", b =>
                {
                    b.Navigation("DisciplineOfferings");

                    b.Navigation("Groups");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.User", b =>
                {
                    b.Navigation("ExamResults");

                    b.Navigation("LectureAttendances");

                    b.Navigation("PracticalResults");

                    b.Navigation("StudentProgresses");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
