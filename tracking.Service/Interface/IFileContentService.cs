using System;
using System.Threading.Tasks;
using tracking.Model.Entity;

namespace tracking.Service.Interface
{
	public interface IFileContentService
	{
		Task<FileContent?> Find(Guid id);
		Task<FileContent> Add(FileContent fileContent);
	}
}
