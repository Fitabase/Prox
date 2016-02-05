namespace Prox
{
	public class ParticipantViewSettings : KeySettingsBase
	{
		public string WelcomeText { get { return GetValue<string> (Resources.Strings.DefaultParticipantViewWelcomeText); } set { SetValue (value); } }

		public string ContactText { get { return GetValue<string> (Resources.Strings.DefaultParticipantViewContactText); } set { SetValue (value); } }

		public string Email { get { return GetValue<string> (Resources.Strings.DefaultParticipantViewContactEmail); } set { SetValue (value); } }
	}
}