using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Prox.Droid;

[assembly: ExportRenderer (typeof(Entry), typeof(NoPaddingEntryRenderer))]


namespace Prox.Droid
{
	public class NoPaddingEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);

			if (e.NewElement == null)
				return;

			Control.SetPadding (0, 0, 0, 0);
			Control.SetBackgroundColor (Android.Graphics.Color.Transparent);
		}
	}
}