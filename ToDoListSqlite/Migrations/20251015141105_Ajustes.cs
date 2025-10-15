using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListSqlite.Migrations
{
    /// <inheritdoc />
    public partial class Ajustes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isComplete",
                table: "Tasks",
                newName: "IsComplete");

            migrationBuilder.AddColumn<int>(
                name: "IdCategoria",
                table: "Tasks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    IdCategoria = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NomeCategoria = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.IdCategoria);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_IdCategoria",
                table: "Tasks",
                column: "IdCategoria");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Categorias_IdCategoria",
                table: "Tasks",
                column: "IdCategoria",
                principalTable: "Categorias",
                principalColumn: "IdCategoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Categorias_IdCategoria",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_IdCategoria",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IdCategoria",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "IsComplete",
                table: "Tasks",
                newName: "isComplete");
        }
    }
}
