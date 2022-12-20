using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeededDefaultUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "644D2C88-837C-41E8-AA6D-00DC1D92922C", "b2aa8a79-a123-470f-9872-d0755cb9f63d", "Administrator", "ADMIN" },
                    { "DA119D4C-F5E5-49B3-A11D-21FB05A788D7", "49fb258e-9425-4fc2-8672-e375a5dab5cf", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "72790144-CBE4-471B-90BD-71CFFD724717", 0, "d13c2f8f-695b-4d6e-9706-e94d1529edcb", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEI6vprkXnuYwxOFZminFfYkwWN/0X5gCMpU36KbA1HJjXwZclHKfMWw09mgagZzM2A==", null, false, "ef64e41e-d682-46b3-8074-a193fbf005db", false, "admin@bookstore.com" },
                    { "DDD96588-CCAB-42B2-A34F-67DE54FFCABE", 0, "4a6dfa6b-2d0f-423e-af48-466fd8ca48af", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER@BOOKSTORE.COM", "AQAAAAEAACcQAAAAEBYCSfQe5tHXcx+M7e9yRn9i/gmAzCPoQm/GfU/BCb6rPM9zT+7g43frpw+sex90gw==", null, false, "808e2cd0-4d00-4500-9265-e5650833b948", false, "user@bookstore.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "644D2C88-837C-41E8-AA6D-00DC1D92922C", "72790144-CBE4-471B-90BD-71CFFD724717" },
                    { "DA119D4C-F5E5-49B3-A11D-21FB05A788D7", "DDD96588-CCAB-42B2-A34F-67DE54FFCABE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "644D2C88-837C-41E8-AA6D-00DC1D92922C", "72790144-CBE4-471B-90BD-71CFFD724717" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "DA119D4C-F5E5-49B3-A11D-21FB05A788D7", "DDD96588-CCAB-42B2-A34F-67DE54FFCABE" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "644D2C88-837C-41E8-AA6D-00DC1D92922C");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "DA119D4C-F5E5-49B3-A11D-21FB05A788D7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72790144-CBE4-471B-90BD-71CFFD724717");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "DDD96588-CCAB-42B2-A34F-67DE54FFCABE");
        }
    }
}
