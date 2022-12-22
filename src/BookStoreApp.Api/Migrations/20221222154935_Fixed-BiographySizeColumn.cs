using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixedBiographySizeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Authors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Authors",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "644D2C88-837C-41E8-AA6D-00DC1D92922C",
                column: "ConcurrencyStamp",
                value: "3041d9f6-5b69-4c69-9ec1-cec9815ce933");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DA119D4C-F5E5-49B3-A11D-21FB05A788D7",
                column: "ConcurrencyStamp",
                value: "5c77a914-4335-4e01-a782-de1a656b5415");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72790144-CBE4-471B-90BD-71CFFD724717",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9d94e44f-5bf4-4c27-ac26-e7c2a65230de", "AQAAAAEAACcQAAAAEIRjLY7dkKiEpIReMKBrhxTxc5fLsOamhHmDQ+slvVWrrDBuprQSqaJuYGwsS3n56w==", "bf2812da-1606-4ae9-b1fd-f9beda593179" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "DDD96588-CCAB-42B2-A34F-67DE54FFCABE",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "73d62fa6-5b53-479a-9c5f-b3f955b4665a", "AQAAAAEAACcQAAAAEDdl7wGiQSU+P+/1It6nU/TAtCflhSu0eoDafuHmidpiyH3g5psKlXtpWliofN1YrQ==", "1a742e01-a4eb-4607-ab34-4b0fd6e7feb1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Authors",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "Authors",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(250)",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "644D2C88-837C-41E8-AA6D-00DC1D92922C",
                column: "ConcurrencyStamp",
                value: "b2aa8a79-a123-470f-9872-d0755cb9f63d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DA119D4C-F5E5-49B3-A11D-21FB05A788D7",
                column: "ConcurrencyStamp",
                value: "49fb258e-9425-4fc2-8672-e375a5dab5cf");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72790144-CBE4-471B-90BD-71CFFD724717",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d13c2f8f-695b-4d6e-9706-e94d1529edcb", "AQAAAAEAACcQAAAAEI6vprkXnuYwxOFZminFfYkwWN/0X5gCMpU36KbA1HJjXwZclHKfMWw09mgagZzM2A==", "ef64e41e-d682-46b3-8074-a193fbf005db" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "DDD96588-CCAB-42B2-A34F-67DE54FFCABE",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a6dfa6b-2d0f-423e-af48-466fd8ca48af", "AQAAAAEAACcQAAAAEBYCSfQe5tHXcx+M7e9yRn9i/gmAzCPoQm/GfU/BCb6rPM9zT+7g43frpw+sex90gw==", "808e2cd0-4d00-4500-9265-e5650833b948" });
        }
    }
}
