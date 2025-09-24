using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShortUrl.LinkManager.API.Migrations
{
    /// <inheritdoc />
    public partial class report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClickEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Ts = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Referrer = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClickEvents", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClickEvents_Code",
                table: "ClickEvents",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_ClickEvents_LinkId_Ts",
                table: "ClickEvents",
                columns: new[] { "LinkId", "Ts" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClickEvents");
        }
    }
}
