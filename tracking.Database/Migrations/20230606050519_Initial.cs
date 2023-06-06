using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tracking.Database.Migrations
{
	/// <inheritdoc />
	public partial class Initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "FileContent",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "BLOB", nullable: false),
					MD5 = table.Column<Guid>(type: "BLOB", nullable: false),
					Content = table.Column<byte[]>(type: "BLOB", nullable: true),
					CreatedBy = table.Column<Guid>(type: "BLOB", nullable: true),
					CreatedOn = table.Column<DateTime>(type: "DATETIME", nullable: true),
					ModifiedBy = table.Column<Guid>(type: "BLOB", nullable: true),
					ModifiedOn = table.Column<DateTime>(type: "DATETIME", nullable: true),
					ModifiedReason = table.Column<string>(type: "TEXT", nullable: true),
					IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
					DeletedBy = table.Column<Guid>(type: "BLOB", nullable: true),
					DeletedOn = table.Column<DateTime>(type: "DATETIME", nullable: true),
					DeletedReason = table.Column<string>(type: "TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_FileContent", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "FileExists",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "BLOB", nullable: false),
					MD5 = table.Column<Guid>(type: "BLOB", nullable: false),
					FileName = table.Column<string>(type: "TEXT", nullable: true),
					Timestamp = table.Column<DateTime>(type: "DATETIME", nullable: false),
					CreatedBy = table.Column<Guid>(type: "BLOB", nullable: true),
					CreatedOn = table.Column<DateTime>(type: "DATETIME", nullable: true),
					ModifiedBy = table.Column<Guid>(type: "BLOB", nullable: true),
					ModifiedOn = table.Column<DateTime>(type: "DATETIME", nullable: true),
					ModifiedReason = table.Column<string>(type: "TEXT", nullable: true),
					IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
					DeletedBy = table.Column<Guid>(type: "BLOB", nullable: true),
					DeletedOn = table.Column<DateTime>(type: "DATETIME", nullable: true),
					DeletedReason = table.Column<string>(type: "TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_FileExists", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "FileHistory",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "BLOB", nullable: false),
					MD5 = table.Column<Guid>(type: "BLOB", nullable: false),
					FileName = table.Column<string>(type: "TEXT", nullable: true),
					Timestamp = table.Column<DateTime>(type: "DATETIME", nullable: false),
					StackId = table.Column<Guid>(type: "BLOB", nullable: true),
					ContentId = table.Column<Guid>(type: "BLOB", nullable: true),
					CreatedBy = table.Column<Guid>(type: "BLOB", nullable: true),
					CreatedOn = table.Column<DateTime>(type: "DATETIME", nullable: true),
					ModifiedBy = table.Column<Guid>(type: "BLOB", nullable: true),
					ModifiedOn = table.Column<DateTime>(type: "DATETIME", nullable: true),
					ModifiedReason = table.Column<string>(type: "TEXT", nullable: true),
					IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
					DeletedBy = table.Column<Guid>(type: "BLOB", nullable: true),
					DeletedOn = table.Column<DateTime>(type: "DATETIME", nullable: true),
					DeletedReason = table.Column<string>(type: "TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_FileHistory", x => x.Id);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "FileContent");

			migrationBuilder.DropTable(
				name: "FileExists");

			migrationBuilder.DropTable(
				name: "FileHistory");
		}
	}
}
