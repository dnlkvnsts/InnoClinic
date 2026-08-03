using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InnoClinic.Profiles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedDoctorsData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecializationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CareerStartYear = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "IsActive", "SpecializationName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), true, "Терапевт" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), true, "Хирург" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "AccountId", "CareerStartYear", "DateOfBirth", "FirstName", "LastName", "MiddleName", "OfficeAddress", "PhotoUrl", "SpecializationId", "Status" },
                values: new object[,]
                {
                    { new Guid("aa11aa11-1111-1111-1111-111111111111"), new Guid("d45cb46b-13f7-452f-820c-83220f87900d"), 2010, new DateTime(1985, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Иван", "Иванов", "Сергеевич", "г. Минск, каб. 301", "https://example.com/photos/ivanov.jpg", new Guid("11111111-1111-1111-1111-111111111111"), "At work" },
                    { new Guid("bb22bb22-2222-2222-2222-222222222222"), new Guid("5f3ecf97-bafc-48bb-a8f3-d0a7e117b0fd"), 2018, new DateTime(1990, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Алексей", "Петров", "Николаевич", "г. Минск, каб. 405", null, new Guid("22222222-2222-2222-2222-222222222222"), "At work" },
                    { new Guid("cc33cc33-3333-3333-3333-333333333333"), new Guid("2e531410-e543-4f30-93ed-741cc6ab4947"), 2005, new DateTime(1978, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ольга", "Иванова", "Михайловна", "г. Минск, каб. 302", null, new Guid("11111111-1111-1111-1111-111111111111"), "On vacation" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors",
                column: "SpecializationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
