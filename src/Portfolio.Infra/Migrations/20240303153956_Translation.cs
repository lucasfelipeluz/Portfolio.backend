using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Infra.Migrations
{
	public partial class Translation : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "role",
				table: "users",
				type: "INT",
				nullable: false,
				defaultValue: 2
			);

			migrationBuilder
				.AddColumn<string>(name: "description_en", table: "skills", type: "VARCHAR(500)", nullable: true)
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder
				.AddColumn<string>(name: "title_en", table: "skills", type: "VARCHAR(100)", nullable: true)
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder
				.AlterColumn<string>(
					name: "description",
					table: "projects",
					type: "VARCHAR(500)",
					nullable: false,
					oldClrType: typeof(string),
					oldType: "VARCHAR(100)"
				)
				.Annotation("MySql:CharSet", "utf8mb4")
				.OldAnnotation("MySql:CharSet", "utf8mb4");

			migrationBuilder
				.AddColumn<string>(name: "description_en", table: "projects", type: "VARCHAR(500)", nullable: true)
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder
				.AddColumn<string>(name: "title_en", table: "projects", type: "VARCHAR(100)", nullable: true)
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder
				.AddColumn<string>(name: "description_en", table: "activities", type: "VARCHAR(500)", nullable: true)
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder
				.AddColumn<string>(name: "title_en", table: "activities", type: "VARCHAR(100)", nullable: true)
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder
				.AddColumn<string>(
					name: "address",
					table: "about_me",
					type: "VARCHAR(200)",
					nullable: false,
					defaultValue: ""
				)
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder
				.AddColumn<string>(name: "job_title_en", table: "about_me", type: "VARCHAR(50)", nullable: true)
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder
				.AddColumn<string>(name: "text_en", table: "about_me", type: "VARCHAR(500)", nullable: true)
				.Annotation("MySql:CharSet", "utf8mb4");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(name: "role", table: "users");

			migrationBuilder.DropColumn(name: "description_en", table: "skills");

			migrationBuilder.DropColumn(name: "title_en", table: "skills");

			migrationBuilder.DropColumn(name: "description_en", table: "projects");

			migrationBuilder.DropColumn(name: "title_en", table: "projects");

			migrationBuilder.DropColumn(name: "description_en", table: "activities");

			migrationBuilder.DropColumn(name: "title_en", table: "activities");

			migrationBuilder.DropColumn(name: "address", table: "about_me");

			migrationBuilder.DropColumn(name: "job_title_en", table: "about_me");

			migrationBuilder.DropColumn(name: "text_en", table: "about_me");

			migrationBuilder
				.AlterColumn<string>(
					name: "description",
					table: "projects",
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
