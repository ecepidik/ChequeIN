using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ChequeIN.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LedgerAccounts",
                columns: table => new
                {
                    LedgerAccountID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Number = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccounts", x => x.LedgerAccountID);
                });

            migrationBuilder.CreateTable(
                name: "MailingAddress",
                columns: table => new
                {
                    MailingAddressID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    Line1 = table.Column<string>(type: "TEXT", nullable: false),
                    Line2 = table.Column<string>(type: "TEXT", nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: false),
                    Province = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailingAddress", x => x.MailingAddressID);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    UserProfileID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AuthenticationIdentifier = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserProfileID);
                });

            migrationBuilder.CreateTable(
                name: "AccountType",
                columns: table => new
                {
                    AccountTypeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LedgerAccountID = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    UserProfileID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.AccountTypeID);
                    table.ForeignKey(
                        name: "FK_AccountType_LedgerAccounts_LedgerAccountID",
                        column: x => x.LedgerAccountID,
                        principalTable: "LedgerAccounts",
                        principalColumn: "LedgerAccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountType_UserProfiles_UserProfileID",
                        column: x => x.UserProfileID,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChequeReqs",
                columns: table => new
                {
                    ChequeReqID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ApprovedBy = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    FreeFood = table.Column<bool>(type: "INTEGER", nullable: false),
                    GST = table.Column<float>(type: "REAL", nullable: false),
                    HST = table.Column<float>(type: "REAL", nullable: false),
                    LedgerAccountID = table.Column<int>(type: "INTEGER", nullable: false),
                    MailingAddressID = table.Column<int>(type: "INTEGER", nullable: true),
                    OnlinePurchases = table.Column<bool>(type: "INTEGER", nullable: false),
                    PST = table.Column<float>(type: "REAL", nullable: false),
                    PayeeName = table.Column<string>(type: "TEXT", nullable: false),
                    PreTax = table.Column<float>(type: "REAL", nullable: false),
                    ToBeMailed = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserProfileID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequeReqs", x => x.ChequeReqID);
                    table.ForeignKey(
                        name: "FK_ChequeReqs_LedgerAccounts_LedgerAccountID",
                        column: x => x.LedgerAccountID,
                        principalTable: "LedgerAccounts",
                        principalColumn: "LedgerAccountID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChequeReqs_MailingAddress_MailingAddressID",
                        column: x => x.MailingAddressID,
                        principalTable: "MailingAddress",
                        principalColumn: "MailingAddressID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChequeReqs_UserProfiles_UserProfileID",
                        column: x => x.UserProfileID,
                        principalTable: "UserProfiles",
                        principalColumn: "UserProfileID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdministratorApprover = table.Column<string>(type: "TEXT", nullable: true),
                    ChequeReqID = table.Column<int>(type: "INTEGER", nullable: true),
                    Feedback = table.Column<string>(type: "TEXT", nullable: true),
                    SelectedStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                    table.ForeignKey(
                        name: "FK_Status_ChequeReqs_ChequeReqID",
                        column: x => x.ChequeReqID,
                        principalTable: "ChequeReqs",
                        principalColumn: "ChequeReqID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupportingDocument",
                columns: table => new
                {
                    SupportingDocumentID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ChequeReqID = table.Column<int>(type: "INTEGER", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    FileIdentifier = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportingDocument", x => x.SupportingDocumentID);
                    table.ForeignKey(
                        name: "FK_SupportingDocument_ChequeReqs_ChequeReqID",
                        column: x => x.ChequeReqID,
                        principalTable: "ChequeReqs",
                        principalColumn: "ChequeReqID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountType_LedgerAccountID",
                table: "AccountType",
                column: "LedgerAccountID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountType_UserProfileID",
                table: "AccountType",
                column: "UserProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeReqs_LedgerAccountID",
                table: "ChequeReqs",
                column: "LedgerAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeReqs_MailingAddressID",
                table: "ChequeReqs",
                column: "MailingAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeReqs_UserProfileID",
                table: "ChequeReqs",
                column: "UserProfileID");

            migrationBuilder.CreateIndex(
                name: "IX_Status_ChequeReqID",
                table: "Status",
                column: "ChequeReqID");

            migrationBuilder.CreateIndex(
                name: "IX_SupportingDocument_ChequeReqID",
                table: "SupportingDocument",
                column: "ChequeReqID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "SupportingDocument");

            migrationBuilder.DropTable(
                name: "ChequeReqs");

            migrationBuilder.DropTable(
                name: "LedgerAccounts");

            migrationBuilder.DropTable(
                name: "MailingAddress");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
