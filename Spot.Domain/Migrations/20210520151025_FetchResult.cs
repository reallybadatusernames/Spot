using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spot.Domain.Migrations
{
    public partial class FetchResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FetchDate",
                table: "FetchResults",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "AllowRedirect",
                table: "Fetches",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "518acc2c-cfd8-4713-ac27-ecdbac4291fd",
                column: "ConcurrencyStamp",
                value: "2526aa1c-6ee8-4be7-a016-d9b114b666f6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "861fbbcd-4bfd-4ab2-b12c-fe75a7fd53d7",
                column: "ConcurrencyStamp",
                value: "fe1acc68-9ed8-4905-aea0-2f5a4acbc1a9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "917d367a-2f4f-45cc-8d1c-e4e01ab42c7f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63cdda64-9f0b-4ba5-9256-48b22b27fd79", "AQAAAAEAACcQAAAAEOQnY/NA22vStMK2X4pcvaqFFuc1rEL81jGRIBD5JpdBnWb4RXhgyL86xznhVMrKQg==", "90fcea06-1d70-40ef-a2d6-f9b3ce7c78c4" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FetchDate",
                table: "FetchResults");

            migrationBuilder.DropColumn(
                name: "AllowRedirect",
                table: "Fetches");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "518acc2c-cfd8-4713-ac27-ecdbac4291fd",
                column: "ConcurrencyStamp",
                value: "1738f619-7dc2-4eac-9e6f-bef47f953b4c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "861fbbcd-4bfd-4ab2-b12c-fe75a7fd53d7",
                column: "ConcurrencyStamp",
                value: "3ef6aa40-a3b4-44fe-9584-ca80571543d8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "917d367a-2f4f-45cc-8d1c-e4e01ab42c7f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de50cf14-a917-4ef3-a22b-684af1ccfcba", "AQAAAAEAACcQAAAAEJ23GVhSLbf2+T0yFYxN2LDIbhLbdaay/O0BvQFxnabQJP7gTjTz0XayMNpAhfRY5Q==", "1de03d54-2d2c-49ec-b69a-c0464e7fcfa6" });
        }
    }
}
