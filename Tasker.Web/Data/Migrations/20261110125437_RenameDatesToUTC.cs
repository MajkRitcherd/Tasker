using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasker.Web.Migrations
{
    /// <inheritdoc />
    public partial class RenameDatesToUTC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "TaskItems",
                newName: "DueDateUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "TaskItems",
                newName: "CreatedAtUTC");

            migrationBuilder.RenameColumn(
                name: "CompletedAt",
                table: "TaskItems",
                newName: "CompletedAtUTC");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Projects",
                newName: "CreatedAtUTC");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TaskItems",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "TaskItems",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDateUTC",
                table: "TaskItems",
                newName: "DueDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUTC",
                table: "TaskItems",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CompletedAtUTC",
                table: "TaskItems",
                newName: "CompletedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUTC",
                table: "Projects",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "TaskItems",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "TaskItems",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
