using Microsoft.EntityFrameworkCore.Migrations;

namespace diplom.Migrations
{
    public partial class AddedRolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f1b08599-58fb-4d05-8afa-6659337b319b", "cd79577c-1f3b-46cb-b1bd-afe963fd1077", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "97615302-257f-4ffc-a268-309b3f10b5fa", "c42bfc2c-44e5-494a-ad89-d08e4aa5242b", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97615302-257f-4ffc-a268-309b3f10b5fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1b08599-58fb-4d05-8afa-6659337b319b");
        }
    }
}
