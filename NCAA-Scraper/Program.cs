using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NCAA_Scraper.Models;

namespace NCAA_Scraper
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			var teamScraper = new TeamListScraper();
			var teamList = teamScraper.TeamList;

			var playerScraper = new PlayerListScraper(teamList, YearList);
			var playerList = playerScraper.PlayerList;


		}

		//Comment out the seasons you don't need
		public static List<YearModel> YearList = new List<YearModel>
		{
			new YearModel() {YearCode = 12260, SeasonName = "2015-2016"},
			new YearModel() {YearCode = 12020, SeasonName = "2014-2015"},
			new YearModel() {YearCode = 11540, SeasonName = "2013-2014"},
			new YearModel() {YearCode = 11220, SeasonName = "2012-2013"},
			new YearModel() {YearCode = 10740, SeasonName = "2011-2012"},
			new YearModel() {YearCode = 10440, SeasonName = "2010-2011"},
			new YearModel() {YearCode = 10260, SeasonName = "2009-2010"}
		};
	}
}
