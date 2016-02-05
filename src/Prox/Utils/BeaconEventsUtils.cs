using System;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CsvHelper;
using System.IO;
using CsvHelper.TypeConversion;

namespace Prox
{
	public class BeaconEventsUtils
	{
		private readonly SQLiteConnection _sqlConnection;
		private readonly INotificationService _notificationService;


		public BeaconEventsUtils (SQLiteConnection sqlConnection, INotificationService notificationService)
		{
			_sqlConnection = sqlConnection;
			_notificationService = notificationService;

			sqlConnection.CreateTable<BeaconEvent> ();

			var options = new TypeConverterOptions
			{
				Format = "MM/d/yyyy HH:mm:ss tz",
			};
			TypeConverterOptionsFactory.AddOptions<DateTime>(options);
		}


		public void Enter (BeaconDevice beaconDevice)
		{
			WriteEventAndNotify (beaconDevice, EventType.Enter);
		}

		public void Left (BeaconDevice beaconDevice)
		{
			WriteEventAndNotify (beaconDevice, EventType.Exit);
		}

		public IEnumerable<BeaconEvent> GetEvents (DateTime from, DateTime to)
		{
			return _sqlConnection.GetAllWithChildren<BeaconEvent> (e => e.Time >= from && e.Time <= to, true);
		}

		public string ExportEventsToCsv (DateTime from, DateTime to)
		{
			var events = GetEvents (from, to);

			var eventsToExport = events.Where (b => b.Beacon != null).Select (e => {
				_sqlConnection.GetChildren (e.Beacon);
				var category = e.Beacon.Category?.GetExportPath ();
				return new ExportItem {
					Type = e.Type,
					Time = e.Time,
					UUID = e.Beacon.Device.Uuid,
					Major = e.Beacon.Device.Major,
					Minor = e.Beacon.Device.Minor,
					Category = category
				};
			});

			var sb = new StringBuilder ();
			using (var writer = new CsvWriter (new StringWriter (sb))) {
				writer.WriteRecords (eventsToExport);
			}

			return sb.ToString ();
		}


		private void WriteEventAndNotify (BeaconDevice beaconDevice, EventType eventType)
		{
			var beacon = _sqlConnection.Table<Beacon> ().Where (b => b.DeviceId == beaconDevice.Id).FirstOrDefault ();
			if (beacon == null)
				return;
			
			_sqlConnection.GetChildren (beacon);

			var ev = new BeaconEvent {
				Beacon = beacon,
				Type = eventType,
				Time = DateTime.Now
			};

			_sqlConnection.InsertWithChildren (ev);

			if (App.Settings.Common.IsNotificationsOnEvents)
				_notificationService.Show (string.Format (Resources.Strings.NotificaionMessageFormat, eventType, beacon.Name));
		}


		//item for exporing to csv
		class ExportItem
		{
			public EventType Type { get; set; }

			public DateTime Time { get; set; }

			public string UUID { get; set; }

			public int Major { get; set; }

			public int Minor { get; set; }

			public string Category { get; set; }
		}
	}
}