--delete from hangfire.job;
--delete from Videomatic.Playlists;
--delete from Videomatic.videos;

select 
(select count(1) from Videomatic.Playlists) as PlaylistCount,
(select count(1) from Videomatic.Videos) as VideosCount ,
(select count(1) from Videomatic.PlaylistVideos )as PlaylistVideosCount ,
(select count(1) from Videomatic.Transcripts) as TranscriptsCount ,
(select count(1) from Videomatic.TranscriptLines) as TranscriptLinesCount 
--(select count(1) from hangfire.job) as JobCount;

select * from Videomatic.Playlists;
select * from Videomatic.Videos;
select * from Videomatic.Transcripts;
select * from HangFire.Job;

--PlaylistCount	VideosCount	PlaylistVideosCount	TranscriptsCount	TranscriptLinesCount
--3	348	353	641	180886