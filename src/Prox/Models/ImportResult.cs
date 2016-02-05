namespace Prox
{
	public class ImportResult
	{
		public bool IsSuccess { get; set; }

		public Beacon[] Imported { get; set; }

		public Beacon[] Updated { get; set; }

		public Beacon[] Skipped { get; set; }
	}
}