using System;
using SQLite;
using System.IO;
using Xamarin.Forms;
using Prox.iOS;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;

[assembly: Dependency (typeof (SqLiteService))]

namespace Prox.iOS
{
	public class SqLiteService : ISqLiteService
	{
		public SQLiteConnection GetConnection ()
		{
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var libraryPath = Path.Combine (documentsPath, "..", "Library");
			var path = Path.Combine(libraryPath, Resources.Sql.DatabaseName);
			var conn = new SQLiteConnection(new SQLitePlatformIOS(), path);

			return conn;
		}
	}
}