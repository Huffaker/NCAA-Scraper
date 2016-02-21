using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCAA_Scraper.Models
{
	public class PlayerModel
	{
		public int PlayerID { get; set; }
		public string PlayerName { get; set; }
		public int TeamID { get; set; }
		public int YearCode { get; set; }
		public string PlayerPosition { get; set; }
		public string PlayerYear { get; set; }
		public string PlayerHeight { get; set; }
		public int? GamesPlayed { get; set; }
		public int? GamesStarted { get; set; }
    }
}
