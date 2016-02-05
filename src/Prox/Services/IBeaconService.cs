using System;
using System.Collections.Generic;

namespace Prox
{
	public interface IBeaconService
	{
		event EventHandler NoAuth;
		event EventHandler<BeaconEventArgs> NearestFound;
		event EventHandler<BeaconEventArgs> Entered;
		event EventHandler<BeaconEventArgs> Left;

		void Start (IEnumerable<BeaconDevice> beacons);

		void StartFindNearest ();

		void StopFindNearest ();

		void AddBeaconToMonitoring (BeaconDevice beacon);

		void RemoveBeaconFromMonitoring (BeaconDevice beacon);
	}

	public class BeaconEventArgs : EventArgs
	{
		public BeaconDevice BeaconDevice { get; set; }
	}
}