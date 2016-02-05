using Xamarin.Forms;

namespace Prox
{
	public partial class BeaconViewCell : ViewCell
	{
		#region Title bindable property

		public static BindableProperty TitleProperty = BindableProperty.Create<BeaconViewCell, string> (p => p.Title, null);

		public string Title { 
			get {
				return (string)GetValue (TitleProperty);
			}
			set {
				SetValue (TitleProperty, value);
			}
		}

		#endregion

		#region Image bindable property

		public static BindableProperty ImageProperty = BindableProperty.Create<BeaconViewCell, string> (p => p.Image, null);

		public string Image { 
			get {
				return (string)GetValue (ImageProperty);
			}
			set {
				SetValue (ImageProperty, value);
			}
		}

		#endregion

		public BeaconViewCell ()
		{
			InitializeComponent ();
		}
	}
}