using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using System.Configuration;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        string connectionString = ConfigurationManager.ConnectionStrings["NPGeek"].ConnectionString;

        SurveySqlDAL dal;

        public HomeController()
        {
            dal = new SurveySqlDAL(connectionString);
        }

        // GET: Home
        public ActionResult Index()
        {
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);

            List<Park> allParks = dal.GetAllParkData();

            return View("Index", allParks);
        }
        // GET: /Home/ParkDetails/@parkCode

        // GET: ParkDetails
        public ActionResult ParkDetails(string id)
        {
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);
            List<Park> parkList = dal.GetAllParkData();
            Park model = null;

            foreach (Park p in parkList)
            {
                if (id == p.ParkCode)
                {
                    model = p;
                }


            }
            return View("ParkDetails", model);
        }


        // GET: Survey
        public ActionResult Survey()
        {
            return View("Survey");
        }

        // POST: Survey
        [HttpPost]
        public ActionResult Survey(Survey model)
        {
            if (!dal.InsertSurvey(model))
            {
                return View("Survey", model);
            }
            return RedirectToAction("SurveyResults");
        }

        // GET: Weather
        public ActionResult Weather(string id)
        {
            IParkSqlDAL dal = new ParkSqlDAL(connectionString);
            List<Park> ParkList = dal.GetAllParkData();
            List<Weather> weather = new List<Weather>();

            foreach (Park park in ParkList)
            {
                if (id == park.ParkCode)
                {
                    IWeatherSqDAL thisDal = new WeatherSqlDAL(connectionString);
                    weather = thisDal.GetWeatherByParkCode(park.ParkCode);
                }
            }

            bool isFahrenheit;
            Session["Temperature"] = Request.Params["Temperature"];
            if (Session["Temperature"].ToString() != null)
            {
                if (Session["Temperature"].ToString() == "F")
                {
                    isFahrenheit = true;
                }
                else
                {
                    isFahrenheit = false;
                }
            }
            else
            {
                isFahrenheit = true;
            }

            //Fahrenheit to Celsius
            for (int i = 0; i < weather.Count; i++)
            {
                if (isFahrenheit)
                {
                    if (weather[i].Temperature == "F")
                    {
                        continue;
                    }
                    else
                    {
                        weather[i].Temperature = "F";
                        weather[i].High =
                    }
                }
            }
        }

        public int ConvertFahrenheitToCelsius(double temp, string fahrTempOrCelTemp)
        {
            double newTemp = 0.00;
            if (fahrTempOrCelTemp.ToLower() == "c")
            {
                newTemp = ((temp - 32) * 5.0 / 9);
            }
            else if (fahrTempOrCelTemp.ToLower() == "f")
            {
                newTemp = (temp * (9 / 5.0) + 32);
            }

            return (int)newTemp;
        }

        //GET: SurveyResults
        public ActionResult SurveyResults()
        {
            SurveySqlDAL dal = new SurveySqlDAL(connectionString);
            List<Survey> allSurveys = dal.ViewAllSurveys();

            return View("SurveyResults", allSurveys);
        }
    }
}