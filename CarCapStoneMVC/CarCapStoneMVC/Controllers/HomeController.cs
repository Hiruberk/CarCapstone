using CarCapStoneMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CarCapStoneMVC.Controllers
{
    public class HomeController : Controller
    {
        private CarDAL car = new CarDAL();

        public IActionResult Index()
        {
            List<Car> cars = car.GetCars();

            return View(cars);
        }


        public IActionResult Results(int year, string make, string model, string color)
        {
            string yr;
            if (year != 0)
            {
                yr = year.ToString();
            }
            else
            {
                yr = null;
            }

            Dictionary<string, string> searches = new Dictionary<string, string>();
            searches.Add("year", yr);
            searches.Add("make", make);
            searches.Add("model", model);
            searches.Add("color", color);

            Dictionary<string, string> notNullSearches = searches.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);

            List<Car> cars = new List<Car>();
            if (notNullSearches.Count != 0)
            {
                try
                {
                    cars = car.GetCars(notNullSearches.First().Key, notNullSearches.First().Value);
                }
                catch
                {
                    return View("Results", cars);
                }
            }
            else
            {
                return RedirectToAction("Index");
            }

            notNullSearches.Remove(notNullSearches.First().Key);

            if (notNullSearches.Count != 0)
            {
                foreach (KeyValuePair<string, string> kvp in notNullSearches)
                {
                    cars = cars.Where(x => x.ToString().ToLower().Contains(kvp.Value.ToLower())).ToList();
                }
            }

            return View("Results", cars);
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
}
