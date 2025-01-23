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
    [Migration("20241126084911_migration_2024-11-26_00-49-07")]
    partial class migration_20241126_004907
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("Fyter.CoreBusiness.FighterModel.Fighter", b =>
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

            modelBuilder.Entity("Fyter.CoreBusiness.FighterModel.FighterRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FighterRequests");
                });

            modelBuilder.Entity("Fyter.CoreBusiness.FighterModel.TopMoves", b =>
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

            modelBuilder.Entity("Fyter.CoreBusiness.FighterModel.Fighter", b =>
                {
                    b.HasOne("Fyter.CoreBusiness.FighterModel.Fighter", "BaseFighter")
                        .WithMany()
                        .HasForeignKey("BaseFighterFighterId");

                    b.HasOne("Fyter.CoreBusiness.FighterModel.Fighter", null)
                        .WithMany("AlterEgos")
                        .HasForeignKey("BaseFighterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.OwnsOne("Fyter.CoreBusiness.FighterModel.FighterInfo", "FighterInfo", b1 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.FighterHeight", "Height", b2 =>
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

                    b.OwnsOne("Fyter.CoreBusiness.FighterModel.Grappling", "Grappling", b1 =>
                        {
                            b1.Property<int>("FighterId")
                                .HasColumnType("INTEGER");

                            b1.HasKey("FighterId");

                            b1.ToTable("Fighters");

                            b1.WithOwner()
                                .HasForeignKey("FighterId");

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "BottomControl", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "ClinchControl", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "ClinchStriking", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "GroundStriking", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "SubmissionDefense", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "SubmissionOffense", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "TakeDowns", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "TopControl", b2 =>
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

                    b.OwnsOne("Fyter.CoreBusiness.FighterModel.Health", "Health", b1 =>
                        {
                            b1.Property<int>("FighterId")
                                .HasColumnType("INTEGER");

                            b1.HasKey("FighterId");

                            b1.ToTable("Fighters");

                            b1.WithOwner()
                                .HasForeignKey("FighterId");

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "Body", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "Cardio", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "Chin", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "CutResistant", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "Legs", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "Recovery", b2 =>
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

                    b.OwnsOne("Fyter.CoreBusiness.FighterModel.StandUp", "StandUp", b1 =>
                        {
                            b1.Property<int>("FighterId")
                                .HasColumnType("INTEGER");

                            b1.HasKey("FighterId");

                            b1.ToTable("Fighters");

                            b1.WithOwner()
                                .HasForeignKey("FighterId");

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "Accuracy", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "Blocking", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "FootWork", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "HeadMovement", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "KickPower", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "KickSpeed", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "PunchPower", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "PunchSpeed", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "SwitchStance", b2 =>
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

                            b1.OwnsOne("Fyter.CoreBusiness.FighterModel.Stat", "TakedownDefense", b2 =>
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

            modelBuilder.Entity("Fyter.CoreBusiness.FighterModel.TopMoves", b =>
                {
                    b.HasOne("Fyter.CoreBusiness.FighterModel.Fighter", null)
                        .WithMany("TopMoves")
                        .HasForeignKey("FighterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fyter.CoreBusiness.FighterModel.Fighter", b =>
                {
                    b.Navigation("AlterEgos");

                    b.Navigation("TopMoves");
                });
#pragma warning restore 612, 618
        }
    }
}
