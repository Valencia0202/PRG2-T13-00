using Microsoft.VisualBasic;
using PRG2_T13_00;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

// Zhe Ling features 2, 3, 5, 6 & 9, 
// Valencia features 1, 4, 7 & 8.  
internal class Program
{
    static void Main(string[] args)
    {

        // 1)	Load files (airlines and boarding gates)a
        //load the airlines.csv file
        string[] csvlinesAirline = File.ReadAllLines("airlines.csv");
        Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
        //	create the Airline objects based on the data loaded
        for (int i = 1; i < csvlinesAirline.Length; i++) // Skip header row
        {
            string[] line = csvlinesAirline[i].Split(',');
            string airlineName = line[0].Trim();
            string airlineCode = line[1].Trim();

            Airline airline = new Airline
            {
                Name = airlineName,
                Code = airlineCode
            };

            airlineDict[airlineCode] = airline; // add the Airlines objects into an Airline Dictionary
        }

        //	load the boardinggates.csv file
        string[] csvlinesBG = File.ReadAllLines("boardinggates.csv");
        Dictionary<string, BoardingGate> BGDict = new Dictionary<string, BoardingGate>();
        // create the Boarding Gate objects based on the data loaded
        for (int i = 1; i < csvlinesBG.Length; i++) // Skip header row
        {
            string[] line = csvlinesBG[i].Split(',');
            string gateName = line[0].Trim();
            bool supportsCFFT = bool.Parse(line[1].Trim());
            bool supportsDDJB = bool.Parse(line[2].Trim());
            bool supportsLWTT = bool.Parse(line[3].Trim());

            // Create a new BoardingGate object using the parameterized constructor
            BoardingGate boardingGate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT, null);

            // add the Boarding Gate objects into a Boarding Gate dictionary
            BGDict.Add(gateName, boardingGate);
        }


        // 2)	Load files (flights)
        string[] csvlines = File.ReadAllLines("Flights.csv");
        Dictionary<string, Flight> flightdict = new Dictionary<string, Flight>();
        for (int i = 1; i < csvlines.Length; i++)
        {
            string[] lines = csvlines[i].Split(',');
            string flightno = lines[0];
            string origin = lines[1];
            string dest = lines[2];
            DateTime datetime = Convert.ToDateTime(lines[3]);
            string code = lines[4];
            //create new flight object
            Flight flight = new Flight(flightno, origin, dest, datetime, "On Time");
            flightdict.Add(flight.FlightNumber, flight);
            //add to self created dict or the dict in airline class
        }

        // print contents of dictionary

        //menu printing
        static void Main(string[] args)
        {
            // Load the data as you already have
            // Airline, BoardingGate, and Flight dictionaries

            }
            else if (code == "LWTT")
            {
                Flight flight3 = new LWTTFlight(flightno, origin, dest, datetime, "On Time", 500.00);
                flightdict.Add(flight3.FlightNumber, flight3);

            }
            else
            {
                Flight flight4 = new NORMFlight(flightno, origin, dest, datetime, "On Time");
                flightdict.Add(flight4.FlightNumber, flight4);

            }

        }

        // print contents of dictionary


        //menu printing
        void Displaymenu()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("Welcome to Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine("1. List All Flights");
            Console.WriteLine("2. List Boarding Gates");
            Console.WriteLine("3. Assign a Boarding Gate to a Flight");
            Console.WriteLine("4. Create Flight");
            Console.WriteLine("5. Display Airline Flights");
            Console.WriteLine("6. Modify Flight Details");
            Console.WriteLine("7. Display Flight Schedule");
            Console.WriteLine("0. Exit");

        }



        // 3)	List all flights with their basic information
        void Displayflights()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Flights for Changi Airport Terminal 5");
            string[] heading = csvlines[0].Split(",");
            Console.WriteLine("=============================================");
            // Adjusted column widths for better formatting
            Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-20}", heading[0], "Airline Name", heading[1], heading[2], heading[3]);

            // Loop through each flight in flightdict and print the details
            foreach (var flight in flightdict)
            {

                // Extract the airline code (e.g., "SQ" from "SQ 115")
                var flightcode = flight.Key.Split(' ')[0];
                // Retrieve the airline name using the airline code
                var fname = airlineDict.ContainsKey(flightcode) ? airlineDict[flightcode].Name : "Unknown Airline";

                // Print the flight details
                Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-20}", flight.Key, fname, flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime.ToString("dd/MM/yyyy hh:mm tt"));
            }
        }
        Displayflights();

        // 4)	List all boarding gates(V)
        static void listBG(Dictionary<string, BoardingGate> BGDict)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Borading Gatess for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"GateName",-15}{"DDJB",-20}{"CFFT",-20}{"LWTT",-20}");
            foreach (var item in BGDict)
            {

                Console.WriteLine($"{item.Value.GateName,-15}{item.Value.SupportsDDJB,-20}{item.Value.SupportsCFFT,-20}{item.Value.SupportsLWTT,-20}");
            }
        }

        // 5)	Assign a boarding gate to a flight
  

        //// 6)	Create a new flight

        void createflight()
        {
            void addflight()
            {
                Console.WriteLine("Enter new flight (Flight Number, Origin, Destination, and Expected Departure/Arrival Time) ");
                var flight = Console.ReadLine();
                Console.WriteLine("Would you like to enter any special request code? Y/N");
                string ans = Console.ReadLine();
                if (ans == "Y")
                {

                }
                else { }
                string[] flightinfo = flight.Split(',');
                DateTime ET = Convert.ToDateTime(flightinfo[3]);
                Flight flight1 = new Flight(flightinfo[0], flightinfo[1], flightinfo[2], ET, "On Time");
                flightdict.Add(flightinfo[0], flight1);
                //append new info to csv file
            }
            while (true)
            {
                addflight();
                Console.WriteLine("Would you like to add another flight? Y/N");
                string add = Console.ReadLine();
                if (add == "Y")
                {
                    createflight();
                }
                else
                {
                    Console.WriteLine("Flight(s) have been successfully added.");
                    break;
                }
            }

        }

            }
            else if (opt == 2)
            {
                
            }
       
        }





        //9	Validations (and feedback)

    }
}

    //5	Assign a boarding gate to a flight (ZL)
