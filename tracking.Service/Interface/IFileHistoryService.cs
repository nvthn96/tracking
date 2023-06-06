using tracking.Model.Entity;
using tracking.Model.Input;
using tracking.Model.Output;

namespace tracking.Service.Interface
{
	public interface IFileHistoryService
	{
		Pagging<FileHistory> GetPagging(Pagging<FileHistory> pagging);
		Task<IEnumerable<FileHistory>> GetSequenceById(Guid id, int count);
		IEnumerable<FileHistory> GetSequenceByStackId(Guid stackId, int count);
		Task<AddFileHistory> Add(AddFileHistory addFileHistory);
	}
}
