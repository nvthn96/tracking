using System.Text;
using tracking.Database.FileDatabase;
using tracking.DI;
using tracking.Model.Entity;
using tracking.Service;
using tracking.Service.Interface;

namespace tracking.Test.TestDatabase.TestUnitOfWork.TestBaseUnitOfWork
{
	[TestClass]
	public class TestRunTransaction
	{
		private FileUnitOfWork fileUnitOfWork = new();

		[TestInitialize]
		public async Task Initial()
		{
			DIContainer.Clear();
			DIContainer.Add<IFileContentService, FileContentService>();
			DIContainer.Add<IFileExistsService, FileExistsService>();
			DIContainer.Add<IFileHistoryService, FileHistoryService>();
			DIContainer.Add<FileUnitOfWork>();

			DIContainer.Build();

			await fileUnitOfWork.FileContent.ClearTableAsync();
			await fileUnitOfWork.FileHistory.ClearTableAsync();
		}

		[TestMethod]
		public async Task Transaction_success()
		{
			var fileContent = new FileContent()
			{
				MD5 = new Guid(),
				Content = Encoding.UTF8.GetBytes("file content"),
			};

			var fileHistory = new FileHistory()
			{
				MD5 = fileContent.MD5,
				FileName = "file name 1",
				StackId = new Guid(),
			};

			FileContent contentResult = new();
			FileHistory historyResult = new();

			async Task<bool> func(FileUnitOfWork unitOfWork)
			{
				contentResult = await unitOfWork.FileContent.AddAsync(fileContent);
				historyResult = await unitOfWork.FileHistory.AddAsync(fileHistory);
				return true;
			}

			await fileUnitOfWork.RunTransaction(fileUnitOfWork, func);

			Assert.AreEqual(fileContent.MD5, contentResult.MD5);
			Assert.IsTrue(fileContent.Content!.SequenceEqual(contentResult.Content!));

			Assert.AreEqual(fileHistory.MD5, historyResult.MD5);
			Assert.AreEqual(fileHistory.FileName, historyResult.FileName);

			var fileContentFinded = await fileUnitOfWork.FileContent.FindAsync(x => x.MD5 == fileContent.MD5);
			var fileHistoryFinded = await fileUnitOfWork.FileHistory.FindAsync(x => x.MD5 == fileHistory.MD5);

			Assert.IsNotNull(fileContentFinded);
			Assert.IsNotNull(fileHistoryFinded);

			Assert.AreEqual(fileContent.MD5, fileContentFinded.MD5);
			Assert.IsTrue(fileContent.Content!.SequenceEqual(fileContentFinded.Content!));

			Assert.AreEqual(fileHistory.MD5, fileHistoryFinded.MD5);
			Assert.AreEqual(fileHistory.FileName, fileHistoryFinded.FileName);
		}

		[TestMethod]
		public async Task Transaction_throw()
		{
			var fileContent = new FileContent()
			{
				MD5 = new Guid(),
				Content = Encoding.UTF8.GetBytes("file content"),
			};

			var fileHistory = new FileHistory()
			{
				MD5 = fileContent.MD5,
				FileName = "file name 1",
				StackId = new Guid(),
			};

			FileContent contentResult = new();
			FileHistory historyResult = new();

			async Task<bool> func(FileUnitOfWork unitOfWork)
			{
				await unitOfWork.FileContent.AddAsync(fileContent);
				throw new Exception();
				//await unitOfWork.FileHistory.AddAsync(fileHistory);
				//return true;
			}

			await fileUnitOfWork.RunTransaction(fileUnitOfWork, func);

			var fileContentFinded = await fileUnitOfWork.FileContent.FindAsync(x => x.MD5 == fileContent.MD5);
			var fileHistoryFinded = await fileUnitOfWork.FileHistory.FindAsync(x => x.MD5 == fileHistory.MD5);

			Assert.IsNull(fileContentFinded);
			Assert.IsNull(fileHistoryFinded);

			var contentCount = await fileUnitOfWork.FileContent.CountAsync();
			var historyCount = await fileUnitOfWork.FileHistory.CountAsync();

			Assert.AreEqual(0, contentCount);
			Assert.AreEqual(0, historyCount);
		}
	}
}
