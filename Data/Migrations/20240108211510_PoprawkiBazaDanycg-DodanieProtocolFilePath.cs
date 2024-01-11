using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoragewithComputerParts.Data.Migrations
{
    /// <inheritdoc />
    public partial class PoprawkiBazaDanycgDodanieProtocolFilePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProtocolFilePath",
                table: "Protocols",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProtocolFilePath",
                table: "Protocols");
        }
    }
}
