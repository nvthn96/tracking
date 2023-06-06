using System;
using System.Threading.Tasks;
using tracking.Model.Entity;

namespace tracking.Service.Interface
{
	public interface IFileExistsService
	{
		Task<bool> IsExists(Guid MD5);
		Task<FileExists> Add(FileExists fileExists);
	}
}
