﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NSIDictionaryService.Data;

#nullable disable

namespace NSIDictionaryService.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20241209110337_RealDictVersions")]
    partial class RealDictVersions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NSIDictionaryService.Data.Models.DictProperty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date");

                    b.Property<int>("DeletedUserId")
                        .HasColumnType("int");

                    b.Property<string>("DictionaryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("date");

                    b.Property<int>("EditUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PropertyCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("NSIDictionaryService.Data.Models.DictVersion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date");

                    b.Property<int>("DeletedUserId")
                        .HasColumnType("int");

                    b.Property<string>("DictionaryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("date");

                    b.Property<int>("EditUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("PublicationDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("VersionCode")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Versions");
                });

            modelBuilder.Entity("NSIDictionaryService.Data.Models.Dictionaries.V006Dictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("date");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date");

                    b.Property<int>("DeletedUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("date");

                    b.Property<int>("EditUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.HasKey("Id");

                    b.ToTable("V006");
                });

            modelBuilder.Entity("NSIDictionaryService.Data.Models.Dictionaries.V012Dictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("date");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date");

                    b.Property<int>("DeletedUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("date");

                    b.Property<int>("EditUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<int>("UMPId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("V012");
                });

            modelBuilder.Entity("NSIDictionaryService.Data.Models.Dictionaries.V021Dictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("date");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date");

                    b.Property<int>("DeletedUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("date");

                    b.Property<int>("EditUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("PostName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("V021");
                });

            modelBuilder.Entity("NSIDictionaryService.Data.Models.Dictionaries.V025Dictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("BeginDate")
                        .HasColumnType("date");

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date");

                    b.Property<int>("DeletedUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("date");

                    b.Property<int>("EditUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)");

                    b.HasKey("Id");

                    b.ToTable("V025");
                });

            modelBuilder.Entity("NSIDictionaryService.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date");

                    b.Property<int>("DeletedUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("date");

                    b.Property<int>("EditUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("NSIDictionaryService.Data.Models.Upload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date");

                    b.Property<int>("DeletedUserId")
                        .HasColumnType("int");

                    b.Property<string>("DictCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DictVersionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("date");

                    b.Property<int>("EditUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UploadMethodId")
                        .HasColumnType("int");

                    b.Property<int>("UploadResultId")
                        .HasColumnType("int");

                    b.Property<int>("UploadingUserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DictVersionId");

                    b.HasIndex("UploadMethodId");

                    b.HasIndex("UploadResultId");

                    b.ToTable("Uploads");
                });

            modelBuilder.Entity("NSIDictionaryService.Data.Models.UploadMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UploadMethods");
                });

            modelBuilder.Entity("NSIDictionaryService.Data.Models.UploadResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UploadResults");
                });

            modelBuilder.Entity("NSIDictionaryService.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("date");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("date");

                    b.Property<int>("DeletedUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EditDate")
                        .HasColumnType("date");

                    b.Property<int>("EditUserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NSIDictionaryService.Data.Models.Upload", b =>
                {
                    b.HasOne("NSIDictionaryService.Data.Models.DictVersion", "DictVersion")
                        .WithMany()
                        .HasForeignKey("DictVersionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NSIDictionaryService.Data.Models.UploadMethod", "UploadMethod")
                        .WithMany()
                        .HasForeignKey("UploadMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NSIDictionaryService.Data.Models.UploadResult", "UploadResult")
                        .WithMany()
                        .HasForeignKey("UploadResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DictVersion");

                    b.Navigation("UploadMethod");

                    b.Navigation("UploadResult");
                });
#pragma warning restore 612, 618
        }
    }
}
