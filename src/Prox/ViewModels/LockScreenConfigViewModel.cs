using System;

namespace Prox
{
	public class LockScreenConfigViewModel : PageViewModel
	{
		public bool IsUse { get { return _isUse; } set { Set (ref _isUse, value); } }

		public string LockoutText { get { return _lockText; } set { Set (ref _lockText, value); } }

		public string Passphrase { get { return _passPhrase; } set { Set (ref _passPhrase, value); } }


		private bool _isUse;
		private string _lockText;
		private string _passPhrase;


		public override void OnAppearing ()
		{
			IsUse = App.Settings.LockScreen.IsUse;
			LockoutText = App.Settings.LockScreen.Text;
			Passphrase = App.Settings.LockScreen.PassPhrase;
		}

		public override void OnDisappearing ()
		{
			App.Settings.LockScreen.IsUse = IsUse;
			App.Settings.LockScreen.Text = LockoutText;
			App.Settings.LockScreen.PassPhrase = Passphrase;
			App.Settings.Save ();
		}
	}
}