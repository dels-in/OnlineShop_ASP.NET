using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTypeOfPropertiesUserinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                    name: "FirstName",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.AlterColumn<string>(
                    name: "LastName",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.AlterColumn<string>(
                    name: "Address",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.AlterColumn<string>(
                    name: "Address2",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.AlterColumn<string>(
                    name: "City",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.AlterColumn<string>(
                    name: "Region",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.AlterColumn<string>(
                    name: "PostCode",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: true,
                    oldClrType: typeof(string),
                    oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "FirstName",
                keyValue: null,
                column: "Picture",
                value: "");

            migrationBuilder.AlterColumn<string>(
                    name: "FirstName",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "longtext",
                    oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "LastName",
                keyValue: null,
                column: "Picture",
                value: "");

            migrationBuilder.AlterColumn<string>(
                    name: "LastName",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "longtext",
                    oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Address",
                keyValue: null,
                column: "Picture",
                value: "");

            migrationBuilder.AlterColumn<string>(
                    name: "Address",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "longtext",
                    oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Address2",
                keyValue: null,
                column: "Picture",
                value: "");

            migrationBuilder.AlterColumn<string>(
                    name: "Address2",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "longtext",
                    oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "City",
                keyValue: null,
                column: "Picture",
                value: "");

            migrationBuilder.AlterColumn<string>(
                    name: "City",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "longtext",
                    oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "Region",
                keyValue: null,
                column: "Picture",
                value: "");

            migrationBuilder.AlterColumn<string>(
                    name: "Region",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "longtext",
                    oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
            
            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "PostCode",
                keyValue: null,
                column: "Picture",
                value: "");

            migrationBuilder.AlterColumn<string>(
                    name: "PostCode",
                    table: "UserInfo",
                    type: "longtext",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "longtext",
                    oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
