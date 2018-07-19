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
    public class ParkSqlDALTests
    {
        public string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";

        private const string SQL_GetAllParkData = "SELECT * FROM park ORDER BY state";
        private const string SQL_GetParkDataByCode = "SELECT * FROM park WHERE parkCode = @parkCode";
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
                    SqlCommand cmd = new SqlCommand(SQL_GetAllParkData, conn);
                    int rowAffected = cmd.ExecuteNonQuery();
                    cmd = new SqlCommand(SQL_GetParkDataByCode, conn);
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
        public void ParkSqlDAL_GetAllParkData_Test()
        {
            //Arrange
            IParkSqlDAL parkDAL = new ParkSqlDAL(connectionString);
            //Act
            List<Park> allParks = parkDAL.GetAllParkData();
            //Assert
            Assert.IsNotNull(parkDAL);
            Assert.IsNotNull(allParks);
            Assert.AreEqual(10, allParks.Count);
        }

        [TestMethod]
        public void ParkSqlDAL_GetParkDataByCode_Test()
        {
            //Arrange
            IParkSqlDAL parkDal = new ParkSqlDAL(connectionString);
            //Act
            Park affectedPark = parkDal.GetParkDataByCode("CVNP");
            //Assert
            Assert.IsNotNull(parkDal);
            Assert.IsNotNull(affectedPark);
            Assert.AreEqual("Woodland",affectedPark.Climate);
            Assert.AreEqual("John Muir",affectedPark.InspirationalQuoteSource);
            
        }
    }
}
