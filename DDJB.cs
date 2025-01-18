using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG2_T13_00
{
    internal class DDJB:Flight
    {
        public double RequestFee {  get; set; }
        public override double CalculateFees()
        {
            double fee = 300;
           return fee;
        }
        public override string ToString()
        {
            return base.ToString() + "RequestFee:" + RequestFee;
        }
    }
}
