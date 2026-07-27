using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoClinic.Profiles.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSpecializationTableInProfiles : Migration
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
            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Doctors");

            migrationBuilder.AddColumn<Guid>(
                name: "SpecializationId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Doctors",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql(@"
        INSERT INTO [Specializations] ([Id], [SpecializationName], [IsActive])
        SELECT NEWID(), [Specialization], 1
        FROM [Doctors]
        WHERE [Specialization] IS NOT NULL AND [Specialization] <> ''
        GROUP BY [Specialization];
    ");

            migrationBuilder.Sql(@"
        UPDATE d
        SET d.[SpecializationId] = s.[Id]
        FROM [Doctors] d
        INNER JOIN [Specializations] s ON d.[Specialization] = s.[SpecializationName];
    ");

            migrationBuilder.Sql(@"
        UPDATE [Doctors]
        SET [AccountId] = TRY_CAST([UserId] AS uniqueidentifier)
        WHERE [UserId] IS NOT NULL;
    ");

            migrationBuilder.Sql(@"
        IF EXISTS (SELECT 1 FROM [Doctors] WHERE [SpecializationId] IS NULL)
        BEGIN
            DECLARE @DefaultSpecId UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000001';
            
            IF NOT EXISTS (SELECT 1 FROM [Specializations] WHERE [Id] = @DefaultSpecId)
            BEGIN
                INSERT INTO [Specializations] ([Id], [SpecializationName], [IsActive])
                VALUES (@DefaultSpecId, 'Default', 1);
            END

            UPDATE [Doctors]
            SET [SpecializationId] = @DefaultSpecId
            WHERE [SpecializationId] IS NULL;
        END

        UPDATE [Doctors]
        SET [AccountId] = '00000000-0000-0000-0000-000000000000'
        WHERE [AccountId] IS NULL;
    ");

            migrationBuilder.AlterColumn<Guid>(
                name: "SpecializationId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Doctors",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors",
                column: "SpecializationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                table: "Doctors",
                column: "SpecializationId",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Doctors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Specializations_SpecializationId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_SpecializationId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
