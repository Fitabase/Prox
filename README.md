# Prox
A simple beacon test app for research. Captures enter / exit events, saves data locally, and exports to .csv

## Initial Configuration Settings
1. Open /Prox/Utils/Resources.cs
2. (option A) Set FromEmail, ToEmail, SmtpHost,SmtpPassword settings or
3. (option B) Set Dropbox 
4. If you'd like to use TestFairy for distribution, set TestFairy key in /Prox/Prox.iOS/AppDelegate.cs
5. Compile using Xamarin Studio https://(www.xamarin.com) and XCode (subscriptions to each required).