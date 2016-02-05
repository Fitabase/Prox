using SQLiteNetExtensions.Attributes;

namespace Prox
{
	public class Beacon : SqlModel
	{
		public string Name { get; set; }

		[ManyToOne]
		public Category Category { get; set; }

		[ForeignKey (typeof(Category))]
		public int? CategoryId { get; set; }

		[OneToOne (CascadeOperations = CascadeOperation.All)]
		public BeaconDevice Device { get; set; }

		[ForeignKey (typeof(BeaconDevice))]
		public int? DeviceId { get; set; }
	}
}