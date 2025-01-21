using PRG2_T13_00;

internal class Program
{
    private static void Main(string[] args)
    {
        //1




        //2
        string[] csvlines = File.ReadAllLines("Flights.csv");
        Dictionary<string,Flight> flightdict = new Dictionary<string,Flight>();
        for(int i = 1; i < csvlines.Length; i++)
        {
            string[] lines = csvlines[i].Split(',');
            string flightno = lines[0];
            string origin = lines[1];
            string dest = lines[2];
            DateTime datetime = Convert.ToDateTime(lines[3]);
            Flight flight = new Flight(flightno,origin,dest,datetime,"On Time");
            flightdict.Add(flight.FlightNumber,flight);
        }
        foreach(var item in flightdict)
        {
            Console.Write("{0}\n        {1}\n",item.Key,item.Value);
        }


        //3


        //4


        //5


        //6



        //7


        //8




        //9

    }
}



