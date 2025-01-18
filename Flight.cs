using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_00
{
    abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public Flight() { }
        public Flight(string Fn, string o, string d, DateTime ET, string s)
        {
            FlightNumber = Fn;
            Origin = o;
            Destination = d;
            ExpectedTime = ET;
            Status = s;
        }
        public virtual double CalculateFees()
        {
            // Default implementation
            return 0;
        }
        public override string ToString()
        {
            return base.ToString() + "FlightNumber:" + FlightNumber + "Origin:" + Origin + "Destination:" + Destination + "Expected Time:" + ExpectedTime.ToString("dd/MM/yyyy");
        }
    }
}
