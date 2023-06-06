namespace tracking.Model.Output
{
	public class Pagging<T> where T : class
	{
		public int Page { get; set; } = 1;
		public int Size { get; set; } = 10;
		public int Total { get; set; } = 0;
		public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
	}
}
