using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using TestFairyLib;

namespace Prox.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			LoadApplication (new App ());

			//TestFairy.Begin ("PUT TEST FAIRY KEY HERE");

			return base.FinishedLaunching (app, options);
		}
	}
}