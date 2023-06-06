using tracking.Database.UnitOfWork;
using tracking.Model.Entity;

namespace tracking.Database.FileDatabase
{
	public class FileUnitOfWork : BaseUnitOfWork<FileContext>
	{
		public readonly IRepository<FileExists> FileExists;
		public readonly IRepository<FileContent> FileContent;
		public readonly IRepository<FileHistory> FileHistory;

		public FileUnitOfWork()
		{
			FileExists = new BaseRepository<FileExists>(_context);
			FileContent = new BaseRepository<FileContent>(_context);
			FileHistory = new BaseRepository<FileHistory>(_context);
		}
	}
}
