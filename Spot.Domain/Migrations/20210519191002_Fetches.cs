using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Spot.Domain.Migrations
{
    public partial class Fetches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UsesHttps",
                table: "Sites",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Fetches",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SiteId = table.Column<Guid>(nullable: false),
                    AbsolutePath = table.Column<string>(nullable: true),
                    Enabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fetches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fetches_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FetchResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FetchId = table.Column<Guid>(nullable: false),
                    ResultCode = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FetchResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FetchResults_Fetches_FetchId",
                        column: x => x.FetchId,
                        principalTable: "Fetches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Fetches_SiteId",
                table: "Fetches",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_FetchResults_FetchId",
                table: "FetchResults",
                column: "FetchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FetchResults");

            migrationBuilder.DropTable(
                name: "Fetches");

            migrationBuilder.DropColumn(
                name: "UsesHttps",
                table: "Sites");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "518acc2c-cfd8-4713-ac27-ecdbac4291fd",
                column: "ConcurrencyStamp",
                value: "3cdaa875-c5f7-45e8-9cd7-4334afdd1d52");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "861fbbcd-4bfd-4ab2-b12c-fe75a7fd53d7",
                column: "ConcurrencyStamp",
                value: "a94b831f-52be-49e8-84a0-8c547ad99de6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "917d367a-2f4f-45cc-8d1c-e4e01ab42c7f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "339c1df8-d927-4b98-a8ba-b2703aa7f702", "AQAAAAEAACcQAAAAEKug6XvVNvyDS3ibJ4qJ6RFpL8brIjJqZZ9hlCAqTesVdvgVvI6Pb44GkTOv04k8gg==", "e15fde53-ff72-414d-abb3-59a31d586b18" });
        }
    }
}
