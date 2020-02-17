﻿// <auto-generated />
using GameWebProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GameWebProject.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200130025724_AddPhotoPathMethod")]
    partial class AddPhotoPathMethod
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GameWebProject.Models.SaleModel.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category");

                    b.Property<int>("Condition");

                    b.Property<string>("LookingFor")
                        .IsRequired();

                    b.Property<string>("PhotoPath");

                    b.Property<int>("Platform");

                    b.Property<string>("ProductName")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = 1,
                            Condition = 0,
                            LookingFor = "Fifa19",
                            Platform = 3,
                            ProductName = "God of War III"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
