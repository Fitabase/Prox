namespace Prox
{
	public class BeaconsExportViewModel
	{
		public string Json { get; set; }

		public BeaconsExportViewModel ()
		{
			Json = App.Settings.Beacons.Export ();
		}
	}
}