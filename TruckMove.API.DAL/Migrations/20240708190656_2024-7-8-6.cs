using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruckMove.API.DAL.Migrations
{
    public partial class _2024786 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Attachments_PermitsAndPlates",
            //    table: "Attachments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Attachments_Users_CreatedById",
            //    table: "Attachments");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Attachments_Users_UpdatedById",
            //    table: "Attachments");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Attachments",
            //    table: "Attachments");

            //migrationBuilder.RenameTable(
            //    name: "Attachments",
            //    newName: "Attachment");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Attachments_UpdatedById",
            //    table: "Attachment",
            //    newName: "IX_Attachment_UpdatedById");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Attachments_PermitAndPlateId",
            //    table: "Attachment",
            //    newName: "IX_Attachment_PermitAndPlateId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Attachments_CreatedById",
            //    table: "Attachment",
            //    newName: "IX_Attachment_CreatedById");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Url",
            //    table: "Attachment",
            //    type: "nvarchar(max)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nchar(10)",
            //    oldFixedLength: true,
            //    oldMaxLength: 10,
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<bool>(
            //    name: "IsActive",
            //    table: "Attachment",
            //    type: "bit",
            //    nullable: false,
            //    oldClrType: typeof(bool),
            //    oldType: "bit",
            //    oldDefaultValueSql: "(CONVERT([bit],(1)))");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Attachment",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Attachment",
            //    table: "Attachment",
            //    column: "Id");

            migrationBuilder.CreateTable(
                name: "TaskAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermitAndPlateId = table.Column<int>(type: "int", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "(CONVERT([bit],(1)))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskAttachments_PermitsAndPlates",
                        column: x => x.PermitAndPlateId,
                        principalTable: "PermitsAndPlates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskAttachments_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskAttachments_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachments_CreatedById",
                table: "TaskAttachments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachments_PermitAndPlateId",
                table: "TaskAttachments",
                column: "PermitAndPlateId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskAttachments_UpdatedById",
                table: "TaskAttachments",
                column: "UpdatedById");

            ////migrationBuilder.AddForeignKey(
            ////    name: "FK_Attachment_PermitsAndPlates_PermitAndPlateId",
            ////    table: "Attachment",
            ////    column: "PermitAndPlateId",
            ////    principalTable: "PermitsAndPlates",
            ////    principalColumn: "Id");

            ////migrationBuilder.AddForeignKey(
            ////    name: "FK_Attachment_Users_CreatedById",
            ////    table: "Attachment",
            ////    column: "CreatedById",
            ////    principalTable: "Users",
            ////    principalColumn: "Id");

            ////migrationBuilder.AddForeignKey(
            ////    name: "FK_Attachment_Users_UpdatedById",
            ////    table: "Attachment",
            ////    column: "UpdatedById",
            ////    principalTable: "Users",
            ////    principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Attachment_PermitsAndPlates_PermitAndPlateId",
            //    table: "Attachment");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Attachment_Users_CreatedById",
            //    table: "Attachment");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Attachment_Users_UpdatedById",
            //    table: "Attachment");

            //migrationBuilder.DropTable(
            //    name: "TaskAttachments");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Attachment",
            //    table: "Attachment");

            //migrationBuilder.RenameTable(
            //    name: "Attachment",
            //    newName: "Attachments");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Attachment_UpdatedById",
            //    table: "Attachments",
            //    newName: "IX_Attachments_UpdatedById");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Attachment_PermitAndPlateId",
            //    table: "Attachments",
            //    newName: "IX_Attachments_PermitAndPlateId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_Attachment_CreatedById",
            //    table: "Attachments",
            //    newName: "IX_Attachments_CreatedById");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Url",
            //    table: "Attachments",
            //    type: "nchar(10)",
            //    fixedLength: true,
            //    maxLength: 10,
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(max)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<bool>(
            //    name: "IsActive",
            //    table: "Attachments",
            //    type: "bit",
            //    nullable: false,
            //    defaultValueSql: "(CONVERT([bit],(1)))",
            //    oldClrType: typeof(bool),
            //    oldType: "bit");

            //migrationBuilder.AlterColumn<int>(
            //    name: "Id",
            //    table: "Attachments",
            //    type: "int",
            //    nullable: false,
            //    oldClrType: typeof(int),
            //    oldType: "int")
            //    .OldAnnotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_Attachments",
            //    table: "Attachments",
            //    column: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Attachments_PermitsAndPlates",
            //    table: "Attachments",
            //    column: "PermitAndPlateId",
            //    principalTable: "PermitsAndPlates",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Attachments_Users_CreatedById",
            //    table: "Attachments",
            //    column: "CreatedById",
            //    principalTable: "Users",
            //    principalColumn: "Id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Attachments_Users_UpdatedById",
            //    table: "Attachments",
            //    column: "UpdatedById",
            //    principalTable: "Users",
            //    principalColumn: "Id");
        }
    }
}
