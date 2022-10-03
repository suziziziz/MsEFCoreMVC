﻿using System;

using Microsoft.EntityFrameworkCore.Migrations;

using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MsEFCoreMVC.Migrations {
  public partial class ComplexDataModel : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.AlterColumn<string>(
          name: "LastName",
          table: "Student",
          type: "character varying(50)",
          maxLength: 50,
          nullable: false,
          defaultValue: "",
          oldClrType: typeof(string),
          oldType: "character varying(50)",
          oldMaxLength: 50,
          oldNullable: true);

      migrationBuilder.AlterColumn<string>(
          name: "FirstName",
          table: "Student",
          type: "character varying(50)",
          maxLength: 50,
          nullable: false,
          defaultValue: "",
          oldClrType: typeof(string),
          oldType: "character varying(50)",
          oldMaxLength: 50,
          oldNullable: true);

      migrationBuilder.AlterColumn<string>(
          name: "Title",
          table: "Course",
          type: "character varying(50)",
          maxLength: 50,
          nullable: true,
          oldClrType: typeof(string),
          oldType: "text",
          oldNullable: true);

      // migrationBuilder.AddColumn<int>(
      //     name: "DepartmentID",
      //     table: "Course",
      //     type: "integer",
      //     nullable: false,
      //     defaultValue: 0);

      migrationBuilder.CreateTable(
          name: "Instructor",
          columns: table => new {
            ID = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            HireDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_Instructor", x => x.ID);
          });

      migrationBuilder.CreateTable(
          name: "CourseAssignment",
          columns: table => new {
            InstructorID = table.Column<int>(type: "integer", nullable: false),
            CourseID = table.Column<int>(type: "integer", nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_CourseAssignment", x => new { x.CourseID, x.InstructorID });
            table.ForeignKey(
                      name: "FK_CourseAssignment_Course_CourseID",
                      column: x => x.CourseID,
                      principalTable: "Course",
                      principalColumn: "CourseID",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_CourseAssignment_Instructor_InstructorID",
                      column: x => x.InstructorID,
                      principalTable: "Instructor",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Department",
          columns: table => new {
            DepartmentID = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
            Budget = table.Column<decimal>(type: "money", nullable: false),
            StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            InstructorID = table.Column<int>(type: "integer", nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Department", x => x.DepartmentID);
            table.ForeignKey(
                      name: "FK_Department_Instructor_InstructorID",
                      column: x => x.InstructorID,
                      principalTable: "Instructor",
                      principalColumn: "ID");
          });

      migrationBuilder.Sql("INSERT INTO \"Department\" (\"Name\", \"Budget\", \"StartDate\") VALUES ('Temp', 0.00, (SELECT CURRENT_DATE))");
      // Default value for FK points to department created above, with
      // defaultValue changed to 1 in following AddColumn statement.

      migrationBuilder.AddColumn<int>(
          name: "DepartmentID",
          table: "Course",
          nullable: false,
          defaultValue: 1);

      migrationBuilder.CreateTable(
          name: "OfficeAssignment",
          columns: table => new {
            InstructorID = table.Column<int>(type: "integer", nullable: false),
            Location = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_OfficeAssignment", x => x.InstructorID);
            table.ForeignKey(
                      name: "FK_OfficeAssignment_Instructor_InstructorID",
                      column: x => x.InstructorID,
                      principalTable: "Instructor",
                      principalColumn: "ID",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Course_DepartmentID",
          table: "Course",
          column: "DepartmentID");

      migrationBuilder.CreateIndex(
          name: "IX_CourseAssignment_InstructorID",
          table: "CourseAssignment",
          column: "InstructorID");

      migrationBuilder.CreateIndex(
          name: "IX_Department_InstructorID",
          table: "Department",
          column: "InstructorID");

      migrationBuilder.AddForeignKey(
          name: "FK_Course_Department_DepartmentID",
          table: "Course",
          column: "DepartmentID",
          principalTable: "Department",
          principalColumn: "DepartmentID",
          onDelete: ReferentialAction.Cascade);
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropForeignKey(
          name: "FK_Course_Department_DepartmentID",
          table: "Course");

      migrationBuilder.DropTable(
          name: "CourseAssignment");

      migrationBuilder.DropTable(
          name: "Department");

      migrationBuilder.DropTable(
          name: "OfficeAssignment");

      migrationBuilder.DropTable(
          name: "Instructor");

      migrationBuilder.DropIndex(
          name: "IX_Course_DepartmentID",
          table: "Course");

      migrationBuilder.DropColumn(
          name: "DepartmentID",
          table: "Course");

      migrationBuilder.AlterColumn<string>(
          name: "LastName",
          table: "Student",
          type: "character varying(50)",
          maxLength: 50,
          nullable: true,
          oldClrType: typeof(string),
          oldType: "character varying(50)",
          oldMaxLength: 50);

      migrationBuilder.AlterColumn<string>(
          name: "FirstName",
          table: "Student",
          type: "character varying(50)",
          maxLength: 50,
          nullable: true,
          oldClrType: typeof(string),
          oldType: "character varying(50)",
          oldMaxLength: 50);

      migrationBuilder.AlterColumn<string>(
          name: "Title",
          table: "Course",
          type: "text",
          nullable: true,
          oldClrType: typeof(string),
          oldType: "character varying(50)",
          oldMaxLength: 50,
          oldNullable: true);
    }
  }
}
