﻿// <auto-generated />
using System;
using CookBookBE.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CookBookBE.Migrations
{
    [DbContext(typeof(RecipeContext))]
    [Migration("20220810201939_AddNewFieldsToRecipeDb")]
    partial class AddNewFieldsToRecipeDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CookBookBE.DbModels.DbIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid?>("DbRecipeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DbRecipeId");

                    b.ToTable("DbIngredient");
                });

            modelBuilder.Entity("CookBookBE.DbModels.DbTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid?>("DbRecipeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DbRecipeId");

                    b.ToTable("DbTag");
                });

            modelBuilder.Entity("CookBookBE.Models.DbRecipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("CookBookBE.DbModels.DbIngredient", b =>
                {
                    b.HasOne("CookBookBE.Models.DbRecipe", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("DbRecipeId");
                });

            modelBuilder.Entity("CookBookBE.DbModels.DbTag", b =>
                {
                    b.HasOne("CookBookBE.Models.DbRecipe", null)
                        .WithMany("Tags")
                        .HasForeignKey("DbRecipeId");
                });

            modelBuilder.Entity("CookBookBE.Models.DbRecipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
