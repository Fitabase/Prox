using System;
using SQLite.Net;
using SQLite.Net.Attributes;

namespace Prox
{
	public interface ISqLiteService
	{
		SQLiteConnection GetConnection ();
	}

	public class EncounterRecord
	{
		[PrimaryKey, AutoIncrement]
		public long ID { get; set; }

		public DateTime Time { get; set; }

		public string Uuid { get; set; }

		public int Major { get; set; }

		public int Minor{ get; set; }
	}
}