using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductsDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ComparisonId", "Cost", "Description", "Genre", "LibraryId", "MetacriticScore", "Name", "Source", "WishlistId" },
                values: new object[,]
                {
                    { 1, null, 4499m, "Build your dream squad, outsmart your rivals and experience the thrill of big European nights in the UEFA Champions League. Progress never stops when you’re pursuing footballing greatness.", "Simulation", null, 84, "FOOTBALL MANAGER 2024", "/images/fm2024.jpg", null },
                    { 2, null, 8999m, "In this sequel to the Legend of Zelda: Breath of the Wild game, you’ll decide your own path through the sprawling landscapes of Hyrule and the islands floating in the vast skies above.", "Action", null, 96, "THE LEGEND OF ZELDA: TEARS OF THE KINGDOM", "/images/zelda.jpg", null },
                    { 3, null, 2999m, "Carve your own clever path to vengeance in the award winning adventure from developer FromSoftware, creators of Bloodborne and the Dark Souls series. Take Revenge. Restore Your Honor. Kill Ingeniously.", "Action", null, 90, "SEKIRO", "/images/sekiro.jpg", null },
                    { 4, null, 5999m, "In this new generation role-playing game, which takes place in space, you can create any character and explore the universe the way you want. Embark on a journey and uncover the greatest mystery of humanity.", "RPG", null, 83, "STARFIELD", "/images/starfield.jpeg", null },
                    { 5, null, 499m, "Connected adds an all-new robust multiplayer expansion to the huge variety of addictive and innovative single-player modes that Tetris Effect is known for, with all-new co-op and online or local multiplayer modes!", "Puzzle", null, 78, "TETRIS", "/images/tetris.jpg", null },
                    { 6, null, 4999m, "Baldur’s Gate 3 is a story-rich, party-based RPG set in the universe of Dungeons & Dragons, where your choices shape a tale of fellowship and betrayal, survival and sacrifice, and the lure of absolute power.", "RPG", null, 96, "BALDUR'S GATE 3", "/images/baldursgate.jpeg", null },
                    { 7, null, 199m, "Every day, millions of players worldwide enter battle as one of over a hundred heroes. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has taken on a life of its own.", "MOBA", null, 96, "DOTA 2", "/images/dota2.jpg", null },
                    { 8, null, 5999m, "RDR2 is the epic tale of outlaw Arthur Morgan and the infamous Van der Linde gang, on the run across America at the dawn of the modern age. Also includes access to the shared living world of Red Dead Online.", "Action", null, 97, "RED DEAD REDEMPTION 2", "/images/rdr2.png", null },
                    { 9, null, 999m, "Katana ZERO is a stylish neo-noir, platformer featuring breakneck action and instant-death combat. Slash, dash, and manipulate time to unravel your past in a beautifully brutal acrobatic display.", "Platformer", null, 83, "KATANA ZERO", "/images/katana_zero.png", null },
                    { 10, null, 3499m, "Then, there was fire. Re-experience the critically acclaimed, genre-defining game that started it all. Beautifully remastered, return to Lordran in stunning high-definition detail running at 60fps.", "Souls-like", null, 84, "DARK SOULS", "/images/dark_souls.jpg", null },
                    { 11, null, 999m, "Experience the emotional storytelling and unforgettable characters in The Last of Us. The action of the games in the series takes place in a post-apocalyptic future on the territory of the former United States of America after a global epidemic caused by a dangerously mutated mushroom cordyceps.", "Action-adventure", null, 95, "THE LAST OF US", "/images/tlou.jpeg", null },
                    { 12, null, 1999m, "Disco Elysium - The Final Cut is a groundbreaking role playing game. You’re a detective with a unique skill system at your disposal and a whole city to carve your path across. Interrogate unforgettable characters, crack murders or take bribes. Become a hero or an absolute disaster of a human being.", "RPG", null, 91, "DISCO ELYSIUM", "/images/disco_elysium.jpeg", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
