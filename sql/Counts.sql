--delete from hangfire.job;
--delete from Videomatic.Playlists;
--delete from Videomatic.videos;

select 
(select count(1) from Videomatic.Playlists) as PlaylistCount,
(select count(1) from Videomatic.Videos) as VideosCount ,
(select count(1) from Videomatic.PlaylistVideos )as PlaylistVideosCount ,
(select count(1) from Videomatic.Transcripts) as TranscriptsCount 

select * from Videomatic.Playlists;
select * from Videomatic.Videos;
select * from Videomatic.Transcripts;
--select * from HangFire.Job;
