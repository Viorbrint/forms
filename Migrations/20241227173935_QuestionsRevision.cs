using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forms.Migrations
{
    /// <inheritdoc />
    public partial class QuestionsRevision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_QuestionTypes_QuestionTypeId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleLineAnswer_Forms_FormId",
                table: "SingleLineAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_SingleLineAnswer_Questions_QuestionId",
                table: "SingleLineAnswer");

            migrationBuilder.DropTable(
                name: "BooleanAnswers");

            migrationBuilder.DropTable(
                name: "MultiLineAnswer");

            migrationBuilder.DropTable(
                name: "NumberAnswers");

            migrationBuilder.DropTable(
                name: "QuestionTypes");

            migrationBuilder.DropIndex(
                name: "IX_Questions_QuestionTypeId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SingleLineAnswer",
                table: "SingleLineAnswer");

            migrationBuilder.DropColumn(
                name: "QuestionTypeId",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "SingleLineAnswer",
                newName: "Answer");

            migrationBuilder.RenameIndex(
                name: "IX_SingleLineAnswer_QuestionId",
                table: "Answer",
                newName: "IX_Answer_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_SingleLineAnswer_FormId",
                table: "Answer",
                newName: "IX_Answer_FormId");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AnswerText",
                table: "Answer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "AnswerBoolean",
                table: "Answer",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AnswerNumber",
                table: "Answer",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Answer",
                type: "character varying(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SingleLineAnswer_AnswerText",
                table: "Answer",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Answer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answer",
                table: "Answer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Forms_FormId",
                table: "Answer",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Questions_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Forms_FormId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Questions_QuestionId",
                table: "Answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answer",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AnswerBoolean",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "AnswerNumber",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "SingleLineAnswer_AnswerText",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Answer");

            migrationBuilder.RenameTable(
                name: "Answer",
                newName: "SingleLineAnswer");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_QuestionId",
                table: "SingleLineAnswer",
                newName: "IX_SingleLineAnswer_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_FormId",
                table: "SingleLineAnswer",
                newName: "IX_SingleLineAnswer_FormId");

            migrationBuilder.AddColumn<string>(
                name: "QuestionTypeId",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "AnswerText",
                table: "SingleLineAnswer",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_SingleLineAnswer",
                table: "SingleLineAnswer",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BooleanAnswers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FormId = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<string>(type: "text", nullable: false),
                    AnswerBoolean = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooleanAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooleanAnswers_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooleanAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultiLineAnswer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FormId = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<string>(type: "text", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: false)
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
                name: "NumberAnswers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FormId = table.Column<string>(type: "text", nullable: false),
                    QuestionId = table.Column<string>(type: "text", nullable: false),
                    AnswerNumber = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumberAnswers_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NumberAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    QuestionTypeName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionTypeId",
                table: "Questions",
                column: "QuestionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_BooleanAnswers_FormId",
                table: "BooleanAnswers",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_BooleanAnswers_QuestionId",
                table: "BooleanAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiLineAnswer_FormId",
                table: "MultiLineAnswer",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiLineAnswer_QuestionId",
                table: "MultiLineAnswer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_NumberAnswers_FormId",
                table: "NumberAnswers",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_NumberAnswers_QuestionId",
                table: "NumberAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTypes_QuestionTypeName",
                table: "QuestionTypes",
                column: "QuestionTypeName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_QuestionTypes_QuestionTypeId",
                table: "Questions",
                column: "QuestionTypeId",
                principalTable: "QuestionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleLineAnswer_Forms_FormId",
                table: "SingleLineAnswer",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SingleLineAnswer_Questions_QuestionId",
                table: "SingleLineAnswer",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
