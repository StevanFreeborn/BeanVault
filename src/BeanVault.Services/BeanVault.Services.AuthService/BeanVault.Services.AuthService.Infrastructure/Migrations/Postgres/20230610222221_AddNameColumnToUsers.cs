﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanVault.Services.AuthService.Infrastructure.Migrations.Postgres
{
  /// <inheritdoc />
  public partial class AddNameColumnToUsers : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.AddColumn<string>(
          name: "Name",
          table: "AspNetUsers",
          type: "text",
          nullable: false,
          defaultValue: "");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropColumn(
          name: "Name",
          table: "AspNetUsers");
    }
  }
}