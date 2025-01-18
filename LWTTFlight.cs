using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_00
{
    internal class LWTTFlight:Flight
    {
        public double RequestFee { get; set; }
        public override double CalculateFees()
        {
            double Fees = 500;
            return Fees;
        }
        public string ToString()
        {
            return base.ToString() + "RequestFee:"+RequestFee;
        }
    }
}
