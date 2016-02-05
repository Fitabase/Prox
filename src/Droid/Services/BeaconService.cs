using System;
using Xamarin.Forms;
using Prox.Droid;
using System.Collections.Generic;

[assembly:Dependency (typeof(BeaconService))]

namespace Prox.Droid
{
	public class BeaconService : IBeaconService
	{
		public event EventHandler NoAuth;
		public event EventHandler<BeaconEventArgs> NearestFound;
		public event EventHandler<BeaconEventArgs> Entered;
		public event EventHandler<BeaconEventArgs> Left;

		public void Start (IEnumerable<BeaconDevice> beacons)
		{
			
		}

		public void StartFindNearest ()
		{
			
		}

		public void StopFindNearest ()
		{
			
		}

		public void AddBeaconToMonitoring (BeaconDevice beacon)
		{
			
		}

		public void RemoveBeaconFromMonitoring (BeaconDevice beacon)
		{
			
		}
	}
}