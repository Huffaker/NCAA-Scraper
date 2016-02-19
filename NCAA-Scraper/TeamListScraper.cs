using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCAA_Scraper.Models;
using Newtonsoft.Json;

namespace NCAA_Scraper
{
	public class TeamListScraper : Scraper
	{
		public List<TeamModel> TeamList { get; private set; } 
		public TeamListScraper()
		{

			javascriptCode = @"
var results = [];
var runLoop = function(){
	$('table').eq(0).find('tr a').each(function(index, i) {
		results.push({
			teamId:$(i).attr('href').split('/')[2],
			teamName: $(i).text()});
		});
		return results;
	};
return JSON.stringify(runLoop());";

			const string url = "http://stats.ncaa.org/team/inst_team_list?academic_year=2016&conf_id=-1&division=1&sport_code=MBB";

			try
			{
				RunScrap(url);
			}
			finally
			{
				CleanUpBrowser();
			}
		}

		protected override void ProcessResult()
		{
			TeamList = JsonConvert.DeserializeObject<List<TeamModel>>(scrapResult);
		}
    }
}
