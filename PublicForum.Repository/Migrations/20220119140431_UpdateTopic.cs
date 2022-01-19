using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PublicForum.Repository.Migrations
{
    public partial class UpdateTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Topics",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Topics",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Topics");
        }
    }
}
