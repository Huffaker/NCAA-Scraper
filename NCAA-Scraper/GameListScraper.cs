using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCAA_Scraper.Models;

namespace NCAA_Scraper
{
	public class GameListScraper: Scraper
	{
		public GameListScraper(IEnumerable<PlayerModel> playerList)
		{
			var url = "http://stats.ncaa.org/player/index?game_sport_year_ctl_id=12260&stats_player_seq=1734758.0";

			javascriptCode = @"
var results = [];
var runLoop = function() {
	$('#game_breakdown_div').find('tbody').eq(1).find('tr:not("".heading"")').find('a:not(""[target]"")').each(function(index, i){ 
      var row = $(i).closest('tr');
		results.push({
			GameDate: row.find('td').eq(0).text(),
			WasHomeGame: !$(i).text().startsWith('@'),
			OpponentTeamID:$(i).attr('href').split('/')[2],
		});
	});
	return results;
};
$('html').html(JSON.stringify(runLoop()));";
		}

		protected override void ProcessResult()
		{
			throw new NotImplementedException();
		}
	}
}
