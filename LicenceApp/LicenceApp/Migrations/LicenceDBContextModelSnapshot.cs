﻿// <auto-generated />
using System;
using LicenceApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LicenceApp.Migrations
{
    [DbContext(typeof(LicenceDBContext))]
    partial class LicenceDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LicenceApp.models.GlobalDao.ApplicationDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("APP_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("APP_DESCRIPTION");

                    b.Property<int?>("Identifiant")
                        .HasColumnType("int")
                        .HasColumnName("APP_IDENTIFIANT");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("APP_NOM");

                    b.HasKey("Id");

                    b.ToTable("APPLICATION");
                });

            modelBuilder.Entity("LicenceApp.models.GlobalDao.ClientDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CL_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("CL_ADRESSE");

                    b.Property<string>("CodePostal")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("CL_CODE_POSTAL");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("CL_EMAIL");

                    b.Property<int?>("Identifiant")
                        .HasMaxLength(20)
                        .HasColumnType("int")
                        .HasColumnName("CL_IDENTIFIANT");

                    b.Property<string>("Pays")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("CL_PAYS");

                    b.Property<string>("RaisonSocial")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("CL_RAISON_SOCIAL");

                    b.Property<int>("Telephone")
                        .HasMaxLength(8)
                        .HasColumnType("int")
                        .HasColumnName("CL_TELEPHONE");

                    b.Property<string>("Ville")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("CL_VILLE");

                    b.HasKey("Id");

                    b.ToTable("CLIENTS");
                });

            modelBuilder.Entity("LicenceApp.models.GlobalDao.EnumerationDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EN_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("EN_CODE");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("EN_NOM");

                    b.HasKey("Id");

                    b.ToTable("ENUMERATION");
                });

            modelBuilder.Entity("LicenceApp.models.GlobalDao.EnumerationValeurDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("EV_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EnumerationId")
                        .HasColumnType("int")
                        .HasColumnName("EN_ID");

                    b.Property<string>("Valeur")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("EV_VALEUR");

                    b.HasKey("Id");

                    b.HasIndex("EnumerationId");

                    b.ToTable("ENUMERATION_VALUE");
                });

            modelBuilder.Entity("LicenceApp.models.GlobalDao.UserDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("U_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("U_EMAIL");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("U_FIRSTNAME");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("U_LASTNAME");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("U_PASSWORD_HASH");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("U_PASSWORD_SALT");

                    b.Property<short>("Role")
                        .HasColumnType("smallint")
                        .HasColumnName("U_ROLE");

                    b.HasKey("Id");

                    b.ToTable("USERS");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "sabrijm123@gmail.com",
                            FirstName = "sabri",
                            LastName = "jammoussi",
                            PasswordHash = new byte[] { 45, 108, 72, 114, 216, 36, 60, 66, 33, 28, 38, 96, 127, 46, 81, 244, 237, 219, 131, 214, 22, 155, 40, 128, 232, 201, 14, 82, 40, 102, 140, 26, 194, 57, 28, 28, 172, 126, 70, 68, 180, 211, 89, 116, 234, 159, 220, 14, 72, 114, 23, 55, 153, 99, 0, 111, 7, 79, 112, 27, 10, 12, 129, 19 },
                            PasswordSalt = new byte[] { 176, 243, 65, 132, 9, 162, 46, 243, 77, 231, 231, 21, 149, 6, 10, 125, 74, 176, 146, 222, 77, 9, 244, 25, 176, 60, 179, 248, 196, 218, 246, 232, 65, 225, 112, 13, 42, 142, 144, 93, 162, 113, 243, 222, 8, 189, 162, 27, 124, 204, 34, 109, 160, 237, 183, 250, 165, 35, 231, 12, 200, 27, 146, 222, 7, 30, 181, 89, 56, 235, 172, 35, 25, 210, 60, 183, 87, 185, 169, 11, 83, 20, 171, 145, 125, 123, 169, 120, 10, 174, 244, 115, 160, 237, 247, 192, 176, 35, 60, 27, 253, 70, 83, 56, 194, 84, 29, 64, 29, 130, 4, 27, 88, 157, 144, 206, 196, 159, 223, 167, 87, 30, 214, 15, 6, 87, 255, 30 },
                            Role = (short)0
                        });
                });

            modelBuilder.Entity("LicenceApp.models.GlobalDao.EnumerationValeurDao", b =>
                {
                    b.HasOne("LicenceApp.models.GlobalDao.EnumerationDao", "Enumeration")
                        .WithMany("Valeurs")
                        .HasForeignKey("EnumerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enumeration");
                });

            modelBuilder.Entity("LicenceApp.models.GlobalDao.EnumerationDao", b =>
                {
                    b.Navigation("Valeurs");
                });
#pragma warning restore 612, 618
        }
    }
}
