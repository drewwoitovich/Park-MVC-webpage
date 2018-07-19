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
    public class SurveySqlDALTests
    {
        public string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";

        private const string SQL_InsertSurvey = "INSERT INTO [dbo].[survey_result] ([parkCode], " +
                                                "[emailAddress], [state], [activityLevel]) VALUES (@parkCode, @emailAddress, " +
                                                "@state, @activityLevel)";
        private const string SQL_ViewAllSurveys = "SELECT * FROM survey_result";

        private const string SQL_GetSumActivityLevelByPark = @"Select sum(CONVERT(INT, activityLevel))" +
                                                             "from survey_result as p WHERE parkCode=@parkCode GROUP BY parkCode;";

        private int rowsAffected = 0;
        private TransactionScope tran;

        [TestInitialize]
        public void Initializer()
        {
            tran = new TransactionScope();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_InsertSurvey, conn);

                    cmd.Parameters.AddWithValue("@parkCode", "CVNP");
                    cmd.Parameters.AddWithValue("@emailAddress", "test");
                    cmd.Parameters.AddWithValue("@state", "test");
                    cmd.Parameters.AddWithValue("@activityLevel", "test");


                    rowsAffected = cmd.ExecuteNonQuery();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void SQL_ViewAllSurveys_Tests()
        {
            //Arrange
            SurveySqlDAL surveyDal = new SurveySqlDAL();

            //Act
            List<Survey> allSurveys = surveyDal.ViewAllSurveys();

            //Assert
            Assert.IsNotNull(surveyDal);
            Assert.IsNotNull(allSurveys);

        }

        [TestMethod]
        public void SQL_InsertSurvey_Tests()
        {
            SurveySqlDAL surveyDal = new SurveySqlDAL();

            Assert.IsNotNull(surveyDal);
            Assert.AreEqual(1, rowsAffected);
        }
    }
}
