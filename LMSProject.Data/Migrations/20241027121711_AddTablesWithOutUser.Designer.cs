﻿// <auto-generated />
using System;
using LMSProject.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LMSProject.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241027121711_AddTablesWithOutUser")]
    partial class AddTablesWithOutUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssignmentId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.HasKey("AssignmentId");

                    b.HasIndex("CourseId");

                    b.ToTable("Assignments", (string)null);
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Certificate", b =>
                {
                    b.Property<int>("CertificateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificateId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("IssueDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CertificateId");

                    b.HasIndex("CourseId");

                    b.ToTable("Certificates", (string)null);
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Level")
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.HasKey("CourseId");

                    b.ToTable("Courses", (string)null);
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnrollmentId"));

                    b.Property<int>("CourserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EnrollmentId");

                    b.HasIndex("CourserId");

                    b.ToTable("Enrollments", (string)null);
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Forum", b =>
                {
                    b.Property<int>("ForumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ForumId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("VARCHAR");

                    b.HasKey("ForumId");

                    b.HasIndex("CourseId");

                    b.ToTable("Forums", (string)null);
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.ForumPost", b =>
                {
                    b.Property<int>("ForumPostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ForumPostId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)")
                        .HasColumnName("VARCHAR");

                    b.Property<int>("ForumId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PostDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ForumPostId");

                    b.HasIndex("ForumId");

                    b.ToTable("ForumPosts", (string)null);
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Grade", b =>
                {
                    b.Property<int>("GradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GradeId"));

                    b.Property<int>("SubmissionId")
                        .HasColumnType("int");

                    b.Property<float>("grade")
                        .HasColumnType("real");

                    b.HasKey("GradeId");

                    b.HasIndex("SubmissionId")
                        .IsUnique();

                    b.ToTable("Grades", (string)null);
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Lesson", b =>
                {
                    b.Property<int>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LessonId"));

                    b.Property<string>("Content")
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR");

                    b.Property<int>("ModuleId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR");

                    b.HasKey("LessonId");

                    b.HasIndex("ModuleId");

                    b.ToTable("Lessons", (string)null);
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Module", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ModuleId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("VARCHAR");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("VARCHAR");

                    b.HasKey("ModuleId");

                    b.HasIndex("CourseId");

                    b.ToTable("Modules", (string)null);
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NotificationId"));

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<DateTime>("SendDate")
                        .HasColumnType("datetime2");

                    b.HasKey("NotificationId");

                    b.ToTable("Notifications", (string)null);
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Submission", b =>
                {
                    b.Property<int>("SubmissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubmissionId"));

                    b.Property<int>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("VARCHAR");

                    b.Property<int>("GradeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SubmissionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SubmissionId");

                    b.HasIndex("AssignmentId");

                    b.ToTable("Submissions", (string)null);
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Assignment", b =>
                {
                    b.HasOne("LMSProject.Data.Data.Enities.Course", "Course")
                        .WithMany("Assignments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Certificate", b =>
                {
                    b.HasOne("LMSProject.Data.Data.Enities.Course", "Course")
                        .WithMany("Certificates")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Enrollment", b =>
                {
                    b.HasOne("LMSProject.Data.Data.Enities.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Forum", b =>
                {
                    b.HasOne("LMSProject.Data.Data.Enities.Course", "Course")
                        .WithMany("Forums")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.ForumPost", b =>
                {
                    b.HasOne("LMSProject.Data.Data.Enities.Forum", "Forum")
                        .WithMany("ForumPosts")
                        .HasForeignKey("ForumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Forum");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Grade", b =>
                {
                    b.HasOne("LMSProject.Data.Data.Enities.Submission", "Submission")
                        .WithOne("grade")
                        .HasForeignKey("LMSProject.Data.Data.Enities.Grade", "SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Lesson", b =>
                {
                    b.HasOne("LMSProject.Data.Data.Enities.Module", "Module")
                        .WithMany("Lessons")
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Module");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Module", b =>
                {
                    b.HasOne("LMSProject.Data.Data.Enities.Course", "Course")
                        .WithMany("Modules")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Submission", b =>
                {
                    b.HasOne("LMSProject.Data.Data.Enities.Assignment", "Assignment")
                        .WithMany("Submissions")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Assignment", b =>
                {
                    b.Navigation("Submissions");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Course", b =>
                {
                    b.Navigation("Assignments");

                    b.Navigation("Certificates");

                    b.Navigation("Enrollments");

                    b.Navigation("Forums");

                    b.Navigation("Modules");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Forum", b =>
                {
                    b.Navigation("ForumPosts");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Module", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("LMSProject.Data.Data.Enities.Submission", b =>
                {
                    b.Navigation("grade")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
