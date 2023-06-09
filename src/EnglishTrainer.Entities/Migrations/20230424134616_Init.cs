﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnglishTrainer.Entities.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dictionary",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    translations = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dictionary", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "IrregularVerbs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    infinitive = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    past_simple = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    past_participle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    short_translate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IrregularVerbs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Examples",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    english_sentence = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    russian_sentence = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    word_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examples", x => x.id);
                    table.ForeignKey(
                        name: "FK_Examples_Dictionary_word_id",
                        column: x => x.word_id,
                        principalTable: "Dictionary",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Examples_word_id",
                table: "Examples",
                column: "word_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Examples");

            migrationBuilder.DropTable(
                name: "IrregularVerbs");

            migrationBuilder.DropTable(
                name: "Dictionary");
        }
    }
}
