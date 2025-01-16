using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_00
{
        public class Terminal
        {
            // Attributes
            public string TerminalName { get; set; }
            private Dictionary<string, Airline> Airlines = new Dictionary<string, Airline>();
            private Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();
            private Dictionary<string, BoardingGate> BoardingGates = new Dictionary<string, BoardingGate>();
            private Dictionary<string, double> GateFees = new Dictionary<string, double>();

            // Constructor
            public Terminal(string terminalName)
            {
                TerminalName = terminalName;
            }

            // Methods

            // Add an airline to the dictionary
            public bool AddAirline(Airline airline)
            {
                if (airline == null || Airlines.ContainsKey(airline.Name))
                {
                    return false;
                }
                Airlines.Add(airline.Name, airline);
                return true;
            }

            // Add a boarding gate to the dictionary
            public bool AddBoardingGate(BoardingGate boardingGate)
            {
                if (boardingGate == null || boardingGates.ContainsKey(boardingGate.GateID))
                {
                    return false;
                }
                BoardingGates.Add(boardingGate.GateID, boardingGate);
                return true;
            }

            // Get the airline associated with a specific flight
            public Airline GetAirlineFromFlight(Flight flight)
            {
                if (flight == null || !flights.ContainsKey(flight.FlightID))
                {
                    return null;
                }

                string airlineName = flight.AirlineName;
                return airlines.ContainsKey(airlineName) ? airlines[airlineName] : null;
            }

            // Print airline fees
            public void PrintAirlineFees()
            {
                Console.WriteLine("Airline Fees:");
                foreach (var gateFee in gateFees)
                {
                    Console.WriteLine($"Gate {gateFee.Key}: ${gateFee.Value}");
                }
            }

            // ToString override
            public override string ToString()
            {
                return $"Terminal Name: {TerminalName}\n" +
                       $"Number of Airlines: {airlines.Count}\n" +
                       $"Number of Flights: {flights.Count}\n" +
                       $"Number of Boarding Gates: {boardingGates.Count}";
            }
        }
    }
