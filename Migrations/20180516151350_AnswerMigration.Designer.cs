﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SIE.Context;
using System;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace SIE.Migrations
{
    [DbContext(typeof(SIEContext))]
    [Migration("20180516151350_AnswerMigration")]
    partial class AnswerMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("SIE.Context.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CurrentState");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime?>("EndDate");

                    b.Property<DateTime?>("ExpirationDate");

                    b.Property<int>("PersonId");

                    b.Property<int>("RoomId");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("RoomId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("SIE.Context.Answer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ActivityId");

                    b.Property<DateTime?>("EvaluatedDate");

                    b.Property<double>("Grade");

                    b.Property<string>("Justification");

                    b.Property<int>("PersonId");

                    b.Property<int>("RoomId");

                    b.Property<DateTime>("SentDate");

                    b.Property<string>("UserAnswer")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("PersonId");

                    b.HasIndex("RoomId");

                    b.ToTable("Answer");
                });

            modelBuilder.Entity("SIE.Context.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action")
                        .IsRequired();

                    b.Property<DateTime>("DateAction");

                    b.Property<int>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("History");
                });

            modelBuilder.Entity("SIE.Context.Institution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Institution");
                });

            modelBuilder.Entity("SIE.Context.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Cpf")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int?>("InstitutionId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("Profile");

                    b.Property<int>("Sex");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("SIE.Context.RelStudentRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active");

                    b.Property<DateTime?>("ExitDate");

                    b.Property<DateTime>("JoinDate");

                    b.Property<int>("PersonId");

                    b.Property<int>("RoomId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("RoomId");

                    b.ToTable("RelStudentRoom");
                });

            modelBuilder.Entity("SIE.Context.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<int>("CurrentState");

                    b.Property<string>("Description");

                    b.Property<DateTime?>("EndDate");

                    b.Property<DateTime?>("ExpirationDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("NumberOfStudents");

                    b.Property<int>("PersonId");

                    b.Property<DateTime?>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("SIE.Context.Activity", b =>
                {
                    b.HasOne("SIE.Context.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SIE.Context.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SIE.Context.Answer", b =>
                {
                    b.HasOne("SIE.Context.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SIE.Context.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SIE.Context.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SIE.Context.History", b =>
                {
                    b.HasOne("SIE.Context.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SIE.Context.RelStudentRoom", b =>
                {
                    b.HasOne("SIE.Context.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SIE.Context.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SIE.Context.Room", b =>
                {
                    b.HasOne("SIE.Context.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
