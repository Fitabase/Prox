using System;
using System.Threading.Tasks;
using Dropbox.Api;
using System.IO;
using System.Text;
using Dropbox.Api.Files;

namespace Prox
{
	public class DropboxUtils : IExporter
	{
		public async Task<bool> SendAsync (string csv, string participantId)
		{
			var fileName = string.Format (Resources.Export.FileNameFormat, participantId, DateTime.Now);
			var filePath = string.Format ("/{0}", fileName);
			try {
				using (var dbx = new DropboxClient (Resources.Export.Dropbox.AccessToken)) {
					using (var mem = new MemoryStream (Encoding.UTF8.GetBytes (csv))) {
						await dbx.Files.UploadAsync (filePath, WriteMode.Overwrite.Instance, body: mem);
					}
					return true;
				}
			} catch {
				return false;
			}
		}
	}
}