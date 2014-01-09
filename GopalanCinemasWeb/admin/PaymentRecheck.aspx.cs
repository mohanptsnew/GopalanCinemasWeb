using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using GopalanCinemasBL;
using GopalanCinemasEntities;
namespace GopalanCinemasWeb.admin
{
    public partial class PaymentRecheck : System.Web.UI.Page
    {
        public static string[] strresult;
        public static string[] strurl;
        public static string[] strpareq;
        public static string[] strpid;
        public static string[] strtid;
        public static string[] streci;
        public static string[] strauthid;
        public static string[] strrefid;
        public static string[] stravr;
        public static string[] strpostdate;
        public static string[] strtransid;
        public static string[] strmailid;
        public static string[] stramount;
        
        public int refundFlag = 1, refundMail = 1, hid;
        
        string TotalAmount, TransactionID, BookingInfoId, FirstName, BookId, EmailId, Mobile,PaymentID;
        
        public string strTransportalID, strGCTransportalPwd, strGCUrl, strGCPaymentURL, strGCDualURL;
        public DataTable dtRefund;
        string strCinemaID;
       
        booking_bal objRefund = new booking_bal();
        protected void Page_Load(object sender, EventArgs e)
        {
            strTransportalID = System.Configuration.ConfigurationManager.AppSettings["GCTransportalID"].ToString();
            strGCTransportalPwd = System.Configuration.ConfigurationManager.AppSettings["GCTransportalPwd"].ToString();
            strGCUrl = System.Configuration.ConfigurationManager.AppSettings["GCUrl"].ToString();
            strGCPaymentURL = System.Configuration.ConfigurationManager.AppSettings["GCPaymentURL"].ToString();
            strGCDualURL = System.Configuration.ConfigurationManager.AppSettings["GCDualURL"].ToString();
            if (!IsPostBack)
            {
                dtRefund = objRefund.getRefundedDetails();

                if (dtRefund.Rows.Count > 0)
                {

                    for (int i = 0; i < dtRefund.Rows.Count; i++)
                    {
                        BookingInfoId = dtRefund.Rows[i]["BookingInfoID"].ToString();
                        TotalAmount = dtRefund.Rows[i]["OverAllAmount"].ToString();
                        TransactionID = dtRefund.Rows[i]["TransactionID"].ToString();
                        FirstName = dtRefund.Rows[i]["LastName"].ToString();
                        EmailId = Convert.ToString(dtRefund.Rows[i]["Email"].ToString());
                        Mobile = Convert.ToString(dtRefund.Rows[i]["Mobile"].ToString());
                        PaymentID = Convert.ToString(dtRefund.Rows[i]["PaymentID"].ToString());
                        BookId = Convert.ToString(dtRefund.Rows[i]["bookid"].ToString());
                        strCinemaID = dtRefund.Rows[i]["Cinema_str"].ToString();
                        sendRefundRequest(strTransportalID, strGCTransportalPwd, FirstName, TotalAmount, TransactionID,BookingInfoId, BookId, EmailId, Mobile, PaymentID, strCinemaID);
                        Response.Write("<br/>");
                    }   
                }
            }
        }

        
        public void sendRefundRequest(string strTransportalID, string strGCTransportalPwd, string FirstName, string totalAmount, string TransactionID, string BookingInfoId, string BookID, string EmailId, string Mobile,string PaymentID, string cinema)
        {
             string getResultValue="";
            try
            {
                strTransportalID = System.Configuration.ConfigurationManager.AppSettings["GCTransportalID" + cinema].ToString();
                strGCTransportalPwd = System.Configuration.ConfigurationManager.AppSettings["GCTransportalPwd" + cinema].ToString();
                string tranportalid = "<id>" + strTransportalID + "</id>";
                string password = "<password>" + strGCTransportalPwd + "</password>";
                string member = "<member>" + FirstName + "</member>";	 //Mandatory
                string currencycode = "<currencycode>356</currencycode>";  // Mandatory
                string action = "<action>8</action>";   //Mandatory for Action code "1" & "4"	
                string amt = "<amt>" + totalAmount + "</amt>";  //Mandatory
                string trackid = "<trackid>" + PaymentID + "</trackid>"; //Mandatory
                string transid = "<transid>" + PaymentID + "</transid>"; //Optional
                string udf1 = "<udf1>UDF" + BookingInfoId + "</udf1>";
                string udf2 = "<udf2>" + EmailId + "</udf2>";
                string udf3 = "<udf3>" + Mobile + "</udf3>";
                string udf4 = "<udf4></udf4>";
                string udf5 = "<udf5>PaymentID</udf5>";
                StringBuilder sb = new StringBuilder();
                sb.Append("<id>" + strTransportalID + "</id>");
                sb.Append("<BR>");
                sb.Append("<password>" + strGCTransportalPwd + "</password>");
                sb.Append("<BR>");
                sb.Append("<member>" + FirstName + "</member>");
                sb.Append("<BR>");
                sb.Append("<currencycode>356</currencycode>");
                sb.Append("<BR>");
                sb.Append("<action>8</action>");
                sb.Append("<BR>");
                sb.Append("<amt>" + totalAmount + "</amt>");
                sb.Append("<BR>");
                sb.Append("<udf5>PaymentID</udf5>");
                sb.Append("<BR>");

                Response.Write(sb);
                string data = tranportalid + password + currencycode + action + amt + trackid + transid + udf5;
                
                string TranUrl = "https://securepg.fssnet.co.in/pgway/servlet/TranPortalXMLServlet";
                System.IO.StreamWriter myWriter = null;
                System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strGCDualURL);//send data using objxmlhttp object
                objRequest.Method = "POST";
                objRequest.ContentLength = data.Length;
                objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
                myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(data);
                myWriter.Close();

                System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();
                using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
                {

                    string strresponce = sr.ReadToEnd();
                    strresult = GetStringInBetween(strresponce, "<result>", "</result>", false, false);
                    strpid = GetStringInBetween(strresponce, "<payid>", "</payid>", false, false);
                    strtid = GetStringInBetween(strresponce, "<trackid>", "</trackid>", false, false);
                    strauthid = GetStringInBetween(strresponce, "<auth>", "</auth>", false, false);
                    strrefid = GetStringInBetween(strresponce, "<ref>", "</ref>", false, false);
                    stravr = GetStringInBetween(strresponce, "<avr>", "</avr>", false, false);
                    strpostdate = GetStringInBetween(strresponce, "<postdate>", "</postdate>", false, false);
                    strtransid = GetStringInBetween(strresponce, "<tranid>", "</tranid>", false, false);
                    strmailid = GetStringInBetween(strresponce, "<udf2>", "</udf2>", false, false);
                    stramount = GetStringInBetween(strresponce, "<amt>", "</amt>", false, false);
                    getResultValue = strresult[0].ToString();
                    Response.Write(getResultValue);
                }
                if (getResultValue == "CAPTURED")
                {
                    Response.Write("Captured");
                }
                else
                {
                    Response.Write("Failed");
                }
              
            }
            catch (Exception ex)
            {
                InfluxRepository.ErrorLog.ErrorLog.LogErrorToLogFile(ex, "Gopalan Cinemas");
            }
        }

        public static string[] GetStringInBetween(string strSource, string strBegin, string strEnd, bool includeBegin, bool includeEnd)
        {
            string[] result = { "", "" };
            int iIndexOfBegin = strSource.IndexOf(strBegin);
            if (iIndexOfBegin != -1)
            {
                // include the Begin string if desired

                if (includeBegin)
                {
                    iIndexOfBegin -= strBegin.Length;
                }
                strSource = strSource.Substring(iIndexOfBegin + strBegin.Length);
                int iEnd = strSource.IndexOf(strEnd);
                if (iEnd != -1)
                {  // include the End string if desired
                    if (includeEnd)
                    { iEnd += strEnd.Length; }
                    result[0] = strSource.Substring(0, iEnd);
                    // advance beyond this segment
                    if (iEnd + strEnd.Length < strSource.Length)
                    { result[1] = strSource.Substring(iEnd + strEnd.Length); }
                }
            }
            else
            {
                result[1] = strSource;
            }
            return result;
        }
    }
}
