using Microsoft.EntityFrameworkCore;
using tracking.Model.Entity;

namespace tracking.Database.FileDatabase
{
	public class FileContext : DbContext
	{
		public FileContext() { }
		public FileContext(DbContextOptions<FileContext> options) : base(options) { }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseSqlite(@"Data Source=.\FileDB.db");

		public DbSet<FileContent> FileContent { get; set; }
		public DbSet<FileExists> FileExists { get; set; }
		public DbSet<FileHistory> FileHistory { get; set; }
	}
}
