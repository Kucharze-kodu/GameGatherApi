﻿// <auto-generated />
using System;
using GameGather.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GameGather.Infrastructure.Migrations
{
    [DbContext(typeof(GameGatherDbContext))]
    partial class GameGatherDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GameGather.Domain.Aggregates.Comments.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateComment")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SessionGameId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SessionGameId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("GameGather.Domain.Aggregates.PostGames.PostGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DayPost")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("GameMasterId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("GameTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("SessionGameId")
                        .HasColumnType("integer");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SessionGameId");

                    b.ToTable("PostGames");
                });

            modelBuilder.Entity("GameGather.Domain.Aggregates.SessionGameLists.SessionGameList", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("SessionGameId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "SessionGameId");

                    b.HasIndex("SessionGameId");

                    b.ToTable("SessionGameLists");
                });

            modelBuilder.Entity("GameGather.Domain.Aggregates.SessionGames.SessionGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GameMasterId")
                        .HasColumnType("integer");

                    b.Property<string>("GameMasterName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SessionGames");
                });

            modelBuilder.Entity("GameGather.Domain.Aggregates.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreatedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("LastModifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Role");

                    b.Property<DateTime?>("VerifiedOnUtc")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });


            modelBuilder.Entity("GameGather.Domain.Aggregates.Comments.Comment", b =>
                {
                    b.HasOne("GameGather.Domain.Aggregates.SessionGames.SessionGame", null)
                        .WithMany("Comments")
                        .HasForeignKey("SessionGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameGather.Domain.Aggregates.PostGames.PostGame", b =>
                {
                    b.HasOne("GameGather.Domain.Aggregates.SessionGames.SessionGame", null)
                        .WithMany("PostGames")
                        .HasForeignKey("SessionGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameGather.Domain.Aggregates.SessionGameLists.SessionGameList", b =>
                {
                    b.HasOne("GameGather.Domain.Aggregates.SessionGames.SessionGame", null)
                        .WithMany("SessionGameLists")
                        .HasForeignKey("SessionGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameGather.Domain.Aggregates.Users.User", null)
                        .WithMany("SessionGames")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                });

            modelBuilder.Entity("GameGather.Domain.Aggregates.Users.User", b =>
                {
                    b.OwnsOne("GameGather.Domain.Aggregates.Users.ValueObjects.Ban", "Ban", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("CreatedOnUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("CreatedOnUtc");

                            b1.Property<DateTime?>("ExpiresOnUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("ExpiresOnUtc");

                            b1.Property<string>("Message")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Message");

                            b1.HasKey("UserId");

                            b1.ToTable("Bans", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("GameGather.Domain.Aggregates.Users.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("LastModifiedOnUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("LastModifiedOnUtc");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Value");

                            b1.HasKey("UserId");

                            b1.ToTable("Passwords", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("GameGather.Domain.Aggregates.Users.ValueObjects.ResetPasswordToken", "ResetPasswordToken", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("CreatedOnUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("CreatedOnUtc");

                            b1.Property<DateTime>("ExpiresOnUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("ExpiresOnUtc");

                            b1.Property<DateTime>("LastSendOnUtc")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int>("Type")
                                .HasColumnType("integer")
                                .HasColumnName("Type");

                            b1.Property<DateTime?>("UsedOnUtc")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("Value");

                            b1.HasKey("UserId");

                            b1.ToTable("ResetPasswordTokens", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("GameGather.Domain.Aggregates.Users.ValueObjects.VerificationToken", "VerificationToken", b1 =>
                        {
                            b1.Property<int>("UserId")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("CreatedOnUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("CreatedOnUtc");

                            b1.Property<DateTime>("ExpiresOnUtc")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("ExpiresOnUtc");

                            b1.Property<DateTime>("LastSendOnUtc")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<int>("Type")
                                .HasColumnType("integer")
                                .HasColumnName("Type");

                            b1.Property<DateTime?>("UsedOnUtc")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<Guid>("Value")
                                .HasColumnType("uuid")
                                .HasColumnName("Value");

                            b1.HasKey("UserId");

                            b1.ToTable("VerificationTokens", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Ban");

                    b.Navigation("Password")
                        .IsRequired();

                    b.Navigation("ResetPasswordToken");

                    b.Navigation("VerificationToken")
                        .IsRequired();
                });

            modelBuilder.Entity("GameGather.Domain.Aggregates.SessionGames.SessionGame", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PostGames");

                    b.Navigation("SessionGameLists");
                });

            modelBuilder.Entity("GameGather.Domain.Aggregates.Users.User", b =>
                {
                    b.Navigation("SessionGames");
                });
#pragma warning restore 612, 618
        }
    }
}
