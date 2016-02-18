using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCAA_Scraper.Models;
using Newtonsoft.Json;

namespace NCAA_Scraper
{
	public class TeamListScraper : Scraper
	{
		public TeamListScraper()
		{
			url = "http://stats.ncaa.org/team/inst_team_list?academic_year=2016&conf_id=-1&division=1&sport_code=MBB";
			javascriptCode = @"var results = [];var runLoop = function(){$($('table')[0]).find('tr a').each(function(index, i) {results.push({teamId:$(i).attr('href').split('/')[2],teamName: $(i).text()});});return results;};JSON.stringify(runLoop());";
		}

		protected override void ProcessResult(object sender, EventArgs e)
		{
			var result = JsonConvert.DeserializeObject<List<TeamModel>>(scrapResult);
			var playerScraper = new PlayerListScraper(result);
			playerScraper.RunScrap();
        }
    }
}
