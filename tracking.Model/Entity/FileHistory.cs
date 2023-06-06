namespace tracking.Model.Entity
{
	public class FileHistory : BaseEntity
	{
		public Guid MD5 { get; set; }
		public string? FileName { get; set; }
		public DateTime Timestamp { get; set; }
		public Guid? StackId { get; set; }
		public Guid? ContentId { get; set; }
	}
}
