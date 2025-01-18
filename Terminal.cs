using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_00
{
        class Terminal
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
                if (boardingGate == null || BoardingGates.ContainsKey(boardingGate.GateName))
                {
                    return false;
                }
                BoardingGates.Add(boardingGate.GateName, boardingGate);
                return true;
            }

            // Get the airline associated with a specific flight
            public Airline GetAirlineFromFlight(Flight flight)
            {
                if (flight == null || !Flights.ContainsKey(flight.FlightNumber))
                {
                    return null;
                }

                string airlineName = flight.FlightNumber;
                return Airline.ContainsKey(airlineName) ? Airlines[airlineName] : null;
            }

            // Print airline fees
            public void PrintAirlineFees()
            {
                Console.WriteLine("Airline Fees:");
                foreach (var gateFee in GateFees)
                {
                    Console.WriteLine($"Gate {gateFee.Key}: ${gateFee.Value}");
                }
            }

            // ToString override
            public override string ToString()
            {
                return $"Terminal Name: {TerminalName}\n" +
                       $"Number of Airlines: {Airlines.Count}\n" +
                       $"Number of Flights: {Flights.Count}\n" +
                       $"Number of Boarding Gates: {BoardingGates.Count}";
            }
        }
    }
