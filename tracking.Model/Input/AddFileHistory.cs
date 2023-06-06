using tracking.Model.Entity;

namespace tracking.Model.Input
{
	public class AddFileHistory
	{
		public FileHistory FileHitory { get; set; } = new FileHistory();
		public FileContent FileContent { get; set; } = new FileContent();
	}
}
