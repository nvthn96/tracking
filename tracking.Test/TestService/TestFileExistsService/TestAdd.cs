using tracking.Database.FileDatabase;
using tracking.DI;
using tracking.Model.Entity;
using tracking.Service;
using tracking.Service.Interface;

namespace tracking.Test.TestService.TestFileExistsService
{
	[TestClass]
	public class TestAdd
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
		public async Task Add_a_file()
		{
			var newFile = new FileExists()
			{
				MD5 = new Guid(),
				FileName = "FileTest1.txt",
				Timestamp = DateTime.UtcNow,
			};

			var fileExistsService = DIContainer.Get<IFileExistsService>();
			var fileResult = await fileExistsService.Add(newFile);

			Assert.IsNotNull(fileResult);
			Assert.AreEqual(fileResult.MD5, newFile.MD5);
			Assert.AreEqual(fileResult.FileName, newFile.FileName);
			Assert.AreEqual(fileResult.Timestamp, newFile.Timestamp);

			var file = await fileUnitOfWork.FileExists.FindAsync(x => x.MD5 == newFile.MD5);
			Assert.IsNotNull(file);
			Assert.AreEqual(file.MD5, fileResult.MD5);
			Assert.AreEqual(file.FileName, fileResult.FileName);
			Assert.AreEqual(file.Timestamp, fileResult.Timestamp);
		}
	}
}
