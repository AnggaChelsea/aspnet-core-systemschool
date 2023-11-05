﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetAngularAuthWebApi.Context;

#nullable disable

namespace NetAngularAuthWebApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231104224425_addfileupload")]
    partial class addfileupload
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.CartCourse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("CartCourses");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AuthorsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CartCourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("EditorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageBanner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<Guid?>("SchoolClassId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TypeCourse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorsId");

                    b.HasIndex("CartCourseId");

                    b.HasIndex("EditorId");

                    b.HasIndex("SchoolClassId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.CourseStudent", b =>
                {
                    b.Property<Guid>("CourseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("CourseStudent", (string)null);
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.FileDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("FileData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FileType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FileDetails");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.Jadwal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("JamMapel")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("KelasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MapelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SchoolClassId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("MapelId");

                    b.HasIndex("SchoolClassId");

                    b.ToTable("Jadwals");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.Mapel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsTugas")
                        .HasColumnType("bit");

                    b.Property<string>("NamaMapel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Mapels");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.Roles", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameFileUpload")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RolesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RolesId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.TimeSchool", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("TimeSchools");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.SchoolClass", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageSchool")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameOfHeadSchool")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameOfSchool")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeClose")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TimeStart")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("SchoolClasses");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsChangeSchool")
                        .HasColumnType("bit");

                    b.Property<int>("NIM")
                        .HasColumnType("int");

                    b.Property<string>("NameSchoolBefore")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegisterTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("RolesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SchoolClassId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RolesId");

                    b.HasIndex("SchoolClassId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.CartCourse", b =>
                {
                    b.HasOne("NetAngularAuthWebApi.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.Course", b =>
                {
                    b.HasOne("NetAngularAuthWebApi.Models.Domain.Teacher", "Authors")
                        .WithMany("CourseWritenn")
                        .HasForeignKey("AuthorsId");

                    b.HasOne("NetAngularAuthWebApi.Models.Domain.CartCourse", null)
                        .WithMany("Courses")
                        .HasForeignKey("CartCourseId");

                    b.HasOne("NetAngularAuthWebApi.Models.Domain.Teacher", "Editor")
                        .WithMany()
                        .HasForeignKey("EditorId");

                    b.HasOne("NetAngularAuthWebApi.Models.SchoolClass", null)
                        .WithMany("Courses")
                        .HasForeignKey("SchoolClassId");

                    b.Navigation("Authors");

                    b.Navigation("Editor");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.CourseStudent", b =>
                {
                    b.HasOne("NetAngularAuthWebApi.Models.Domain.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetAngularAuthWebApi.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.Jadwal", b =>
                {
                    b.HasOne("NetAngularAuthWebApi.Models.Domain.Mapel", "Mapels")
                        .WithMany()
                        .HasForeignKey("MapelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NetAngularAuthWebApi.Models.SchoolClass", "SchoolClass")
                        .WithMany("Jadwals")
                        .HasForeignKey("SchoolClassId");

                    b.Navigation("Mapels");

                    b.Navigation("SchoolClass");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.Mapel", b =>
                {
                    b.HasOne("NetAngularAuthWebApi.Models.Domain.Teacher", "Teachers")
                        .WithMany("Mapels")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.Teacher", b =>
                {
                    b.HasOne("NetAngularAuthWebApi.Models.Domain.Roles", "Roles")
                        .WithMany("Teachers")
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Student", b =>
                {
                    b.HasOne("NetAngularAuthWebApi.Models.Domain.Roles", "Roles")
                        .WithMany("Students")
                        .HasForeignKey("RolesId");

                    b.HasOne("NetAngularAuthWebApi.Models.SchoolClass", "SchoolClass")
                        .WithMany("Students")
                        .HasForeignKey("SchoolClassId");

                    b.Navigation("Roles");

                    b.Navigation("SchoolClass");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.CartCourse", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.Roles", b =>
                {
                    b.Navigation("Students");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.Domain.Teacher", b =>
                {
                    b.Navigation("CourseWritenn");

                    b.Navigation("Mapels");
                });

            modelBuilder.Entity("NetAngularAuthWebApi.Models.SchoolClass", b =>
                {
                    b.Navigation("Courses");

                    b.Navigation("Jadwals");

                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
