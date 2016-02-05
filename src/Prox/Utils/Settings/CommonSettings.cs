namespace Prox
{
	public class CommonSettings : KeySettingsBase
	{
		public string ParticipantId { get { return GetValue<string> (); } set { SetValue (value); } }

		public bool IsNotificationsOnEvents { get { return GetValue<bool> (); } set { SetValue (value); } }
	}
}