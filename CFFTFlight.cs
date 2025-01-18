using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_00
{
    internal class CFFTFlight:Flight
    {
        public double RequestFee {  get; set; }
       
        public override double CalculateFees()
        {
            double fee = 150;
            return fee;
        }
        public override string ToString()
        {
            return base.ToString() + "RequestFee: "+ RequestFee;
        }
    }
}
