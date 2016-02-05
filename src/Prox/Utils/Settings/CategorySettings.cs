using System.Collections.Generic;
using System.Linq;
using SQLite.Net;

namespace Prox
{
	public class CategorySettings : SqlSettingsBase<Category>
	{
		public CategorySettings (SQLiteConnection sqlConnection) : base (sqlConnection)
		{
			if (Collection.Count == 0) {
				var defaultCategory = new Category { Name = Resources.Strings.DefaultCategoryName };
				Add (defaultCategory);
			}
		}

		protected override void Initialize (SQLiteConnection sqlConnection)
		{
			sqlConnection.CreateTable<Category> ();
		}

		public Category GetDefaultCategory ()
		{
			return Collection.First (c => c.ParentId == null);
		}

		public Category GetCategoryById (int id)
		{
			return Collection.FirstOrDefault (c => c.Id == id);
		}

		public IEnumerable<Category> GetChildCategories (int parentId)
		{
			return Collection.Where (c => c.ParentId == parentId);
		}

		public Category AddCategoryToParent (Category parentCategory, string name = null)
		{
			var c = new Category {
				Name = name,
				ParentId = parentCategory.Id,
				ParentPath = parentCategory.GetFullPath ()
			};
					
			return Add (c);
		}

		public Category GetOrCreateCategoryByPath (string path)
		{
			if (string.IsNullOrEmpty (path))
				return null;

			var categoris = path.Split (';');

			var rootPath = categoris [0];
			var rootCategory = GetDefaultCategory ();

			//if root category has another name - change it to new name in import
			if (rootCategory.Name != rootPath) {
				rootCategory.Name = rootPath;
				Update (rootCategory);
			}

			if (categoris.Length == 1)
				return rootCategory;
			else
				return GetOrCreateCategoryByPath (categoris.Skip (1).ToArray (), rootCategory);
		}


		private Category GetOrCreateCategoryByPath (string[] path, Category parent)
		{
			var currentName = path [0];

			var current = Items.FirstOrDefault (c => c.Name == currentName && c.ParentId == parent.Id);
			if (current == null) {
				current = AddCategoryToParent (parent, currentName);
			}

			if (path.Length == 1)
				return current;
			else
				return GetOrCreateCategoryByPath (path.Skip (1).ToArray (), current);
		}
	}
}