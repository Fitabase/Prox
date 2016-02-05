using SQLite.Net;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prox
{
	public class BeaconsSettings : SqlSettingsBase<Beacon>
	{
		private readonly CategorySettings _categorySettings;

		public BeaconsSettings (SQLiteConnection sqlConnection, CategorySettings categorySettings) : base (sqlConnection)
		{
			_categorySettings = categorySettings;
		}

		protected override void Initialize (SQLiteConnection sqlConnection)
		{
			sqlConnection.CreateTable<Beacon> ();
			sqlConnection.CreateTable<BeaconDevice> ();
		}

		public void RemoveCategoryFromBeacons (int categoryId)
		{
			foreach (var item in Collection.Where(b => b.CategoryId == categoryId)) {
				item.CategoryId = null;
				item.Category = null;

				Update (item);
			}
		}

		public string Export ()
		{
			var exportItems = Items.Where (i => i.Device != null).Select (i => {
				return new ExportBeacon {
					Name = i.Name,
					UUID = i.Device.Uuid,
					Major = i.Device.Major,
					Minor = i.Device.Minor,
					Category = i.Category?.GetExportPath ()
				};
			});

			return JsonConvert.SerializeObject (exportItems);
		}

		public async Task<ImportResult> Import (string json)
		{
			ExportBeacon[] beacons;
			try {
				beacons = JsonConvert.DeserializeObject<ExportBeacon[]> (json);
			} catch {
				return new ImportResult {
					IsSuccess = false
				};
			}

			var imported = new List<Beacon> ();
			var updated = new List<Beacon> ();
			var skipped = new List<Beacon> ();

			//find all beacons which includes device information
			var withDeviceInfo = beacons.Where (b => !string.IsNullOrEmpty (b.UUID));
			foreach (var item in withDeviceInfo) {
				var existing = Items.FirstOrDefault (i => item.UUID == i.Device?.Uuid && item.Major == i.Device?.Major && item.Minor == i.Device?.Minor);
	
				if (existing != null) {
					//we shouldn't do any process if no differs
					var isDiffers = existing.Name != item.Name || existing.Category?.GetExportPath () != item.Category;
					if (!isDiffers) {
						skipped.Add (existing);
						continue;
					}
						
					//ask to overwrite existing
					var message = string.Format (Resources.Strings.ImportBeaconOverwriteQuestionMessageFormat, existing.Name, item.Name, existing.Category?.GetExportPath (), item.Category);
					var isOverwrite = await App.Current.MainPage.DisplayAlert (Resources.Strings.ImportBeaconOverwriteQuestionTitle, message, Resources.Strings.Yes, Resources.Strings.No);

					if (isOverwrite) {
						existing.Name = item.Name;
						existing.Category = _categorySettings.GetOrCreateCategoryByPath (item.Category);
						Update (existing);

						updated.Add (existing);
					}
				} else {
					var beacon = new Beacon {
						Name = item.Name,
						Category = _categorySettings.GetOrCreateCategoryByPath (item.Category),
						Device = new BeaconDevice {
							Uuid = item.UUID,
							Major = item.Major,
							Minor = item.Minor
						}
					};
					Add (beacon);
					imported.Add (beacon);
				}
			}   
				
			return new ImportResult {
				IsSuccess = true,
				Imported = imported.ToArray (),
				Updated = updated.ToArray (),
				Skipped = skipped.ToArray()
			};
		}
	}
}