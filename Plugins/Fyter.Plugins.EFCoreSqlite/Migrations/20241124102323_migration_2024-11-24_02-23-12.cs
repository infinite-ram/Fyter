using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fyter.Plugins.EFCoreSqlite.Migrations
{
    /// <inheritdoc />
    public partial class migration_20241124_022312 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fighters",
                columns: table => new
                {
                    FighterId = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    FighterStars = table.Column<double>(type: "REAL", nullable: false),
                    FighterStandUpStars = table.Column<double>(type: "REAL", nullable: false),
                    FighterGrapplingStars = table.Column<double>(type: "REAL", nullable: false),
                    FighterHealthStars = table.Column<double>(type: "REAL", nullable: false),
                    EgoName = table.Column<string>(type: "TEXT", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FighterStyle = table.Column<string>(type: "TEXT", nullable: false),
                    StandUp_PunchSpeed_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    StandUp_PunchSpeed_Stars = table.Column<double>(type: "REAL", nullable: false),
                    StandUp_PunchPower_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    StandUp_PunchPower_Stars = table.Column<double>(type: "REAL", nullable: false),
                    StandUp_Accuracy_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    StandUp_Accuracy_Stars = table.Column<double>(type: "REAL", nullable: false),
                    StandUp_Blocking_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    StandUp_Blocking_Stars = table.Column<double>(type: "REAL", nullable: false),
                    StandUp_HeadMovement_Value = table.Column<int>(
                        type: "INTEGER",
                        nullable: false
                    ),
                    StandUp_HeadMovement_Stars = table.Column<double>(
                        type: "REAL",
                        nullable: false
                    ),
                    StandUp_FootWork_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    StandUp_FootWork_Stars = table.Column<double>(type: "REAL", nullable: false),
                    StandUp_SwitchStance_Value = table.Column<int>(
                        type: "INTEGER",
                        nullable: false
                    ),
                    StandUp_SwitchStance_Stars = table.Column<double>(
                        type: "REAL",
                        nullable: false
                    ),
                    StandUp_TakedownDefense_Value = table.Column<int>(
                        type: "INTEGER",
                        nullable: false
                    ),
                    StandUp_TakedownDefense_Stars = table.Column<double>(
                        type: "REAL",
                        nullable: false
                    ),
                    StandUp_KickPower_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    StandUp_KickPower_Stars = table.Column<double>(type: "REAL", nullable: false),
                    StandUp_KickSpeed_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    StandUp_KickSpeed_Stars = table.Column<double>(type: "REAL", nullable: false),
                    Grappling_TakeDowns_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Grappling_TakeDowns_Stars = table.Column<double>(type: "REAL", nullable: false),
                    Grappling_TopControl_Value = table.Column<int>(
                        type: "INTEGER",
                        nullable: false
                    ),
                    Grappling_TopControl_Stars = table.Column<double>(
                        type: "REAL",
                        nullable: false
                    ),
                    Grappling_BottomControl_Value = table.Column<int>(
                        type: "INTEGER",
                        nullable: false
                    ),
                    Grappling_BottomControl_Stars = table.Column<double>(
                        type: "REAL",
                        nullable: false
                    ),
                    Grappling_SubmissionOffense_Value = table.Column<int>(
                        type: "INTEGER",
                        nullable: false
                    ),
                    Grappling_SubmissionOffense_Stars = table.Column<double>(
                        type: "REAL",
                        nullable: false
                    ),
                    Grappling_SubmissionDefense_Value = table.Column<int>(
                        type: "INTEGER",
                        nullable: false
                    ),
                    Grappling_SubmissionDefense_Stars = table.Column<double>(
                        type: "REAL",
                        nullable: false
                    ),
                    Grappling_GroundStriking_Value = table.Column<int>(
                        type: "INTEGER",
                        nullable: false
                    ),
                    Grappling_GroundStriking_Stars = table.Column<double>(
                        type: "REAL",
                        nullable: false
                    ),
                    Grappling_ClinchStriking_Value = table.Column<int>(
                        type: "INTEGER",
                        nullable: false
                    ),
                    Grappling_ClinchStriking_Stars = table.Column<double>(
                        type: "REAL",
                        nullable: false
                    ),
                    Grappling_ClinchControl_Value = table.Column<int>(
                        type: "INTEGER",
                        nullable: false
                    ),
                    Grappling_ClinchControl_Stars = table.Column<double>(
                        type: "REAL",
                        nullable: false
                    ),
                    Health_Cardio_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Health_Cardio_Stars = table.Column<double>(type: "REAL", nullable: false),
                    Health_Chin_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Health_Chin_Stars = table.Column<double>(type: "REAL", nullable: false),
                    Health_Body_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Health_Body_Stars = table.Column<double>(type: "REAL", nullable: false),
                    Health_Legs_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Health_Legs_Stars = table.Column<double>(type: "REAL", nullable: false),
                    Health_Recovery_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Health_Recovery_Stars = table.Column<double>(type: "REAL", nullable: false),
                    Health_CutResistant_Value = table.Column<int>(type: "INTEGER", nullable: false),
                    Health_CutResistant_Stars = table.Column<double>(type: "REAL", nullable: false),
                    FighterInfo_NickName = table.Column<string>(type: "TEXT", nullable: true),
                    FighterInfo_Age = table.Column<int>(type: "INTEGER", nullable: false),
                    FighterInfo_Height_Feet = table.Column<int>(type: "INTEGER", nullable: false),
                    FighterInfo_Height_Inches = table.Column<int>(type: "INTEGER", nullable: false),
                    FighterInfo_Weight = table.Column<int>(type: "INTEGER", nullable: false),
                    FighterInfo_Reach = table.Column<int>(type: "INTEGER", nullable: false),
                    FighterInfo_Stance = table.Column<string>(type: "TEXT", nullable: false),
                    FighterInfo_HomeTown = table.Column<string>(type: "TEXT", nullable: false),
                    FighterInfo_FightingOutOf = table.Column<string>(type: "TEXT", nullable: false),
                    Perks = table.Column<string>(type: "TEXT", nullable: false),
                    Division = table.Column<string>(type: "TEXT", nullable: false),
                    IsAlterEgo = table.Column<bool>(type: "INTEGER", nullable: false),
                    BaseFighterId = table.Column<int>(type: "INTEGER", nullable: true),
                    BaseFighterFighterId = table.Column<int>(type: "INTEGER", nullable: true),
                    AddedByUserId = table.Column<string>(type: "TEXT", nullable: true),
                    LastUpdatedByUserId = table.Column<string>(type: "TEXT", nullable: true),
                    IsOutdated = table.Column<bool>(
                        type: "INTEGER",
                        nullable: false,
                        defaultValue: false
                    ),
                    OutdatedStats = table.Column<string>(type: "TEXT", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fighters", x => x.FighterId);
                    table.ForeignKey(
                        name: "FK_Fighters_Fighters_BaseFighterFighterId",
                        column: x => x.BaseFighterFighterId,
                        principalTable: "Fighters",
                        principalColumn: "FighterId"
                    );
                    table.ForeignKey(
                        name: "FK_Fighters_Fighters_BaseFighterId",
                        column: x => x.BaseFighterId,
                        principalTable: "Fighters",
                        principalColumn: "FighterId",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "TopMoves",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MoveName = table.Column<string>(type: "TEXT", nullable: false),
                    Stars = table.Column<double>(type: "REAL", nullable: false),
                    FighterId = table.Column<int>(type: "INTEGER", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopMoves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopMoves_Fighters_FighterId",
                        column: x => x.FighterId,
                        principalTable: "Fighters",
                        principalColumn: "FighterId",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Fighters_BaseFighterFighterId",
                table: "Fighters",
                column: "BaseFighterFighterId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Fighters_BaseFighterId",
                table: "Fighters",
                column: "BaseFighterId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_TopMoves_FighterId",
                table: "TopMoves",
                column: "FighterId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "TopMoves");

            migrationBuilder.DropTable(name: "Fighters");
        }
    }
}
