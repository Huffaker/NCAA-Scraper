using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NCAA_Scraper.Models;

namespace NCAA_Scraper
{
	public static class BulkInsert
	{


		public static void LoadTeams(IEnumerable<TeamModel> teamList, string conn)
		{
			LoadData(teamList, "STG.Teams", "STG.pMergeTeams", conn);
		}

		public static void LoadPlayers(IEnumerable<PlayerModel> playerList, string conn)
		{
			LoadData(playerList, "STG.Players" , "STG.pMergePlayers", conn);
		}

		public static void LoadGames(IEnumerable<GameModel> gameList, string conn)
		{
			LoadData(gameList, "STG.Games", "STG.pMergeGames", conn);
		}

		public static void LoadData<T>(IEnumerable<T> data, string table, string merge, string connectionString)
		{
			using (var connection = new SqlConnection(connectionString))
			{
				connection.Open();
				using (var bulkCopy = new SqlBulkCopy(				 
								connection,
								 SqlBulkCopyOptions.TableLock |
								 SqlBulkCopyOptions.FireTriggers |
								 SqlBulkCopyOptions.UseInternalTransaction,
								 null))
				{
					bulkCopy.DestinationTableName = table;
					try
					{
						//Clear data from staging table
						var sqlTrunc = "TRUNCATE TABLE " + table;
						var cmdTrunc = new SqlCommand(sqlTrunc, connection);
						cmdTrunc.ExecuteNonQuery();

						//Load in new data
						bulkCopy.WriteToServer(ToDataTable(data.ToList()));

						//Merge new data into live data tables
						var sqlMrg = "EXEC " + merge;
						var cmdMrg = new SqlCommand(sqlMrg, connection);
						cmdMrg.ExecuteNonQuery();

						connection.Close();
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
				}
			}
		}

		public static DataTable ToDataTable<T>(List<T> items)
		{
			var dataTable = new DataTable(typeof(T).Name);

			//Get all the properties
			var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
			foreach (var prop in props)
			{
					//Setting column names as Property names
					dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
			}
			foreach (var item in items)
			{
				var values = new object[props.Length];
				for (var i = 0; i<props.Length; i++)
				{
						//inserting property values to datatable rows
						values[i] = props[i].GetValue(item, null);
				}
				dataTable.Rows.Add(values);
			}
			//put a breakpoint here and check datatable
			return dataTable;
		}

	}
}
