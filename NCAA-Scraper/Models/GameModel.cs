using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCAA_Scraper.Models
{
	public class GameModel
	{
		public int TeamID { get; set; }
		public int OpponentTeamID { get; set; }
		public DateTime GameDate { get; set; }
		public int YearCode { get; set; }
		public bool WasHomeGame { get; set; }
		public bool Win { get; set; }
		public int TeamPoints { get; set; }
		public int OpponentTeamPoints { get; set; }
		public int PlayerID { get; set; }
		public int? MinutesPlayed { get; set; }
		public int? FieldGoalsMade { get; set; }
		public int? FieldGoalAttempts { get; set; }
		public int? ThreePointsMade { get; set; }
		public int? ThreePointAttempts { get; set; }
		public int? FreeThrows { get; set; }
		public int? FreeThrowAttempts { get; set; }
		public int? Points { get; set; }
		public int? OffensiveReBounds { get; set; }
		public int? DefensiveReBounds { get; set; }
		public int? Assists { get; set; }
		public int? TurnOvers { get; set; }
		public int? Steals { get; set; }
		public int? Blocks { get; set; }
		public int? Fouls { get; set; }
	}
}
