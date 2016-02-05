namespace Prox
{
	public class BeaconDeviceViewModel : ViewModelBase
	{
		public bool IsConnected { get { return _isconnected; } set { Set (ref _isconnected, value); } }

		public string Uuid { get { return _uuid; } set { Set (ref _uuid, value); } }

		public int Major { get { return _major; } set { Set (ref _major, value); } }

		public int Minor { get { return _minor; } set { Set (ref _minor, value); } }


		private bool _isconnected;
		private string _uuid;
		private int _major;
		private int _minor;


		public void Set (BeaconDevice device)
		{
			IsConnected = device != null;
			if (IsConnected) {
				Uuid = device.Uuid;
				Minor = device.Minor;
				Major = device.Major;
			}
		}
	}
}