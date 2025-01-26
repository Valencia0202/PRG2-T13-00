using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;



//==========================================================
// Student Number	: S10268058
// Student Name	: Chai Zhe Ling
// Partner Name	: Valencia Amora Chua Lie Cia
//==========================================================
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
        public double CalculateFees(string destination, string origin, int totalFlights, List<Flight> flights)
        {
            // Validate inputs
            if (string.IsNullOrEmpty(destination) || string.IsNullOrEmpty(origin))
                throw new ArgumentException("Destination and origin cannot be null or empty.");
            if (totalFlights < 0)
                throw new ArgumentException("Total flights must be a non-negative number.");

            double fees = 0;

            // Base fees based on origin and destination
            if (destination == "SIN")
            {
                fees = 500;
            }
            else if (origin == "SIN")
            {
                fees = 800;
            }

            // Initialize total discount
            double totalDiscount = 0;

            // Apply specific promotions first
            // Promotion 1: Discount for every 3 flights
            totalDiscount += (totalFlights / 3) * 350;

            // Promotion 2, 3, and 4: Flight-specific promotions
            foreach (var flight in flights)
            {
                // Time-based discount
                if (flight.ExpectedTime.Hour < 11 || flight.ExpectedTime.Hour > 21)
                {
                    totalDiscount += 110;
                }

                // Origin-based discount
                if (flight.Origin == "DXB" || flight.Origin == "BKK" || flight.Origin == "NRT")
                {
                    totalDiscount += 25;
                }

                // Special request discount
                if (string.IsNullOrEmpty(flight.Status) || flight.Status != "SpecialRequest")
                {
                    totalDiscount += 50;
                }
            }

            // Apply the most general promotion last
            // Promotion 5: Bulk flight discount
            if (totalFlights > 5)
            {
                double baseFees = totalFlights * 500; // Example base fees
                totalDiscount += 0.03 * baseFees;    // 3% off the total bill
            }

            // Apply discounts to fees
            fees -= totalDiscount;

            // Ensure fees do not go below zero
            return Math.Max(fees, 0);
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
