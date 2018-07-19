using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Capstone.Web.Models;
using System.Configuration;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAL : ISurveySqlDAL
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["NPGeek"].ConnectionString;

        private const string SQL_InsertSurvey = "INSERT INTO [dbo].[survey_result] ([parkCode], " +
            "[emailAddress], [state], [activityLevel]) VALUES (@parkCode, @emailAddress, " +
            "@state, @activityLevel); SELECT SCOPE_IDENTITY();";
        private const string SQL_ViewAllSurveys = "SELECT * FROM survey_result";

        private const string SQL_GetHighestVoteTotal = @"select p.parkName, COUNT(s.parkCode) as number_of_votes from survey_result s join park p on p.parkCode = s.parkCode GROUP BY p.parkName ORDER BY number_of_votes DESC";

        public SurveySqlDAL(string databaseConnectionString)
        {
            connectionString = databaseConnectionString;
        }

        public bool InsertSurvey(Survey newSurvey)
        {

            //Survey s = new Survey();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_InsertSurvey, conn);

                    cmd.Parameters.AddWithValue("@parkCode", newSurvey.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", newSurvey.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", newSurvey.State);
                    cmd.Parameters.AddWithValue("@activityLevel", newSurvey.ActivityLevel);

                    int newId = Convert.ToInt32(cmd.ExecuteScalar());

                    newSurvey.SurveyId = newId;

                    return true;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //public List<Survey> ViewAllSurveys()
        //{
        //    List<Survey> allSurveysList = new List<Survey>();

        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand cmd = new SqlCommand(SQL_ViewAllSurveys, conn);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                Survey s = new Survey();

        //                s.SurveyId = Convert.ToInt32(reader["surveyId"]);
        //                s.ParkCode = Convert.ToString(reader["parkCode"]);
        //                s.State = Convert.ToString(reader["state"]);
        //                s.EmailAddress = Convert.ToString(reader["emailAddress"]);
        //                s.ActivityLevel = Convert.ToString(reader["activityLevel"]);

        //                allSurveysList.Add(s);

        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //    return allSurveysList;
        //}

        public List<SurveyResults> GetHighestVoteTotal()
        {
            List<SurveyResults> surveyResultsList = new List<SurveyResults>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetHighestVoteTotal, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SurveyResults sr = new SurveyResults();

                        sr.ParkName = Convert.ToString(reader["parkName"]);
                        sr.VoteTotal = Convert.ToInt32(reader["number_of_votes"]);

                        surveyResultsList.Add(sr);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return surveyResultsList;
        }
    }
}
