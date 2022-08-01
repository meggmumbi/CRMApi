using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cicapi.Models
{
    public class Beneficiary
    {
        public string Name { get; set; }

        public string Relationship { get; set; }        

        public double Allocation { get; set; }       

        public int IdNumber { get; set; }       

        public string DateOfBirth { get; set; }

        public string EffectiveDate { get; set; }

        public int LineNo { get; set; }
    }
}