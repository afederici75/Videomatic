--delete from hangfire.job;
--delete from Playlists;
--delete from videos;

select 
(select count(1) from Playlists) as PlaylistCount,
(select count(1) from videos) as VideosCount ,
(select count(1) from PlaylistVideos )as PlaylistVideosCount ,
(select count(1) from Transcripts) as TranscriptsCount ,
(select count(1) from TranscriptLines) as TranscriptLinesCount ,
(select count(1) from hangfire.job) as JobCount;

select * from Playlists;
select * from HangFire.Job;
