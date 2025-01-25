using Microsoft.VisualBasic;
using PRG2_T13_00;
using System;
using System.Collections.Generic;
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
            if (code == "DDJB")
            {
                Flight flight = new DDJBFlight(flightno, origin, dest, datetime, "On Time",300.00);
                flightdict.Add(flight.FlightNumber, flight);
            }
            else if (code == "CFFT")
            {
                Flight flight = new CFFTFlight(flightno, origin, dest, datetime, "On Time", 150.00);
                flightdict.Add(flight.FlightNumber, flight);

            }
            else if(code == "LWTT")
            {
                Flight flight = new LWTTFlight(flightno, origin, dest, datetime, "On Time", 500.00);
                flightdict.Add(flight.FlightNumber, flight);

            }
            else
            {
                Flight flight = new NORMFlight(flightno, origin, dest, datetime, "On Time");
                flightdict.Add(flight.FlightNumber, flight);

            }
            foreach(var f in flightdict)
            {
                Console.WriteLine("{0} {1}",f.Key, f.Value);
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
        listBG(BGDict);

        // 5)	Assign a boarding gate to a flight
        static void AssignBG(Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> bgDict)
        {
            while (true)
            {
                Console.WriteLine("Enter Flight Number: ");
                string flightno = Console.ReadLine();
                if (!flightDict.ContainsKey(flightno))
                {
                    Console.WriteLine("Flight not found. Please try again.");
                    continue;
                }
                Flight flight = flightDict[flightno];
                Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                Console.WriteLine($"Origin: {flight.Origin}");
                Console.WriteLine($"Destination: {flight.Destination}");
                Console.WriteLine($"Expected Time: {flight.ExpectedTime}");
                string code = "";
                if (flight is CFFTFlight)
                {
                    code = "CFFT";
                }
                else if (flight is LWTTFlight)
                {
                    code = "LWTT";
                }
                else if (flight is CFFTFlight)
                {
                    code = "CFFT";
                }
                else
                {
                    code = "None";
                }
                Console.WriteLine($"Special Request Code: {code}");

                //whiletrue
                Console.WriteLine("Enter Boarding Gate: ");
                string boardingGateName = Console.ReadLine();
                if (!bgDict.ContainsKey(boardingGateName))
                {
                    Console.WriteLine("Invalid Boarding Gate. Please try again.");
                    continue;
                }
                // add boarding gate to flight

                BoardingGate boardingGate = bgDict[boardingGateName];
                if (boardingGate.Flight != null)
                {
                    Console.WriteLine($"The Boarding Gate {boardingGateName} is already assigned to Flight {boardingGate.AssignedFlight}.");
                    continue;
                }
                boardingGate.Flight = flight.FlightNumber;
                flight.BoardingGate = boardingGateName;
            }
        }
     

        // 6)	Create a new flight

        void Createflight()
        {
            while (true)
            {    
                Console.Write("Flight Number:");
                var flightno = Console.ReadLine();

                Console.Write("Origin: ");
                string origin = Console.ReadLine();
                Console.Write("Destination:");
                string dest = Console.ReadLine();
                Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm):");
                DateTime ET = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("Enter Special Request Code (CFFT/DDJB/LWTT/None):");
                string code = Console.ReadLine();
                
                Airline airline = new Airline();
                if (code == "None")
                {
                    Flight flights = new NORMFlight(flightno, origin, dest, ET, "On Time");
                    airline.AddFlight(flights);
                }

                else if (code == "DDJB")
                {
                    Flight flights = new DDJBFlight(flightno, origin, dest, ET, "On Time", 300.00);
                    airline.AddFlight(flights);
                }
                else if (code == "CFFT")
                {
                    Flight flights = new CFFTFlight(flightno, origin, dest, ET, "On Time", 150.00);
                    airline.AddFlight(flights);

                }
                else if (code == "LWTT")
                {
                    Flight flights = new LWTTFlight(flightno, origin, dest, ET, "On Time", 500.00);
                    airline.AddFlight(flights);

                }

                //append new info to csv file
                using (StreamWriter sw = new StreamWriter("flights.csv", true))
                {
                    Dictionary<string, Flight> flightdict = airline.Flights;
                    string flightdetails = "";
                    foreach (var f in flightdict)
                    {
                        if (code == "None")
                        {
                            flightdetails = $"{f.Value.FlightNumber},{f.Value.Origin},{f.Value.Destination},{f.Value.ExpectedTime.ToString("h:mm tt")}";
                        }
                        else
                        {
                            flightdetails = $"{f.Value.FlightNumber},{f.Value.Origin},{f.Value.Destination},{f.Value.ExpectedTime.ToString("h:mm tt")},{code}";

                        }
                    }
                    sw.WriteLine(flightdetails);

                }
                Console.WriteLine("Flight" + flightno + "has been added !");
                //prompt user to add another flight
                Console.Write("Would you like to add another flight? (Y/N)");
                string add = Console.ReadLine().ToUpper();
                if (add == "N")
                {
                    break;
                }

            }
        }

        Createflight();

        // 7)	Display full flight details from an airline(V)




        // 8)	Modify flight details(V)



        //9	Validations (and feedback)





        //5	Assign a boarding gate to a flight (ZL)
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
    }

}