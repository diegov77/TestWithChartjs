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
        private string apiUrl = "https://api.coindesk.com/v1/bpi/historical/close.json";

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PupulateChart([FromBody] ChartModel dates)
        {
            string startDate = dates.StartDate;
            string endDate = dates.EndDate;

            List<BpiModel> data = _chartRepo.GetDataChart(startDate, endDate);

            return Json(data);
        }

        public dynamic GetDataChart(string startDate, string endDate)
        {
            string url =
                "{api-url}?start={api-start}&end={api-end}"
                    .Replace("{api-url}", apiUrl)
                    .Replace("{api-start}", startDate)
                    .Replace("{api-end}", endDate);


            //using (WebClientEx wc = new WebClientEx(600000))
            using (WebClient wc = new WebClient())
            {
                var downlaod = wc.DownloadString(url);

                var response = JsonConvert.DeserializeObject<dynamic>(downlaod);
                //List<dynamic> response = JsonConvert.DeserializeObject<List<dynamic>>(jObject.response.ToString());

                return response;
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

    //"Object reference not set to an instance of an object."
}
