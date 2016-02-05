using System;
using SQLiteNetExtensions.Attributes;

namespace Prox
{
	public class BeaconEvent : SqlModel
	{
		public DateTime Time { get; set; }

		public EventType Type { get; set; }

		[ManyToOne]
		public Beacon Beacon { get; set; }

		[ForeignKey (typeof(Beacon))]
		public int? BeaconId { get; set; }
	}

	public enum EventType
	{
		Enter,
		Exit
	}
}