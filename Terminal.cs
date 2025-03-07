﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



//==========================================================
// Student Number	: S10268058
// Student Name	: Chai Zhe Ling
// Partner Name	: Valencia Amora Chua Lie Cia
//==========================================================
namespace PRG2_T13_00
{
        class Terminal
        {
            // Attributes
            public string TerminalName { get; set; }
            public Dictionary<string, Airline> Airlines = new Dictionary<string, Airline>();
            public Dictionary<string, Flight> Flights = new Dictionary<string, Flight>();
            public Dictionary<string, BoardingGate> BoardingGates = new Dictionary<string, BoardingGate>();
            public Dictionary<string, double> GateFees = new Dictionary<string, double>();

            // Constructor
            public Terminal(string terminalName)
            {
                TerminalName = terminalName;
            
            }

            // Methods

            // Add an airline to the dictionary
            public bool AddAirline(Airline airline)
            {
                if (airline == null || Airlines.ContainsKey(airline.Code)) // Use Code as key 
                {
                    return false;
                }
                Airlines.Add(airline.Code, airline); // Store airlines by Code 
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
            if (flight == null || string.IsNullOrEmpty(flight.FlightNumber) || !Flights.ContainsKey(flight.FlightNumber))
            {
                return null;
            }

            // Extract the airline code directly within the method
            string airlineCode = flight.FlightNumber.Substring(0, 2).ToUpper();

            // Check if the airline code exists in the Airlines dictionary
            return Airlines.ContainsKey(airlineCode) ? Airlines[airlineCode] : null;
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
            return $"Loading Airlines...\n" +
                   $"{Airlines.Count} Airlines Loaded!\n" +
                   $"Loading Boarding Gates...\n" +
                   $"{BoardingGates.Count} Boarding Gates Loaded!" +
                   $"Loading Flights...\n"+
                   $"{Flights.Count} Flights Loaded!\n";
            }
        }
    }
