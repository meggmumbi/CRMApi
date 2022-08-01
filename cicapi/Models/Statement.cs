using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cicapi.Models
{
    public class Statement
    {
       // public string TransactionDate { get; set; }        
       //
       // public string ReceiptNumber { get; set; }
       //
       // public string TransactionType { get; set; }       
       //
       // public string FundCode { get; set; }
       //
       // public string FundName { get; set; }        
       //
       // public double CustomerAccountNumber { get; set; }
       //
       // public string CustomerName { get; set; }
       //
       // public string AccountNumber { get; set; }
       //
       // public double NoOfUnits { get; set; }
       //
       // public double Value { get; set; }
       //
       // public string ModeOfPayment { get; set; }
       //
       // public string Currency { get; set; }
       //
       // public double ExchangeRate { get; set; }



        public string PostingDate { get; set; }

        public DateTime UnformatedDate { get; set; }

        public string DocumentNumber { get; set; }

        public double Amount { get; set; }

        public string FormatedAmount { get; set; }

        public string ContributionType { get; set; }

        public Statement(string contributionType, string postingDate, double amount)
        {
            this.ContributionType = contributionType;
            this.PostingDate = postingDate;
            this.Amount = amount;
        }

        public Statement()
        {
        }
    }
}