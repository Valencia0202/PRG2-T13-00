using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_00
{
    public class BoardingGate
    {
        public string GateName { get; set; }
        public bool SupportsCFFT { get; set; }
        public bool SupportsDDJB { get; set; }
        public bool SupportsLWTT { get; set; }
        public Flight Flight { get; set; }

        //constructor
        public BoardingGate(string gateName, bool supportsCFFT, bool supportsDDJB, bool supportsLWTT, Flight flight)
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
            return $"Name: {GateName}\n" +
                   $"CFFT: {SupportsCFFT}\n" +
                   $"DDJB:  {SupportsDDJB}\n" +
                   CalculateFees() +
                   $"LWTT: {SupportsLWTT}";
        }
    }
}
