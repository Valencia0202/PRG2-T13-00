using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace PRG2_T13_00
{
    internal class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();

        public Airline() { }
        public Airline(string name, string c, Dictionary<string, Flight> f)
        {
            Name = name;
            Code = c;
            Flights = f;
        }
        public bool AddFlight(Flight flight)
        {
            Flights.Add(flight.FlightNumber, flight);
            return true;
        }
        public double CalculateFees(string destination, string origin)
        {
            double fees = 0;

            if (destination == "SIN")
            {
                fees = 500; // Fee when destination is SIN
            }
            else if (origin == "SIN")
            {
                fees = 800; // Fee when origin is SIN
            }

            return fees;
        }

        public bool RemoveFlight(Flight flight)
        {
            if (Flights.ContainsKey(flight.FlightNumber))
            {
                Flights.Remove(flight.FlightNumber);
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return base.ToString() + "\tName: " + Name + "\tCode:" + Code;
        }
    }
}
