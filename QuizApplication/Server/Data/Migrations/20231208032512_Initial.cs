using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QuizApplication.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaType",
                columns: table => new
                {
                    MediaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mediatype = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaType", x => x.MediaId);
                });

            migrationBuilder.CreateTable(
                name: "MediaFiles",
                columns: table => new
                {
                    MediaFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkMediaTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaFileName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSizeInBytes = table.Column<long>(type: "bigint", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaFiles", x => x.MediaFileId);
                    table.ForeignKey(
                        name: "FK_MediaFiles_MediaType_FkMediaTypeId",
                        column: x => x.FkMediaTypeId,
                        principalTable: "MediaType",
                        principalColumn: "MediaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FkFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionPath = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TimeLimit = table.Column<int>(type: "int", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_ApplicationUser_FkUserId",
                        column: x => x.FkUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Questions_MediaFiles_FkFileId",
                        column: x => x.FkFileId,
                        principalTable: "MediaFiles",
                        principalColumn: "MediaFileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_FkQuestionId",
                        column: x => x.FkQuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizItems",
                columns: table => new
                {
                    QuizItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FkUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FkQuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsScored = table.Column<bool>(type: "bit", nullable: false),
                    TimeSpent = table.Column<int>(type: "int", nullable: false),
                    Started_At = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizItems", x => x.QuizItemId);
                    table.ForeignKey(
                        name: "FK_QuizItems_ApplicationUser_FkUserId",
                        column: x => x.FkUserId,
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuizItems_Questions_FkQuestionId",
                        column: x => x.FkQuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId");
                });

            migrationBuilder.InsertData(
                table: "MediaType",
                columns: new[] { "MediaId", "Mediatype" },
                values: new object[,]
                {
                    { new Guid("9205858c-a471-4104-91a6-e8f331464fac"), "image" },
                    { new Guid("acc14f8b-8dc6-4db1-8714-2bd7b5065031"), "video" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Content",
                table: "Answers",
                column: "Content",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_FkQuestionId",
                table: "Answers",
                column: "FkQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_FkMediaTypeId",
                table: "MediaFiles",
                column: "FkMediaTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaFiles_MediaFileName",
                table: "MediaFiles",
                column: "MediaFileName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaType_Mediatype",
                table: "MediaType",
                column: "Mediatype",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_FkFileId",
                table: "Questions",
                column: "FkFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_FkUserId",
                table: "Questions",
                column: "FkUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuestionPath",
                table: "Questions",
                column: "QuestionPath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizItems_FkQuestionId",
                table: "QuizItems",
                column: "FkQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizItems_FkUserId",
                table: "QuizItems",
                column: "FkUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "QuizItems");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "MediaFiles");

            migrationBuilder.DropTable(
                name: "MediaType");
        }
    }
}
