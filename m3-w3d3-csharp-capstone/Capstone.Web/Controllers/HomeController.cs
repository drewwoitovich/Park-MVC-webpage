﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString;

        public HomeController()
        {
            connectionString = @"Data Source=.\sqlexpress;Initial Catalog=NPGeek;Integrated Security=True";
        }

        // GET: Home
        public ActionResult Index()
        {
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);

            List<Park> allParks = dal.GetAllParkData();

            return View("Index", allParks);
        }

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

        public ActionResult Survey()
        {
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);



            return View("Survey");
        }

        public ActionResult SurveyResults()
        {
            return View("SurveyResults");
        }
        
    }
}