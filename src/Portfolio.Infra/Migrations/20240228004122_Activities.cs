using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Infra.Migrations
{
	public partial class Activities : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder
				.CreateTable(
					name: "activities",
					columns: table => new
					{
						id = table
							.Column<int>(type: "int", nullable: false)
							.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
						title = table
							.Column<string>(type: "VARCHAR(100)", nullable: false)
							.Annotation("MySql:CharSet", "utf8mb4"),
						description = table
							.Column<string>(type: "VARCHAR(500)", nullable: false)
							.Annotation("MySql:CharSet", "utf8mb4"),
						icon = table
							.Column<string>(
								type: "VARCHAR(50)",
								nullable: false,
								comment: "Font Awesome Icon (https://fontawesome.com/v5/search)"
							)
							.Annotation("MySql:CharSet", "utf8mb4"),
						created_at = table.Column<DateTime>(
							type: "TIMESTAMP",
							nullable: false,
							defaultValueSql: "CURRENT_TIMESTAMP"
						)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_activities", x => x.id);
					}
				)
				.Annotation("MySql:CharSet", "utf8mb4");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(name: "activities");
		}
	}
}
