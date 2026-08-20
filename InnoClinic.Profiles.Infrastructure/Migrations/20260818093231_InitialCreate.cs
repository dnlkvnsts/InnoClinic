using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InnoClinic.Profiles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLinkedToAccount = table.Column<bool>(type: "bit", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

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
                table: "Patients",
                columns: new[] { "Id", "AccountId", "DateOfBirth", "FirstName", "IsLinkedToAccount", "LastName", "MiddleName", "Phone", "PhotoUrl" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Emily", true, "Brown", "Grace", "+1234567890", "https://example.com/photos/emilybrown.jpg" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, new DateTime(1988, 11, 23, 0, 0, 0, 0, DateTimeKind.Utc), "James", false, "Wilson", null, "+10987654321", null }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "IsActive", "SpecializationName" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), true, "Therapist" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), true, "Surgeon" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), true, "Cardiologist" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "AccountId", "CareerStartYear", "DateOfBirth", "FirstName", "LastName", "MiddleName", "OfficeAddress", "PhotoUrl", "SpecializationId", "Status" },
                values: new object[,]
                {
                    { new Guid("bb22bb22-2222-2222-2222-222222222222"), new Guid("55555555-5555-5555-5555-555555555555"), 2018, new DateTime(1990, 8, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Alex", "Petrov", "Nikolaevich", "Minsk, office 405", null, new Guid("22222222-2222-2222-2222-222222222222"), "At work" },
                    { new Guid("cc33cc33-3333-3333-3333-333333333333"), new Guid("66666666-6666-6666-6666-666666666666"), 2005, new DateTime(1978, 11, 2, 0, 0, 0, 0, DateTimeKind.Utc), "Olga", "Ivanova", "Mikhailovna", "Minsk, office 302", null, new Guid("11111111-1111-1111-1111-111111111111"), "On vacation" },
                    { new Guid("dd44dd44-4444-4444-4444-444444444444"), new Guid("77777777-7777-7777-7777-777777777777"), 2012, new DateTime(1985, 3, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Alex", "Smith", "John", "Minsk, office 210", null, new Guid("33333333-3333-3333-3333-333333333333"), "At work" }
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
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
