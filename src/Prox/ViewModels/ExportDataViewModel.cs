using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prox
{
	public class ExportDataViewModel : ViewModelBase
	{
		public DateTime From { get; set; }

		public DateTime To { get; set; }

		public ICommand ExportToEmailCommand { get; set; }

		public ICommand ExportToDropboxCommand { get; set; }

		public bool IsProcessing { get { return _isProcessing; } set { Set (ref _isProcessing, value); } }


		private bool _isProcessing;


		public ExportDataViewModel ()
		{
			From = DateTime.Today.AddDays (-7);
			To = DateTime.Today;

			ExportToEmailCommand = new Command (ExportToEmailCommandExecute);
			ExportToDropboxCommand = new Command (ExportToDropboxCommandExecute);
		}


		private void ExportToEmailCommandExecute ()
		{
			Export (DependencyService.Get<IMailService> ());
		}

		private void ExportToDropboxCommandExecute ()
		{
			Export (new DropboxUtils ());
		}

		private async void Export (IExporter exporter)
		{
			if (string.IsNullOrEmpty (App.Settings.Common.ParticipantId)) {
				await App.Current.MainPage.DisplayAlert (Resources.Strings.Error, Resources.Strings.ExportErrorNoParticipantMessage, Resources.Strings.Ok);
				return;
			}

			//to block user for doing anything while uploading info
			await App.Navigation.PushModalAsync (new ExportWaitPage ());

			//from time 00:00:00 for From date to 23:59:59 time at To date
			var csv = App.EncounterUtils.ExportEventsToCsv (From, To.AddHours(23).AddMinutes(59).AddSeconds(59));

			var isSuccess = await exporter.SendAsync (csv, App.Settings.Common.ParticipantId);
			if (isSuccess)
				await App.Current.MainPage.DisplayAlert (Resources.Strings.Success, Resources.Strings.ExportSuccessMessage, Resources.Strings.Ok);
			else
				await App.Current.MainPage.DisplayAlert (Resources.Strings.Error, Resources.Strings.ExportErrorMessage, Resources.Strings.Ok);

			await App.Navigation.PopModalAsync ();
		}
	}
}