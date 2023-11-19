﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.Db;

#nullable disable

namespace OnlineShop.Db.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231119053112_AddedRoles")]
    partial class AddedRoles
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("CartId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Comparison", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Comparisons");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Library", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Libraries");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OrderStatus")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserInfoId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid?>("ComparisonId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("LibraryId")
                        .HasColumnType("char(36)");

                    b.Property<int>("MetacriticScore")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("WishlistId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("ComparisonId");

                    b.HasIndex("LibraryId");

                    b.HasIndex("WishlistId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Cost = 4499m,
                            Description = "Build your dream squad, outsmart your rivals and experience the thrill of big European nights in the UEFA Champions League. Progress never stops when you’re pursuing footballing greatness.",
                            Genre = "Simulation",
                            MetacriticScore = 84,
                            Name = "FOOTBALL MANAGER 2024",
                            Source = "/images/fm2024.jpg"
                        },
                        new
                        {
                            Id = 2,
                            Cost = 8999m,
                            Description = "In this sequel to the Legend of Zelda: Breath of the Wild game, you’ll decide your own path through the sprawling landscapes of Hyrule and the islands floating in the vast skies above.",
                            Genre = "Action",
                            MetacriticScore = 96,
                            Name = "THE LEGEND OF ZELDA: TEARS OF THE KINGDOM",
                            Source = "/images/zelda.jpg"
                        },
                        new
                        {
                            Id = 3,
                            Cost = 2999m,
                            Description = "Carve your own clever path to vengeance in the award winning adventure from developer FromSoftware, creators of Bloodborne and the Dark Souls series. Take Revenge. Restore Your Honor. Kill Ingeniously.",
                            Genre = "Action",
                            MetacriticScore = 90,
                            Name = "SEKIRO",
                            Source = "/images/sekiro.jpg"
                        },
                        new
                        {
                            Id = 4,
                            Cost = 5999m,
                            Description = "In this new generation role-playing game, which takes place in space, you can create any character and explore the universe the way you want. Embark on a journey and uncover the greatest mystery of humanity.",
                            Genre = "RPG",
                            MetacriticScore = 83,
                            Name = "STARFIELD",
                            Source = "/images/starfield.jpeg"
                        },
                        new
                        {
                            Id = 5,
                            Cost = 499m,
                            Description = "Connected adds an all-new robust multiplayer expansion to the huge variety of addictive and innovative single-player modes that Tetris Effect is known for, with all-new co-op and online or local multiplayer modes!",
                            Genre = "Puzzle",
                            MetacriticScore = 78,
                            Name = "TETRIS",
                            Source = "/images/tetris.jpg"
                        },
                        new
                        {
                            Id = 6,
                            Cost = 4999m,
                            Description = "Baldur’s Gate 3 is a story-rich, party-based RPG set in the universe of Dungeons & Dragons, where your choices shape a tale of fellowship and betrayal, survival and sacrifice, and the lure of absolute power.",
                            Genre = "RPG",
                            MetacriticScore = 96,
                            Name = "BALDUR'S GATE 3",
                            Source = "/images/baldursgate.jpeg"
                        },
                        new
                        {
                            Id = 7,
                            Cost = 199m,
                            Description = "Every day, millions of players worldwide enter battle as one of over a hundred heroes. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has taken on a life of its own.",
                            Genre = "MOBA",
                            MetacriticScore = 96,
                            Name = "DOTA 2",
                            Source = "/images/dota2.jpg"
                        },
                        new
                        {
                            Id = 8,
                            Cost = 5999m,
                            Description = "RDR2 is the epic tale of outlaw Arthur Morgan and the infamous Van der Linde gang, on the run across America at the dawn of the modern age. Also includes access to the shared living world of Red Dead Online.",
                            Genre = "Action",
                            MetacriticScore = 97,
                            Name = "RED DEAD REDEMPTION 2",
                            Source = "/images/rdr2.png"
                        },
                        new
                        {
                            Id = 9,
                            Cost = 999m,
                            Description = "Katana ZERO is a stylish neo-noir, platformer featuring breakneck action and death combat. Slash, dash, and manipulate time to unravel your past in a beautifully brutal acrobatic display.",
                            Genre = "Platformer",
                            MetacriticScore = 83,
                            Name = "KATANA ZERO",
                            Source = "/images/katana_zero.png"
                        },
                        new
                        {
                            Id = 10,
                            Cost = 3499m,
                            Description = "Then, there was fire. Re-experience the critically acclaimed, genre-defining game that started it all. Beautifully remastered, return to Lordran in stunning high-definition detail running at 60fps.",
                            Genre = "Souls-like",
                            MetacriticScore = 84,
                            Name = "DARK SOULS",
                            Source = "/images/dark_souls.jpg"
                        },
                        new
                        {
                            Id = 11,
                            Cost = 999m,
                            Description = "Experience the emotional storytelling and unforgettable characters in The Last of Us. The action of the games in the series takes place in a post-apocalyptic future on the territory of the former United States of America after a global epidemic caused by a dangerously mutated mushroom cordyceps.",
                            Genre = "Action-adventure",
                            MetacriticScore = 95,
                            Name = "THE LAST OF US",
                            Source = "/images/tlou.jpeg"
                        },
                        new
                        {
                            Id = 12,
                            Cost = 1999m,
                            Description = "Disco Elysium - The Final Cut is a groundbreaking role playing game. You’re a detective with a unique skill system at your disposal and a whole city to carve your path across. Interrogate unforgettable characters, crack murders or take bribes. Become a hero or an absolute disaster of a human being.",
                            Genre = "RPG",
                            MetacriticScore = 91,
                            Name = "DISCO ELYSIUM",
                            Source = "/images/disco_elysium.jpeg"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.UserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Address2")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsChecked")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Wishlist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Wishlists");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.CartItem", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Cart", null)
                        .WithMany("CartItems")
                        .HasForeignKey("CartId");

                    b.HasOne("OnlineShop.Db.Models.Order", null)
                        .WithMany("CartItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.UserInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Comparison", null)
                        .WithMany("Products")
                        .HasForeignKey("ComparisonId");

                    b.HasOne("OnlineShop.Db.Models.Library", null)
                        .WithMany("Products")
                        .HasForeignKey("LibraryId");

                    b.HasOne("OnlineShop.Db.Models.Wishlist", null)
                        .WithMany("Products")
                        .HasForeignKey("WishlistId");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Comparison", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Library", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Wishlist", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
