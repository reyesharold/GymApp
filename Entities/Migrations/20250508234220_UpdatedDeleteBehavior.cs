using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Membership_MembershipId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Member_UserId",
                table: "Payment");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Membership_MembershipId",
                table: "Member",
                column: "MembershipId",
                principalTable: "Membership",
                principalColumn: "MembershipId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Member_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "Member",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Membership_MembershipId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Payment_Member_UserId",
                table: "Payment");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Payment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Membership_MembershipId",
                table: "Member",
                column: "MembershipId",
                principalTable: "Membership",
                principalColumn: "MembershipId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Member_UserId",
                table: "Payment",
                column: "UserId",
                principalTable: "Member",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
