using PRG2_T13_00;

internal class Program
{
    private static void Main(string[] args)
    {
        //1




        //2
        string[] csvlines = File.ReadAllLines("Flights.csv");
        for(int i = 0; i < csvlines.Length; i++)
        {
            string[] lines = csvlines[i].Split(',');
            string flightno = lines[0];
            string origin = lines[1];
            string dest = lines[2];
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



