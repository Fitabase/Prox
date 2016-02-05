using System;
using Xamarin.Forms;

namespace Prox
{
	public class BeaconsImportViewModel : ViewModelBase
	{
		public Command RunCommand { get; set; }

		public string Json {
			get{ return _json; }
			set {
				if (Set (ref _json, value))
					RunCommand.ChangeCanExecute ();
			}
		}


		private string _json;


		public BeaconsImportViewModel ()
		{
			RunCommand = new Command (RunCommandExecute, CanRunCommandExecute);
		}


		private async void RunCommandExecute ()
		{
			var isImport = await App.Current.MainPage.DisplayAlert (Resources.Strings.ImportQuestionTitle, Resources.Strings.ImportQuestionMessage, Resources.Strings.Yes, Resources.Strings.No);
			if (!isImport)
				return;
			
			var importResult = await App.Settings.Beacons.Import (Json);

			string importAlertTitle;
			string importAlertMessage;

			if (importResult.IsSuccess) {
				//start imported beacons to monitoring
				foreach (var beacon in importResult.Imported) {
					App.BeaconService.AddBeaconToMonitoring (beacon.Device);
				}
					
				importAlertTitle = Resources.Strings.Success;
				importAlertMessage = string.Format (Resources.Strings.ImportSuccessMessageFormat, importResult.Imported.Length, importResult.Updated.Length, importResult.Skipped.Length);
			} else {
				importAlertTitle = Resources.Strings.Error;
				importAlertMessage = Resources.Strings.ImportErrorWrongFormat;
			}

			await App.Current.MainPage.DisplayAlert (importAlertTitle, importAlertMessage, Resources.Strings.Ok);
		}

		private bool CanRunCommandExecute ()
		{
			return !string.IsNullOrEmpty (Json);
		}
	}
}