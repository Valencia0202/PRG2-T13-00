using PRG2_T13_00;
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


        // Optional: Print the dictionary to verify
        Console.WriteLine("Boarding Gates:");
        foreach (var item in BGDict)
        {
            Console.WriteLine($"Gate: {item.Value.GateName}, CFFT: {item.Value.SupportsCFFT}, DDJB: {item.Value.SupportsDDJB}, LWTT: {item.Value.SupportsLWTT}");
        }


        // Print the contents of the Airline dictionary
        Console.WriteLine("Airlines:");
        foreach (var item in airlineDict)
        {
            Console.WriteLine($"Name: {item.Value.Name}, Code: {item.Value.Code}");
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
            Flight flight = new Flight(flightno,origin,dest,datetime,"On Time");
            flightdict.Add(flight.FlightNumber,flight);
        }
        // print contents of dictionary
     
        //menu printing
        void displaymenu()
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
        void displayflights()
        {
            Console.WriteLine("=============================================");
            Console.WriteLine("List of Flights for Changi Airport Terminal 5");
            string[] heading = csvlines[0].Split(",");
            Console.WriteLine("=============================================");
            Console.WriteLine("{0,10} {1,15} {2,20} {3,10} {4,10}",heading[0],"Airline Name", heading[1], heading[2], heading[3]);
            foreach(var flight in  flightdict)
            {
                var flightcode = flight.Key.Split(' ')[0];
                var fname = airlineDict[flightcode].Name;
                Console.WriteLine("{0,-10} {1,15} {2,20} {3,10} {4,10}",flight.Key,fname,flight.Value.Origin,flight.Value.Destination,flight.Value.ExpectedTime);
                
            }

        }
        displayflights();

        // 4)	Assign a boarding gate to a flight


        // 5)	Create a new flight


        // 6)	Display full flight details from an airline

        // 7)	Modify flight details


        //8)	 Display scheduled flights in chronological order, with boarding gates assignments where applicable





        //9	Validations (and feedback)
    }
}

