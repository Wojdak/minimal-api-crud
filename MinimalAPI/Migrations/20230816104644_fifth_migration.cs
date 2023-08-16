using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalAPI.Migrations
{
    /// <inheritdoc />
    public partial class fifth_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$pi515kdmgpUzXey08Jn9zu17X4FbsG8VGkesBxj3ciZv8eXKeRs3q");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$pi515kdmgpUzXey08Jn9zu17X4FbsG8VGkesBxj3ciZv8eXKeRs3q");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$S7G38F2Q1RF4gehQQAd2Tul0pwyjOZD.gPBFHXoBfjTyBp6/QtflG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "$2a$11$S7G38F2Q1RF4gehQQAd2Tul0pwyjOZD.gPBFHXoBfjTyBp6/QtflG");
        }
    }
}
