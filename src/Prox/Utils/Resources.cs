namespace Prox
{
	public static class Resources
	{
		public static class Strings
		{
			public const string ApplicationName = "Prox";

			public const string DefaultCategoryName = "Default";
			public const string DefaultLockScreenText = "The settings for this experiment have been locked by the study admin.";
			public const string DefaultParticipantViewContactText = "Questions?";
			public const string DefaultParticipantViewWelcomeText = "Your phone is currently recording your encounters with proximity sensors.";
			public const string DefaultParticipantViewContactEmail = "email@server.com";


			public const string AddBeaconTitle = "Add Beacon";
			public const string SelectCategoryTitle = "Press to select";

			public const string CategoryDeleteTitle = "Category deleting";
			public const string CategoryDeleteQuestion = "Category will be deleted from all beacons. Continue?";

			public const string BeaconDeleteTitle = "Beacon deleting";
			public const string BeaconDeleteQuestion = "Beacon will be deleted. Continue?";

			public const string ExportErrorMessage = "Error while exporing";
			public const string ExportErrorNoParticipantMessage = "No ParticipantID configured";
			public const string ExportSuccessMessage = "Exported successfully";

			public const string ImportQuestionTitle = "Import";
			public const string ImportQuestionMessage = "Process import?";
			public const string ImportSuccessMessageFormat = "Import completed successfully. Imported {0}, updated {1}, skipped {2}";
			public const string ImportErrorWrongFormat = "Import JSON has wrong format";
			public const string ImportBeaconOverwriteQuestionTitle = "Beacon overwriting";
			public const string ImportBeaconOverwriteQuestionMessageFormat = "Overwrite beacon name from {0} to {1} and it's category from {2} to {3}";

			public const string LoginPasswordWrongMessage = "Passphrase wrong";

			public const string NotificaionMessageFormat = "{0} - {1}";

			public const string Success = "Success";
			public const string Error = "Error";
			public const string Yes = "Yes";
			public const string No = "No";
			public const string Ok = "OK";
			public const string Cancel = "Cancel";
		}

		public static class Sql
		{
			public const string DatabaseName = "encounters.db3";
			public const string EncountersTableName = "Encounters";
		}

		public static class Export
		{
			public const string FileNameFormat = "export_{0}_{1:dd.MM.yyyy_HH.mm}.csv";

			public static class Email
			{
				public const string FromEmail = "email@sever.com";
				public const string FromNameFormat = "Participant {0}";
				public const string ToEmail = "email@sever.com";
				public const string ToName = "Your team name";
				public const string SubjectFormat = "Export from {0} created at {1:dd.MM.yyyy HH:mm}";
				public const string SmtpHost = "smtp.server.com";
				public const int SmtpPort = 587;
				public const string SmtpPassword = "smtpPassword";
			}

			public static class Dropbox
			{
				public const string AccessToken = "place_dropbox_access_token_here";
				public const string Folder = "ProxTest";
			}
		}
	}
}