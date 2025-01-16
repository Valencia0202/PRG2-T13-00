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

        }
        public override string ToString()
        {
            return base.ToString() + "RequestFee: "+ RequestFee;
        }
    }
}
