using tracking.Database.FileDatabase;
using tracking.DI;
using tracking.Model.Entity;
using tracking.Service.Interface;

namespace tracking.Service
{
	public class FileContentService : IFileContentService
	{
		private readonly FileUnitOfWork fileUnitOfWork;

		public FileContentService()
		{
			fileUnitOfWork = DIContainer.Get<FileUnitOfWork>();
		}

		public async Task<FileContent?> Find(Guid id)
		{
			var result = await fileUnitOfWork.FileContent.FindAsync(x => x.Id == id);
			return result;
		}

		public async Task<FileContent> Add(FileContent fileContent)
		{
			var result = await fileUnitOfWork.FileContent.AddAsync(fileContent);
			return result;
		}
	}
}
