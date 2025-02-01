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
    class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        //constructor
        public BoardingGate(string gateName, bool supportsDDJB, bool supportsCFFT, bool supportsLWTT, Flight flight)
        {
            GateName = gateName;
            SupportsCFFT = supportsCFFT;
            SupportsDDJB = supportsDDJB;
            SupportsLWTT=supportsLWTT; 
            Flight = flight;
        }

        public double CalculateFees()
        {
            double baseFee = 300; // Base fee for 
            return baseFee;
        }

        public override string ToString()
        {
            return $"Boarding Gate Name: {GateName}\n" +
                   $"Supports DDJB: {SupportsDDJB}\n" +
                   $"Supports CFFT: {SupportsCFFT}\n" +
                   $"Supports LWTT: {SupportsLWTT}";
        }
    }
}
