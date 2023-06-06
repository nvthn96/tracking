using tracking.Database.FileDatabase;
using tracking.DI;
using tracking.Model.Entity;
using tracking.Model.Input;
using tracking.Model.Output;
using tracking.Service.Interface;

namespace tracking.Service
{
	public class FileHistoryService : IFileHistoryService
	{
		private readonly FileUnitOfWork fileUnitOfWork;
		public FileHistoryService()
		{
			fileUnitOfWork = DIContainer.Get<FileUnitOfWork>();
		}

		public Pagging<FileHistory> GetPagging(Pagging<FileHistory> pagging)
		{
			var result =
				from filestackId in fileUnitOfWork.FileHistory.GetAll()
					.OrderByDescending(e => e.Timestamp)
					.Select(x => x.StackId)
					.Distinct()
					.Skip(pagging.Page * pagging.Size)
					.Take(pagging.Size)
				from filehistory in fileUnitOfWork.FileHistory.GetAll()
					.Where(x => x.StackId == filestackId)
					.OrderByDescending(e => e.Timestamp)
					.Take(1)
				select filehistory;

			pagging.Items = result;
			return pagging;
		}

		public async Task<IEnumerable<FileHistory>> GetSequenceById(Guid id, int count)
		{
			var fileHistory = await fileUnitOfWork.FileHistory.FindAsync(x => x.Id == id);
			if (fileHistory == null) { return Enumerable.Empty<FileHistory>(); }

			var result = fileUnitOfWork.FileHistory
				.Filter(x => x.StackId == fileHistory.StackId)
				.OrderByDescending(x => x.Timestamp).Take(count).ToList();

			return result;
		}

		public IEnumerable<FileHistory> GetSequenceByStackId(Guid stackId, int count)
		{
			var result = fileUnitOfWork.FileHistory
				.Filter(x => x.StackId == stackId)
				.OrderByDescending(x => x.Timestamp).Take(count).ToList();

			return result;
		}

		public async Task<AddFileHistory> Add(AddFileHistory addFileHistory)
		{
			addFileHistory.FileHitory.StackId = addFileHistory.FileHitory.StackId ?? new Guid();

			async Task<bool> addFunc(FileUnitOfWork unitOfWork)
			{
				var fileContent = await unitOfWork.FileContent.AddAsync(addFileHistory.FileContent);

				addFileHistory.FileHitory.ContentId = fileContent.Id;
				await unitOfWork.FileHistory.AddAsync(addFileHistory.FileHitory);

				return true;
			}

			await fileUnitOfWork.RunTransaction(fileUnitOfWork, addFunc);

			return addFileHistory;
		}
	}
}
