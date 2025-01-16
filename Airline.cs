﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public  double CalculateFees()
        {

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
            return base.ToString() + "Name: " + Name + "\tCode:" + Code;
        }
    }
}
