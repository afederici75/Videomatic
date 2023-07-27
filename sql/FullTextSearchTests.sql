--SELECT is_fulltext_enabled FROM sys.databases WHERE database_id = DB_ID() -- Installed?
--exec sp_fulltext_database 'enable'; // Installs

--select * FROM sys.fulltext_catalogs

CREATE FULLTEXT CATALOG FTCVideomatic AS DEFAULT;

CREATE FULLTEXT INDEX ON dbo.Videos(Name) 
  KEY INDEX PK_Videos ON FTCVideomatic 
  WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;