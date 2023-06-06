using tracking.Database.FileDatabase;
using tracking.DI;
using tracking.Model.Entity;
using tracking.Service.Interface;

namespace tracking.Service
{
	public class FileExistsService : IFileExistsService
	{
		private readonly FileUnitOfWork fileUnitOfWork;

		public FileExistsService()
		{
			fileUnitOfWork = DIContainer.Get<FileUnitOfWork>();
		}

		public async Task<bool> IsExists(Guid MD5)
		{
			var count = await fileUnitOfWork.FileExists.CountAsync(x => x.MD5 == MD5);
			return count == 1;
		}

		public async Task<FileExists> Add(FileExists fileExists)
		{
			var result = await fileUnitOfWork.FileExists.AddAsync(fileExists);
			return result;
		}
	}
}
