using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cicapi.Models
{
    public class CustomerAccountInformation
    {
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
        public string CustomerAccountNumber
        {
            get;
            set;
        }
        public string AccountNumber
        {
            get;
            set;
        }
        public DateTime InceptionDate
        {
            get;
            set;
        }
        public string IntermediaryCode
        {
            get;
            set;
        }
        public decimal NoOfUnits
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