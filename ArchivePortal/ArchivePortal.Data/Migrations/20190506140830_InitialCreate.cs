using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArchivePortal.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_PipelineArchives",
                columns: table => new
                {
                    PipelineArchivesId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MessageId = table.Column<Guid>(nullable: false),
                    Body = table.Column<byte[]>(nullable: true),
                    Property = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: false),
                    IsCompressed = table.Column<bool>(nullable: false),
                    CompressedSize = table.Column<long>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    InterchangeId = table.Column<Guid>(nullable: false),
                    ReceiveLocation = table.Column<string>(nullable: true),
                    SendPort = table.Column<string>(nullable: true),
                    ReceivedFilename = table.Column<string>(nullable: true),
                    BodyIntro = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PipelineArchives", x => x.PipelineArchivesId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_PipelineArchives");
        }
    }
}
