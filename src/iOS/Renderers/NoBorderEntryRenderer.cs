using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using UIKit;
using Prox.iOS;

[assembly: ExportRenderer (typeof(Entry), typeof(NoBorderEntryRenderer))]

namespace Prox.iOS
{
	public class NoBorderEntryRenderer : EntryRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged (e);

			if (e.NewElement == null)
				return;
			
			Control.BorderStyle = UITextBorderStyle.None;
		}
	}
}