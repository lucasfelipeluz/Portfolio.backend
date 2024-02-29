using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Infra.Migrations
{
	public partial class SystemVariables : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder
				.AlterColumn<string>(
					name: "description",
					table: "skills",
					type: "VARCHAR(500)",
					nullable: false,
					oldClrType: typeof(string),
					oldType: "VARCHAR(100)"
				)
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder
				.CreateTable(
					name: "system_variables",
					columns: table => new
					{
						name = table
							.Column<string>(type: "VARCHAR(100)", nullable: false)
							.Annotation("MySql:CharSet", "utf8mb4"),
						value = table
							.Column<string>(type: "VARCHAR(100)", nullable: false)
							.Annotation("MySql:CharSet", "utf8mb4")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_system_variables", x => x.name);
					}
				)
				.Annotation("MySql:CharSet", "utf8mb4");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(name: "system_variables");

			migrationBuilder
				.AlterColumn<string>(
					name: "description",
					table: "skills",
					type: "VARCHAR(100)",
					nullable: false,
					oldClrType: typeof(string),
					oldType: "VARCHAR(500)"
				)
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");
		}
	}
}
