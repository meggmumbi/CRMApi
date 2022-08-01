using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cicapi.Models
{
    public class TransactionHistoryModel
    {
        public DateTime TransactionDate { get; set; }

        public string ReceiptNumber { get; set; }

        public string TransactionType { get; set; }

        public string FundCode { get; set; }

        public string FundName { get; set; }

        public string CustomerAccountNumber { get; set; }

        public string CustomerName { get; set; }

        public string AccountNumber { get; set; }

        public double NoOfUnits { get; set; }

        public double Value { get; set; }

        public string ModeOfPayment { get; set; }

        public string Currency { get; set; }

        public double ExchangeRate { get; set; }
    }
}