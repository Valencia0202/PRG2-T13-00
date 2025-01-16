using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_00
{
    public class BoardingGate
    {
        public string gateName { get; set; }
        public bool supportsCFFT { get; set; }
        public bool supportsDDJB { get; set; }
        public bool supportsLWTT { get; set; }
        public Flight flight { get; set; }

        //constructor
        public BoardingGate(string getname, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT, Flight flight)
        {
            gateName = getname;
            supportsCFFT = supportsCFFT;
            supportsDDJB = supportsDDJB;
            supportsLWTT=supportsLWTT; 
            flight = flight;
        }

        public double CalculateFees()
        { return flight.CalculateFees(); }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
