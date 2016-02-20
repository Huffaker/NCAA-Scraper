using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCAA_Scraper.Models;
using Newtonsoft.Json;

namespace NCAA_Scraper
{
	public class GameListScraper: Scraper
	{
		private readonly PlayerModel _player;
		public List<GameModel> GameList { get; private set; }
		
		public GameListScraper(IEnumerable<PlayerModel> playerList)
		{

			javascriptCode = @"
var results = [];
var runLoop = function() {
	$('#game_breakdown_div').find('tbody').eq(1).find('tr:not("".heading"")').find('a:not(""[target],[title]"")').each(function(index, i){ 
      var row = $(i).closest('tr');
		results.push({
			GameDate: row.find('td').eq(0).text(),
			WasHomeGame: !($(i).text().substring(0,1)=='@'),
			OpponentTeamID:$(i).attr('href').split('/')[2],
			Win: row.find('td').eq(2).text().trim().substring(0,1)=='W',
			TeamPoints: row.find('td').eq(2).text().replace(/[A-Z) ]/g,'').split('-')[0],
			OpponentTeamPoints: row.find('td').eq(2).text().replace(/[A-Z) ]/g,'').split('-')[1].split('(')[0],
			MinutesPlayed: row.find('td').eq(3).text().trim().split(':')[0].replace(/[^0-9.]/g,''),
			FieldGoalsMade: row.find('td').eq(4).text().trim().replace(/[^0-9.]/g,''),
			FieldGoalAttempts: row.find('td').eq(5).text().trim().replace(/[^0-9.]/g,''),
			ThreePointsMade: row.find('td').eq(6).text().trim().replace(/[^0-9.]/g,''),
			ThreePointAttempts: row.find('td').eq(7).text().trim().replace(/[^0-9.]/g,''),
			FreeThrows: row.find('td').eq(8).text().trim().replace(/[^0-9.]/g,''),
			FreeThrowAttempts: row.find('td').eq(9).text().trim().replace(/[^0-9.]/g,''),
			Points: row.find('td').eq(10).text().trim().replace(/[^0-9.]/g,''),
			OffensiveReBounds: row.find('td').eq(11).text().trim().replace(/[^0-9.]/g,''),
			DefensiveReBounds: row.find('td').eq(12).text().trim().replace(/[^0-9.]/g,''),
			Assists: row.find('td').eq(14).text().trim().replace(/[^0-9.]/g,''),
			TurnOvers: row.find('td').eq(15).text().trim().replace(/[^0-9.]/g,''),
			Steals: row.find('td').eq(16).text().trim().replace(/[^0-9.]/g,''),
			Blocks: row.find('td').eq(17).text().trim().replace(/[^0-9.]/g,''),
			Fouls: row.find('td').eq(18).text().trim().replace(/[^0-9.]/g,''),
		});
	});
	return results;
};
return JSON.stringify(runLoop());";

			try
			{
				GameList = new List<GameModel>();
				foreach (var player in playerList)
				{
					var url = "http://stats.ncaa.org/player/index?game_sport_year_ctl_id=" + player.YearCode + "&stats_player_seq=" + player.PlayerID;
					_player = player;
					RunScrap(url);
				}
			}
			finally
			{
				CleanUpBrowser();
			}

		}

		protected override void ProcessResult(string url)
		{
			if (scrapResult == null)
			{
				LogResult(url, 0);
				return;
			}
			var result = JsonConvert.DeserializeObject<List<GameModel>>(scrapResult);
			if (result == null)
			{
				LogResult(url, 0);
				return;
			}
			foreach (var game in result)
			{
				game.TeamID = _player.TeamID;
				game.PlayerID = _player.PlayerID;
				game.YearCode = _player.YearCode;
			}
			GameList.AddRange(result);
			LogResult(url, result.Count);
		}
	}
}
