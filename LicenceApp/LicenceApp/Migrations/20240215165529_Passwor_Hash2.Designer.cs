﻿// <auto-generated />
using LicenceApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LicenceApp.Migrations
{
    [DbContext(typeof(LicenceDBContext))]
    [Migration("20240215165529_Passwor_Hash2")]
    partial class Passwor_Hash2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LicenceApp.models.UserDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("U_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("U_EMAIL");

                    b.Property<string>("FirstName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("U_FIRSTNAME");

                    b.Property<string>("LastName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("U_LASTNAME");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varbinary(30)")
                        .HasColumnName("U_PASSWORD_HASH");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("U_PASSWORD_SALT");

                    b.Property<short>("Role")
                        .HasColumnType("smallint")
                        .HasColumnName("U_ROLE");

                    b.HasKey("Id");

                    b.ToTable("USERS2");
                });
#pragma warning restore 612, 618
        }
    }
}
