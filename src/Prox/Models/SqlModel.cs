using SQLite.Net.Attributes;

namespace Prox
{
	public abstract class SqlModel
	{
		[AutoIncrement, PrimaryKey]
		public int? Id { get; set; }
	}
}