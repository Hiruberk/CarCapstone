using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarCapStoneMVC.Models
{

    public class Car
    {
        public int id { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public int year { get; set; }
        public string color { get; set; }

        public override string ToString()
        {
            return @$"{make}, {model}, {year}, {color}";
        }
    }

}
