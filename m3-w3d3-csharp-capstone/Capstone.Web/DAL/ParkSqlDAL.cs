using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class ParkSqlDAL : IParkSqlDAL
    {
        public string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";

        private const string SQL_GetAllParkData = "SELECT * FROM park ORDER BY state";
        private const string SQL_GetParkDataByCode = "SELECT * FROM park WHERE parkCode = @parkCode";

        public ParkSqlDAL(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        public List<Park> GetAllParkData()
        {
            List<Park> allParksList = new List<Park>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllParkData, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Park p = new Park();

                        p.ParkCode = Convert.ToString(reader["parkCode"]);
                        p.ParkName = Convert.ToString(reader["parkName"]);
                        p.State = Convert.ToString(reader["state"]);
                        p.Acreage = Convert.ToInt32(reader["acreage"]);
                        p.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                        p.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                        p.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        p.Climate = Convert.ToString(reader["climate"]);
                        p.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        p.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        p.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                        p.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        p.ParkDescription = Convert.ToString(reader["parkDescription"]);

                        allParksList.Add(p);
                    }
                }
                return allParksList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Park GetParkDataByCode(string parkCode)
        {
            Park p = new Park();

            string parkCodeWithApostraphes = "'" + parkCode + "'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetParkDataByCode, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCodeWithApostraphes);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        p.ParkCode = Convert.ToString(reader["parkCode"]);
                        p.ParkName = Convert.ToString(reader["parkName"]);
                        p.State = Convert.ToString(reader["state"]);
                        p.Acreage = Convert.ToInt32(reader["acreage"]);
                        p.ElevationInFeet = Convert.ToInt32(reader["elevationInFeet"]);
                        p.MilesOfTrail = Convert.ToInt32(reader["milesOfTrail"]);
                        p.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                        p.Climate = Convert.ToString(reader["climate"]);
                        p.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                        p.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                        p.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                        p.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                        p.ParkDescription = Convert.ToString(reader["parkDescription"]);
                    }
                }
                return p;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}