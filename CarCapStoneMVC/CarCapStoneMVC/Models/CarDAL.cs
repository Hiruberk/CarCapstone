using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CarCapStoneMVC.Models
{
    public class CarDAL
    {
        public string GetData()
        {
            string url = @"https://localhost:44362/api/Cars";

            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string json = rd.ReadToEnd();

            return json;
        }

        public List<Car> GetCars()
        {
            string json = GetData();

            List<Car> c = JsonConvert.DeserializeObject<List<Car>>(json);
            return c;
        }

        public string GetData(string field, string query)
        {
            //URL can be different based upon endpoint/API 
            string url = $"https://localhost:44362/api/Cars/{field}/{query}";

            //Web Requests sometimes need Headers/User Agent prop
            HttpWebRequest request = WebRequest.CreateHttp(url);
            HttpWebResponse response = null;

            response = (HttpWebResponse)request.GetResponse();
            StreamReader rd = new StreamReader(response.GetResponseStream());
            string json = rd.ReadToEnd();
            return json;

        }

        public List<Car> GetCars(string field, string query)
        {
            string json = GetData(field, query);

            List<Car> c = JsonConvert.DeserializeObject<List<Car>>(json);
            return c;
        }
    }
}
