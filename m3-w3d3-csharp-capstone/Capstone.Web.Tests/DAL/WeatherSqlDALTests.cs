using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Transactions;
namespace Capstone.Web.Tests.DAL
{
    [TestClass]
    public class WeatherSqlDALTests
    {
        [TestClass]
        public class ParkSqlDALTests
        {
            public string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";

            private const string SQL_GetWeatherForPark = "SELECT * FROM weather WHERE parkCode = @parkCode";
            private const string SQL_GetAllWeather = "SELECT * FROM weather;";
            private TransactionScope tran;

            [TestInitialize]
            public void initializer()
            {
                tran = new TransactionScope();
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(SQL_GetAllWeather, conn);
                        int rowAffected = cmd.ExecuteNonQuery();
                        cmd = new SqlCommand(SQL_GetWeatherForPark, conn);
                        cmd.Parameters.AddWithValue("@parkCode", "CVNP");
                    }
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    throw;
                }
            }

            [TestCleanup]
            public void cleanup()
            {
                tran.Dispose();
            }

            [TestMethod]
            public void ParkSqlDAL_GetAllWeather_Test()
            {
                //Arrange
                IWeatherSqDAL weatherDAL = new WeatherSqlDAL(connectionString);
                //Act
                List<Weather> allWeather = weatherDAL.GetAllWeather();
                //Assert
                Assert.IsNotNull(weatherDAL);
                Assert.IsNotNull(allWeather);
            }

            [TestMethod]
            public void ParkSqlDAL_GetWeatherByCode_Test()
            {
                //Arrange
                IWeatherSqDAL weatherDAL = new WeatherSqlDAL(connectionString);
                //Act
                List<Weather> weather = weatherDAL.GetWeatherByParkCode("CVNP");
                //Assert
                Assert.IsNotNull(weatherDAL);
                Assert.IsNotNull(weather);
            }
        }
    }
}
