﻿using System;
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
            private Dictionary<string, Airline> airlines = new Dictionary<string, Airline>();
            private Dictionary<string, Flight> flights = new Dictionary<string, Flight>();
            private Dictionary<string, BoardingGate> boardingGates = new Dictionary<string, BoardingGate>();
            private Dictionary<string, double> gateFees = new Dictionary<string, double>();

            // Constructor
            public Terminal(string terminalName)
            {
                TerminalName = terminalName;
            }

            // Methods

            // Add an airline to the dictionary
            public bool AddAirline(Airline airline)
            {
                if (airline == null || airlines.ContainsKey(airline.Name))
                {
                    return false;
                }
                airlines.Add(airline.Name, airline);
                return true;
            }

            // Add a boarding gate to the dictionary
            public bool AddBoardingGate(BoardingGate boardingGate)
            {
                if (boardingGate == null || boardingGates.ContainsKey(boardingGate.GateID))
                {
                    return false;
                }
                boardingGates.Add(boardingGate.GateID, boardingGate);
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
