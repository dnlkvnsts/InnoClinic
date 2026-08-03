using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnoClinic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BackfillEmailConfirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE AspNetUsers SET EmailConfirmed = 1 WHERE EmailConfirmed = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
