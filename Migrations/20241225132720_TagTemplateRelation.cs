using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forms.Migrations
{
    /// <inheritdoc />
    public partial class TagTemplateRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Templates_TemplateId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TemplateId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "TagTemplate",
                columns: table => new
                {
                    TagsId = table.Column<string>(type: "text", nullable: false),
                    TemplatesId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTemplate", x => new { x.TagsId, x.TemplatesId });
                    table.ForeignKey(
                        name: "FK_TagTemplate_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTemplate_Templates_TemplatesId",
                        column: x => x.TemplatesId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagTemplate_TemplatesId",
                table: "TagTemplate",
                column: "TemplatesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagTemplate");

            migrationBuilder.AddColumn<string>(
                name: "TemplateId",
                table: "Tags",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TemplateId",
                table: "Tags",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Templates_TemplateId",
                table: "Tags",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id");
        }
    }
}
