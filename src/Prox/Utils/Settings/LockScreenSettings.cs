namespace Prox
{
	public class LockScreenSettings : KeySettingsBase
	{
		public bool IsUse { get { return GetValue<bool> (false); } set { SetValue (value); } }

		public string Text { get { return GetValue<string> (Resources.Strings.DefaultLockScreenText); } set { SetValue (value); } }

		public string PassPhrase { get { return GetValue<string> (); } set { SetValue (value); } }
	}
}