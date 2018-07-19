using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.DAL
{
    public interface ISurveySqlDAL
    {
        List<Survey> ViewAllSurveys();

        Survey InsertSurvey(string parkCode, string emailAddress, string state, string activityLevel);
    }
}