using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Infra.Migrations
{
	public partial class addImageContext : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder
				.CreateTable(
					name: "images",
					columns: table => new
					{
						id = table
							.Column<int>(type: "int", nullable: false)
							.Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
						name = table
							.Column<string>(type: "VARCHAR(200)", nullable: false)
							.Annotation("MySql:CharSet", "utf8mb4"),
						folder = table
							.Column<string>(type: "VARCHAR(50)", nullable: false)
							.Annotation("MySql:CharSet", "utf8mb4"),
						is_active = table.Column<sbyte>(type: "TINYINT", nullable: false, defaultValue: (sbyte)1),
						created_at = table.Column<DateTime>(
							type: "TIMESTAMP",
							nullable: false,
							defaultValueSql: "CURRENT_TIMESTAMP"
						),
						updated_at = table.Column<DateTime>(
							type: "TIMESTAMP",
							nullable: false,
							defaultValueSql: "CURRENT_TIMESTAMP"
						)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_images", x => x.id);
					}
				)
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder
				.CreateTable(
					name: "projects_images",
					columns: table => new
					{
						project_id = table.Column<int>(type: "int", nullable: false),
						image_id = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_projects_images", x => new { x.project_id, x.image_id });
						table.ForeignKey(
							name: "FK_projects_images_images_image_id",
							column: x => x.image_id,
							principalTable: "images",
							principalColumn: "id",
							onDelete: ReferentialAction.Cascade
						);
						table.ForeignKey(
							name: "FK_projects_images_projects_project_id",
							column: x => x.project_id,
							principalTable: "projects",
							principalColumn: "id",
							onDelete: ReferentialAction.Cascade
						);
					}
				)
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder
				.CreateTable(
					name: "skills_images",
					columns: table => new
					{
						skill_id = table.Column<int>(type: "int", nullable: false),
						image_id = table.Column<int>(type: "int", nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_skills_images", x => new { x.skill_id, x.image_id });
						table.ForeignKey(
							name: "FK_skills_images_images_image_id",
							column: x => x.image_id,
							principalTable: "images",
							principalColumn: "id",
							onDelete: ReferentialAction.Cascade
						);
						table.ForeignKey(
							name: "FK_skills_images_skills_skill_id",
							column: x => x.skill_id,
							principalTable: "skills",
							principalColumn: "id",
							onDelete: ReferentialAction.Cascade
						);
					}
				)
				.Annotation("MySql:CharSet", "utf8mb4");

			migrationBuilder.CreateIndex(
				name: "IX_projects_images_image_id",
				table: "projects_images",
				column: "image_id"
			);

			migrationBuilder.CreateIndex(name: "IX_skills_images_image_id", table: "skills_images", column: "image_id");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(name: "projects_images");

			migrationBuilder.DropTable(name: "skills_images");

			migrationBuilder.DropTable(name: "images");
		}
	}
}
