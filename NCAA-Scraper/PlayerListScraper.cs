using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCAA_Scraper.Models;
using Newtonsoft.Json;

namespace NCAA_Scraper
{
	public class PlayerListScraper : Scraper
	{
		private List<TeamModel> _teamList; 
		private List<PlayerModel> _playerList;
		private int TeamIndex = 0;
		private int YearIndex = 0;
		 
		public PlayerListScraper(List<TeamModel> teamList)
		{
			_teamList = teamList;
			if (_teamList.Count == 0)
				throw new Exception("No Teams Provided");

			url = "http://stats.ncaa.org/team/" + _teamList[TeamIndex].TeamID + "/stats/" + Program.YearList[YearIndex].YearCode;
			javascriptCode = "var results = [];var runLoop = function() {$($('tbody')[1]).find('a').each(function(index, i) {results.push({ PlayerID: $(i).attr('href').split('stats_player_seq=')[1], PlayerName: $(i).text() });});return results;};JSON.stringify(runLoop());";
		}

		public void LoadPlayer(string teamID, string yearCode)
		{
			url = "http://stats.ncaa.org/team/" + teamID + "/stats/" + yearCode;
		}
        protected override void ProcessResult(object sender, EventArgs e)
		{
			var result = JsonConvert.DeserializeObject<List<PlayerModel>>(scrapResult);
	        if (result == null)
		        return;
	        foreach (var player in result)
	        {
		        player.YearCode = Program.YearList[YearIndex].YearCode;
		        player.TeamID = _teamList[TeamIndex].TeamID;
	        }
			_playerList.AddRange(result);

		}
	}
}
