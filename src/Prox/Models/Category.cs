using System.Linq;

namespace Prox
{
	public class Category : SqlModel
	{
		public string Name { get; set; }

		public int? ParentId { get; set; }

		public string ParentPath { get; set; }


		public string GetFullPath ()
		{
			if (ParentId == null)
				return Name;
			else
				return string.Format ("{0} > {1}", ParentPath, Name);
		}

		public string GetExportPath ()
		{
			var path = GetFullPath ();
			return string.Join (";", path.Split ('>').Select (c => c.Trim ()));
		}
	}
}