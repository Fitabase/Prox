using Newtonsoft.Json;

namespace Prox
{
	public class ExportBeacon
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("uuid")]
		public string UUID { get; set; }

		[JsonProperty("major")]
		public int Major { get; set; }

		[JsonProperty("minor")]
		public int Minor { get; set; }

		[JsonProperty("category")]
		public string Category { get; set; }
	}
}