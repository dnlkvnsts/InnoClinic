using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InnoClinic.Profiles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDoctorAndPatientSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "AccountId", "DateOfBirth", "FirstName", "IsLinkedToAccount", "LastName", "MiddleName", "Phone", "PhotoUrl" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Emily", true, "Brown", "Grace", "+1234567890", "https://example.com/photos/emilybrown.jpg" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), null, new DateTime(1988, 11, 23, 0, 0, 0, 0, DateTimeKind.Utc), "James", false, "Wilson", null, "+10987654321", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: new Guid("20000000-0000-0000-0000-000000000002"));

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "AccountId", "DateOfBirth", "FirstName", "IsLinkedToAccount", "LastName", "MiddleName", "Phone", "PhotoUrl" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(1995, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Emily", true, "Brown", "Grace", "+1234567890", "https://example.com/photos/emilybrown.jpg" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, new DateTime(1988, 11, 23, 0, 0, 0, 0, DateTimeKind.Utc), "James", false, "Wilson", null, "+10987654321", null }
                });
        }
    }
}
