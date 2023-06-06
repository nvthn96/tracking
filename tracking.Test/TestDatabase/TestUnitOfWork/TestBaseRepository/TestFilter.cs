using System.Text;
using tracking.Database.FileDatabase;
using tracking.DI;
using tracking.Model.Entity;
using tracking.Service;
using tracking.Service.Interface;

namespace tracking.Test.TestDatabase.TestUnitOfWork.TestBaseRepository
{
	[TestClass]
	public class TestFilter
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
		public async Task Filter()
		{
			var files = fileUnitOfWork.FileContent.GetAll();
			var fileCount = 0;
			foreach (var file in files)
			{
				if (fileCount % 10 == 0)
				{
					await fileUnitOfWork.FileContent.DeleteAsync(file.Id);
				}
				fileCount++;
			}

			var filted = fileUnitOfWork.FileContent.Filter(x => x.IsDeleted == true);
			Assert.AreEqual(numberOfItems / 10, filted.Count());
			var filtedCount = 0;
			foreach (var file in filted)
			{
				Assert.IsTrue(file.Content != null);
				Assert.AreEqual("file content " + filtedCount, Encoding.UTF8.GetString(file.Content));
				filtedCount += 10;
			}
		}
	}
}
