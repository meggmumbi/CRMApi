using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cicapi.Models
{
    public class AccountStatements
    {
        public string CustomerAccountNumber
        {
            get;
            set;
        }
        public string CustomerName
        {
            get;
            set;
        }
        public string FundCode
        {
            get;
            set;
        }
        public string FundName
        {
            get;
            set;
        }
        public string AccountNumber
        {
            get;
            set;
        }
        public decimal NoOfUnits
        {
            get;
            set;
        }
        public float FundValue
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }
    }
}