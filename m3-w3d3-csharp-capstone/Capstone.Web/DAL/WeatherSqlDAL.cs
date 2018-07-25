using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDAL : IWeatherSqDAL
    {
        public string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";

        private const string SQL_GetWeatherForPark = "SELECT * FROM weather WHERE parkCode = @parkCode";
        private const string SQL_GetAllWeather = "SELECT * FROM weather;";

        public WeatherSqlDAL(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        public List<Weather> GetWeatherByParkCode(string parkCode)
        {

            List<Weather> fiveDayForecast = new List<Weather>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetWeatherForPark, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Weather w = new Weather();
                        w.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        w.Forecast = Convert.ToString(reader["forecast"]);
                        w.High = Convert.ToInt32(reader["high"]);
                        w.Low = Convert.ToInt32(reader["low"]);
                        //w.ParkCode = Convert.ToString(reader["parkCode"]);
                        w.Temperature = "F";
                        fiveDayForecast.Add(w);
                    }
                }
                return fiveDayForecast;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Weather> GetAllWeather()
        {

            List<Weather> allWeatherList = new List<Weather>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetAllWeather, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Weather w = new Weather();
                        w.FiveDayForecastValue = Convert.ToInt32(reader["fiveDayForecastValue"]);
                        w.Forecast = Convert.ToString(reader["forecast"]);
                        w.High = Convert.ToInt32(reader["high"]);
                        w.Low = Convert.ToInt32(reader["low"]);
                        w.ParkCode = Convert.ToString(reader["parkCode"]);
                        w.Temperature = "F";
                        allWeatherList.Add(w);
                    }
                }
                return allWeatherList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}