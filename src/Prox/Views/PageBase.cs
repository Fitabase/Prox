using Xamarin.Forms;

namespace Prox
{
	public class PageBase : ContentPage
	{
		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			var viewModel = BindingContext as PageViewModel;
			if (viewModel != null)
				viewModel.OnAppearing ();
		}

		protected override void OnDisappearing ()
		{
			base.OnDisappearing ();

			var viewModel = BindingContext as PageViewModel;
			if (viewModel != null)
				viewModel.OnDisappearing ();
		}
	}
}