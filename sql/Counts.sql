--delete from hangfire.job;
--delete from Videomatic.Playlists;
--delete from Videomatic.videos;

select 
(select count(1) from Videomatic.Playlists) as PlaylistCount,
(select count(1) from Videomatic.Videos) as VideosCount ,
(select count(1) from Videomatic.PlaylistVideos )as PlaylistVideosCount ,
(select count(1) from Videomatic.Transcripts) as TranscriptsCount 
--(select count(1) from hangfire.job) as JobCount;
--3 playlists: 3	347	353	735

--select * from Videomatic.Artifacts;
select * from Videomatic.Playlists;
select * from Videomatic.PlaylistVideos;
select * from Videomatic.Videos;
--select * from Videomatic.Transcripts;
--select * from HangFire.Job;

--1	35	34	51	16777
--2	300	305	609	171192
--2	300	305	610	171582
--2	300	305	610	171582
-----------
--3	348	353	655	184446
--3	348	353	655	184446
