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
    class NORMFlight:Flight
    {
        public override double CalculateFees()
        {
            if (base.CalculateFees() > 0)
            {
                double fees = base.CalculateFees() + 300 ;
                return fees;
            }
            return base.CalculateFees();
        }
        public NORMFlight(string Fn, string o, string d, DateTime ET, string s) : base(Fn, o, d, ET, s) {}
        public override string ToString()
        {
            return base.ToString();
            
        }
    }
}
