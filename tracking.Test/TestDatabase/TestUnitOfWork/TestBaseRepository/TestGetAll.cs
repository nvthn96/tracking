using System.Text;
using tracking.Database.FileDatabase;
using tracking.DI;
using tracking.Model.Entity;
using tracking.Service;
using tracking.Service.Interface;

namespace tracking.Test.TestDatabase.TestUnitOfWork.TestBaseRepository
{
	[TestClass]
	public class TestGetAll
	{
		private FileUnitOfWork fileUnitOfWork = new();
		private readonly int numberOfItems = 100;

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

			for (var i = 0; i < numberOfItems; i++)
			{
				var newFile = new FileContent()
				{
					MD5 = new Guid(),
					Content = Encoding.UTF8.GetBytes("file content " + i),
				};
				await fileUnitOfWork.FileContent.AddAsync(newFile);
			}
		}

		[TestMethod]
		public void Get_all()
		{
			var files = fileUnitOfWork.FileContent.GetAll();
			Assert.AreEqual(numberOfItems, files.Count());

			var fileCount = 0;
			foreach (var file in files)
			{
				Assert.IsTrue(file.Content != null);
				Assert.AreEqual("file content " + fileCount, Encoding.UTF8.GetString(file.Content));
				fileCount++;
			}
		}
	}
}
