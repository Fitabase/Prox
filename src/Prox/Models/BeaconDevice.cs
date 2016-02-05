using SQLite.Net.Attributes;

namespace Prox
{
	public class BeaconDevice : SqlModel
	{
		[Ignore]
		public string DeviceId { get { return string.Format ("{0}_{1}_{2}", Uuid, Major, Minor); } }

		public string Uuid { get; set; }

		public int Major { get; set; }

		public int Minor { get; set; }
	}
}