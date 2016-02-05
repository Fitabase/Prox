using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prox
{
	public class BeaconViewModel
	{
		public event Action<BeaconViewModel> Deleted = b => {};


		public string Name { get { return Model.Name; } }

		public Beacon Model { get; set; }

		public ICommand DeleteCommand { get; set; }


		public BeaconViewModel (Beacon beacon)
		{
			Model = beacon;

			DeleteCommand = new Command (DeleteCommandExecute);
		}


		private async void DeleteCommandExecute ()
		{
			if (await App.Current.MainPage.DisplayAlert (Resources.Strings.BeaconDeleteTitle, Resources.Strings.BeaconDeleteQuestion, Resources.Strings.Yes, Resources.Strings.No)) {
				App.Settings.Beacons.Delete (Model.Id.Value);
				if (Model.Device != null)
					App.BeaconService.RemoveBeaconFromMonitoring (Model.Device);

				Deleted (this);
			}
		}
	}
}