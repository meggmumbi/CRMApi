using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using cicapi.Utils;
using cicapi.Models;
using System;
using cicapi.Properties;
using System.Collections.Generic;
using cicapi.NavOData;
using System.Linq.Expressions;
using System.Data.Services.Client;
using Newtonsoft.Json;
using log4net;
using System.Configuration;
using System.Net;
using RestSharp;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace cicapi.Controllers
{
    //cic API
     [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "X-My-Header")]
    [BasicAuthentication]
    [RoutePrefix("api/values")]

    public class ValuesController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
               


        [HttpGet]
        [Route("getAccountinformation")]
        public IHttpActionResult GetAccountInformation(CustomerDetails value)
        {
            User user = new User(value.CustomerNumber,"");
            List<CustomerAccountInformation> intermediaries1 = new List<CustomerAccountInformation>();
            var objectresJson = "";
            try
            {
                DataServiceQuery<Customers> intermediaries2 = DBConfig.ReturnNav(user.Myscheme.SchemeODataUrl).Customers;
                Expression<Func<Customers, bool>> predicate = (Expression<Func<Customers, bool>>)(r => r.No == value.CustomerNumber);
                foreach (Customers intermediaries3 in (IEnumerable<Customers>)intermediaries2.Where<Customers>(predicate))
                {
                    CustomerAccountInformation intermediary1 = new CustomerAccountInformation();
                    intermediary1.IntermediaryCode = intermediaries3.Purchaser_Code;
                    intermediary1.CustomerAccountNumber = intermediaries3.No;
                    intermediary1.FundCode = intermediaries3.Sponsor_Code;
                    intermediary1.FundName = intermediaries3.Sponsor_Name;
                    intermediary1.InceptionDate = Convert.ToDateTime((object)intermediaries3.Scheme_Join_Date);                    
                    intermediary1.AccountNumber = intermediaries3.No; 
                    intermediaries1.Add(intermediary1);
                    objectresJson = JsonConvert.SerializeObject(intermediaries1);
                    Log.Info("Intermediary Details Fetched Successfully");



                }
                return Json(intermediaries1);
            }
            catch (Exception ex)
            {
                var response = new { Title = "error", ResponseCode = "201", Description = ex.Message };
                var resposonseobJson = JsonConvert.SerializeObject(response);
                Log.Error(ex.Message);
                return Json(response);
            }

        }

        [HttpGet]
        [Route("getaccountstatement")]
        public IHttpActionResult AccountStatement([FromBody] StatementDetails value)
        {
            User user = new User(value.CustomerNumber, "");
            StatementDetails statementDetails = new StatementDetails();
            string customerNumber = value.CustomerNumber;
            var objectresJson = "";

            try
            {
                DateTime from = DateTime.Parse(value.FromDate);
                DateTime toDate = DateTime.Parse(value.ToDate);

                string bigText = "";

                new DBConfig().ObjNav(user.Myscheme.SchemeWsUrl).FnMemberStatementStream(customerNumber, ref bigText, from, toDate);
                
                statementDetails.Byte_Array = Convert.FromBase64String(bigText);
                statementDetails.CustomerNumber = value.CustomerNumber;
                statementDetails.FromDate = value.FromDate;
                statementDetails.ToDate = value.ToDate;
                statementDetails.Status = "success";

                objectresJson = JsonConvert.SerializeObject(statementDetails);
                Log.Info("Account Statement Generated Successfully");
                return Json(statementDetails);

            }
            catch (Exception ex)
            {
                var response = new { Title = "error", ResponseCode = "201", Description = ex.Message };
                var resposonseobJson = JsonConvert.SerializeObject(response);
                Log.Error(ex.Message);
                return Json(response);

            }
           
        }

        [HttpGet]
        [Route("gettransactionhistory")]
        public IHttpActionResult TransactionHistory(CustomerDetails value)
        {
            List<TransactionHistoryModel> source = new List<TransactionHistoryModel>();
            User user = new User(value.CustomerNumber, "");
            var objectresJson = "";
            try
            {
                IQueryable<TransactionHistory> queryable = DBConfig.ReturnNav(user.Myscheme.SchemeODataUrl).TransactionHistory.Where<TransactionHistory>((Expression<Func<TransactionHistory, bool>>)(r => r.Vendor_No == value.CustomerNumber));
               
                foreach (TransactionHistory memberStatement in (IEnumerable<TransactionHistory>)queryable)
                {
                    TransactionHistoryModel statement = new TransactionHistoryModel();                    
                    statement.TransactionDate = Convert.ToDateTime((object)memberStatement.Posting_Date);
                    statement.ReceiptNumber = memberStatement.Document_No;
                    statement.CustomerAccountNumber = memberStatement.Vendor_No;
                    statement.CustomerName = memberStatement.Name;
                    statement.Currency = memberStatement.Currency_Code;
                    statement.Value = Convert.ToDouble((object)memberStatement.Amount) * -1.0;
                    statement.TransactionType = memberStatement.Transaction_Type;
                    source.Add(statement);
                }

                List<TransactionHistoryModel> list = source.OrderByDescending<TransactionHistoryModel, DateTime>((Func<TransactionHistoryModel, DateTime>)(o => o.TransactionDate)).ToList<TransactionHistoryModel>();
                List<TransactionHistoryModel> memberStatement1 = new List<TransactionHistoryModel>();
                string str1 = "";
                string str2 = "";
                double num = 0.0;
                

                objectresJson = JsonConvert.SerializeObject(source);
                Log.Info("customer could not be verified");
                return Json(source);
            }
            catch (Exception ex)
            {
                var response = new { Title = "error", ResponseCode = "201", Description = ex.Message };
                var resposonseobJson = JsonConvert.SerializeObject(response);
                Log.Error(ex.Message);
                return Json(response);
            }
            
            
        }


        [HttpGet]
        [Route("getbeneficiaries")]
        public IHttpActionResult Beneficiaries(CustomerDetails value)
        {
            User user = new User(value.CustomerNumber, "");
            List<Beneficiary> beneficiaries1 = new List<Beneficiary>();
            var objectresJson = "";
            try
            {
                DataServiceQuery<Beneficiaries> beneficiaries2 = DBConfig.ReturnNav(user.Myscheme.SchemeODataUrl).Beneficiaries;
                Expression<Func<Beneficiaries, bool>> predicate = (Expression<Func<Beneficiaries, bool>>)(r => r.Vendor_No == value.CustomerNumber);
                foreach (Beneficiaries beneficiaries3 in (IEnumerable<Beneficiaries>)beneficiaries2.Where<Beneficiaries>(predicate))
                {
                    Beneficiary beneficiary1 = new Beneficiary();
                    beneficiary1.Relationship = beneficiaries3.Beneficiary;
                    beneficiary1.Name = beneficiaries3.Name;
                    beneficiary1.Allocation = Convert.ToDouble((object)beneficiaries3.Allocation);
                    beneficiary1.LineNo = beneficiaries3.Line_No;
                    beneficiary1.IdNumber = !(beneficiaries3.ID_No.Trim() != "") ? 0 : Convert.ToInt32(beneficiaries3.ID_No);
                    Beneficiary beneficiary2 = beneficiary1;
                    DateTime dateTime = Convert.ToDateTime((object)beneficiaries3.Date_Of_Birth);
                    string str1 = dateTime.ToString("MM/dd/yyyy");
                    beneficiary2.DateOfBirth = str1;
                    Beneficiary beneficiary3 = beneficiary1;
                    dateTime = Convert.ToDateTime((object)beneficiaries3.Effective_date);
                    string str2 = dateTime.ToString("MM/dd/yyyy");
                    beneficiary3.EffectiveDate = str2;
                    beneficiaries1.Add(beneficiary1);                                     
                    objectresJson = JsonConvert.SerializeObject(beneficiaries1);
                    Log.Info("Beneficiary Details Fetched Successfully");



                }
                return Json(beneficiaries1);
            }
            catch (Exception ex)
            {
                var response = new { Title = "error", ResponseCode = "201", Description = ex.Message };
                var resposonseobJson = JsonConvert.SerializeObject(response);
                Log.Error(ex.Message);
                return Json(response);
            }
            
        }

        [HttpGet]
        [Route("getintermediaries")]
        public IHttpActionResult GetIntermediaries(Schemes value)
        {
            Schemes user = new Schemes(value.SchemeName);
            List<Intermediary> intermediaries1 = new List<Intermediary>();
            var objectresJson = "";
            try
            {
                DataServiceQuery<Intermediaries> intermediaries2 = DBConfig.ReturnNav(user.SchemeODataUrl).Intermediaries;
                Expression<Func<Intermediaries, bool>> predicate = (Expression<Func<Intermediaries, bool>>)(r => r.Blocked == false);
                foreach (Intermediaries intermediaries3 in (IEnumerable<Intermediaries>)intermediaries2.Where<Intermediaries>(predicate))
                {
                    Intermediary intermediary1 = new Intermediary();
                    intermediary1.IntermediaryCode = intermediaries3.Code;
                    intermediary1.FullNames = intermediaries3.Name;
                    intermediary1.Email = intermediaries3.E_Mail;
                    intermediary1.SecondaryEmailAddress = intermediaries3.E_Mail_2;
                    intermediary1.MobileNumber = intermediaries3.Mobile_Phone_No;
                    intermediary1.SecondaryPhoneNumber = intermediaries3.Phone_No;
                    intermediary1.KRAPINNumber = intermediaries3.VAT_PIN_Registration_No;
                    intermediary1.PostalAddress = intermediaries3.Address;
                    intermediary1.PostalCode = intermediaries3.Post_Code;
                    intermediary1.PostalCity = intermediaries3.City;
                    intermediary1.Country = intermediaries3.Country_Region_Code; 
                    intermediaries1.Add(intermediary1);
                    objectresJson = JsonConvert.SerializeObject(intermediaries1);
                    Log.Info("Intermediary Details Fetched Successfully");



                }
                return Json(intermediaries1);
            }
            catch (Exception ex)
            {
                var response = new { Title = "error", ResponseCode = "201", Description = ex.Message };
                var resposonseobJson = JsonConvert.SerializeObject(response);
                Log.Error(ex.Message);
                return Json(response);
            }

        }
        [HttpGet]
        [Route("getschemes")]
        public IHttpActionResult GetSchemes()
        {
            
            List<SchemeListModel> schemes1 = new List<SchemeListModel>();
            var objectresJson = "";
            try
            {
                DataServiceQuery<CompaniesCRM> companies1 = DBConfig.ReturnNav(ConfigurationManager.AppSettings["ODATA_URI"]).CompaniesCRM;
                foreach (CompaniesCRM companies2 in (IEnumerable<CompaniesCRM>)companies1)
                {
                    SchemeListModel scheme1 = new SchemeListModel();
                    scheme1.SchemeName = companies2.Name;

                    schemes1.Add(scheme1);
                    objectresJson = JsonConvert.SerializeObject(schemes1);
                    Log.Info("Scheme Details Fetched Successfully");
                } 

                return Json(schemes1);
            }
            catch (Exception ex)
            {
                var response = new { Title = "error", ResponseCode = "201", Description = ex.Message };
                var resposonseobJson = JsonConvert.SerializeObject(response);
                Log.Error(ex.Message);
                return Json(response);
            }

        }
        [HttpGet]
        [Route("getintermediarystatement")]
        public IHttpActionResult IntermediaryStatement([FromBody] IntermediaryDetails value)
        {
            
            Schemes user = new Schemes(value.SchemeName);
            IntermediaryDetails intemediaryDetails = new IntermediaryDetails();
            var objectresJson = "";

            try
            {               

                string bigText = "";

                new DBConfig().ObjNav(user.SchemeWsUrl).FnIntermediaryrStatement(value.IntermediaryCode, ref bigText);


                intemediaryDetails.Byte_Array = Convert.FromBase64String(bigText);
                intemediaryDetails.IntermediaryCode = value.IntermediaryCode;
                intemediaryDetails.SchemeName = value.SchemeName;
                intemediaryDetails.Status = "success";

                objectresJson = JsonConvert.SerializeObject(intemediaryDetails);
                Log.Info("Intermediary Statement Generated Successfully");
                return Json(intemediaryDetails);

            }
            catch (Exception ex)
            {
                var response = new { Title = "error", ResponseCode = "201", Description = ex.Message };
                var resposonseobJson = JsonConvert.SerializeObject(response);
                Log.Error(ex.Message);
                return Json(response);

            }

        }

       


    }

}
