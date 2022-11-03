using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace diplom.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "Address", "Age", "Name" },
                values: new object[,]
                {
                    { new Guid("80515d34-f56c-4e0d-85a9-1e3ebde9abda"), "Atemar, house, 125", "20", "Sergey Shatrov" },
                    { new Guid("167e2f3a-3c45-452a-b59e-130eaa101d50"), "Atemar, house, 126", "22", "Sergey Falileev" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "ClientId", "Cost", "Product", "Quantity" },
                values: new object[,]
                {
                    { new Guid("b9fc089c-463a-47d5-9763-1bd0a94f1f03"), new Guid("00000000-0000-0000-0000-000000000000"), 3000, "Oil Motul", 3 },
                    { new Guid("92a60611-bff8-487f-ab72-7c27a073fd19"), new Guid("00000000-0000-0000-0000-000000000000"), 1500, "Oil Ralf", 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("167e2f3a-3c45-452a-b59e-130eaa101d50"));

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("80515d34-f56c-4e0d-85a9-1e3ebde9abda"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("92a60611-bff8-487f-ab72-7c27a073fd19"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: new Guid("b9fc089c-463a-47d5-9763-1bd0a94f1f03"));
        }
    }
}
