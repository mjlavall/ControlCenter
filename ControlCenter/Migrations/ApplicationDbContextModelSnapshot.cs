using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ControlCenter.Models;

namespace ControlCenter.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("ControlCenter.Models.GroceryItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Complete");

                    b.Property<int>("Count");

                    b.Property<Guid?>("GroceryListId");

                    b.Property<string>("Name");

                    b.Property<Guid?>("UnitId");

                    b.HasKey("Id");

                    b.HasIndex("GroceryListId");

                    b.HasIndex("UnitId");

                    b.ToTable("GroceryItem");
                });

            modelBuilder.Entity("ControlCenter.Models.GroceryList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Created");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("GroceryLists");
                });

            modelBuilder.Entity("ControlCenter.Models.GroceryUnit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("GroceryUnit");
                });

            modelBuilder.Entity("ControlCenter.Models.GroceryItem", b =>
                {
                    b.HasOne("ControlCenter.Models.GroceryList")
                        .WithMany("Items")
                        .HasForeignKey("GroceryListId");

                    b.HasOne("ControlCenter.Models.GroceryUnit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");
                });
        }
    }
}
