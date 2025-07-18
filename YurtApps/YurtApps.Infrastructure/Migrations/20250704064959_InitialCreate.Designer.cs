﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YurtApps.Infrastructure;

#nullable disable

namespace YurtApps.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250704064959_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YurtApps.Domain.Entities.Dormitory", b =>
                {
                    b.Property<int>("DormitoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DormitoryId"));

                    b.Property<string>("DormitoryAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<short>("DormitoryCapacity")
                        .HasColumnType("smallint");

                    b.Property<string>("DormitoryName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("DormitoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Dormitory");
                });

            modelBuilder.Entity("YurtApps.Domain.Entities.Room", b =>
                {
                    b.Property<int>("RoomId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomId"));

                    b.Property<int>("DormitoryId")
                        .HasColumnType("int");

                    b.Property<short>("RoomCapacity")
                        .HasColumnType("smallint");

                    b.Property<short>("RoomNumber")
                        .HasColumnType("smallint");

                    b.HasKey("RoomId");

                    b.HasIndex("DormitoryId");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("YurtApps.Domain.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<int>("RoomId")
                        .HasColumnType("int");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("StudentPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("StudentSurname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("StudentId");

                    b.HasIndex("RoomId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("YurtApps.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("YurtApps.Domain.Entities.Dormitory", b =>
                {
                    b.HasOne("YurtApps.Domain.Entities.User", "User")
                        .WithMany("Dormitories")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("YurtApps.Domain.Entities.Room", b =>
                {
                    b.HasOne("YurtApps.Domain.Entities.Dormitory", "Dormitory")
                        .WithMany("Rooms")
                        .HasForeignKey("DormitoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dormitory");
                });

            modelBuilder.Entity("YurtApps.Domain.Entities.Student", b =>
                {
                    b.HasOne("YurtApps.Domain.Entities.Room", "Room")
                        .WithMany("Students")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");
                });

            modelBuilder.Entity("YurtApps.Domain.Entities.Dormitory", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("YurtApps.Domain.Entities.Room", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("YurtApps.Domain.Entities.User", b =>
                {
                    b.Navigation("Dormitories");
                });
#pragma warning restore 612, 618
        }
    }
}
