using System.Windows.Input;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace Prox
{
	public class SetupBeaconsViewModel : PageViewModel
	{
		public ICommand AddBeaconCommand { get; set; }

		public ICommand ExportCommand { get; set; }

		public ICommand ImportCommand { get; set; }

		public ObservableCollection<BeaconViewModel> Beacons { get { return _beacons; } set { Set (ref _beacons, value); } }

		public BeaconViewModel SelectedBeacon {
			get { return _selectedBeacon; }
			set {
				if (Set (ref _selectedBeacon, value) && value != null) {
					SelectedBeacon = null;

					OnBeaconSelected (value);
				}
			}
		}


		private ObservableCollection<BeaconViewModel> _beacons;
		private BeaconViewModel _selectedBeacon;


		public SetupBeaconsViewModel ()
		{
			AddBeaconCommand = new Command (AddBeaconCommandExecute);
			ExportCommand = new Command (ExportCommandExecute);
			ImportCommand = new Command (ImportCommandExecute);
		}


		private void OnBeaconSelected (BeaconViewModel beacon)
		{
			EditBeacon (beacon.Model);
		}

		private void AddBeaconCommandExecute ()
		{
			EditBeacon (null);
		}

		private void ExportCommandExecute ()
		{
			App.Navigation.PushAsync (new BeaconsExportPage { BindingContext = new BeaconsExportViewModel () });
		}

		private void ImportCommandExecute ()
		{
			App.Navigation.PushAsync (new BeaconsImportPage { BindingContext = new BeaconsImportViewModel () });
		}

		private void EditBeacon (Beacon beacon)
		{
			App.Navigation.PushAsync (new EditBeaconPage { BindingContext = new EditBeaconViewModel (beacon) });
		}


		public override void OnAppearing ()
		{
			Beacons = new ObservableCollection<BeaconViewModel> ();
			foreach (var beacon in App.Settings.Beacons.Items) {
				var viewModel = new BeaconViewModel (beacon);
				viewModel.Deleted += OnBeaconDeleted;

				Beacons.Add (viewModel);
			}
		}

		private void OnBeaconDeleted (BeaconViewModel beacon)
		{
			beacon.Deleted -= OnBeaconDeleted;

			Beacons.Remove (beacon);
		}
	}
}