using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessClub.Repositories.Ef.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "members",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                phone_number = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                membership_start_date = table.Column<DateOnly>(type: "date", nullable: false),
                membership_end_date = table.Column<DateOnly>(type: "date", nullable: false),
                passport_number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                last_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                first_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                patronymic = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                gender = table.Column<int>(type: "int", nullable: false),
                date_of_birth = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_members", x => x.id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "specializations",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_specializations", x => x.id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "trainers",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                specialization_id = table.Column<int>(type: "int", nullable: false),
                work_experience_years = table.Column<int>(type: "int", nullable: false),
                passport_number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                last_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                first_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                patronymic = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                gender = table.Column<int>(type: "int", nullable: false),
                date_of_birth = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_trainers", x => x.id);
                table.ForeignKey(
                    name: "fk_trainers_specializations",
                    column: x => x.specialization_id,
                    principalTable: "specializations",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "training_sessions",
            columns: table => new
            {
                id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                member_id = table.Column<int>(type: "int", nullable: false),
                trainer_id = table.Column<int>(type: "int", nullable: false),
                date_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                duration = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                hall_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                is_trial = table.Column<bool>(type: "tinyint(1)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_training_sessions", x => x.id);
                table.ForeignKey(
                    name: "fk_training_sessions_members",
                    column: x => x.member_id,
                    principalTable: "members",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "fk_training_sessions_trainers",
                    column: x => x.trainer_id,
                    principalTable: "trainers",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "ix_trainers_specialization_id",
            table: "trainers",
            column: "specialization_id");

        migrationBuilder.CreateIndex(
            name: "ix_training_sessions_member_id",
            table: "training_sessions",
            column: "member_id");

        migrationBuilder.CreateIndex(
            name: "ix_training_sessions_trainer_id",
            table: "training_sessions",
            column: "trainer_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "training_sessions");

        migrationBuilder.DropTable(
            name: "members");

        migrationBuilder.DropTable(
            name: "trainers");

        migrationBuilder.DropTable(
            name: "specializations");
    }
}
