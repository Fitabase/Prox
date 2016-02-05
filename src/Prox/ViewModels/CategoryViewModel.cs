using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Prox
{
	public class CategoryViewModel
	{
		public event Action<CategoryViewModel> Deleted = c => {};


		public string Name { get { return Model.Name; } }

		public Category Model { get; set; }

		public ICommand DeleteCommand { get; set; }


		public CategoryViewModel (Category category)
		{
			Model = category;

			DeleteCommand = new Command (DeleteCommandExecute);
		}


		private async void DeleteCommandExecute ()
		{
			if (await App.Current.MainPage.DisplayAlert (Resources.Strings.CategoryDeleteTitle, Resources.Strings.CategoryDeleteQuestion, Resources.Strings.Yes, Resources.Strings.No)) {
				App.Settings.Beacons.RemoveCategoryFromBeacons (Model.Id.Value);
				App.Settings.Categories.Delete (Model.Id.Value);

				Deleted (this);
			}
		}
	}
}