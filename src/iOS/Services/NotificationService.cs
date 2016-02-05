using UIKit;
using Xamarin.Forms;
using Prox.iOS;
using Foundation;

[assembly:Dependency (typeof(NotificationService))]

namespace Prox.iOS
{
	public class NotificationService : INotificationService
	{
		public void Start ()
		{
			var settings = UIUserNotificationSettings.GetSettingsForTypes (UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null);
			UIApplication.SharedApplication.RegisterUserNotificationSettings (settings);
		}

		public void Show (string message)
		{
			Device.BeginInvokeOnMainThread (() => {
				var notification = new UILocalNotification {
					FireDate = NSDate.FromTimeIntervalSinceNow (0),
					AlertBody = message
				};
				UIApplication.SharedApplication.ScheduleLocalNotification (notification);
			});
		}
	}
}