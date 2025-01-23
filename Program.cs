using Microsoft.VisualBasic;
using PRG2_T13_00;
using System.Collections.Generic;
using System.Security.Cryptography;
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
            //create new flight object
            Flight flight = new Flight(flightno, origin, dest, datetime, "On Time");
            flightdict.Add(flight.FlightNumber, flight);
        }

        // print contents of dictionary

        //menu printing
        static void Main(string[] args)
        {
            // Load the data as you already have
            // Airline, BoardingGate, and Flight dictionaries

            while (true)
            {
                displaymenu();
                Console.Write("Please select your option: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        displayflights();
                        break;
                    case "2":
                        DisplayBG(BGDict);
                        break;
                    case "3":
                        AssignBG(flightdict, BGDict);
                        break;
                    case "4":
                        // Implement Create Flight functionality
                        break;
                    case "5":
                        // Implement Display Airline Flights functionality
                        break;
                    case "6":
                        // Implement Modify Flight Details functionality
                        break;
                    case "7":
                        // Implement Display Flight Schedule functionality
                        break;
                    case "0":
                        Console.WriteLine("Exiting...");
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        break;
                }
            }
        }

        //while (true)
        //{
        //    displaymenu();
        //    Console.Write("Please select your option: ");
        //    string option = Console.ReadLine();
        //    if (option == "1")
        //    {
        //        displayflights();
        //    }
        //    else if (option =="2")
        //    {
        //        listBG(BGDict);
        //    }
        //}



        // 3)	List all flights with their basic information
        void displayflights()
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

        
       displayflights();

        // 4)	List all boarding gates(V)
        static void listBG(Dictionary<string, BoardingGate> bGDict)
        {
            Console.WriteLine($"{"GateName",-15}{"DDJB",-20}{"CFFT",-20}{"LWTT",-20}");
            foreach (var item in bGDict)
            {

                Console.WriteLine($"{item.Value.GateName,-15}{item.Value.SupportsDDJB,-20}{item.Value.SupportsCFFT,-20}{item.Value.SupportsLWTT,-20}");
            }
        }

    // 5)	Assign a boarding gate to a flight
    static void AssignBG(Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> bgDict)
    {
        while (true)
        {
            Console.WriteLine("Enter Flight Number:");
            string flightNumber = Console.ReadLine();

            if (!flightDict.ContainsKey(flightNumber))
            {
                Console.WriteLine("Flight not found. Please try again.");
                continue;
            }

            var flight = flightDict[flightNumber];
            Console.WriteLine($"Flight Number: {flight.FlightNumber}");
            Console.WriteLine($"Origin: {flight.Origin}");
            Console.WriteLine($"Destination: {flight.Destination}");
            Console.WriteLine($"Expected Time: {flight.ExpectedTime}");
            Console.WriteLine($"Special Request Code: {flight.SpecialRequestCode ?? "None"}");

            while (true)
            {
                Console.WriteLine("Enter Boarding Gate Name:");
                string boardingGateName = Console.ReadLine();

                if (!bgDict.ContainsKey(boardingGateName))
                {
                    Console.WriteLine("Invalid Boarding Gate. Please try again.");
                    continue;
                }

                var boardingGate = bgDict[boardingGateName];
                if (boardingGate.AssignedFlight != null)
                {
                    Console.WriteLine($"The Boarding Gate {boardingGateName} is already assigned to Flight {boardingGate.AssignedFlight}.");
                    continue;
                }

                // Assign the gate
                boardingGate.AssignedFlight = flight.FlightNumber;
                flight.BoardingGate = boardingGateName;
                break;
            }

            Console.WriteLine($"Flight Number: {flight.FlightNumber}");
            Console.WriteLine($"Origin: {flight.Origin}");
            Console.WriteLine($"Destination: {flight.Destination}");
            Console.WriteLine($"Expected Time: {flight.ExpectedTime}");
            Console.WriteLine($"Special Request Code: {flight.SpecialRequestCode ?? "None"}");
            Console.WriteLine($"Boarding Gate Name: {flight.BoardingGate}");

            Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
            string updateStatus = Console.ReadLine().ToUpper();

            if (updateStatus == "Y")
            {
                Console.WriteLine("1. Delayed");
                Console.WriteLine("2. Boarding");
                Console.WriteLine("3. On Time");
                Console.WriteLine("Please select the new status of the flight:");
                int statusOption;
                if (int.TryParse(Console.ReadLine(), out statusOption))
                {
                    switch (statusOption)
                    {
                        case 1:
                            flight.Status = "Delayed";
                            break;
                        case 2:
                            flight.Status = "Boarding";
                            break;
                        case 3:
                            flight.Status = "On Time";
                            break;
                        default:
                            Console.WriteLine("Invalid option. Status set to default: On Time.");
                            flight.Status = "On Time";
                            break;
                    }
                }
            }

            Console.WriteLine($"Flight {flight.FlightNumber} has been assigned to Boarding Gate {flight.BoardingGate}!");
            Console.WriteLine($"Current Status: {flight.Status}");
            break;
        }
    }

        // 6)	Create a new flight


    // 7)	Display full flight details from an airline(V)




    // 8)	Modify flight details(V)



    //9	Validations (and feedback)
}


    //5	Assign a boarding gate to a flight (ZL)
