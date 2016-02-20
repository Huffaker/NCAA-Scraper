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

		public PlayerListScraper(IEnumerable<TeamModel> teamList, IEnumerable<YearModel> yearCodes)
		{
			javascriptCode = @"
var results = [];
var runLoop = function() {
	$('tbody').eq(1).find('a').each(function(index, i) {
		results.push({ 
			PlayerID: $(i).attr('href').split('stats_player_seq=')[1], 
			PlayerName: $(i).text() });
		});
	return results;
};
return JSON.stringify(runLoop());";

			try
			{
				PlayerList = new List<PlayerModel>();
				foreach (var yearCode in yearCodes.Take(1))
				{
					foreach (var team in teamList.Take(5))
					{
						var url = "http://stats.ncaa.org/team/" + team.TeamID + "/stats/" + yearCode.YearCode;
						_teamId = team.TeamID;
						_yearCode = yearCode.YearCode;
						RunScrap(url);
					}
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
