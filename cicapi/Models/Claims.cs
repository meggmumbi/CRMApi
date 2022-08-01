using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cicapi.Models
{
    public class Claims
    {
        public string CustomerAccountNumber
        {
            get;
            set;
        }
        public string ClaimNumber
        {
            get;
            set;
        }
        public string IncurredDate
        {
            get;
            set;
        }
        public string ClauseCode
        {
            get;
            set;
        }
        public string ClauseCodeDescription
        {
            get;
            set;
        }
        public string AccountNumber
        {
            get;
            set;
        }
        public string CustomerName
        {
            get;
            set;
        }
        public float TotalClaimAmount
        {
            get;
            set;
        }
        public float InvoicedAmount
        {
            get;
            set;
        }
        public float PayableAmount
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