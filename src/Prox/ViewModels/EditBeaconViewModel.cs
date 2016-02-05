using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prox
{
	public class EditBeaconViewModel : PageViewModel
	{
		public string Title { get; set; }

		public string Name { 
			get { return _name; }
			set {
				if (Set (ref _name, value)) {
					//remove > and ; simbols as they used in export
					Name = Name.Replace (";", "").Replace (">", "");
				}
			} 
		}

		public BeaconDeviceViewModel Device { get; set; }

		public string Category { get { return _category; } set { Set (ref _category, value); } }

		public ICommand EditCategoryCommand { get; set; }

		public ICommand ChangeBeaconCommand { get; set; }


		private string _name;
		private string _category;
		private SelectCategoryViewModel _selectCategory;
		private readonly Beacon _model;


		public EditBeaconViewModel (Beacon beacon)
		{
			//if no beacon - we create new temporary beacon without ID to determine on this page closing is this beacon should be saved
			_model = beacon ?? new Beacon ();

			//we never save beacon without a name, so if no beacon - it is creating of new
			Title = _model.Name ?? Resources.Strings.AddBeaconTitle;
			Name = _model.Name;
			Device = new BeaconDeviceViewModel ();
			Device.Set (_model.Device);

			Category = _model.Category != null ? _model.Category.GetFullPath () : Resources.Strings.SelectCategoryTitle;

			EditCategoryCommand = new Command (EditCategoryCommandExecute);
			ChangeBeaconCommand = new Command (ChangeBeaconCommandExecute);	
		}


		private void StartDiscovering ()
		{
			App.BeaconService.NearestFound += OnBeaconFound;
			App.BeaconService.StartFindNearest ();
		}

		private void StopDiscovering ()
		{
			App.BeaconService.NearestFound -= OnBeaconFound;
			App.BeaconService.StopFindNearest ();
		}

		private void OnBeaconFound (object sender, BeaconEventArgs foundBeaconArgs)
		{
			_model.Device = foundBeaconArgs.BeaconDevice;
			Device.Set (foundBeaconArgs.BeaconDevice);
		}


		private void EditCategoryCommandExecute ()
		{
			_selectCategory = new SelectCategoryViewModel (_model?.Category);
			App.Navigation.PushAsync (new SelectCategoryPage { BindingContext = _selectCategory });
		}

		private void ChangeBeaconCommandExecute ()
		{
			_model.Device = null;
			Device.Set (null);

			StartDiscovering ();
		}


		public override void OnAppearing ()
		{
			if (!Device.IsConnected)
				StartDiscovering ();

			//user selected category for this beacon
			if (_selectCategory != null) {
				_model.Category = _selectCategory.CurrentCategory;
				Category = _selectCategory.CurrentCategory.GetFullPath ();

				_selectCategory = null;
			} else {
				//we enter edit mode - no monitoring for encounters
				if (Device.IsConnected)
					App.BeaconService.RemoveBeaconFromMonitoring (_model.Device);
			}
		}

		public override void OnDisappearing ()
		{
			StopDiscovering ();

			if (_selectCategory != null)
				return;

			_model.Name = Name;

			//if model has no ID - it means that it is temporary model - need check if we need to save it
			if (!_model.Id.HasValue) {
				//is any changes made
				var isAnyChanges = !string.IsNullOrEmpty (Name) || _model.Category != null || _model.Device != null;
				if (isAnyChanges) {
					//beacon always whould have any name 
					var caregoryName = Category == Resources.Strings.SelectCategoryTitle ? null : Category;
					_model.Name = Name ?? caregoryName ?? _model.Device.DeviceId;

					App.Settings.Beacons.Add (_model);
				}
			} else {
				App.Settings.Beacons.Update (_model);
			}

			//we exit edit mode - start monitoring for encounters
			if (_model.Device != null)
				App.BeaconService.AddBeaconToMonitoring (_model.Device);
		}
	}
}