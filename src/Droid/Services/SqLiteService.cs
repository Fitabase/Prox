using System;
using System.IO;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using Xamarin.Forms;
using Prox.Droid;

[assembly:Dependency (typeof(SqLiteService))]

namespace Prox.Droid
{
	public class SqLiteService : ISqLiteService
	{
		public SQLiteConnection GetConnection ()
		{
			string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var path = Path.Combine(documentsPath, Resources.Sql.DatabaseName);
			var conn = new SQLiteConnection(new SQLitePlatformAndroid(), path);

			return conn;
		}
	}
}