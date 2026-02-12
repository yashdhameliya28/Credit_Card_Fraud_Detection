using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Credit_Card_Fraud_Detection.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    joinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    job = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    city_pop = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "TxnTables",
                columns: table => new
                {
                    txnID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    timeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    modelRiskScore = table.Column<double>(type: "float", nullable: true),
                    modelPredicted = table.Column<bool>(type: "bit", nullable: true),
                    isFraud = table.Column<bool>(type: "bit", nullable: true),
                    userID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TxnTables", x => x.txnID);
                    table.ForeignKey(
                        name: "FK_TxnTables_Users_userID",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FraudAlerts",
                columns: table => new
                {
                    alertID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    riskScore = table.Column<double>(type: "float", nullable: false),
                    decision = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    alertStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    txnID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FraudAlerts", x => x.alertID);
                    table.ForeignKey(
                        name: "FK_FraudAlerts_TxnTables_txnID",
                        column: x => x.txnID,
                        principalTable: "TxnTables",
                        principalColumn: "txnID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FraudAlerts_txnID",
                table: "FraudAlerts",
                column: "txnID");

            migrationBuilder.CreateIndex(
                name: "IX_TxnTables_userID",
                table: "TxnTables",
                column: "userID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FraudAlerts");

            migrationBuilder.DropTable(
                name: "TxnTables");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
