using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Prox.Droid;

[assembly: ExportRenderer (typeof(Editor), typeof(NoPaddingEditorRenderer))]

namespace Prox.Droid
{
	public class NoPaddingEditorRenderer : EditorRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Editor> e)
		{
			base.OnElementChanged (e);

			if (e.NewElement == null)
				return;

			Control.SetPadding (0, 0, 0, 0);
			Control.SetBackgroundColor (Android.Graphics.Color.Transparent);
		}
	}
}