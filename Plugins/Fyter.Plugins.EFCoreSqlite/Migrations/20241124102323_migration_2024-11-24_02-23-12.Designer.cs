﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Fyter.Plugins.EFCoreSqlite;

#nullable disable

namespace Fyter.Plugins.EFCoreSqlite.Migrations
{
    [DbContext(typeof(FyterSqliteContext))]
    [Migration("20241124102323_migration_2024-11-24_02-23-12")]
    partial class migration_20241124_022312
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Fyter.CoreBusiness.Fighter", b =>
                {
                    b.Property<int>("FighterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AddedByUserId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("BaseFighterFighterId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BaseFighterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Division")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("EgoName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("FighterGrapplingStars")
                        .HasColumnType("REAL");

                    b.Property<double>("FighterHealthStars")
                        .HasColumnType("REAL");

                    b.Property<double>("FighterStandUpStars")
                        .HasColumnType("REAL");

                    b.Property<double>("FighterStars")
                        .HasColumnType("REAL");

                    b.Property<string>("FighterStyle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsAlterEgo")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsOutdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastUpdatedByUserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OutdatedStats")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Perks")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("FighterId");

                    b.HasIndex("BaseFighterFighterId");

                    b.HasIndex("BaseFighterId");

                    b.ToTable("Fighters");
                });

            modelBuilder.Entity("Fyter.CoreBusiness.TopMoves", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("FighterId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MoveName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Stars")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("FighterId");

                    b.ToTable("TopMoves");
                });

            modelBuilder.Entity("Fyter.CoreBusiness.Fighter", b =>
                {
                    b.HasOne("Fyter.CoreBusiness.Fighter", "BaseFighter")
                        .WithMany()
                        .HasForeignKey("BaseFighterFighterId");

                    b.HasOne("Fyter.CoreBusiness.Fighter", null)
                        .WithMany("AlterEgos")
                        .HasForeignKey("BaseFighterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Fyter.CoreBusiness.FighterInfo", "FighterInfo", b1 =>
                        {
                            b1.Property<int>("FighterId")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Age")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("FightingOutOf")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("HomeTown")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("NickName")
                                .HasColumnType("TEXT");

                            b1.Property<int>("Reach")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Stance")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<int>("Weight")
                                .HasColumnType("INTEGER");

                            b1.HasKey("FighterId");

                            b1.ToTable("Fighters");

                            b1.WithOwner()
                                .HasForeignKey("FighterId");

                            b1.OwnsOne("Fyter.CoreBusiness.FighterHeight", "Height", b2 =>
                                {
                                    b2.Property<int>("FighterInfoFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("Feet")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("Inches")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("FighterInfoFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("FighterInfoFighterId");
                                });

                            b1.Navigation("Height")
                                .IsRequired();
                        });

                    b.OwnsOne("Fyter.CoreBusiness.Grappling", "Grappling", b1 =>
                        {
                            b1.Property<int>("FighterId")
                                .HasColumnType("INTEGER");

                            b1.HasKey("FighterId");

                            b1.ToTable("Fighters");

                            b1.WithOwner()
                                .HasForeignKey("FighterId");

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "BottomControl", b2 =>
                                {
                                    b2.Property<int>("GrapplingFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("GrapplingFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("GrapplingFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "ClinchControl", b2 =>
                                {
                                    b2.Property<int>("GrapplingFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("GrapplingFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("GrapplingFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "ClinchStriking", b2 =>
                                {
                                    b2.Property<int>("GrapplingFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("GrapplingFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("GrapplingFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "GroundStriking", b2 =>
                                {
                                    b2.Property<int>("GrapplingFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("GrapplingFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("GrapplingFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "SubmissionDefense", b2 =>
                                {
                                    b2.Property<int>("GrapplingFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("GrapplingFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("GrapplingFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "SubmissionOffense", b2 =>
                                {
                                    b2.Property<int>("GrapplingFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("GrapplingFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("GrapplingFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "TakeDowns", b2 =>
                                {
                                    b2.Property<int>("GrapplingFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("GrapplingFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("GrapplingFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "TopControl", b2 =>
                                {
                                    b2.Property<int>("GrapplingFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("GrapplingFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("GrapplingFighterId");
                                });

                            b1.Navigation("BottomControl")
                                .IsRequired();

                            b1.Navigation("ClinchControl")
                                .IsRequired();

                            b1.Navigation("ClinchStriking")
                                .IsRequired();

                            b1.Navigation("GroundStriking")
                                .IsRequired();

                            b1.Navigation("SubmissionDefense")
                                .IsRequired();

                            b1.Navigation("SubmissionOffense")
                                .IsRequired();

                            b1.Navigation("TakeDowns")
                                .IsRequired();

                            b1.Navigation("TopControl")
                                .IsRequired();
                        });

                    b.OwnsOne("Fyter.CoreBusiness.Health", "Health", b1 =>
                        {
                            b1.Property<int>("FighterId")
                                .HasColumnType("INTEGER");

                            b1.HasKey("FighterId");

                            b1.ToTable("Fighters");

                            b1.WithOwner()
                                .HasForeignKey("FighterId");

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "Body", b2 =>
                                {
                                    b2.Property<int>("HealthFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("HealthFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("HealthFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "Cardio", b2 =>
                                {
                                    b2.Property<int>("HealthFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("HealthFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("HealthFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "Chin", b2 =>
                                {
                                    b2.Property<int>("HealthFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("HealthFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("HealthFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "CutResistant", b2 =>
                                {
                                    b2.Property<int>("HealthFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("HealthFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("HealthFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "Legs", b2 =>
                                {
                                    b2.Property<int>("HealthFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("HealthFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("HealthFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "Recovery", b2 =>
                                {
                                    b2.Property<int>("HealthFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("HealthFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("HealthFighterId");
                                });

                            b1.Navigation("Body")
                                .IsRequired();

                            b1.Navigation("Cardio")
                                .IsRequired();

                            b1.Navigation("Chin")
                                .IsRequired();

                            b1.Navigation("CutResistant")
                                .IsRequired();

                            b1.Navigation("Legs")
                                .IsRequired();

                            b1.Navigation("Recovery")
                                .IsRequired();
                        });

                    b.OwnsOne("Fyter.CoreBusiness.StandUp", "StandUp", b1 =>
                        {
                            b1.Property<int>("FighterId")
                                .HasColumnType("INTEGER");

                            b1.HasKey("FighterId");

                            b1.ToTable("Fighters");

                            b1.WithOwner()
                                .HasForeignKey("FighterId");

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "Accuracy", b2 =>
                                {
                                    b2.Property<int>("StandUpFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("StandUpFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("StandUpFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "Blocking", b2 =>
                                {
                                    b2.Property<int>("StandUpFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("StandUpFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("StandUpFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "FootWork", b2 =>
                                {
                                    b2.Property<int>("StandUpFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("StandUpFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("StandUpFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "HeadMovement", b2 =>
                                {
                                    b2.Property<int>("StandUpFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("StandUpFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("StandUpFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "KickPower", b2 =>
                                {
                                    b2.Property<int>("StandUpFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("StandUpFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("StandUpFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "KickSpeed", b2 =>
                                {
                                    b2.Property<int>("StandUpFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("StandUpFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("StandUpFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "PunchPower", b2 =>
                                {
                                    b2.Property<int>("StandUpFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("StandUpFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("StandUpFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "PunchSpeed", b2 =>
                                {
                                    b2.Property<int>("StandUpFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("StandUpFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("StandUpFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "SwitchStance", b2 =>
                                {
                                    b2.Property<int>("StandUpFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("StandUpFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("StandUpFighterId");
                                });

                            b1.OwnsOne("Fyter.CoreBusiness.Stat", "TakedownDefense", b2 =>
                                {
                                    b2.Property<int>("StandUpFighterId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<double>("Stars")
                                        .HasColumnType("REAL");

                                    b2.Property<int>("Value")
                                        .HasColumnType("INTEGER");

                                    b2.HasKey("StandUpFighterId");

                                    b2.ToTable("Fighters");

                                    b2.WithOwner()
                                        .HasForeignKey("StandUpFighterId");
                                });

                            b1.Navigation("Accuracy")
                                .IsRequired();

                            b1.Navigation("Blocking")
                                .IsRequired();

                            b1.Navigation("FootWork")
                                .IsRequired();

                            b1.Navigation("HeadMovement")
                                .IsRequired();

                            b1.Navigation("KickPower")
                                .IsRequired();

                            b1.Navigation("KickSpeed")
                                .IsRequired();

                            b1.Navigation("PunchPower")
                                .IsRequired();

                            b1.Navigation("PunchSpeed")
                                .IsRequired();

                            b1.Navigation("SwitchStance")
                                .IsRequired();

                            b1.Navigation("TakedownDefense")
                                .IsRequired();
                        });

                    b.Navigation("BaseFighter");

                    b.Navigation("FighterInfo")
                        .IsRequired();

                    b.Navigation("Grappling")
                        .IsRequired();

                    b.Navigation("Health")
                        .IsRequired();

                    b.Navigation("StandUp")
                        .IsRequired();
                });

            modelBuilder.Entity("Fyter.CoreBusiness.TopMoves", b =>
                {
                    b.HasOne("Fyter.CoreBusiness.Fighter", null)
                        .WithMany("TopMoves")
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fyter.CoreBusiness.Fighter", b =>
                {
                    b.Navigation("AlterEgos");

                    b.Navigation("TopMoves");
                });
#pragma warning restore 612, 618
        }
    }
}
