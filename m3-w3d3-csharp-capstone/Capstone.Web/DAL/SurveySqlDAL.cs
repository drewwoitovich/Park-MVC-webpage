﻿using System;
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

        private const string SQL_GetSumActivityLevelByPark = @"Select sum(CONVERT(INT, activityLevel))" +
                                                             "from survey_result as p WHERE parkCode=@parkCode GROUP BY parkCode;";

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

        public string ActivityLevelByPark(string parkCode)
        {
            string rate = String.Empty;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(SQL_GetSumActivityLevelByPark, conn);
                    cmd.Parameters.AddWithValue("@parkCode", parkCode);
                    int value = (int) cmd.ExecuteScalar();
                    return value.ToString();
                }
            }
            catch (Exception)
            {
                return "0".ToString();
            }
        }
    }
}
