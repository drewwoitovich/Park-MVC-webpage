using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDAL : IWeatherSqDAL
    {
        private string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";

        private const string SQL_GetWeatherForPark = "SELECT * FROM weather WHERE parkCode = @parkCode";

        public Weather GetWeatherByParkCode(string parkCode)
        {
            Weather w = new Weather();

            string parkCodeWithApostraphes = "'" + parkCode + "'";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetWeatherForPark, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCodeWithApostraphes);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        w.ParkCode = Convert.ToString(reader["parkCode"]);
                        w.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        w.High = Convert.ToInt32(reader["high"]);
                        w.Low = Convert.ToInt32(reader["low"]);
                        w.Forecast = Convert.ToString(reader["forecast"]);
                    }
                }
                return w;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}