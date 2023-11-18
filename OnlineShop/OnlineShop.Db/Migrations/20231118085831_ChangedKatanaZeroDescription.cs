using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class ChangedKatanaZeroDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Katana ZERO is a stylish neo-noir, platformer featuring breakneck action and death combat. Slash, dash, and manipulate time to unravel your past in a beautifully brutal acrobatic display.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Katana ZERO is a stylish neo-noir, platformer featuring breakneck action and instant-death combat. Slash, dash, and manipulate time to unravel your past in a beautifully brutal acrobatic display.");
        }
    }
}
