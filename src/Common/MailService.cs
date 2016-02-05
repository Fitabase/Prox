using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text;
using Xamarin.Forms;
#if __IOS__
using Prox.iOS;
#endif 
#if __ANDROID__
using Prox.Droid;
#endif 
using System;
using System.Threading.Tasks;

[assembly:Dependency (typeof(MailService))]

#if __IOS__
namespace Prox.iOS
#endif 

#if __ANDROID__
namespace Prox.Droid
#endif
{
	public class MailService : IMailService
	{
		public async Task<bool> SendAsync (string csv, string participantId)
		{
			try {
				var fromAddress = new MailAddress (Resources.Export.Email.FromEmail, string.Format (Resources.Export.Email.FromNameFormat, participantId));
				var toAddress = new MailAddress (Resources.Export.Email.ToEmail, Resources.Export.Email.ToName);
				var exportTime = DateTime.Now;
				var subject = string.Format (Resources.Export.Email.SubjectFormat, participantId, exportTime);
				var fileName = string.Format (Resources.Export.FileNameFormat, participantId, exportTime);

				using (var smtp = new SmtpClient {
					Host = Resources.Export.Email.SmtpHost,
					Port = Resources.Export.Email.SmtpPort,
					EnableSsl = true,
					DeliveryMethod = SmtpDeliveryMethod.Network,
					UseDefaultCredentials = false,
					Credentials = new NetworkCredential (fromAddress.Address, Resources.Export.Email.SmtpPassword)
				}) {
					using (var message = new MailMessage (fromAddress, toAddress) { Subject = subject, Body = string.Empty }) {
						using (var contentStream = new MemoryStream (Encoding.UTF8.GetBytes (csv))) {
							message.Attachments.Add (new Attachment (contentStream, fileName));

							await Task.Run (() => smtp.Send (message));

							return true;
						}
					}
				}
			} catch {
				return false;
			}
		}
	}
}