using System.Text;
using tracking.Components;
using tracking.DI;
using tracking.Model.Entity;
using tracking.Service.Interface;

namespace tracking
{
	class Program
	{
		static async Task Main(string[] args)
		{
			Initial.App();

			var content = "file content";

			var fileContentService = DIContainer.Get<IFileContentService>();
			var newFile = new FileContent()
			{
				MD5 = new Guid(),
				Content = Encoding.UTF8.GetBytes(content),
			};

			var result = await fileContentService.Add(newFile);

			var getFile = await fileContentService.Find(result.Id);
			var getContent = Encoding.UTF8.GetString(getFile?.Content ?? new byte[0]);

			Console.WriteLine(content == getContent);
		}
	}
}
