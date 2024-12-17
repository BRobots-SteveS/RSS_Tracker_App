# RSS_Tracker_App
.Net8 MAUI app and API for tracking generic RSS, mRSS and iTunes RSS feeds

## Powershell commands
Due to the Data layer being in a different solution this project can not be scaffolded by using the basic commands.
`add-migration InitialMigration` and `update-database` do not work directly here.
You need to use the following:
- `dotnet ef migrations add InitialMigration --project Rss_Tracking_Data --startup-project Rss_Tracking_Api` to create the migration
- `dotnet ef database update InitialMigration --project Rss_Tracking_Data --startup-project Rss_Tracking_Api` to apply the migration
