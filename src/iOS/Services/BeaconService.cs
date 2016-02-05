using System;
using Foundation;
using CoreLocation;
using Xamarin.Forms;
using Prox.iOS;
using System.Collections.Generic;
using System.Linq;

[assembly:Dependency (typeof(BeaconService))]

namespace Prox.iOS
{
	public class BeaconService : CLLocationManagerDelegate, IBeaconService
	{
		public event EventHandler NoAuth = delegate {};
		public event EventHandler<BeaconEventArgs> NearestFound = delegate {};
		public event EventHandler<BeaconEventArgs> Entered = delegate{};
		public event EventHandler<BeaconEventArgs> Left = delegate{};


		private const string uuid = "B9407F30-F5F8-466E-AFF9-25556B57FE6D";
		private const double NearestDistance = 0.03;

		private readonly CLLocationManager _locationManager;
		private readonly CLBeaconRegion _allBeaconsRegion;

		private readonly List<BeaconDevice> _beacons;

		private readonly object _sync;


		public BeaconService ()
		{
			_locationManager = new CLLocationManager ();
			_locationManager.Delegate = this;

			_beacons = new List<BeaconDevice> ();

			_allBeaconsRegion = new CLBeaconRegion (new NSUuid (uuid), "All Beacons Region");

			_sync = new object ();
		}


		public void Start (IEnumerable<BeaconDevice> beacons)
		{
			_locationManager.RequestAlwaysAuthorization ();

			if (beacons == null)
				return;

			lock (_sync) {
				foreach (var item in beacons) {
					AddBeaconToMonitoring (item);
				}
			}
		}

		public void AddBeaconToMonitoring (BeaconDevice beacon)
		{
			lock (_sync) {
				var monitored = _locationManager.MonitoredRegions;
				if (monitored.All (m => (m as CLBeaconRegion)?.Identifier != beacon.DeviceId)) {
					var beaconRegion = new CLBeaconRegion (new NSUuid (beacon.Uuid), (ushort)beacon.Major, (ushort)beacon.Minor, beacon.DeviceId);
					beaconRegion.NotifyOnEntry = true;
					beaconRegion.NotifyOnExit = true;

					_locationManager.StartMonitoring (beaconRegion);
				}
				_beacons.Add (beacon);
			}
		}

		public void RemoveBeaconFromMonitoring (BeaconDevice beacon)
		{
			lock (_sync) {
				var foundBeacon = _beacons.FirstOrDefault (b => b.DeviceId == beacon.DeviceId);
				if (foundBeacon != null)
					_beacons.Remove (foundBeacon);

				var foundBeaconRegion = _locationManager.MonitoredRegions.FirstOrDefault (br => (br as CLRegion)?.Identifier == beacon.DeviceId) as CLBeaconRegion;
				if (foundBeaconRegion != null)
					_locationManager.StopMonitoring (foundBeaconRegion);
			}
		}

		public void StartFindNearest ()
		{
			_locationManager.StartRangingBeacons (_allBeaconsRegion);
		}

		public void StopFindNearest ()
		{
			_locationManager.StopRangingBeacons (_allBeaconsRegion);
		}


		//logic for locationmanager
		public override void AuthorizationChanged (CLLocationManager manager, CLAuthorizationStatus status)
		{
			if (status != CLAuthorizationStatus.AuthorizedAlways) {
				NoAuth (this, EventArgs.Empty);
			}
		}

		public override void DidRangeBeacons (CLLocationManager manager, CLBeacon[] beacons, CLBeaconRegion region)
		{
			CLBeacon beacon;
			lock (_sync) {
				//we need uniq beacon which is close then 5sm
				beacon = beacons.FirstOrDefault (b => _beacons.All (eb => eb.Major != b.Major.Int32Value || eb.Minor != b.Minor.Int32Value) && b.Proximity == CLProximity.Immediate && b.Accuracy < NearestDistance);
				if (beacon == null)
					return;

				System.Diagnostics.Debug.WriteLine ("FindBeacon " + beacon);

				//We found one - no need to find again as we will find it again in some seconds
				StopFindNearest ();
			}

			var nearestBeacon = new BeaconDevice {
				Uuid = beacon.ProximityUuid.AsString (),
				Major = beacon.Major.Int32Value,
				Minor = beacon.Minor.Int32Value
			};
			NearestFound (this, new BeaconEventArgs { BeaconDevice = nearestBeacon });
		}

		public override void RegionEntered (CLLocationManager manager, CLRegion region)
		{
			BeaconDevice beacon;
			lock (_sync) {
				beacon = _beacons.FirstOrDefault (b => b.DeviceId == region.Identifier);
			}
				
			System.Diagnostics.Debug.WriteLine ("Entered " + beacon);

			if (beacon != null)
				Entered (this, new BeaconEventArgs { BeaconDevice = beacon });
		}

		public override void RegionLeft (CLLocationManager manager, CLRegion region)
		{
			BeaconDevice beacon;
			lock (_sync) {
				beacon = _beacons.FirstOrDefault (b => b.DeviceId == region.Identifier);
			}
				
			System.Diagnostics.Debug.WriteLine ("Left " + region);

			if (beacon != null)
				Left (this, new BeaconEventArgs { BeaconDevice = beacon });
		}
	}
}