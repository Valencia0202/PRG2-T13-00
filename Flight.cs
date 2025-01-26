using System;
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
    abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public Flight() { }
        public Flight(string Fn, string o, string d, DateTime ET, string s)
        {
            FlightNumber = Fn;
            Origin = o;
            Destination = d;
            ExpectedTime = ET;
            Status = s;
        }
        public virtual double CalculateFees()
        {
            double fees = 0;

            if (Destination == "SIN")
            {
                fees = 500; // Fee when destination is SIN
            }
            else if (Origin == "SIN")
            {
                fees = 800; // Fee when origin is SIN
            }

            return fees;

        }
        public override string ToString()
        {
            return $"FlightNumber:{FlightNumber:10} \tOrigin:{Origin:10}  \tDestination:{Destination:10}  \tExpected Time: {ExpectedTime.ToString("MM/dd/yyyy h:mm tt"):10}";
        }
    }
}
