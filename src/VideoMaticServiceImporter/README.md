# VideoMaticServiceImporter

This is a background service that executes long jobs using [Hangfire](https://www.hangfire.io/).

Notes:
-This service currently imports videos and playlists from YouTube
-Schedules the import of the video transcripts
-Schedules artifact production