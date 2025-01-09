# RSS_Tracker_App
.Net8 MAUI app and API for tracking generic RSS, mRSS and iTunes RSS feeds
This is a school project for a single semester

## Powershell commands
Due to the Data layer being in a different solution this project can not be scaffolded by using the basic commands.
`add-migration InitialMigration` and `update-database` do not work directly here.

You need to use the following:
- `dotnet ef migrations add InitialMigration --project Rss_Tracking_Data --startup-project Rss_Tracking_Api` to create the migration
- `dotnet ef database update InitialMigration --project Rss_Tracking_Data --startup-project Rss_Tracking_Api` to apply the migration

## Changes to run
Set up a Dev Tunnel for the API.

In the MAUI app project navigate to `/Repositories/BaseRepository.cs` and change the BaseURL to your Dev Tunnel URL.

## Grading list
### Basic requirements
- [x] Built in dotnet MAUI for Android and Windows
- [x] App contains at least 5 screens
- [ ] Flyout / Tab navigation is provided (sadly Disabled due to an extra)
- [x] Reusable styling is applied and reused at least four times and provided into the App.xaml
- [x] Filtering and sorting can be applied to the lists in the application
- [ ] The app contains a settings page
- [x] The app utilizes binding
- [x] The app uses compiled binding
- [x] There is at least 1 useful custom converter/behavior implemented
- [x] MVVM pattern is upheld through-out the application
- [x] Data in the application is fetched from an external Database which is surfaced by a RESTful API (also in the project)

### Extra's
- [ ] Usage of the phone's camera to make pictures or videos in the application
- [x] Custom logo for the application
- [x] Custom splash screen for the application
- [x] Advanced queries on the Database through filtering
- [x] Advanced CollectionViews ex. Custom Datatemplates, Pull to refresh, grouping
- [x] Use of external packages ex. SyncFusion [UraniumUI](https://github.com/enisn/UraniumUI)
- [ ] Implementation of complex MVVM : Messaging
- [ ] Implementation of gestures
- [x] Clean UI/UX design
- [ ] Usage of local SQLite storage on the device
- [x] Usage of external APIs outside the Database (rss feed URLs are parsed)