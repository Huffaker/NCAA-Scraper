
CREATE SCHEMA [STG]
GO

CREATE TABLE STG.Teams (
	TeamID INT NOT NULL,
	TeamName NVARCHAR(125) NOT NULL,
	YearCode INT NOT NULL,
)

CREATE TABLE STG.Players (
	PlayerID INT NOT NULL,
	PlayerName NVARCHAR(125) NOT NULL,
	TeamID INT NOT NULL,
	YearCode INT NOT NULL,
	PlayerPosition NVARCHAR(10) NULL,
	PlayerYear NVARCHAR(10) NULL,
	[PlayerHeight] NVARCHAR(10) NULL,
    [GamesPlayed] INT NULL,
    [GamesStarted] INT NULL
)

CREATE TABLE STG.Games (
	TeamID INT NOT NULL,
	OpponentTeamID INT NOT NULL,
	GameDate DATETIME NOT NULL,
	YearCode INT NOT NULL,
	WasHomeGame BIT NULL,
	Win BIT NULL,
	TeamPoints INT NOT NULL,
	OpponentTeamPoints INT NOT NULL,
	PlayerID INT NOT NULL,
	MinutesPlayed INT NULL,
	FieldGoalsMade INT NULL,
	FieldGoalAttempts INT NULL,
	ThreePointsMade	INT NULL,
	ThreePointAttempts INT NULL,
	FreeThrows INT NULL,
	FreeThrowAttempts INT NULL,
	Points INT NULL,
	OffensiveReBounds INT NULL,
	DefensiveReBounds INT NULL,
	Assists	INT NULL,
	TurnOvers INT NULL,
	Steals INT NULL,
	Blocks INT NULL,
	Fouls INT NULL
)

CREATE TABLE dbo.Teams (
	TeamID INT NOT NULL,
	TeamName NVARCHAR(125) NOT NULL,
	YearCode INT NOT NULL
)

CREATE TABLE dbo.Players (
	PlayerID INT NOT NULL,
	PlayerName NVARCHAR(125) NOT NULL,
	TeamID INT NOT NULL,
	YearCode INT NOT NULL,
	PlayerPosition NVARCHAR(10) NULL,
	PlayerYear NVARCHAR(10) NULL,
	[PlayerHeight] NVARCHAR(10) NULL,
    [GamesPlayed] INT NULL,
    [GamesStarted] INT NULL
)

CREATE TABLE dbo.Games (
	TeamID INT NOT NULL,
	OpponentTeamID INT NOT NULL,
	GameDate DATETIME NOT NULL,
	YearCode INT NOT NULL,
	WasHomeGame BIT NULL,
	Win BIT NULL,
	TeamPoints INT NOT NULL,
	OpponentTeamPoints INT NOT NULL,
	PlayerID INT NOT NULL,
	MinutesPlayed INT NULL,
	FieldGoalsMade INT NULL,
	FieldGoalAttempts INT NULL,
	ThreePointsMade	INT NULL,
	ThreePointAttempts INT NULL,
	FreeThrows INT NULL,
	FreeThrowAttempts INT NULL,
	Points INT NULL,
	OffensiveReBounds INT NULL,
	DefensiveReBounds INT NULL,
	Assists	INT NULL,
	TurnOvers INT NULL,
	Steals INT NULL,
	Blocks INT NULL,
	Fouls INT NULL
)
GO

CREATE PROCEDURE STG.pMergeTeams AS
BEGIN

MERGE dbo.Teams AS target
USING STG.Teams AS source
	ON (target.[TeamID] = source.[TeamID] AND target.YearCode = source.YearCode)
WHEN NOT MATCHED BY target THEN INSERT
	(TeamID, TeamName, YearCode)
	VALUES(source.TeamID, source.TeamName, source.YearCode);
END
GO

GO
CREATE PROCEDURE [STG].[pMergePlayers] AS
BEGIN

MERGE dbo.Players AS target
USING STG.Players AS source
	ON (target.[PlayerID] = source.[PlayerID]
	AND target.[TeamID] = source.[TeamID]
	AND target.[YearCode] = source.[YearCode])
WHEN NOT MATCHED BY target THEN INSERT
	([PlayerID]
      ,[PlayerName]
      ,[TeamID]
      ,[YearCode]
      ,[PlayerPosition]
      ,[PlayerYear]
	  ,[PlayerHeight]
      ,[GamesPlayed]
      ,[GamesStarted])
	VALUES(
	source.[PlayerID]
      ,source.[PlayerName]
      ,source.[TeamID]
      ,source.[YearCode]
      ,source.[PlayerPosition]
      ,source.[PlayerYear]
	  ,source.[PlayerHeight]
      ,source.[GamesPlayed]
      ,source.[GamesStarted]);
END

GO

GO
CREATE PROCEDURE STG.pMergeGames AS
BEGIN

MERGE dbo.Games AS target
USING STG.Games AS source
	ON (target.[PlayerID] = source.[PlayerID]
	AND target.[TeamID] = source.[TeamID]
	AND target.[YearCode] = source.[YearCode]
	AND target.[GameDate] = source.[GameDate]
	AND target.[OpponentTeamID] = source.[OpponentTeamID])
WHEN NOT MATCHED BY target THEN INSERT
	([TeamID]
      ,[OpponentTeamID]
      ,[GameDate]
      ,[YearCode]
      ,[WasHomeGame]
      ,[Win]
      ,[TeamPoints]
      ,[OpponentTeamPoints]
      ,[PlayerID]
      ,[MinutesPlayed]
      ,[FieldGoalsMade]
      ,[FieldGoalAttempts]
      ,[ThreePointsMade]
      ,[ThreePointAttempts]
      ,[FreeThrows]
      ,[FreeThrowAttempts]
      ,[Points]
      ,[OffensiveReBounds]
      ,[DefensiveReBounds]
      ,[Assists]
      ,[TurnOvers]
      ,[Steals]
      ,[Blocks]
      ,[Fouls])
	VALUES(
	   source.[TeamID]
      ,source.[OpponentTeamID]
      ,source.[GameDate]
      ,source.[YearCode]
      ,source.[WasHomeGame]
      ,source.[Win]
      ,source.[TeamPoints]
      ,source.[OpponentTeamPoints]
      ,source.[PlayerID]
      ,source.[MinutesPlayed]
      ,source.[FieldGoalsMade]
      ,source.[FieldGoalAttempts]
      ,source.[ThreePointsMade]
      ,source.[ThreePointAttempts]
      ,source.[FreeThrows]
      ,source.[FreeThrowAttempts]
      ,source.[Points]
      ,source.[OffensiveReBounds]
      ,source.[DefensiveReBounds]
      ,source.[Assists]
      ,source.[TurnOvers]
      ,source.[Steals]
      ,source.[Blocks]
      ,source.[Fouls]);
END
GO

CREATE VIEW dbo.RemainingPlayerList AS

SELECT 
	P.PlayerID
	,P.PlayerName
	,P.TeamID
	,P.YearCode
	,P.PlayerPosition
	,P.PlayerYear
	,P.PlayerHeight
	,P.GamesPlayed
	,P.GamesStarted
FROM dbo.Players P
LEFT JOIN(SELECT DISTINCT PlayerID, YearCode FROM dbo.Games) G ON G.[PlayerID] = P.PlayerID AND G.[YearCode] = P.[YearCode]
WHERE G.PlayerID IS NULL
GO

CREATE TABLE dbo.Seasons(
	YearCode INT NOT NULL,
	Season NVARCHAR(25) NOT NULL,
)
--Sead the season table
INSERT INTO dbo.Seasons
(YearCode, Season)
VALUES
(12260, '2015-2016'),
(12020, '2014-2015'),
(11540, '2013-2014'),
(11220, '2012-2013'),
(10740, '2011-2012'),
(10440, '2010-2011'),
(10260, '2009-2010')