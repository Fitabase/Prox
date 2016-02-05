using System.Threading.Tasks;

namespace Prox
{
	public interface IExporter
	{
		Task<bool> SendAsync (string csv, string participantId);
	}
}