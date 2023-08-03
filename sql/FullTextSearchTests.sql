--SELECT is_fulltext_enabled FROM sys.databases WHERE database_id = DB_ID() -- Installed?
--exec sp_fulltext_database 'enable'; // Installs
--select * FROM sys.fulltext_catalogs

--CREATE FULLTEXT CATALOG FTCVideomatic AS DEFAULT;

--CREATE FULLTEXT INDEX ON dbo.Videos(Name, Description) 
--  KEY INDEX PK_Videos ON FTCVideomatic 
--  WITH STOPLIST = OFF, CHANGE_TRACKING AUTO;

--DROP FULLTEXT INDEX ON dbo.Videos;

--SELECT * FROM FREETEXTTABLE(dbo.Videos, Description, 'android AND shiva')
--SELECT count(*) FROM Videos;
--SELECT count(*) FROM TranscriptLines;
--SELECT * FROM Videos where CONTAINS(Description, 'shiva');

--https://www.sitepoint.com/sql-server-full-text-search-protips-part-2-contains-vs-freetext/
--CONTAINS is specific, but it allows me to create and/or statements on specific properties
--SELECT * FROM Videos where CONTAINS(Name, 'FORMSOF (Inflectional, shiva) or FORMSOF(Inflectional, god)');
--SELECT * FROM TranscriptLines where CONTAINS(Text, '(FORMSOF (Inflectional, shiva) or FORMSOF(Inflectional, flame)) or video');

--FREETEXT is more automatic
--SELECT * FROM TranscriptLines where FREETEXT(*, 'feet video');

---------------------------------------------------

--SELECT ftt.*, v.*
--FROM Videos v 
----JOIN FREETEXTTABLE(Videos, Name, 'language') as ftt
----JOIN FREETEXTTABLE(Videos, Name, 'god') as ftt
--JOIN FREETEXTTABLE(Videos, Name, 'learnt gods') as ftt -- 3 results: it picks up on 'or' and include
--ON (ftt.[Key]=v.Id)
--ORDER BY ftt.RANK DESC;

--SELECT ftt.*, v.*
--FROM Videos v 
----JOIN CONTAINSTABLE(Videos, Name, 'language OR god') as ftt -- 1 result (Sign video)
--JOIN CONTAINSTABLE(Videos, Name, 'FORMSOF(Inflectional, learnt) OR FORMSOF(Inflectional,god)') as ftt -- 2 results (Sign video and If Reality...)
--ON (ftt.[Key]=v.Id)
--ORDER BY ftt.RANK DESC

----------------------

DECLARE @topRank int
DECLARE @freeText nvarchar(30) = 'huxley god language google';

set @topRank=(SELECT MAX(RANK) FROM FREETEXTTABLE(Videos, *, @freeText, 1))

SELECT 
  (CAST(ftt.RANK as DECIMAL)/@topRank) as matchpercent,
  ftt.*, 
  v.*
FROM Videos v 
INNER JOIN FREETEXTTABLE(Videos, *, @freeText) as ftt
ON (ftt.[Key]=v.Id)
ORDER BY ftt.RANK DESC;