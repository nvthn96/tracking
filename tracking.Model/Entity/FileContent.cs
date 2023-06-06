namespace tracking.Model.Entity
{
	public class FileContent : BaseEntity
	{
		public Guid MD5 { get; set; }
		public byte[]? Content { get; set; }
	}
}
