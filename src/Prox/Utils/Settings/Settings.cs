using Xamarin.Forms;
using System.Threading.Tasks;
using SQLite.Net;
using Newtonsoft.Json;
using System.Linq;

namespace Prox
{
	public class Settings
	{
		public CommonSettings Common { get; set; }

		public LockScreenSettings LockScreen { get; set; }

		public ParticipantViewSettings ParticipantView { get; set; }

		public CategorySettings Categories { get; set; }

		public BeaconsSettings Beacons { get; set; }


		public Settings (SQLiteConnection sqlConnection)
		{
			Common = new CommonSettings ();
			LockScreen = new LockScreenSettings ();
			ParticipantView = new ParticipantViewSettings ();
			Categories = new CategorySettings (sqlConnection);
			Beacons = new BeaconsSettings (sqlConnection, Categories);
		}
			

		public async Task Save ()
		{
			await Application.Current.SavePropertiesAsync ();
		}
	}
}