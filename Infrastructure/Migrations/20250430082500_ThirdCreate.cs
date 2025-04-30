using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ThirdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Places_PlaceId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Users_GuideId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Users_UserId",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.EnsureSchema(
                name: "transactions");

            migrationBuilder.EnsureSchema(
                name: "locations");

            migrationBuilder.EnsureSchema(
                name: "guides");

            migrationBuilder.EnsureSchema(
                name: "useractions");

            migrationBuilder.EnsureSchema(
                name: "auth");

            migrationBuilder.RenameTable(
                name: "WishList",
                newName: "WishList",
                newSchema: "useractions");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "auth");

            migrationBuilder.RenameTable(
                name: "States",
                newName: "States",
                newSchema: "locations");

            migrationBuilder.RenameTable(
                name: "Places",
                newName: "Places",
                newSchema: "locations");

            migrationBuilder.RenameTable(
                name: "GuideProfiles",
                newName: "GuideProfiles",
                newSchema: "guides");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "Countries",
                newSchema: "locations");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "Bookings",
                newSchema: "transactions");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Ratings",
                newSchema: "useractions");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_UserId",
                schema: "useractions",
                table: "Ratings",
                newName: "IX_Ratings_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_PlaceId",
                schema: "useractions",
                table: "Ratings",
                newName: "IX_Ratings_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Rating_GuideId",
                schema: "useractions",
                table: "Ratings",
                newName: "IX_Ratings_GuideId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                schema: "useractions",
                table: "Ratings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Places_PlaceId",
                schema: "useractions",
                table: "Ratings",
                column: "PlaceId",
                principalSchema: "locations",
                principalTable: "Places",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_GuideId",
                schema: "useractions",
                table: "Ratings",
                column: "GuideId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_UserId",
                schema: "useractions",
                table: "Ratings",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Places_PlaceId",
                schema: "useractions",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_GuideId",
                schema: "useractions",
                table: "Ratings");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_UserId",
                schema: "useractions",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                schema: "useractions",
                table: "Ratings");

            migrationBuilder.RenameTable(
                name: "WishList",
                schema: "useractions",
                newName: "WishList");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "auth",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "States",
                schema: "locations",
                newName: "States");

            migrationBuilder.RenameTable(
                name: "Places",
                schema: "locations",
                newName: "Places");

            migrationBuilder.RenameTable(
                name: "GuideProfiles",
                schema: "guides",
                newName: "GuideProfiles");

            migrationBuilder.RenameTable(
                name: "Countries",
                schema: "locations",
                newName: "Countries");

            migrationBuilder.RenameTable(
                name: "Bookings",
                schema: "transactions",
                newName: "Bookings");

            migrationBuilder.RenameTable(
                name: "Ratings",
                schema: "useractions",
                newName: "Rating");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_UserId",
                table: "Rating",
                newName: "IX_Rating_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_PlaceId",
                table: "Rating",
                newName: "IX_Rating_PlaceId");

            migrationBuilder.RenameIndex(
                name: "IX_Ratings_GuideId",
                table: "Rating",
                newName: "IX_Rating_GuideId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Places_PlaceId",
                table: "Rating",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Users_GuideId",
                table: "Rating",
                column: "GuideId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Users_UserId",
                table: "Rating",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
