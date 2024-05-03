﻿// <auto-generated />
using System;
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
    [Migration("20240415095602_v008")]
    partial class v008
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("LicenceApp.models.GlobalDao.AttributeLicenceDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ATT_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int")
                        .HasColumnName("APP_ID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("ATT_DESCRIPTION");

                    b.Property<string>("Intutile")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("ATT_INTUTILE");

                    b.Property<short>("Type")
                        .HasColumnType("smallint")
                        .HasColumnName("ATT_TYPE");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("ATTRIBUTE_LICENCE");
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

            modelBuilder.Entity("LicenceApp.models.GlobalDao.LicenceDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LIC_ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicationId")
                        .HasColumnType("int")
                        .HasColumnName("APP_ID");

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasColumnName("CL_ID");

                    b.Property<DateTime?>("DateExp")
                        .HasColumnType("Date")
                        .HasColumnName("LIC_DATE_EXP");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("LIC_USER_ID");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("ClientId");

                    b.ToTable("LICENCE");
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
                            PasswordHash = new byte[] { 42, 207, 173, 11, 47, 129, 168, 237, 223, 152, 132, 39, 88, 146, 162, 183, 190, 183, 178, 154, 104, 186, 154, 79, 181, 232, 72, 106, 129, 248, 63, 3, 209, 84, 120, 89, 177, 71, 43, 5, 204, 212, 105, 189, 180, 126, 45, 103, 167, 228, 110, 175, 44, 50, 177, 132, 224, 127, 118, 209, 94, 238, 177, 127 },
                            PasswordSalt = new byte[] { 127, 242, 160, 186, 92, 231, 234, 5, 91, 45, 169, 184, 206, 222, 4, 137, 195, 194, 25, 148, 243, 102, 16, 107, 159, 25, 66, 193, 25, 40, 129, 35, 117, 120, 106, 71, 7, 8, 113, 181, 29, 222, 142, 172, 165, 106, 58, 95, 160, 104, 232, 223, 118, 89, 18, 105, 122, 151, 115, 5, 108, 115, 213, 228, 100, 195, 137, 210, 82, 45, 196, 122, 159, 211, 59, 227, 229, 147, 125, 211, 121, 56, 242, 115, 88, 59, 177, 12, 168, 65, 103, 137, 118, 196, 222, 197, 41, 135, 115, 61, 90, 240, 17, 153, 68, 183, 63, 87, 143, 98, 67, 188, 12, 159, 170, 45, 148, 133, 248, 224, 236, 201, 154, 81, 104, 182, 33, 176 },
                            Role = (short)0
                        });
                });

            modelBuilder.Entity("LicenceApp.models.GlobalDao.AttributeLicenceDao", b =>
                {
                    b.HasOne("LicenceApp.models.GlobalDao.ApplicationDao", "Application")
                        .WithMany("Attributes")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");
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

            modelBuilder.Entity("LicenceApp.models.GlobalDao.LicenceDao", b =>
                {
                    b.HasOne("LicenceApp.models.GlobalDao.ApplicationDao", "Application")
                        .WithMany("Licences")
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LicenceApp.models.GlobalDao.ClientDao", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("LicenceApp.models.GlobalDao.ApplicationDao", b =>
                {
                    b.Navigation("Attributes");

                    b.Navigation("Licences");
                });

            modelBuilder.Entity("LicenceApp.models.GlobalDao.EnumerationDao", b =>
                {
                    b.Navigation("Valeurs");
                });
#pragma warning restore 612, 618
        }
    }
}