using Xamarin.Forms;
using System.Linq;

namespace Prox
{
	public class App : Application
	{
		public static INavigation Navigation { get; set; }

		public static Settings Settings { get; set; }

		public static IBeaconService BeaconService { get; set; }

		public static INotificationService NotificationService { get; set; }

		public static BeaconEventsUtils EncounterUtils { get; set; }


		public App ()
		{
			var sqlConnection = DependencyService.Get<ISqLiteService> ().GetConnection ();

			Settings = new Settings (sqlConnection);

			var mePage = new MePage {
				BindingContext = new MeViewModel (),
				Title = "Me"
			};
			var meNavigationPage = new NavigationPage (mePage) {
				Title = "Me",
				Icon = "icon_user.png"
			};

			var settingsPage = new SettingsPage {
				Title = "Settings"
			};
			var navigationSettingsPage = new NavigationPage (settingsPage) {
				Title = "Settings",
				Icon = "icon_settings.png"
			};
			Navigation = navigationSettingsPage.Navigation;

			settingsPage.BindingContext = new SettingsViewModel ();

			var mainPage = new TabbedPage ();
			mainPage.Children.Add (meNavigationPage);
			mainPage.Children.Add (navigationSettingsPage);

			MainPage = mainPage;

			BeaconService = DependencyService.Get<IBeaconService> ();
			NotificationService = DependencyService.Get<INotificationService> ();
			EncounterUtils = new BeaconEventsUtils (sqlConnection, NotificationService);
		}


		protected override void OnStart ()
		{
			BeaconService.Entered += OnBeaconServiceEnter;
			BeaconService.Left += OnBeaconServiceLeft;

			var devices = App.Settings.Beacons.Items.Where (b => b.Device != null).Select (b => b.Device);
			BeaconService.Start (devices);

			NotificationService.Start ();
		}

		private void OnBeaconServiceLeft (object sender, BeaconEventArgs e)
		{
			EncounterUtils.Left (e.BeaconDevice);
		}

		private void OnBeaconServiceEnter (object sender, BeaconEventArgs e)
		{
			EncounterUtils.Enter (e.BeaconDevice);
		}
	}
}