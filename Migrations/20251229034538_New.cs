using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Credit_Card_Fraud_Detection.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "Users",
                type: "char(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1)");

            migrationBuilder.AddColumn<long>(
                name: "deviceID",
                table: "TxnTables",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeviceHistory",
                columns: table => new
                {
                    deviceID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<long>(type: "bigint", nullable: false),
                    deviceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deviceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    firstUseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastUseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceHistory", x => x.deviceID);
                    table.ForeignKey(
                        name: "FK_DeviceHistory_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TxnTables_deviceID",
                table: "TxnTables",
                column: "deviceID");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHistory_userID",
                table: "DeviceHistory",
                column: "userID");

            migrationBuilder.AddForeignKey(
                name: "FK_TxnTables_DeviceHistory_deviceID",
                table: "TxnTables",
                column: "deviceID",
                principalTable: "DeviceHistory",
                principalColumn: "deviceID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TxnTables_DeviceHistory_deviceID",
                table: "TxnTables");

            migrationBuilder.DropTable(
                name: "DeviceHistory");

            migrationBuilder.DropIndex(
                name: "IX_TxnTables_deviceID",
                table: "TxnTables");

            migrationBuilder.DropColumn(
                name: "deviceID",
                table: "TxnTables");

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "Users",
                type: "nvarchar(1)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char(1)");
        }
    }
}
