namespace tracking.Model.Entity
{
	public class FileExists : BaseEntity
	{
		public Guid MD5 { get; set; }
		public string? FileName { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
