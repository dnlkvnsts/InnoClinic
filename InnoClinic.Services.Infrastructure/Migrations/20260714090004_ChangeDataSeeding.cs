using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InnoClinic.Services.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("1523b195-ffa8-4457-844b-3899d5d38b25"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("765da283-1779-4aea-909d-f7a5ba6b32bc"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("ae484f68-911e-46bb-82ad-7daf45a54adb"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("f4cd5bb7-5238-44aa-8fd5-181c27cda78b"));

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CategoryId", "IsActive", "Price", "ServiceName", "SpecializationId" },
                values: new object[,]
                {
                    { new Guid("c1111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), true, 1500m, "Initial Cardiologist Consultation", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("c2222222-2222-2222-2222-222222222222"), new Guid("11111111-1111-1111-1111-111111111111"), true, 1700m, "Neurologist Appointment", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("c3333333-3333-3333-3333-333333333333"), new Guid("22222222-2222-2222-2222-222222222222"), true, 2500m, "Echocardiography (Heart Ultrasound)", null },
                    { new Guid("c4444444-4444-4444-4444-444444444444"), new Guid("33333333-3333-3333-3333-333333333333"), true, 500m, "Complete Blood Count", null }
                });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "SpecializationName",
                value: "Cardiology");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "SpecializationName",
                value: "Neurology");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("c1111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("c2222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("c3333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("c4444444-4444-4444-4444-444444444444"));

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CategoryId", "IsActive", "Price", "ServiceName", "SpecializationId" },
                values: new object[,]
                {
                    { new Guid("1523b195-ffa8-4457-844b-3899d5d38b25"), new Guid("22222222-2222-2222-2222-222222222222"), true, 2500m, "УЗИ сердца", null },
                    { new Guid("765da283-1779-4aea-909d-f7a5ba6b32bc"), new Guid("11111111-1111-1111-1111-111111111111"), true, 1500m, "Первичный прием кардиолога", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("ae484f68-911e-46bb-82ad-7daf45a54adb"), new Guid("11111111-1111-1111-1111-111111111111"), true, 1700m, "Прием невролога", new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("f4cd5bb7-5238-44aa-8fd5-181c27cda78b"), new Guid("33333333-3333-3333-3333-333333333333"), true, 500m, "Общий анализ крови", null }
                });

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "SpecializationName",
                value: "Кардиология");

            migrationBuilder.UpdateData(
                table: "Specializations",
                keyColumn: "Id",
                keyValue: new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                column: "SpecializationName",
                value: "Неврология");
        }
    }
}
