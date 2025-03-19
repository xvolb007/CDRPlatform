﻿// <auto-generated />
using System;
using CDRPlatform.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CDRPlatform.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250319081818_UpdateCostPrecision")]
    partial class UpdateCostPrecision
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CDRPlatform.Domain.Models.CallDetailRecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("CallDate")
                        .HasColumnType("date");

                    b.Property<long?>("CallerId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Cost")
                        .HasPrecision(20, 3)
                        .HasColumnType("decimal(20,3)");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<long?>("Recipient")
                        .HasColumnType("bigint");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CallDetailRecord");
                });
#pragma warning restore 612, 618
        }
    }
}
