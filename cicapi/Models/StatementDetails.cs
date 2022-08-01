using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cicapi.Models
{
    public class StatementDetails
    {
        public string CustomerNumber { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string Status { get; set; }

       // public string Base64String {get;set;}

        public byte[] Byte_Array { get; set; }
    }
}