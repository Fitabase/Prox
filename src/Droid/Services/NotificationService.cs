using Xamarin.Forms;
using Prox.Droid;
using Android.Support.V4.App;
using Android.App;
using Android.Content;

[assembly:Dependency (typeof(NotificationService))]

namespace Prox.Droid
{
	public class NotificationService : INotificationService
	{
		private const int NotificationId = 1000;

		public void Start ()
		{
		}

		public void Show (string message)
		{
			var builder = new NotificationCompat.Builder (Forms.Context)
				.SetAutoCancel (true)                    
				.SetContentTitle ("Button Clicked")    
				.SetSmallIcon (Resource.Drawable.icon)  
				.SetContentText (message);

			var notificationManager = (NotificationManager)Forms.Context.GetSystemService (Context.NotificationService);
			notificationManager.Notify (NotificationId, builder.Build ());
		}
	}
}