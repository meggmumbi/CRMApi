using cicapi.Models;
using cicapi.NavOData;
using cicapi.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace cicapi
{
    public class User
    {
        public string MemberNumber { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Idnumber { get; set; }

        public string Email { get; set; }

        public string DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public Schemes Myscheme { get; set; }

        public double Cumulative { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SchemeJoinDate { get; set; }

        public string ReferralCode { get; set; }

        public string ResponsePrtI { get; set; }

        public string ResponsePrtII { get; set; }

        public bool ChangedPassword { get; set; }

        public List<MemberSchemes> AllSchemes { get; set; }

        public User()
        {
        }

        public User(string memberNo, string password)
        {
            try
            {
                IQueryable<Mobile_users> queryable = DBConfig.ReturnNav(ConfigurationManager.AppSettings["ODATA_URI"]).Mobile_users.Where<Mobile_users>((Expression<Func<Mobile_users, bool>>)(r => r.Member_Number == memberNo));
                bool flag = false;
                foreach (Mobile_users mobileUsers in (IEnumerable<Mobile_users>)queryable)
                {
                    flag = true;
                    this.MemberNumber = mobileUsers.Member_Number;
                    this.Name = mobileUsers.Member_Name;
                    this.Idnumber = mobileUsers.id_No;
                    this.Email = mobileUsers.Email;
                    this.Myscheme = new Schemes(mobileUsers.scheme);
                }
                if (flag)
                    return;
                foreach (Companies company in DBConfig.ReturnNav(ConfigurationManager.AppSettings["ODATA_URI"]).Companies)
                {
                    try
                    {
                        string str1 = ConfigurationManager.AppSettings["ODATA_PART"] + "Company('" + company.Name + "')/";
                        DataServiceQuery<Members> members1 = DBConfig.ReturnNav(str1).Members;
                        Expression<Func<Members, bool>> predicate = (Expression<Func<Members, bool>>)(r => r.No == memberNo);
                        foreach (Members members2 in (IEnumerable<Members>)members1.Where<Members>(predicate))
                        {
                            string str2 = ConfigurationManager.AppSettings["WS_PART"] + company.Name + "/Codeunit/CRMIntegration";
                            string name1 = members2.Name;
                            string eMail = members2.E_Mail;
                            string idNo = members2.ID_No;
                            string name2 = company.Name;
                            string referralCode = new User().GenerateReferralCode();
                            this.MemberNumber = memberNo;
                            this.Name = name1;
                            this.Idnumber = idNo;
                            this.Email = eMail;
                            this.Myscheme = new Schemes(name2);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Name = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Name = ex.Message;
            }
        }

        public double GetCumulative(User user)
        {
            double num = 0.0;
            DataServiceQuery<Member_Statement> memberStatement1 = DBConfig.ReturnNav(user.Myscheme.SchemeODataUrl).Member_Statement;
            Expression<Func<Member_Statement, bool>> predicate = (Expression<Func<Member_Statement, bool>>)(r => r.Vendor_No == this.MemberNumber);
            foreach (Member_Statement memberStatement2 in (IEnumerable<Member_Statement>)memberStatement1.Where<Member_Statement>(predicate))
                num += Convert.ToDouble((object)memberStatement2.Amount);
            return num;
        }

        public string GenerateReferralCode()
        {
            string referralCode = "";
            Random random = new Random();
            for (int index = 0; index < 10; ++index)
            {
                switch (random.Next(1, 26))
                {
                    case 1:
                        referralCode += "Z";
                        break;
                    case 2:
                        referralCode += "Y";
                        break;
                    case 3:
                        referralCode += "X";
                        break;
                    case 4:
                        referralCode += "W";
                        break;
                    case 5:
                        referralCode += "V";
                        break;
                    case 6:
                        referralCode += "U";
                        break;
                    case 7:
                        referralCode += "T";
                        break;
                    case 8:
                        referralCode += "S";
                        break;
                    case 9:
                        referralCode += "R";
                        break;
                    case 10:
                        referralCode += "Q";
                        break;
                    case 11:
                        referralCode += "P";
                        break;
                    case 12:
                        referralCode += "O";
                        break;
                    case 13:
                        referralCode += "N";
                        break;
                    case 14:
                        referralCode += "M";
                        break;
                    case 15:
                        referralCode += "L";
                        break;
                    case 16:
                        referralCode += "K";
                        break;
                    case 17:
                        referralCode += "J";
                        break;
                    case 18:
                        referralCode += "I";
                        break;
                    case 19:
                        referralCode += "H";
                        break;
                    case 20:
                        referralCode += "G";
                        break;
                    case 21:
                        referralCode += "F";
                        break;
                    case 22:
                        referralCode += "E";
                        break;
                    case 23:
                        referralCode += "D";
                        break;
                    case 24:
                        referralCode += "C";
                        break;
                    case 25:
                        referralCode += "B";
                        break;
                    case 26:
                        referralCode += "A";
                        break;
                }
            }
            IQueryable<Mobile_users> queryable = DBConfig.ReturnNav(ConfigurationManager.AppSettings["ODATA_URI"]).Mobile_users.Where<Mobile_users>((Expression<Func<Mobile_users, bool>>)(r => r.referralcode == referralCode));
            bool flag = false;
            try
            {
                foreach (Mobile_users mobileUsers in (IEnumerable<Mobile_users>)queryable)
                    flag = true;
            }
            catch (Exception ex)
            {
            }
            if (!flag)
                return referralCode;
            this.GenerateReferralCode();
            return referralCode;
        }
    }
}
