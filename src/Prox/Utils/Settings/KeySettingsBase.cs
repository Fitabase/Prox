using System;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace Prox
{
	public abstract class KeySettingsBase
	{
		public T GetValue<T> (T defaultValue = default(T), [CallerMemberName]string fieldName = "")
		{
			var key = GetType ().FullName + fieldName;

			object ret;
			Application.Current.Properties.TryGetValue (key, out ret);
			if (ret == null)
				return defaultValue;
			else
				return (T)ret;
		}

		public void SetValue<T> (T value, [CallerMemberName]string fieldName = "")
		{
			var key = GetType ().FullName + fieldName;
			
			Application.Current.Properties [key] = value;
		}
	}
}