using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace KashifMubarak.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendEmail(string name, string email, string message)
        {
            //var _emailInfo = GetEmailInfo();

            //using (var client = new SmtpClient("smtp.gmail.com", 587))
            //{
            //    client.EnableSsl = true;
            //    client.Credentials = new NetworkCredential(_emailInfo["emailId"], _emailInfo["emailPass"]);

            //    var mail = new MailMessage();
            //    mail.From = new MailAddress(_emailInfo["emailId"].ToString());
            //    mail.To.Add(_emailInfo["emailId"].ToString());
            //    mail.Subject = "Website Notification";
            //    mail.Body = message + "\n\n\nEmail provided by user: " + email;

            //    client.Send(mail);
            //}

            return PartialView("_ThankYou");
        }

        private Dictionary<string, string> GetEmailInfo()
        {
            Dictionary<string, string> _retVal = new Dictionary<string, string>();
            SqlConnection _oConn = new SqlConnection();
            SqlCommand _oCmd = new SqlCommand();

            _oConn.ConnectionString = ConfigurationManager.ConnectionStrings["seraphConnectionString"].ConnectionString;
            _oConn.Open();

            _oCmd.Connection = _oConn;
            _oCmd.CommandType = System.Data.CommandType.StoredProcedure;
            _oCmd.CommandText = "GetInfo";

            SqlParameter _emailPasswordParam = new SqlParameter();
            _emailPasswordParam.ParameterName = "@retVal";
            _emailPasswordParam.Direction = System.Data.ParameterDirection.Output;
            _emailPasswordParam.Size = 10;
            _emailPasswordParam.SqlDbType = System.Data.SqlDbType.VarChar;

            _oCmd.Parameters.Add(_emailPasswordParam);

            _oCmd.ExecuteNonQuery();

            _retVal.Add("emailId", ConfigurationManager.AppSettings["EmailId"].ToString() + "@gmail.com");
            _retVal.Add("emailPass", _oCmd.Parameters["@retVal"].Value.ToString());

            return _retVal;
        }
    }
}