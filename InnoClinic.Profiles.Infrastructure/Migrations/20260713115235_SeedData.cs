using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InnoClinic.Profiles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "CareerStartYear", "FirstName", "LastName", "MiddleName", "OfficeAddress", "PhotoUrl", "Specialization", "Status", "UserId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 2015, "John", "Doe", "Robert", "123 Health Ave, Room 101", "https://example.com/photos/johndoe.jpg", "Cardiologist", "At work", "user-guid-1" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 2018, "Alice", "Smith", "Jane", "123 Health Ave, Room 205", "https://example.com/photos/alicesmith.jpg", "Pediatrician", "At work", "user-guid-2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));
        }
    }
}
