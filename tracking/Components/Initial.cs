using tracking.Database.FileDatabase;
using tracking.DI;
using tracking.Service;
using tracking.Service.Interface;

namespace tracking.Components
{
	public class Initial
	{
		public static void App()
		{
			InitialDIContainer();
		}

		public static void InitialDIContainer()
		{
			DIContainer.Add<IFileContentService, FileContentService>();
			DIContainer.Add<IFileExistsService, FileExistsService>();
			DIContainer.Add<IFileHistoryService, FileHistoryService>();
			DIContainer.Add<FileUnitOfWork>();

			DIContainer.Build();
		}
	}
}
