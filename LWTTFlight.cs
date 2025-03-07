﻿using System;
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
    internal class LWTTFlight:Flight
    {
        public double RequestFee { get; set; }
        public override double CalculateFees()
        {
            if (base.CalculateFees() > 0)
            {
                double fees = base.CalculateFees() + RequestFee + 300;
                return fees;
            }
            return base.CalculateFees();
        }
        public LWTTFlight(string Fn, string o, string d, DateTime ET, string s, double Rf) : base(Fn, o, d, ET, s)
        {
            RequestFee = Rf;
        }
        public override string ToString()
        {
            return base.ToString() + "\tRequestFee:"+RequestFee;
        }
    }
}
