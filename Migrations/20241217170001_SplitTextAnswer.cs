using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forms.Migrations
{
    /// <inheritdoc />
    public partial class SplitTextAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TextAnswers");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "MultiLineAnswer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: false),
                    FormId = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiLineAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiLineAnswer_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MultiLineAnswer_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SingleLineAnswer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: false),
                    FormId = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleLineAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SingleLineAnswer_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SingleLineAnswer_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultiLineAnswer_FormId",
                table: "MultiLineAnswer",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiLineAnswer_QuestionId",
                table: "MultiLineAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleLineAnswer_FormId",
                table: "SingleLineAnswer",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_SingleLineAnswer_QuestionId",
                table: "SingleLineAnswer",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultiLineAnswer");

            migrationBuilder.DropTable(
                name: "SingleLineAnswer");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Questions");

            migrationBuilder.CreateTable(
                name: "TextAnswers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FormId = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<string>(type: "text", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TextAnswers_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TextAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TextAnswers_FormId",
                table: "TextAnswers",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_TextAnswers_QuestionId",
                table: "TextAnswers",
                column: "QuestionId");
        }
    }
}
