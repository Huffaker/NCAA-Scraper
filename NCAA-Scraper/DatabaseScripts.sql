
CREATE SCHEMA [STG]
GO

CREATE TABLE STG.Teams (
	TeamID INT NOT NULL,
	TeamName NVARCHAR(125) NOT NULL
)

CREATE TABLE STG.Players (
	PlayerID INT NOT NULL,
	PlayerName NVARCHAR(125) NOT NULL,
	TeamID INT NOT NULL,
	YearCode INT NOT NULL,
	PlayerPosition NVARCHAR(10) NULL,
	PlayerYear NVARCHAR(10) NULL
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
	TeamName NVARCHAR(125) NOT NULL
)

CREATE TABLE dbo.Players (
	PlayerID INT NOT NULL,
	PlayerName NVARCHAR(125) NOT NULL,
	TeamID INT NOT NULL,
	YearCode INT NOT NULL,
	PlayerPosition NVARCHAR(10) NULL,
	PlayerYear NVARCHAR(10) NULL
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
	ON (target.[TeamID] = source.[TeamID])
WHEN NOT MATCHED BY target THEN INSERT
	(TeamID, TeamName)
	VALUES(source.TeamID, source.TeamName);
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
