using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prox
{
	public class MeViewModel : PageViewModel
	{
		public string WelcomeText { get { return _welcomeText; } set { Set (ref _welcomeText, value); } }

		public string ContactText { get { return _contactsText; } set { Set (ref _contactsText, value); } }

		public string ContactEmail { get { return _contactsEmail; } set { Set (ref _contactsEmail, value); } }


		public ICommand QuestionsCommand { get; set; }


		private string _welcomeText;
		private string _contactsText;
		private string _contactsEmail;


		public MeViewModel ()
		{
			QuestionsCommand = new Command (QuestionsCommandExecute);	
		}


		private void QuestionsCommandExecute ()
		{
			Device.OpenUri (new Uri (ContactEmail));
		}


		public override void OnAppearing ()
		{
			WelcomeText = App.Settings.ParticipantView.WelcomeText;
			ContactText = App.Settings.ParticipantView.ContactText;
			ContactEmail = App.Settings.ParticipantView.Email;
		}
	}
}