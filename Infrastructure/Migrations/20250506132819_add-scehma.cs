using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addscehma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.EnsureSchema(
                name: "interactions");

            migrationBuilder.RenameTable(
                name: "Notification",
                newName: "Notification",
                newSchema: "interactions");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message",
                newSchema: "interactions");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                schema: "interactions",
                table: "Message",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                schema: "interactions",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Notification",
                schema: "interactions",
                newName: "Notification");

            migrationBuilder.RenameTable(
                name: "Message",
                schema: "interactions",
                newName: "Messages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");
        }
    }
}
