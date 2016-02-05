using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;

namespace Prox
{
	public class SelectCategoryViewModel : PageViewModel
	{
		public ICommand AddSubCategoryCommand { get; set; }

		public ICommand GoBackCommand { get; set; }

		public string Name { get { return _name; } set { Set (ref _name, value); } }

		public string ParentName { get { return _parentName; } set { Set (ref _parentName, value); } }

		public ObservableCollection<CategoryViewModel> SubCategories { get { return _subCategories; } set { Set (ref _subCategories, value); } }

		public CategoryViewModel SelectedSubCategory {
			get{ return _selectedSubCategory; }
			set {
				if (Set (ref _selectedSubCategory, value) && value != null) {
					SelectedSubCategory = null;

					OnSubCategorySelected (value);
				}
			}
		}

		public bool CanGoBack { get { return _canGoBack; } set { Set (ref _canGoBack, value); } }

		public Category CurrentCategory{ get; set; }


		private string _name;
		private string _parentName;
		private ObservableCollection<CategoryViewModel> _subCategories;
		private CategoryViewModel _selectedSubCategory;
		private bool _canGoBack;

	
		public SelectCategoryViewModel (Category category)
		{
			category = category ?? App.Settings.Categories.GetDefaultCategory ();

			SetCategory (category);

			AddSubCategoryCommand = new Command (AddSubCategoryExecute);
			GoBackCommand = new Command (GoBackCommandExecute);
		}


		private void SetCategory (Category category)
		{
			CurrentCategory = category;

			Name = CurrentCategory.Name;
			if (CurrentCategory.ParentId != null) {
				var parentCategory = App.Settings.Categories.GetCategoryById (CurrentCategory.ParentId.Value);

				CanGoBack = true;
				ParentName = parentCategory.Name;
			} else {
				CanGoBack = false;
			}

			if (SubCategories != null && SubCategories.Count != 0) {
				foreach (var item in SubCategories) {
					item.Deleted -= OnSubCategoryDeleted;
				}
			}
			var categories = App.Settings.Categories.GetChildCategories (category.Id.Value);
			SubCategories = new ObservableCollection<CategoryViewModel> (categories.Select (c => new CategoryViewModel (c)));
			foreach (var item in SubCategories) {
				item.Deleted += OnSubCategoryDeleted;
			}
		}

		private void OnSubCategorySelected (CategoryViewModel category)
		{
			CurrentCategory.Name = Name;
			App.Settings.Categories.Update (CurrentCategory);

			SetCategory (category.Model);
		}

		private void OnSubCategoryDeleted (CategoryViewModel category)
		{
			SubCategories.Remove (category);
		}

		private void AddSubCategoryExecute ()
		{
			CurrentCategory.Name = Name;
			App.Settings.Categories.Update (CurrentCategory);

			var category = App.Settings.Categories.AddCategoryToParent (CurrentCategory);

			SetCategory (category);
		}

		private void GoBackCommandExecute ()
		{
			CurrentCategory.Name = Name;
			App.Settings.Categories.Update (CurrentCategory);

			var category = App.Settings.Categories.GetCategoryById (CurrentCategory.ParentId.Value);

			SetCategory (category);
		}

		public override void OnDisappearing ()
		{
			CurrentCategory.Name = Name;
			App.Settings.Categories.Update (CurrentCategory);

			base.OnDisappearing ();
		}
	}
}