using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cicapi.Models
{
    public class Token
    {
        public string status { get; set; }
        public string message { get; set; }

        public string agentCode { get; set; }
        public string email { get; set; }
        public string accesstoken { get; set; }
        public string expires { get; set; }
        public string timeout { get; set; }
        
        
    }
}