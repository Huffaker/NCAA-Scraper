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
		private readonly int _yearCode;
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

			TeamList = new List<TeamModel>();
			totalCalls = Program.YearList.Count;
			try
			{
				foreach (var year in Program.YearList)
				{
					_yearCode = year.YearCode;
					RunScrap("http://stats.ncaa.org/team/inst_team_list?academic_year=" + year.YearID +"&conf_id=-1&division=1&sport_code=MBB");
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
			var result = JsonConvert.DeserializeObject<List<TeamModel>>(scrapResult);
			if (result == null)
			{
				LogResult(url, 0);
				return;
			}
			foreach (var team in result)
			{
				team.YearCode = _yearCode;
			}
			TeamList.AddRange(result);
			LogResult(url, result.Count);
		}
    }
}
