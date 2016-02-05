using Xamarin.Forms;

namespace Prox
{
	public class LockViewModel : ViewModelBase
	{
		public Command UnlockCommand { get; set; }

		public bool IsLocked { get { return _isLocked; } set { Set (ref _isLocked, value); } }

		public string LockText { get { return _lockText; } set { Set (ref _lockText, value); } }

		public string PassPhrase { get { return _passPhrase; } set { Set (ref _passPhrase, value); } }


		private bool _isLocked;
		private string _lockText;
		private string _passPhrase;


		public LockViewModel ()
		{
			UnlockCommand = new Command (UnlockCommandExecute);
		}


		private async void UnlockCommandExecute ()
		{
			IsLocked = (PassPhrase ?? string.Empty) != (App.Settings.LockScreen.PassPhrase ?? string.Empty);
			if (IsLocked) {
				await App.Current.MainPage.DisplayAlert (Resources.Strings.Error, Resources.Strings.LoginPasswordWrongMessage, Resources.Strings.Ok);
			}

			PassPhrase = string.Empty;
		}
			
		public void OnAppearing ()
		{
			LockText = App.Settings.LockScreen.Text;
			IsLocked = App.Settings.LockScreen.IsUse;
		}
	}
}