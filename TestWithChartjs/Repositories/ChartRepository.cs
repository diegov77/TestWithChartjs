using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Newtonsoft.Json;

namespace TestWithChartjs.Repositories
{
    public class ChartRepository
    {
        //https://api.coindesk.com/v1/bpi/historical/close.json?start=2020-01-01&end=2020-12-31
        private string apiUrl = "https://api.coindesk.com/v1/bpi/historical/close.json";

        //Method to retrive external data from https://api.coindesk.com
        public List<BpiModel> GetDataChart(string startDate, string endDate)
        {
            string url =
                "{api-url}?start={api-start}&end={api-end}"
                    .Replace("{api-url}", apiUrl)
                    .Replace("{api-start}", startDate)
                    .Replace("{api-end}", endDate);


            using (WebClient wc = new WebClient())
            {
                var downlaod = wc.DownloadString(url);

                var data = JsonConvert.DeserializeObject<dynamic>(downlaod);
                string dates = JsonConvert.SerializeObject(data.bpi);
                var bpi = JsonConvert.DeserializeObject<Dictionary<string, string>>(dates);
                
                List<BpiModel> response = new List<BpiModel>();

                for (int i = 0; i < bpi.Count; i++)
                {
                    BpiModel bpiModel = new BpiModel();
                    bpiModel.Date = bpi.ElementAt(i).Key;
                    bpiModel.Amount = bpi.ElementAt(i).Value;

                    response.Add(bpiModel);
                }


                return response;
            }
        }
    }

    public class BpiModel
    {
        public string Date { get; set; }

        public string Amount { get; set; }
    }
}
