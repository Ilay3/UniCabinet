﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UniCabinet.Infrastructure.Data;

#nullable disable

namespace UniCabinet.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241128171841_AddFkDepatrmnetIdForUser")]
    partial class AddFkDepatrmnetIdForUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
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

                    b.ToTable("Roles", (string)null);
                });

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

                    b.ToTable("RoleClaims", (string)null);
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

                    b.ToTable("UserClaims", (string)null);
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

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
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

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.CourseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Number = 1
                        },
                        new
                        {
                            Id = 2,
                            Number = 2
                        },
                        new
                        {
                            Id = 3,
                            Number = 3
                        },
                        new
                        {
                            Id = 4,
                            Number = 4
                        },
                        new
                        {
                            Id = 5,
                            Number = 5
                        });
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.DisciplineDetailEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AutoExamThreshold")
                        .HasColumnType("int");

                    b.Property<int>("CourseId")
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

                    b.Property<int>("PassCount")
                        .HasColumnType("int");

                    b.Property<int>("PracticalCount")
                        .HasColumnType("int");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<int>("SubExamCount")
                        .HasColumnType("int");

                    b.Property<string>("TeacherId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("GroupId");

                    b.HasIndex("SemesterId");

                    b.HasIndex("TeacherId");

                    b.ToTable("DisciplineDetails");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.DisciplineEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpecialtyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.ExamEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisciplineDetailId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineDetailId");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.ExamResultEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ExamId")
                        .HasColumnType("int");

                    b.Property<decimal>("FinalPoint")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<bool>("IsAutomatic")
                        .HasColumnType("bit");

                    b.Property<decimal>("PointAvarage")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ExamId");

                    b.HasIndex("StudentId");

                    b.ToTable("ExamResults");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.GroupEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<string>("TypeGroup")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("SemesterId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.LectureEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisciplineDetailId")
                        .HasColumnType("int");

                    b.Property<decimal>("Number")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<decimal>("PointsCount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineDetailId");

                    b.ToTable("Lectures");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.LectureVisitEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsVisit")
                        .HasColumnType("bit");

                    b.Property<int>("LectureId")
                        .HasColumnType("int");

                    b.Property<decimal>("PointsCount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.HasIndex("StudentId");

                    b.ToTable("LectureVisits");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.PracticalEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DisciplineDetailId")
                        .HasColumnType("int");

                    b.Property<int>("PracticalNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineDetailId");

                    b.ToTable("Practicals");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.PracticalResultEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("Point")
                        .HasColumnType("int");

                    b.Property<int>("PracticalId")
                        .HasColumnType("int");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("PracticalId");

                    b.HasIndex("StudentId");

                    b.ToTable("PracticalResults");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.SemesterEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DayEnd")
                        .HasColumnType("int");

                    b.Property<int>("DayStart")
                        .HasColumnType("int");

                    b.Property<int>("MounthEnd")
                        .HasColumnType("int");

                    b.Property<int>("MounthStart")
                        .HasColumnType("int");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Semesters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DayEnd = 25,
                            DayStart = 1,
                            MounthEnd = 1,
                            MounthStart = 9,
                            Number = 1
                        },
                        new
                        {
                            Id = 2,
                            DayEnd = 30,
                            DayStart = 7,
                            MounthEnd = 6,
                            MounthStart = 2,
                            Number = 2
                        });
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.SpecialtyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.StudentProgressEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisciplineDetailId")
                        .HasColumnType("int");

                    b.Property<int>("FinalGrade")
                        .HasColumnType("int");

                    b.Property<bool>("NeedsRetake")
                        .HasColumnType("bit");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TotalLecturePoints")
                        .HasColumnType("int");

                    b.Property<int>("TotalPoints")
                        .HasColumnType("int");

                    b.Property<int>("TotalPracticalPoints")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineDetailId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentProgresses");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateBirthday")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
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

                    b.Property<string>("Patronymic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpecialtyId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("GroupId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("SpecialtyId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("UniCabinet.Domain.Models.DepartmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DepartmentName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.UserEntity", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.DisciplineDetailEntity", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.CourseEntity", "Course")
                        .WithMany("DisciplineDetails")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.DisciplineEntity", "Discipline")
                        .WithMany("DisciplineDetails")
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.GroupEntity", "Group")
                        .WithMany("DisciplineDetails")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.SemesterEntity", "Semester")
                        .WithMany("DisciplineDetials")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.UserEntity", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Course");

                    b.Navigation("Discipline");

                    b.Navigation("Group");

                    b.Navigation("Semester");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.DisciplineEntity", b =>
                {
                    b.HasOne("UniCabinet.Domain.Models.DepartmentEntity", "Department")
                        .WithMany("Discipline")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("UniCabinet.Domain.Entities.SpecialtyEntity", "Specialty")
                        .WithMany()
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Department");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.ExamEntity", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.DisciplineDetailEntity", "DisciplineDetails")
                        .WithMany("Exams")
                        .HasForeignKey("DisciplineDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DisciplineDetails");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.ExamResultEntity", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.ExamEntity", "Exam")
                        .WithMany("ExamResults")
                        .HasForeignKey("ExamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.UserEntity", "Student")
                        .WithMany("ExamResults")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Exam");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.GroupEntity", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.CourseEntity", "Course")
                        .WithMany("Groups")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.SemesterEntity", "Semester")
                        .WithMany("Groups")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Semester");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.LectureEntity", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.DisciplineDetailEntity", "DisciplineDetails")
                        .WithMany("Lectures")
                        .HasForeignKey("DisciplineDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DisciplineDetails");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.LectureVisitEntity", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.LectureEntity", "Lecture")
                        .WithMany("LectureVisits")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.UserEntity", "Student")
                        .WithMany("LectureVisits")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Lecture");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.PracticalEntity", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.DisciplineDetailEntity", "DisciplineDetails")
                        .WithMany("Practicals")
                        .HasForeignKey("DisciplineDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DisciplineDetails");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.PracticalResultEntity", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.PracticalEntity", "Practical")
                        .WithMany("PracticalResults")
                        .HasForeignKey("PracticalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.UserEntity", "Student")
                        .WithMany("PracticalResults")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Practical");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.StudentProgressEntity", b =>
                {
                    b.HasOne("UniCabinet.Domain.Entities.DisciplineDetailEntity", "DisciplineDetails")
                        .WithMany("StudentProgresses")
                        .HasForeignKey("DisciplineDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniCabinet.Domain.Entities.UserEntity", "Student")
                        .WithMany("StudentProgresses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("DisciplineDetails");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.UserEntity", b =>
                {
                    b.HasOne("UniCabinet.Domain.Models.DepartmentEntity", "DepartmentEntity")
                        .WithMany("User")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("UniCabinet.Domain.Entities.GroupEntity", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId");

                    b.HasOne("UniCabinet.Domain.Entities.SpecialtyEntity", "Specialty")
                        .WithMany("Teachers")
                        .HasForeignKey("SpecialtyId");

                    b.Navigation("DepartmentEntity");

                    b.Navigation("Group");

                    b.Navigation("Specialty");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.CourseEntity", b =>
                {
                    b.Navigation("DisciplineDetails");

                    b.Navigation("Groups");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.DisciplineDetailEntity", b =>
                {
                    b.Navigation("Exams");

                    b.Navigation("Lectures");

                    b.Navigation("Practicals");

                    b.Navigation("StudentProgresses");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.DisciplineEntity", b =>
                {
                    b.Navigation("DisciplineDetails");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.ExamEntity", b =>
                {
                    b.Navigation("ExamResults");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.GroupEntity", b =>
                {
                    b.Navigation("DisciplineDetails");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.LectureEntity", b =>
                {
                    b.Navigation("LectureVisits");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.PracticalEntity", b =>
                {
                    b.Navigation("PracticalResults");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.SemesterEntity", b =>
                {
                    b.Navigation("DisciplineDetials");

                    b.Navigation("Groups");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.SpecialtyEntity", b =>
                {
                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("UniCabinet.Domain.Entities.UserEntity", b =>
                {
                    b.Navigation("ExamResults");

                    b.Navigation("LectureVisits");

                    b.Navigation("PracticalResults");

                    b.Navigation("StudentProgresses");
                });

            modelBuilder.Entity("UniCabinet.Domain.Models.DepartmentEntity", b =>
                {
                    b.Navigation("Discipline");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
