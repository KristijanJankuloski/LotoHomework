using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotoHomework.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addingnumbermatches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumberMatch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    CombinationId = table.Column<int>(type: "int", nullable: false),
                    Ammount = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberMatch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NumberMatch_Combinations_CombinationId",
                        column: x => x.CombinationId,
                        principalTable: "Combinations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_NumberMatch_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumberMatch_CombinationId",
                table: "NumberMatch",
                column: "CombinationId");

            migrationBuilder.CreateIndex(
                name: "IX_NumberMatch_SessionId",
                table: "NumberMatch",
                column: "SessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumberMatch");
        }
    }
}
