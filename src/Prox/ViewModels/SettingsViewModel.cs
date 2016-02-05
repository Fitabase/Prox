using Xamarin.Forms;

namespace Prox
{
	public class SettingsViewModel : PageViewModel
	{
		public LockViewModel LockInfo { get; set; }

		public Command<SettingsItem> ItemSelectedCommand { get; set; }

		public string ParticipantId { get { return _participantId; } set { Set (ref _participantId, value); } }

		public bool IsNotificationsOnEvents { get { return _isNotificationsOnEvents; } set { Set (ref _isNotificationsOnEvents, value); } }


		private string _participantId;
		private bool _isInSettings;
		private bool _isNotificationsOnEvents;


		public SettingsViewModel ()
		{
			LockInfo = new LockViewModel ();
			ItemSelectedCommand = new Command<SettingsItem> (ItemSelectedCommandExecute);
		}


		private void ItemSelectedCommandExecute (SettingsItem parameter)
		{
			Page page = null;

			switch (parameter) {
			case SettingsItem.About:
				page = new AboutPage ();
				break;
			case SettingsItem.ExportData:
				page = new ExportDataPage { BindingContext = new ExportDataViewModel () };
				break;
			case SettingsItem.LockoutScreenConfig:
				page = new LockoutScreenConfigPage { BindingContext = new LockScreenConfigViewModel () };
				break;
			case SettingsItem.ParticipantViewConfig:
				page = new ParticipantViewConfigPage { BindingContext = new ParticipantViewConfigViewModel () };
				break;
			case SettingsItem.SetupBeacons:
				page = new SetupBeaconsPage { BindingContext = new SetupBeaconsViewModel () };
				break;
			}

			App.Navigation.PushAsync (page);

			_isInSettings = true;
		}

		public override void OnAppearing ()
		{
			if (_isInSettings) {
				_isInSettings = false;
			} else {
				ParticipantId = App.Settings.Common.ParticipantId;
				IsNotificationsOnEvents = App.Settings.Common.IsNotificationsOnEvents;

				LockInfo.OnAppearing ();
			}
		}

		public override void OnDisappearing ()
		{
			App.Settings.Common.ParticipantId = ParticipantId;
			App.Settings.Common.IsNotificationsOnEvents = IsNotificationsOnEvents;
			App.Settings.Save ();
		}
	}

	public enum SettingsItem
	{
		About,
		ExportData,
		SetupBeacons,
		ParticipantViewConfig,
		LockoutScreenConfig
	}
}