using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NCAA_Scraper.Models;

namespace NCAA_Scraper.test
{
	[TestClass]
	public class ScraperTests
	{

		//A few quick tests to check that screen scraping logic still works

		[TestMethod]
		public void TeamScrapPullsData()
		{
			//Initialize
			var teamScraper = new TeamListScraper();
			var teamList = teamScraper.TeamList;
			//Assert
			Assert.IsNotNull(teamList);
			Assert.IsTrue(teamList.Count > 0);
			Console.WriteLine("Team List Result Count: " + teamList.Count);
		}

		[TestMethod]
		public void PlayerScrapPullsData()
		{
			//Initialize
			var teamList = new List<TeamModel> {new TeamModel {TeamID = 2, TeamName = "Abilene Christian", YearCode = 12260 } };
            var playerScraper = new PlayerListScraper(teamList);
			var playerList = playerScraper.PlayerList;
			//Assert
			Assert.IsNotNull(playerList);
			Assert.IsTrue(playerList.Count > 0);
			Console.WriteLine("Player List Result Count: " + playerList.Count);
		}

		[TestMethod]
		public void GameScrapPullsData()
		{
			//Initialize
			var playerList = new List<PlayerModel> {new PlayerModel { TeamID = 26172, PlayerID = 1620841, YearCode = 12260 } };
			var gameScraper = new GameListScraper(playerList);
			var games = gameScraper.GameList;
			//Assert
			Assert.IsNotNull(games);
			Assert.IsTrue(games.Count > 0);
			Console.WriteLine("Game List Result Count: " + games.Count);
		}
	}
}
