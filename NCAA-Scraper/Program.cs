using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NCAA_Scraper.Models;

namespace NCAA_Scraper
{
	class Program
	{
		static void Main(string[] args)
		{
			//Check for existing data (Useful for recoving from a crash, skips to only players without data)
			var playerList = BulkInsert.ReadPlayers(ConnectionString);
			if (playerList.Count == 0 || !RecoveryMode)
			{
				//Get team list
				var teamScraper = new TeamListScraper();
				var teamList = teamScraper.TeamList;
				BulkInsert.LoadTeams(teamList, ConnectionString);

				//Get list of players for every team
				var playerScraper = new PlayerListScraper(teamList, YearList);
				playerList = playerScraper.PlayerList.OrderBy(x => x.YearCode).ThenBy(x => x.PlayerID).ToList();
				BulkInsert.LoadPlayers(playerList, ConnectionString);
			}
			//Begin pulling game data
			var gameScrpaer = new GameListScraper(playerList);
			var games = gameScrpaer.GameList;
			BulkInsert.LoadGames(games, ConnectionString);

			Console.WriteLine("Data pull complete, press any key to continue...");
			Console.ReadLine();
		}

		//SQL Database Connection String
		public static readonly string ConnectionString = "Data Source = (localdb)\\mssqllocaldb; " +
		" Integrated Security=true;" +
		"Initial Catalog=NCAAStats;";

		//Comment out the seasons you don't want to pull
		public static List<YearModel> YearList = new List<YearModel>
		{
			  new YearModel() {YearCode = 12260, SeasonName = "2015-2016"},
			//new YearModel() {YearCode = 12020, SeasonName = "2014-2015"},
			//new YearModel() {YearCode = 11540, SeasonName = "2013-2014"},
			//new YearModel() {YearCode = 11220, SeasonName = "2012-2013"},
			//new YearModel() {YearCode = 10740, SeasonName = "2011-2012"},
			//new YearModel() {YearCode = 10440, SeasonName = "2010-2011"},
			//new YearModel() {YearCode = 10260, SeasonName = "2009-2010"}
		};

		//Set this flag if you are attempting to reboot the program after a crash
		const bool RecoveryMode = false;
	}
}
