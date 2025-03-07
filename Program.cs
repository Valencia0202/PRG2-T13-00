﻿using Microsoft.VisualBasic;
using PRG2_T13_00;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

// Zhe Ling features 2, 3, 5, 6 & 9, adv a
// Valencia features 1, 4, 7 & 8.  adv b

//==========================================================
// Student Number	: S10268058
// Student Name	: Chai Zhe Ling
// Partner Name	: Valencia Amora Chua Lie Cia
//==========================================================
internal class Programm
{

    static void Main(string[] args)
    {
        Terminal terminal5 = new Terminal("Terminal 5");

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
            terminal5.AddAirline(airline);


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
            terminal5.AddBoardingGate(boardingGate);
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
            //create new flight object with the codes
            if (code == "DDJB")
            {
                Flight flight = new DDJBFlight(flightno, origin, dest, datetime, "On Time", 300.00);
                flightdict.Add(flight.FlightNumber, flight);
                terminal5.Flights.Add(flight.FlightNumber, flight);

            }
            else if (code == "CFFT")
            {
                Flight flight = new CFFTFlight(flightno, origin, dest, datetime, "On Time", 150.00);
                flightdict.Add(flight.FlightNumber, flight);
                terminal5.Flights.Add(flight.FlightNumber, flight);


            }
            else if (code == "LWTT")
            {
                Flight flight = new LWTTFlight(flightno, origin, dest, datetime, "On Time", 500.00);
                flightdict.Add(flight.FlightNumber, flight);
                terminal5.Flights.Add(flight.FlightNumber, flight);


            }
            else
            {
                Flight flight = new NORMFlight(flightno, origin, dest, datetime, "On Time");
                flightdict.Add(flight.FlightNumber, flight);
                terminal5.Flights.Add(flight.FlightNumber, flight);



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
            Console.WriteLine("8. Mass Assign Gate");
            Console.WriteLine("9. Fees per Airline");
            Console.WriteLine("0. Exit");
        }
        Console.WriteLine(terminal5);
        while (true)

        {
            
            Displaymenu();
            Console.Write("Please select your option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    Displayflights();
                    break;
                case "2":
                    ListBG(BGDict);
                    break;
                case "3":
                    AssignBG(flightdict, BGDict);
                    break;
                case "4":
                    Createflight();
                    break;
                case "5":
                    DisplayAirlineFlight();
                    break;
                case "6":
                    ModifyFlightDetails();
                    break;
                case "7":
                    SortedFlights();
                    break;
                case "8":
                    ProcessFlights();
                    break;
                case "9":
                    DisplayFeePerAirline(terminal5);
                    // Implement Display Flight Schedule functionality
                    break;
                case "0":
                    Console.WriteLine("Goodbye!");
                    return; // Exit the program
                default:
                    Console.WriteLine("Invalid option, please try again.");
                    break;
            }
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
                Console.WriteLine("{0,-15} {1,-25} {2,-20} {3,-20} {4,-20}", flight.Key, fname, flight.Value.Origin, flight.Value.Destination, flight.Value.ExpectedTime.ToString("MM/dd/yyyy hh:mm:ss tt"));
            }
        }

        // 4)	List all boarding gates(V)
        static void ListBG(Dictionary<string, BoardingGate> BGDict)
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Borading Gates for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"GateName",-15}{"DDJB",-20}{"CFFT",-20}{"LWTT",-20}");
            foreach (var item in BGDict)
            {

                Console.WriteLine($"{item.Value.GateName,-15}{item.Value.SupportsDDJB,-20}{item.Value.SupportsCFFT,-20}{item.Value.SupportsLWTT,-20}");
            }
        }


        // 5)	Assign a boarding gate to a flight
        static void AssignBG(Dictionary<string, Flight> flightDict, Dictionary<string, BoardingGate> BGDict)
        {
            while (true)
            {
                Console.WriteLine("Enter Flight Number: ");
                string flightno = Console.ReadLine().Trim().ToUpper();
                if (!flightDict.ContainsKey(flightno))
                {
                    Console.WriteLine("Flight not found. Please try again.");
                    continue;
                }


                while (true)
                {
                    Console.WriteLine("Enter Boarding Gate Name: ");
                    string boardingGateName = Console.ReadLine().Trim().ToUpper();

                    if (!BGDict.ContainsKey(boardingGateName))
                    {
                        Console.WriteLine("Invalid Boarding Gate. Please try again.");
                        continue;
                    }
                    // add boarding gate to flight
                    Flight selectedFlight = flightDict[flightno];
                    Console.WriteLine($"Flight Number: {selectedFlight.FlightNumber}");
                    Console.WriteLine($"Origin: {selectedFlight.Origin}");
                    Console.WriteLine($"Destination: {selectedFlight.Destination}");
                    Console.WriteLine($"Expected Time: {selectedFlight.ExpectedTime}");
                    // get the code
                    string code = "";
                    if (selectedFlight is CFFTFlight)
                    {
                        code = "CFFT";
                    }
                    else if (selectedFlight is LWTTFlight)
                    {
                        code = "LWTT";
                    }
                    else if (selectedFlight is DDJBFlight)
                    {
                        code = "DDJB";
                    }
                    else if (selectedFlight is NORMFlight)
                    {
                        code = "None";
                    }
                    Console.WriteLine($"Special Request Code: {code}");
                    BoardingGate boardingGate = BGDict[boardingGateName];
                    if (boardingGate.Flight != null)
                    {
                        Console.WriteLine($"The Boarding Gate {boardingGateName} is already assigned to Flight {boardingGate.Flight.FlightNumber}.");
                        continue;
                    }
                    //check if the gate supports the code
                    if ((code == "CFFT" && !boardingGate.SupportsCFFT) ||
                        (code == "DDJB" && !boardingGate.SupportsDDJB) ||
                        (code == "LWTT" && !boardingGate.SupportsLWTT))
                    {
                        Console.WriteLine($"The Boarding Gate {boardingGateName} does not support the special request code {code}. Please try again."); //user validaton for boarding gate inputs that do not match special req codes
                        continue;
                    }
                    //assign the boarding gate
                    boardingGate.Flight = selectedFlight;
                    Console.WriteLine(boardingGate.ToString());

                    Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
                    string updateStatus = Console.ReadLine().ToUpper();
                    while (updateStatus != "Y" && updateStatus != "N")
                    {
                        Console.WriteLine("Invalid option. Please enter 'Y' for Yes or 'N' for No:");
                        updateStatus = Console.ReadLine().Trim().ToUpper();
                    }


                    if (updateStatus == "Y")
                    {
                        Console.WriteLine("1. Delayed");
                        Console.WriteLine("2. Boarding");
                        Console.WriteLine("3. On Time");
                        Console.WriteLine("Please select the new status of the flight:");
                        int statusOption; // user input stored
                        // read and convert users input into integer, tryparse to ensure program dos not crash due to invalid user input
                        if (int.TryParse(Console.ReadLine(), out statusOption))
                        {
                            // evaluate status option and perform the cases
                            switch (statusOption)
                            {
                                case 1:
                                    selectedFlight.Status = "Delayed";
                                    break;
                                case 2:
                                    selectedFlight.Status = "Boarding";
                                    break;
                                case 3:
                                    selectedFlight.Status = "On Time";
                                    break;
                                default:
                                    Console.WriteLine("Invalid option. Status set to default: On Time.");
                                    selectedFlight.Status = "On Time";
                                    break;
                            }
                        }
                    }
                    Console.WriteLine($"Flight {selectedFlight.FlightNumber} has been assigned to Boarding Gate {boardingGate.GateName}!");
                    if (updateStatus != "Y" || updateStatus != "N")
                    {

                    }
                    break;
                }
                break;
            }

        }

        // 6)	Create a new flight

        void Createflight()
        {
            while (true)
            {
                Console.Write("Enter Flight Number:");
                var flightno = Console.ReadLine().Trim().ToUpper();
                
                Console.Write("Enter Origin: ");
                string origin = Console.ReadLine();
                Console.Write("Enter Destination:");
                string dest = Console.ReadLine();
                Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm):");
                DateTime ET = Convert.ToDateTime(Console.ReadLine());


                Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None):");
                string code = Console.ReadLine().Trim().ToUpper();

                Airline airline = new Airline();
                //create new flight by code
                if (code == "None")
                {
                    Flight flights = new NORMFlight(flightno, origin, dest, ET, "On Time");
                    airline.AddFlight(flights);
                    flightdict.Add(flights.FlightNumber, flights);
                }

                else if (code == "DDJB")
                {
                    Flight flights = new DDJBFlight(flightno, origin, dest, ET, "On Time", 300.00);
                    airline.AddFlight(flights);
                    flightdict.Add(flights.FlightNumber, flights);
                }
                else if (code == "CFFT")
                {
                    Flight flights = new CFFTFlight(flightno, origin, dest, ET, "On Time", 150.00);
                    airline.AddFlight(flights);
                    flightdict.Add(flights.FlightNumber, flights);
                }
                else if (code == "LWTT")
                {
                    Flight flights = new LWTTFlight(flightno, origin, dest, ET, "On Time", 500.00);
                    airline.AddFlight(flights);
                    flightdict.Add(flights.FlightNumber, flights);
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
                Console.WriteLine($"Flight {flightno} has been added!");
                //prompt user to add another flight
                Console.Write("Would you like to add another flight? (Y/N)");
                string add = Console.ReadLine().ToUpper();
                if (add != "N" && add != "Y")
                {
                    Console.WriteLine("Invalid Option. Please Enter Y for yes and N for no");
                    add = Console.ReadLine().ToUpper();
                }
                if (add == "N")
                {
                    break;
                }

            }
        }


        // 7)	Display full flight details from an airline(V)
        void DisplayAirlineFlight()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Airlines for Changi Airport Terminal 5");
            Console.WriteLine("=============================================");
            Console.WriteLine($"{"Airline Code",-15}{"Airline Name",-20}");
            foreach (var airline in airlineDict.Values)
            {
                // Print airline code and name
                Console.WriteLine($"{airline.Code,-15}{airline.Name,-20}");
            }

            Console.Write("Enter Airline Code: "); //Prompts user for airline code
            string airlineOpt = Console.ReadLine().ToUpper();
            if (!airlineDict.ContainsKey(airlineOpt)) // validates input
            {
                Console.WriteLine("Invalid airline code. Please try again.");
            }

            // Get the airline object
            Airline selectedAirline = airlineDict[airlineOpt];

            Console.WriteLine("=============================================");
            Console.WriteLine($"List of flights for {selectedAirline.Name} ");
            Console.WriteLine("=============================================");

            Console.WriteLine($"{"Flight Number",-15}{"Airline Name",-20}{"Origin",-20}{"Destination",-20}{"Expected Departure/Arrival Time",-25}");
            // Filter and display flights operated by the selected airline
            foreach (var flight in flightdict.Values)
            {
                if (flight.FlightNumber.StartsWith(airlineOpt))
                {
                    Console.WriteLine($"{flight.FlightNumber,-15}{selectedAirline.Name,-20}{flight.Origin,-20}{flight.Destination,-20}{flight.ExpectedTime.ToString("MM/dd/yyyy hh:mm:ss tt"),-25}");
                }
            }
        }

        //8 Modify flight
        void ModifyFlightDetails()
        {
            DisplayAirlineFlight(); //show flights

            foreach (var flight in flightdict.Values)
            {
                string reqcode = "";  // Initialize reqcode

                // Determine reqcode based on the flight type
                if (flight is LWTTFlight)
                {
                    reqcode = "LWTT";
                }
                else if (flight is DDJBFlight)
                {
                    reqcode = "DDJB";
                }
                else if (flight is CFFTFlight)
                {
                    reqcode = "CFFT";
                }
                else
                {
                    reqcode = "NORM"; // Default reqcode for normal flights
                }

                // Display flight details with reqcode

                Console.Write("Choose an existing Flight to modify or delete: ");
                string flightNumber = Console.ReadLine().Trim().ToUpper(); // Normalize input
                if (!flightdict.ContainsKey(flightNumber))
                {
                    Console.WriteLine("Invalid Flight Number. Returning to main menu...");
                    return;
                }

                Console.WriteLine("1. Modify Flight");
                Console.WriteLine("2. Delete Flight");
                Console.Write("Choose an option: ");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine("1. Modify Basic Information");
                    Console.WriteLine("2. Modify Status");
                    Console.WriteLine("3. Modify Special Request Code");
                    Console.WriteLine("4. Modify Boarding Gate");

                    // Retrieve the flight to modify
                    Flight flightToModify = flightdict[flightNumber];
                    var boardingGate = BGDict.Values.FirstOrDefault(gate => gate.Flight == flightToModify);
                    string gateNumber = boardingGate != null ? boardingGate.GateName : "Not Assigned";

                    string modChoice;
                    bool isValidInput = false;

                    // Loop until a valid input is received
                    while (!isValidInput)
                    {
                        Console.Write("Choose an Option: ");
                        modChoice = Console.ReadLine();

                        // Validate the input
                        if (modChoice == "1" || modChoice == "2" || modChoice == "3" || modChoice == "4")
                        {
                            isValidInput = true;

                            switch (modChoice)
                            {
                                case "1":
                                    Console.Write("Enter new Origin: ");
                                    flightToModify.Origin = Console.ReadLine();
                                    Console.Write("Enter new Destination: ");
                                    flightToModify.Destination = Console.ReadLine();
                                    Console.Write("Enter new Expected Time (dd/MM/yyyy hh:mm): ");
                                    DateTime T = Convert.ToDateTime(Console.ReadLine());

                                    Console.WriteLine($"Flight Number: {flightToModify.FlightNumber,-15}");
                                    Console.WriteLine($"Airline Name: ");
                                    Console.WriteLine($"Origin: {flightToModify.Origin,-20}");
                                    Console.WriteLine($"Destination: {flightToModify.Destination,-20}");
                                    Console.WriteLine($"Expected Departure/Arrival Time: {T.ToString("MM/dd/yyyy hh:mm:ss tt"),-25}");
                                    Console.WriteLine($"Status: {flightToModify.Status,-20}");
                                    Console.WriteLine($"Special Request Code: {reqcode,-15}");
                                    Console.WriteLine($"Boarding Gate: {gateNumber,-20}");

                                    break;

                                case "2":
                                    Console.Write("Enter new Status: ");
                                    flightToModify.Status = Console.ReadLine();
                                    Console.WriteLine("Status updated successfully.");
                                    Console.WriteLine("");
                                    Console.WriteLine($"Flight Number: {flightToModify.FlightNumber,-15}");
                                    Console.WriteLine($"Airline Name: ");
                                    Console.WriteLine($"Origin: {flightToModify.Origin,-20}");
                                    Console.WriteLine($"Destination: {flightToModify.Destination,-20}");
                                    Console.WriteLine($"Expected Departure/Arrival Time: {flightToModify.ExpectedTime.ToString("MM/dd/yyyy hh:mm:ss tt"),-25}");
                                    Console.WriteLine($"Status: {flightToModify.Status,-20}");
                                    Console.WriteLine($"Special Request Code: {reqcode,-15}");
                                    Console.WriteLine($"Boarding Gate: {gateNumber,-20}");
                                    break;

                                case "3":
                                    Console.Write("Enter new Special Request Code (CFFT/DDJB/LWTT/None): ");
                                    string specialRequest = Console.ReadLine().Trim().ToUpper();

                                    if (specialRequest == "CFFT" || specialRequest == "LWTT" || specialRequest == "DDJB" || specialRequest == "NONE")
                                    {
                                        string SpecialRequestCode = specialRequest;
                                        Console.WriteLine("Special Request Code updated successfully.");
                                        Console.WriteLine($"Flight Number: {flightToModify.FlightNumber,-15}");
                                        Console.WriteLine($"Airline Name: ");
                                        Console.WriteLine($"Origin: {flightToModify.Origin,-20}");
                                        Console.WriteLine($"Destination: {flightToModify.Destination,-20}");
                                        Console.WriteLine($"Expected Departure/Arrival Time: {flightToModify.ExpectedTime.ToString("MM/dd/yyyy hh:mm:ss tt"),-25}");
                                        Console.WriteLine($"Status: {flightToModify.Status,-20}");
                                        Console.WriteLine($"Special Request Code: {SpecialRequestCode,-15}");
                                        Console.WriteLine($"Boarding Gate: {gateNumber,-20}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Special Request Code. Modification canceled.");
                                    }
                                    break;


                                case "4":
                                    string assignedGate = BGDict.Values.FirstOrDefault(g => g.Flight == flightToModify)?.GateName ?? "None";
                                    Console.WriteLine("Available Boarding Gates:");
                                    Console.WriteLine($"{"Gate Name",-15}{"Supports DDJB",-15}{"Supports CFFT",-15}{"Supports LWTT",-15}{"Assigned Flight",-20}");

                                    foreach (var gate in BGDict.Values)
                                    {
                                        string assignedFlight = gate.Flight != null ? gate.Flight.FlightNumber : "None"; // ✅ Check if a flight is assigned
                                        Console.WriteLine($"{gate.GateName,-15}{gate.SupportsDDJB,-15}{gate.SupportsCFFT,-15}{gate.SupportsLWTT,-15}{assignedFlight,-20}");
                                    }


                                    Console.Write("Enter new Boarding Gate: ");
                                    string newGate = Console.ReadLine().Trim().ToUpper();

                                    if (BGDict.ContainsKey(newGate))
                                    {
                                        BoardingGate selectedGate = BGDict[newGate];

                                        // Check if the gate supports the flight type
                                        if ((flightToModify is DDJBFlight && !selectedGate.SupportsDDJB) ||
                                            (flightToModify is CFFTFlight && !selectedGate.SupportsCFFT) ||
                                            (flightToModify is LWTTFlight && !selectedGate.SupportsLWTT))
                                        {
                                            Console.WriteLine("This gate does not support the flight's special request type. Modification canceled.");
                                        }
                                        else
                                        {
                                            // Update the gate's assigned flight
                                            selectedGate.Flight = flightToModify;
                                            Console.WriteLine($"Flight Number: {flightToModify.FlightNumber,-15}");
                                            Console.WriteLine($"Airline Name: ");
                                            Console.WriteLine($"Origin: {flightToModify.Origin,-20}");
                                            Console.WriteLine($"Destination: {flightToModify.Destination,-20}");
                                            Console.WriteLine($"Expected Departure/Arrival Time: {flightToModify.ExpectedTime.ToString("MM/dd/yyyy hh:mm:ss tt"),-25}");
                                            Console.WriteLine($"Status: {flightToModify.Status,-20}");
                                            Console.WriteLine($"Special Request Code: {reqcode,-15}");
                                            Console.WriteLine($"Boarding Gate: {selectedGate,-20}");
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid Boarding Gate. Modification canceled.");
                                    }
                                    break;

                            }
                            break;
                        }


                        else if (choice == 2)
                        {
                            Console.Write("Are you sure that you would like to delete the flight? (Y/N): ");
                            string ans = Console.ReadLine().Trim().ToUpper(); // Normalize input

                            if (ans == "Y")
                            {
                                Console.Write("Enter the Flight Number to delete: ");
                                string flightNumberToDelete = Console.ReadLine().Trim().ToUpper(); // Normalize input

                                // Check if the flight number exists in the dictionary
                                if (flightdict.ContainsKey(flightNumberToDelete))
                                {
                                    // Remove the flight from the dictionary
                                    flightdict.Remove(flightNumberToDelete);
                                    DisplayAirlineFlight();
                                    Console.WriteLine($"Flight {flightNumberToDelete} has been deleted successfully.");
                                }
                                else
                                {
                                    Console.WriteLine($"Flight {flightNumberToDelete} not found. No flight deleted.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Flight deletion canceled.");
                            }




                        }

                    }
                }break;
            }
        }

        //9 Display Scheduled flights

        void SortedFlights()
        {
            var sortedFlights = flightdict.Values.ToList();

            sortedFlights.Sort();
            Console.WriteLine("Scheduled Flights for the Day:\n");
            string reqcode = "";
            Console.WriteLine($"{"Flight Number",-15} {"Airline Name",-20} {"Origin",-20} {"Destination",-20} {"Expected Departure/Arrival Time",-30} {"Status",-10} {"Boarding Gate",-15}");
            foreach (var flight in sortedFlights)
            {
                if (flight is LWTTFlight)
                {
                    reqcode = "LWTT";
                }
                else if (flight is DDJBFlight)
                {
                    reqcode = "DDJB";
                }
                else if (flight is CFFTFlight)
                {
                    reqcode = "CFFT";
                }
                BoardingGate boardingGate = null;
                foreach (var gate in BGDict.Values)
                {
                    if (gate.Flight == flight)  // Check if this gate is assigned to the flight
                    {
                        boardingGate = gate;
                        break;
                    }
                }
                string boardinginfo = " ";
                if (boardingGate != null)
                {
                    boardinginfo = boardingGate.GateName; //print the assigned boarding gate
                }
                else
                {
                    boardinginfo = "Unassigned";
                }
                var flightCode = flight.FlightNumber.Split(' ')[0];

                var airlineName = airlineDict.ContainsKey(flightCode) ? airlineDict[flightCode].Name : "Unknown Airline";
                string formattedTime = flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt");


                Console.WriteLine($"{flight.FlightNumber,-15} {airlineName,-20} {flight.Origin,-20} {flight.Destination,-20} {formattedTime,-30} {"Scheduled",-10} {boardinginfo,-15}");
            }
        }


        // Advance part a

        //Advance part a
        void ProcessFlights()

        {
            Queue<Flight> unassignedFlightQueue = new Queue<Flight>();

            // Identify unassigned flights
            foreach (var flight in flightdict.Values)
            {
                if (!BGDict.Values.Any(gate => gate.Flight == flight)) //for each gate in the dictionary, it checks whether the Flight property in gates is equal to the flight object provided.
                {
                    unassignedFlightQueue.Enqueue(flight); //add flight to queue if returned true (no gates have the specified flight assigned)
                }
            }

            // Count unassigned flights and gates
            int unassignedFlights = unassignedFlightQueue.Count;
            int unassignedGates = BGDict.Values.Count(gate => gate.Flight == null); //For each gate in the dictionary values, the expression checks if gate.Flight is null—meaning the gate is not assigned to any flight.

            Console.WriteLine($"Total Unassigned Flights: {unassignedFlights}");
            Console.WriteLine($"Total Unassigned Boarding Gates: {unassignedGates}");

            // Process each flight in the queue
            int automaticallyAssignedFlights = 0;
            int manuallyAssignedFlights = 0;
            int alreadyAssignedFlights = flightdict.Count - unassignedFlights;

            while (unassignedFlightQueue.Count > 0)
            {
                Flight flight = unassignedFlightQueue.Dequeue(); // Get the first flight

                BoardingGate assignedGate = null;
                string reqCode = "";
                if (flight is DDJBFlight)
                {
                    reqCode = "DDJB";
                }
                else if (flight is LWTTFlight)
                {
                    reqCode = "LWTT";
                }
                else if (flight is CFFTFlight)
                {
                    reqCode = "CFFT";
                }
                else if (flight is NORMFlight)
                {
                    reqCode = "NORM";
                }
                // Find a matching unassigned boarding gate
                foreach (var gate in BGDict.Values)
                {
                    if (gate.Flight == null) // Gate is unassigned
                    {

                        // Check if the gate supports the flight's special request
                        if (reqCode == "NORM")
                        {
                            // If the flight has no special request, assign it to any unassigned gate
                            assignedGate = gate;
                            break;
                        }
                        else if ((reqCode == "DDJB" && gate.SupportsDDJB) ||
                                 (reqCode == "CFFT" && gate.SupportsCFFT) ||
                                 (reqCode == "LWTT" && gate.SupportsLWTT))
                        {
                            // If the flight has a special request, assign it to a gate that supports the request
                            assignedGate = gate;
                            break;
                        }
                    }
                }

                // Assign the gate to the flight
                if (assignedGate != null)
                {
                    assignedGate.Flight = flight;
                    automaticallyAssignedFlights++; // increase by 1 when a flight is  assigned

                    // Print the flight details
                    var flightCode = flight.FlightNumber.Split(' ')[0];
                    //Get AirlineName
                    var airlineName = airlineDict.ContainsKey(flightCode) ? airlineDict[flightCode].Name : "Unknown Airline";

                    Console.WriteLine("\nFlight Assigned:");
                    Console.WriteLine($"Flight Number: {flight.FlightNumber}");
                    Console.WriteLine($"Airline Name: {airlineName}");
                    Console.WriteLine($"Origin: {flight.Origin}");
                    Console.WriteLine($"Destination: {flight.Destination}");
                    Console.WriteLine($"Expected Departure/Arrival: {flight.ExpectedTime:MM/dd/yyyy hh:mm:ss tt}");
                    Console.WriteLine($"Special Request Code: {(string.IsNullOrEmpty(reqCode) ? "None" : reqCode)}");
                    Console.WriteLine($"Boarding Gate: {assignedGate.GateName}");
                    Console.WriteLine("Status: Assigned\n");
                }
                else
                {
                    Console.WriteLine($"No available gate for Flight {flight.FlightNumber} (Special Request: {reqCode})");
                    manuallyAssignedFlights++;
                }
            }

            // Display summary
            int totalFlightsProcessed = flightdict.Count;
            int totalGatesProcessed = BGDict.Count;
            int alreadyAssignedGates = BGDict.Count - unassignedGates;

            double flightAutoAssignPercentage = (double)automaticallyAssignedFlights / alreadyAssignedFlights * 100;
            double gateAutoAssignPercentage = (double)automaticallyAssignedFlights / alreadyAssignedGates * 100;

            Console.WriteLine("Summary of Flight and Boarding Gate Processing:");
            Console.WriteLine($"Total Flights Processed: {totalFlightsProcessed}");
            Console.WriteLine($"Total Boarding Gates Processed: {totalGatesProcessed}");
            Console.WriteLine($"Already Assigned Flights: {alreadyAssignedFlights}");
            Console.WriteLine($"Already Assigned Gates: {alreadyAssignedGates}");
            Console.WriteLine($"Automatically Assigned Flights: {automaticallyAssignedFlights}");
            Console.WriteLine($"Manually Assigned Flights: {manuallyAssignedFlights}");
            Console.WriteLine($"Percentage of Automatically Assigned Flights: {flightAutoAssignPercentage:F2}%");
            Console.WriteLine($"Percentage of Automatically Assigned Boarding Gates: {gateAutoAssignPercentage:F2}%");

        }

        //Advance part B
        void DisplayFeePerAirline(Terminal terminal5)
        {
            // Ensure all flights have assigned boarding gates before proceeding
            if (!terminal5.Flights.Values.All(f => terminal5.BoardingGates.Values.Any(gate => gate.Flight == f)))
            {
                Console.WriteLine("Ensure all flights have their Boarding Gates assigned before running this feature again.");
                return;
            }

            // Initialize total fees and discounts
            double totalFees = 0, totalDiscounts = 0;
            // Set of airport codes eligible for a discount
            HashSet<string> DiscountOrigins = new HashSet<string> { "DXB", "BKK", "NRT" }; // Discounted origins
           // Dictionary to store final airline fees
            Dictionary<string, double> finalAirlineFees = new Dictionary<string, double>();
            // Associate flights with their respective airlines
            foreach (var flight in terminal5.Flights.Values)
            {
                string airlineCode = flight.FlightNumber.Substring(0, 2).ToUpper(); // Extract airline code

                // Find the corresponding airline
                Airline airline = terminal5.Airlines.Values.FirstOrDefault(a => a.Code == airlineCode);

                if (airline != null)
                {
                    airline.AddFlight(flight); // Add flight to airline
                }
                else
                {
                    Console.WriteLine($"Warning: No airline found for flight {flight.FlightNumber}");
                }
            }


            // Iterate over each airline to calculate fees and discounts

            foreach (var airline in terminal5.Airlines.Values)
            {
                double airlineSubtotal = 0, airlineDiscounts = 0;

                // Retrieve flights associated with the airline
                var flights = terminal5.Flights.Values.Where(f => terminal5.GetAirlineFromFlight(f) == airline).ToList();

                // Skip airlines without flights
                if (flights.Count == 0)
                {
                    Console.WriteLine($"Airline {airline.Name} has no flights.");
                    continue;
                }

                int flightsWithNoRequest = 0, eligibleFlightsForDiscount = 0;

                // Process each flight associated with the airline
                foreach (var flight in flights)
                {
                    double flightFee = 300; // Boarding Gate Base Fee

                    // Additional fees based on departure and arrival locations
                    if (flight.Origin == "SIN") flightFee += 800; // Departing fee
                    if (flight.Destination == "SIN") flightFee += 500; // Arriving fee

                    // Determine the special request code of the flight
                    string reqCode = (flight is CFFTFlight) ? "CFFT" :
                                     (flight is DDJBFlight) ? "DDJB" :
                                     (flight is LWTTFlight) ? "LWTT" : null;


                    // Apply additional fees based on the request type
                    if (reqCode == "CFFT") flightFee += 150;
                    else if (reqCode == "DDJB") flightFee += 300;
                    else flightsWithNoRequest++;

                    if (DiscountOrigins.Contains(flight.Origin)) airlineDiscounts += 25; // $25 Discount for certain origins

                    airlineSubtotal += flightFee;
                    eligibleFlightsForDiscount++;
                }

                // Apply bulk discounts
                airlineDiscounts += (eligibleFlightsForDiscount / 3) * 350; // $350 for every 3 flights
                airlineDiscounts += flightsWithNoRequest * 50; // $50 per flight without special request
                if (flights.Count > 5) airlineDiscounts += (airlineSubtotal * 0.03); // Extra 3% discount for >5 flights

                // Calculate the final fee for the airline
                double finalFee = airlineSubtotal - airlineDiscounts;
                totalFees += airlineSubtotal;
                totalDiscounts += airlineDiscounts;

                // Store final fee per airline
                finalAirlineFees[airline.Name] = finalFee;
            }

            // Process each airline
            foreach (var airline in terminal5.Airlines.Values)
            {

                double airlineSubtotal = 0, airlineDiscounts = 0;
                var flights = terminal5.Flights.Values.Where(f => terminal5.GetAirlineFromFlight(f) == airline).ToList();
                // Display final airline fees separately
                Console.WriteLine("\n--- Final Fees Per Airline ---");
                foreach (var entry in finalAirlineFees)
                {
                    Console.WriteLine($"Airline: {entry.Key} | Final Fee: ${entry.Value:F2}");
                }

                // Display overall summary
                double finalTotal = totalFees - totalDiscounts;
                double discountPercentage = totalFees > 0 ? (totalDiscounts / totalFees) * 100 : 0;

                Console.WriteLine("\n--- Summary for the Day ---");
                Console.WriteLine($"Total Subtotal Fees: ${totalFees:F2}");
                Console.WriteLine($"Total Discounts: -${totalDiscounts:F2}");
                Console.WriteLine($"Final Total Fees: ${finalTotal:F2}");
                Console.WriteLine($"Discount Percentage: {discountPercentage:F2}%");
                break;
            }


        }
    }
}




