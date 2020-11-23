using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestWithChartjs.Models;
using TestWithChartjs.Repositories;

namespace TestWithChartjs.Controllers
{
    public class HomeController : Controller
    {
        ChartRepository _chartRepo = new ChartRepository();

        public IActionResult Index()
        {
            return View();
        }

        //endpoint to receive request to get external data.
        [HttpPost]
        public IActionResult PupulateChart([FromBody] ChartModel dates)
        {
            List<BpiModel> data = new List<BpiModel>();
            try
            {
                string startDate = dates.StartDate;
                string endDate = dates.EndDate;
                //method called from repository class to retrieve external data.
                data = _chartRepo.GetDataChart(startDate, endDate);
                return Json(data);
            }
            catch (Exception ex)
            {
                return Json(data);
            }
            
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


    public class ChartModel
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }

}
