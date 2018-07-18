using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAL : ISurveySqlDAL
    {
        private const string connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";

        private const string SQL_InsertSurvey = "INSERT INTO [dbo].[survey_result] ([parkCode], " +
            "[emailAddress], [state], [activityLevel]) VALUES (@parkCode, @emailAddress, " +
            "@state, @activityLevel)";
        private const string SQL_ViewAllSurveys = "SELECT * FROM survey_result";

        public int InsertSurvey(string parkCode, string emailAddress, string state, string activityLevel)
        {
            int surveyId;

            Survey s = new Survey();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_InsertSurvey, conn);

                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", emailAddress);
                    cmd.Parameters.AddWithValue("@state", state);
                    cmd.Parameters.AddWithValue("@activityLevel", activityLevel);

                    surveyId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return surveyId;
        }

        public List<Survey> ViewAllSurveys()
        {
            List<Survey> allSurveysList = new List<Survey>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_ViewAllSurveys, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Survey s = new Survey();

                        s.SurveyId = Convert.ToInt32(reader["surveyId"]);
                        s.ParkCode = Convert.ToString(reader["parkCode"]);
                        s.State = Convert.ToString(reader["state"]);
                        s.EmailAddress = Convert.ToString(reader["emailAddress"]);
                        s.ActivityLevel = Convert.ToString(reader["activityLevel"]);

                        allSurveysList.Add(s);
                        
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return allSurveysList;
        }
    }
}
   