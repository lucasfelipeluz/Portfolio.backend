using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Infra.Migrations
{
	public partial class FixBoolColumn : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(name: "Id", table: "projects_skills");

			migrationBuilder.AlterColumn<sbyte>(
				name: "is_active",
				table: "users",
				type: "TINYINT",
				nullable: false,
				defaultValue: (sbyte)1,
				oldClrType: typeof(sbyte),
				oldType: "TINYINT",
				oldDefaultValueSql: "1"
			);

			migrationBuilder.AlterColumn<sbyte>(
				name: "is_active",
				table: "skills",
				type: "TINYINT",
				nullable: false,
				defaultValue: (sbyte)1,
				oldClrType: typeof(sbyte),
				oldType: "TINYINT",
				oldDefaultValueSql: "1"
			);

			migrationBuilder.AlterColumn<sbyte>(
				name: "is_active",
				table: "projects",
				type: "TINYINT",
				nullable: false,
				defaultValue: (sbyte)1,
				oldClrType: typeof(sbyte),
				oldType: "TINYINT",
				oldDefaultValueSql: "1"
			);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<sbyte>(
				name: "is_active",
				table: "users",
				type: "TINYINT",
				nullable: false,
				defaultValueSql: "1",
				oldClrType: typeof(sbyte),
				oldType: "TINYINT",
				oldDefaultValue: (sbyte)1
			);

			migrationBuilder.AlterColumn<sbyte>(
				name: "is_active",
				table: "skills",
				type: "TINYINT",
				nullable: false,
				defaultValueSql: "1",
				oldClrType: typeof(sbyte),
				oldType: "TINYINT",
				oldDefaultValue: (sbyte)1
			);

			migrationBuilder.AddColumn<int>(
				name: "Id",
				table: "projects_skills",
				type: "int",
				nullable: false,
				defaultValue: 0
			);

			migrationBuilder.AlterColumn<sbyte>(
				name: "is_active",
				table: "projects",
				type: "TINYINT",
				nullable: false,
				defaultValueSql: "1",
				oldClrType: typeof(sbyte),
				oldType: "TINYINT",
				oldDefaultValue: (sbyte)1
			);
		}
	}
}
