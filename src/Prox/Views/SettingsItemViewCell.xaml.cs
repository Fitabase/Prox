using Xamarin.Forms;
using System.Windows.Input;

namespace Prox
{
	public partial class SettingsItemViewCell : ViewCell
	{
		#region Title bindable property

		public static BindableProperty TitleProperty = BindableProperty.Create<SettingsItemViewCell, string> (p => p.Title, null);

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

		public static BindableProperty ImageProperty = BindableProperty.Create<SettingsItemViewCell, string> (p => p.Image, null);

		public string Image { 
			get {
				return (string)GetValue (ImageProperty);
			}
			set {
				SetValue (ImageProperty, value);
			}
		}

		#endregion

		#region Command bindable property

		public static BindableProperty CommandProperty = BindableProperty.Create<SettingsItemViewCell, ICommand> (p => p.Command, null);

		public ICommand Command { 
			get {
				return (ICommand)GetValue (CommandProperty);
			}
			set {
				SetValue (CommandProperty, value);
			}
		}

		#endregion

		#region CommandParameter bindable property

		public static BindableProperty CommandParameterProperty = BindableProperty.Create<SettingsItemViewCell, object> (p => p.CommandParameter, null);

		public object CommandParameter { 
			get {
				return (object)GetValue (CommandParameterProperty);
			}
			set {
				SetValue (CommandParameterProperty, value);
			}
		}

		#endregion


		public SettingsItemViewCell ()
		{
			InitializeComponent ();
		}


		protected override void OnTapped ()
		{
			base.OnTapped ();

			if (Command != null && Command.CanExecute (CommandParameter))
				Command.Execute (CommandParameter);
		}
	}
}