﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartJobPreppAPI.Entities;

#nullable disable

namespace SmartJobPreppAPI.Migrations
{
    [DbContext(typeof(JobDbContext))]
    [Migration("20250423150552_AddDefaultCreatedAt")]
    partial class AddDefaultCreatedAt
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmartJobPreppAPI.Entities.InterviewAnswer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("dateTime");

                    b.Property<string>("Feedback")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuestionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("InterviewAnswers");
                });

            modelBuilder.Entity("SmartJobPreppAPI.Entities.JobDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("DescriptionText")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__JobDescr__3214EC0701A9311D");

                    b.ToTable("JobDescriptions");
                });

            modelBuilder.Entity("SmartJobPreppAPI.Entities.Question", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<int?>("JobDescriptionId")
                        .HasColumnType("int");

                    b.Property<string>("QuestionText")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("PK__Question__3214EC07E480BB23");

                    b.HasIndex("JobDescriptionId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("SmartJobPreppAPI.Entities.InterviewAnswer", b =>
                {
                    b.HasOne("SmartJobPreppAPI.Entities.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Question");
                });

            modelBuilder.Entity("SmartJobPreppAPI.Entities.Question", b =>
                {
                    b.HasOne("SmartJobPreppAPI.Entities.JobDescription", "JobDescription")
                        .WithMany("Questions")
                        .HasForeignKey("JobDescriptionId")
                        .HasConstraintName("FK__Questions__JobDe__4BAC3F29");

                    b.Navigation("JobDescription");
                });

            modelBuilder.Entity("SmartJobPreppAPI.Entities.JobDescription", b =>
                {
                    b.Navigation("Questions");
                });
#pragma warning restore 612, 618
        }
    }
}
