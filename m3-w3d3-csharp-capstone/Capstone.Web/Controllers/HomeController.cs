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
            WeatherSqlDAL dal = new WeatherSqlDAL(connectionString);
            List<Weather> allWeatherList = dal.GetAllWeather();
            Weather model = null;

            foreach (Weather w in allWeatherList)
            {
                if (id == w.ParkCode)
                {
                    model = w;
                }
            }
            return View("ParkDetails", model);
        }
    }
}