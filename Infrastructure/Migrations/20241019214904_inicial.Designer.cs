﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(TransferContext))]
    [Migration("20241019214904_inicial")]
    partial class inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Transfer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DestAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SrcAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("TypeId");

                    b.ToTable("Transfer", (string)null);
                });

            modelBuilder.Entity("Domain.Models.TransferStatus", b =>
                {
                    b.Property<int>("TransferStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransferStatusId"));

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransferStatusId");

                    b.ToTable("TransferStatus", (string)null);

                    b.HasData(
                        new
                        {
                            TransferStatusId = 1,
                            Status = "Pending"
                        },
                        new
                        {
                            TransferStatusId = 2,
                            Status = "Accepted"
                        },
                        new
                        {
                            TransferStatusId = 3,
                            Status = "Denied"
                        });
                });

            modelBuilder.Entity("Domain.Models.TransferType", b =>
                {
                    b.Property<int>("TransferTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransferTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TransferTypeId");

                    b.ToTable("TransferType", (string)null);

                    b.HasData(
                        new
                        {
                            TransferTypeId = 1,
                            Name = "Varios"
                        },
                        new
                        {
                            TransferTypeId = 2,
                            Name = "Alquileres"
                        },
                        new
                        {
                            TransferTypeId = 3,
                            Name = "Cuotas"
                        },
                        new
                        {
                            TransferTypeId = 4,
                            Name = "Facturas"
                        },
                        new
                        {
                            TransferTypeId = 5,
                            Name = "Seguros"
                        },
                        new
                        {
                            TransferTypeId = 6,
                            Name = "Honorarios"
                        },
                        new
                        {
                            TransferTypeId = 7,
                            Name = "Prestamos"
                        });
                });

            modelBuilder.Entity("Domain.Models.Transfer", b =>
                {
                    b.HasOne("Domain.Models.TransferStatus", "Status")
                        .WithMany("Transfers")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.TransferType", "TransferType")
                        .WithMany("Transfers")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("TransferType");
                });

            modelBuilder.Entity("Domain.Models.TransferStatus", b =>
                {
                    b.Navigation("Transfers");
                });

            modelBuilder.Entity("Domain.Models.TransferType", b =>
                {
                    b.Navigation("Transfers");
                });
#pragma warning restore 612, 618
        }
    }
}
