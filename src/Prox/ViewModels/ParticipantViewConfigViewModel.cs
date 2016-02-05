namespace Prox
{
	public class ParticipantViewConfigViewModel : PageViewModel
	{
		public string HeaderText { get { return _headerText; } set { Set (ref _headerText, value); } }

		public string ContactText { get { return _contactText; } set { Set (ref _contactText, value); } }

		public string Email { get { return _email; } set { Set (ref _email, value); } }


		private string _headerText;
		private string _contactText;
		private string _email;


		public ParticipantViewConfigViewModel ()
		{
			HeaderText = App.Settings.ParticipantView.WelcomeText;
			ContactText = App.Settings.ParticipantView.ContactText;
			Email = App.Settings.ParticipantView.Email;
		}


		public override void OnAppearing ()
		{
			HeaderText = App.Settings.ParticipantView.WelcomeText;
			ContactText = App.Settings.ParticipantView.ContactText;
			Email = App.Settings.ParticipantView.Email;
		}

		public override void OnDisappearing ()
		{
			App.Settings.ParticipantView.WelcomeText = HeaderText;
			App.Settings.ParticipantView.ContactText = ContactText;
			App.Settings.ParticipantView.Email = Email;
			App.Settings.Save ();
		}
	}
}