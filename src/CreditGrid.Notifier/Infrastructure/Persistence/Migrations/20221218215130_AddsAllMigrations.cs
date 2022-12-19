using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CreditGrid.Notifier.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddsAllMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerCreditInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    CreditNumber = table.Column<string>(type: "TEXT", nullable: false),
                    AmountDue = table.Column<decimal>(type: "TEXT", nullable: false),
                    DueDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCreditInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    RecipientsName = table.Column<string>(type: "TEXT", nullable: false),
                    RecipientsEmail = table.Column<string>(type: "TEXT", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", nullable: false),
                    BodyContent = table.Column<string>(type: "TEXT", nullable: false),
                    SentOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    ServerResponse = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TemplateType = table.Column<int>(type: "INTEGER", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerCreditInformation");

            migrationBuilder.DropTable(
                name: "SentMessages");

            migrationBuilder.DropTable(
                name: "Templates");
        }
    }
}
