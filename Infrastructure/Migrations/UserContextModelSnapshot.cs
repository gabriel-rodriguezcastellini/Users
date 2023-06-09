﻿// <auto-generated />
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Diagnostics.CodeAnalysis;

#nullable disable

namespace Infrastructure.Migrations;

[ExcludeFromCodeCoverage]
[DbContext(typeof(UserContext))]
partial class UserContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "7.0.3")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("ApplicationCore.Entities.UserAggregate.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<string>("Address")
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)")
                    .HasComment("The address of the user");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnType("nvarchar(500)")
                    .HasComment("The email of the user");

                b.Property<decimal>("Money")
                    .HasPrecision(14, 2)
                    .HasColumnType("decimal(14,2)")
                    .HasComment("The money of the user");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)")
                    .HasComment("The name of the user")
                    .UseCollation("SQL_Latin1_General_CP1_CI_AS");

                b.Property<string>("Phone")
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnType("nvarchar(256)")
                    .HasComment("The phone of the user");

                b.Property<int>("Type")
                    .HasColumnType("int")
                    .HasComment("The user type");

                b.HasKey("Id");

                b.HasIndex("Email")
                    .IsUnique();

                b.HasIndex("Phone")
                    .IsUnique();

                b.HasIndex("Name", "Address")
                    .IsUnique();

                b.ToTable("Users");
            });
#pragma warning restore 612, 618
    }
}
