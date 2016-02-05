# Prox
A simple beacon test app for research. Captures enter / exit events, saves data locally, and exports to .csv

![Prox Setup Screens](https://github.com/Fitabase/Prox/blob/master/screenshots/01-Setting_PPT_ID.gif?raw=true)

##Purpose
An increasingly used mechanism for establishing context for mobile phones is the strategic placement of proximity sensors, mostly iBeacon that work in connection with downloadable consumer apps. This project provides a tool to better understand how beacons might be used in academic research, and in the larger context might feed in to a JITAI (Just In Time Adaptive Intevention) behavioral system. Prox can be downloaded on to a participant's phone, configured to individual Estimote (https://www.estimote.com) brand beacons, and will store locally all the times when a beacon was within range of the phone. It was designed to be a serverless, simple test tool.

## Authorship
Prox is a small agile experiment created by Fitabase and Arizona State University's Designing Health Lab. It was funded by the Health Data Exploration Project (http://hdexplore.calit2.net/), with generous funding by the Robert Wood Johnson Foundation (http://www.rwjf.org).

## Initial Pre-Build Configuration Settings
1. Open /Prox/Utils/Resources.cs
2. (option A) Set FromEmail, ToEmail, SmtpHost,SmtpPassword settings or
3. (option B) Set Dropbox 
4. If you'd like to use TestFairy for distribution, set TestFairy key in /Prox/Prox.iOS/AppDelegate.cs
5. Compile using Xamarin Studio https://(www.xamarin.com) and XCode (subscriptions to each required).
6. 
