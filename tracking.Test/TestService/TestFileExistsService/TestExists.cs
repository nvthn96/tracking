using tracking.Database.FileDatabase;
using tracking.DI;
using tracking.Model.Entity;
using tracking.Service;
using tracking.Service.Interface;

namespace tracking.Test.TestService.TestFileExistsService
{
	[TestClass]
	public class TestExists
	{
		private FileUnitOfWork fileUnitOfWork = new();

		[TestInitialize]
		public void Initial()
		{
			DIContainer.Clear();
			DIContainer.Add<IFileContentService, FileContentService>();
			DIContainer.Add<IFileExistsService, FileExistsService>();
			DIContainer.Add<IFileHistoryService, FileHistoryService>();
			DIContainer.Add<FileUnitOfWork>();

			DIContainer.Build();

			fileUnitOfWork.FileExists.ClearTableAsync();
		}

		[TestMethod]
		public async Task File_exists()
		{
			var newFile = new FileExists()
			{
				MD5 = new Guid(),
				FileName = "FileTest1.txt",
				Timestamp = DateTime.UtcNow,
			};
			var fileResult = await fileUnitOfWork.FileExists.AddAsync(newFile);

			var fileExistsService = DIContainer.Get<IFileExistsService>();
			var isExists = await fileExistsService.IsExists(fileResult.MD5);
			Assert.AreEqual(true, isExists);
		}

		[TestMethod]
		public async Task File_not_exists()
		{
			var fileExistsService = DIContainer.Get<IFileExistsService>();
			var isExists = await fileExistsService.IsExists(new Guid());
			Assert.AreEqual(false, isExists);
		}
	}
}
