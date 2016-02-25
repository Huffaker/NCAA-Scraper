using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCAA_Scraper.Models;
using Newtonsoft.Json;

namespace NCAA_Scraper
{
	public class PlayerListScraper : Scraper
	{
		private readonly int _teamId;
		private readonly int _yearCode;
		public List<PlayerModel> PlayerList { get; }

		public PlayerListScraper(List<TeamModel> teamList, List<YearModel> yearCodes)
		{
			javascriptCode = @"
var results = [];
var runLoop = function() {
	$('tbody').eq(1).find('a').each(function(index, i) {
		results.push({ 
			PlayerID: $(i).attr('href').split('stats_player_seq=')[1], 
			PlayerName: $(i).text(),
			PlayerPosition: $(i).closest('tr').find('td').eq(3).text(),
			PlayerYear: $(i).closest('tr').find('td').eq(2).text(),
			PlayerHeight: $(i).closest('tr').find('td').eq(4).text(),
			GamesPlayed: $(i).closest('tr').find('td').eq(5).text().replace(/[^0-9.]/g,''),
			GamesStarted: $(i).closest('tr').find('td').eq(6).text().replace(/[^0-9.]/g,''),
		});
	});
	return results;
};
return JSON.stringify(runLoop());";

			totalCalls = teamList.Count();
            try
			{
				PlayerList = new List<PlayerModel>();
				foreach (var team in teamList)
				{
					var url = "http://stats.ncaa.org/team/" + team.TeamID + "/stats/" + team.YearCode;
					_teamId = team.TeamID;
					_yearCode = team.YearCode;
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
			var result = JsonConvert.DeserializeObject<List<PlayerModel>>(scrapResult);
			if (result == null)
			{
				LogResult(url, 0);
				return;
			}
			foreach (var player in result)
			{
				player.YearCode = _yearCode;
				player.TeamID = _teamId;
			}
			PlayerList.AddRange(result);
			LogResult(url, result.Count);
		}
	}
}
