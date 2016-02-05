using System.Collections.Generic;
using SQLite.Net;
using System.Linq;
using System.Collections.ObjectModel;
using SQLiteNetExtensions.Extensions;

namespace Prox
{
	public abstract class SqlSettingsBase<T> where T : SqlModel
	{
		public IReadOnlyCollection<T> Items { get { return new ReadOnlyCollection<T> (Collection); } }


		protected IList<T> Collection { get; private set; }

		protected SQLiteConnection Connection { get; private set; }


		protected SqlSettingsBase (SQLiteConnection sqlConnection)
		{
			Connection = sqlConnection;

			Initialize (sqlConnection);

			Collection = Connection.GetAllWithChildren<T> ();
		}


		protected abstract void Initialize (SQLiteConnection sqlConnection);

		public virtual T Add (T model)
		{
			Connection.InsertOrReplaceWithChildren (model, true);

			Collection.Add (model);

			return model;
		}

		public virtual void Delete (int id)
		{
			var model = Collection.First (c => c.Id == id);
			Connection.Delete<T> (model.Id);

			Collection.Remove (model);
		}

		public virtual void Update (T model)
		{
			Connection.InsertOrReplaceWithChildren (model);
		}
	}
}