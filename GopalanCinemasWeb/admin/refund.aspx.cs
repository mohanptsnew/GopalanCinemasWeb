using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Net;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using GopalanCinemasBL;
using GopalanCinemasEntities;

namespace GopalanCinemasWeb.admin
{
    public partial class refund : System.Web.UI.Page
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

        string TotalAmount, TransactionID, BookingInfoId, FirstName=null,BookId, EmailId,Mobile;
        string strResponceIP, TranInqResponse, ResPaymentId, ResResult, ResErrorText, ResPosdate, ResTranId, ResAuth, ResAmount, ResErrorNo, ResTrackID, ResRef, Resudf1, Resudf2, Resudf3, Resudf4, Resudf5;
        public string strTransportalID, strGCTransportalPwd, strGCUrl, strGCPaymentURL, strGCDualURL;
        public DataTable dtRefund;
        booking_bal objRefund = new booking_bal();
        string MailServer = "smtp.gmail.com";
        string SmtpMailId = "support@gopalancinemas.com";
        string SmtpMailPwd = "gop@%123";
        string SmtpMailPort = "587";
        string MailComponent = "net";
        protected void Page_Load(object sender, EventArgs e)
        {
             //btnGo.Enabled = false;
             strTransportalID = System.Configuration.ConfigurationManager.AppSettings["GCTransportalID"].ToString();
             strGCTransportalPwd = System.Configuration.ConfigurationManager.AppSettings["GCTransportalPwd"].ToString();
             strGCUrl = System.Configuration.ConfigurationManager.AppSettings["GCUrl"].ToString();
             strGCPaymentURL = System.Configuration.ConfigurationManager.AppSettings["GCPaymentURL"].ToString();
             strGCDualURL = System.Configuration.ConfigurationManager.AppSettings["GCDualURL"].ToString();
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (bFromDate.Text != "" && bToDate.Text != "")
            {
                LoadGrid("go");
            }
        }
        private void LoadGrid(string p)
        {
            
            if (bFromDate.Text != "" && bToDate.Text != "")
            {
                string strDates = "";
                string[] strarrFDate = bFromDate.Text.Split('/');
                string strFDate = strarrFDate[2] + "/" + strarrFDate[1] + "/" + strarrFDate[0];
                string[] strarrTDate = bToDate.Text.Split('/');
                string strTDate = strarrTDate[2] + "/" + strarrTDate[1] + "/" + strarrTDate[0];
                 dtRefund = objRefund.getRefundDetails(strFDate, strTDate);
            }
            else
            {
                dtRefund = objRefund.getRefundDetailsAll();
            }

            if (dtRefund.Rows.Count > 0)
            {
                if (p == "Export")
                {
                    StringBuilder sb = new StringBuilder();
                    if (dtRefund.Rows.Count > 0)
                    {
                        sb.Append("<table border='1'>");
                        string strStatus = "";
                        sb.Append("<tr><td><b>Booking ID</b></td><td><b>Email</b></td><td><b>Mobile</b></td><td><b>Show Date</b></td><td><b>Show Time</b></td><td><b>Booking Date</b></td><td><b>Total Amount</b></td><td><b>Payment Status</b></td><td><b>Vista Status</b></td><td><b>Transaction Status</b></td></tr>");
                        for (int i = 0; i < dtRefund.Rows.Count; i++)
                        {
                            strStatus = "False";
                            if (dtRefund.Rows[i]["VistaStatus"] != null && dtRefund.Rows[i]["PGStatus"] != null)
                            {
                                if (dtRefund.Rows[i]["VistaStatus"].ToString() == "True" && dtRefund.Rows[i]["PGStatus"].ToString() == "True")
                                {
                                    strStatus = "True";
                                }
                            }
                            sb.Append("<tr>");
                            sb.Append("<td>" + dtRefund.Rows[i]["BookingID"].ToString() + "</td>");
                            sb.Append("<td>" + dtRefund.Rows[i]["Email"].ToString() + "</td>");
                            sb.Append("<td>" + dtRefund.Rows[i]["Mobile"].ToString() + "</td>");
                            sb.Append("<td>" + dtRefund.Rows[i]["ShowDate"].ToString() + "</td>");
                            sb.Append("<td>" + dtRefund.Rows[i]["ShowTime"].ToString() + "</td>");
                            sb.Append("<td>" + dtRefund.Rows[i]["BookingDate"].ToString() + "</td>");
                            sb.Append("<td>" + dtRefund.Rows[i]["OverAllAmount"].ToString() + "</td>");
                            sb.Append("<td>" + dtRefund.Rows[i]["PGStatus"].ToString() + "</td>");
                            sb.Append("<td>" + dtRefund.Rows[i]["VistaStatus"].ToString() + "</td>");
                            sb.Append("<td>" + strStatus + "</td>");
                            sb.Append("</tr>");
                        }
                        sb.Append("</table>");
                    }

                    Response.AppendHeader("Content-Disposition", "attachment; filename=Reports.xls");
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = "application/vnd.ms-excel";
                    this.EnableViewState = false;
                    Response.Write(sb.ToString());
                    Response.End();
                }
                else if (p == "Go")
                {
                    this.lblmsg.Visible = false;
                    this.gdRefund.Visible = true;
                    gdRefund.DataSource = dtRefund;
                    gdRefund.DataBind();
                }
                else
                {

                    this.lblmsg.Visible = false;
                    this.gdRefund.Visible = true;
                    gdRefund.DataSource = dtRefund;
                    gdRefund.DataBind();
                   
                }
            }
            else
            {
                dtRefund = null;
                gdRefund.Dispose();
                this.gdRefund.Visible = false;
                this.lblmsg.Visible = true;
                this.lblmsg.Text = "There is no records for your selection Criteria";
            }
        }
       
     
        protected void lnkRefund_Click1(object sender, EventArgs e)
        {
            
            strTransportalID = System.Configuration.ConfigurationManager.AppSettings["GCTransportalID"].ToString();
            strGCTransportalPwd = System.Configuration.ConfigurationManager.AppSettings["GCTransportalPwd"].ToString();
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            int rowInd = (int)clickedRow.RowIndex;
            string totalAmount = gdRefund.Rows[rowInd].Cells[6].Text;
            Label transId = (Label)clickedRow.FindControl("lblTransactionId");
            TransactionID = transId.Text;
            Label bookingId = (Label)clickedRow.FindControl("lblBookingDetailID");
            BookingInfoId = bookingId.Text;
            Label firstName = (Label)clickedRow.FindControl("lblFirstName");
            FirstName = firstName.Text;
            Label bookId = (Label)clickedRow.FindControl("lblBookId");
            BookId = bookId.Text;
            Label emailId = (Label)clickedRow.FindControl("lblEmail");
            EmailId = emailId.Text;
            Label mobileNo = (Label)clickedRow.FindControl("lblMobile");
            Mobile = mobileNo.Text;
            
            try{
                        
                        string tranportalid = "<id>" + strTransportalID + "</id>";
                        string password = "<password>" + strGCTransportalPwd + "</password>";
                        string member = "<member>" + FirstName + "</member>";	 //Mandatory
                        string currencycode = "<currencycode>356</currencycode>";  // Mandatory
                        string action = "<action>2</action>";   //Mandatory for Action code "1" & "4"	
                        string amt = "<amt>" + totalAmount + "</amt>";  //Mandatory
                        string trackid = "<trackid>" + BookingInfoId + "_" + BookId + "</trackid>"; //Mandatory
                        string transid = "<transid>" + TransactionID + "</transid>"; //Optional
                        string udf1 = "<udf1>UDF" + BookingInfoId + "</udf1>";
                        string udf2 = "<udf2>" + EmailId + "</udf2>";
                        string udf3 = "<udf3>" + Mobile + "</udf3>";
                        string udf4 = "<udf4></udf4>";
                        string udf5 = "<udf5></udf5>";

                        string data = tranportalid + password + currencycode + action + amt + trackid + member + udf1 + udf2 + udf3 + udf4 + udf5 + transid;
                        string TranUrl = "https://securepg.fssnet.co.in/pgway/servlet/TranPortalXMLServlet";
                        System.IO.StreamWriter myWriter = null;
                        System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(TranUrl);//send data using objxmlhttp object
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
                            strauthid =GetStringInBetween(strresponce, "<auth>", "</auth>", false, false);
                            strrefid = GetStringInBetween(strresponce, "<ref>", "</ref>", false, false);
                            stravr =GetStringInBetween(strresponce, "<avr>", "</avr>", false, false);
                            strpostdate = GetStringInBetween(strresponce, "<postdate>", "</postdate>", false, false);
                            strtransid = GetStringInBetween(strresponce, "<tranid>", "</tranid>", false, false);
                            strmailid = GetStringInBetween(strresponce, "<udf2>", "</udf2>", false, false);
                            stramount = GetStringInBetween(strresponce, "<amt>", "</amt>", false, false);
                            if (strresult[0] == "CAPTURED")
                            {
                                string htmlBody = string.Empty;
                                objRefund.updateRefundStatus(1, BookingInfoId);
                                 string tab = string.Empty;
                                tab = tab + "<table><tr><td colspan='2'>Dear Guest, Your Refund Details</td></tr>";
                                tab = tab + "<table><tr><td colspan='2'>Payment Id:" + strpid[0] + "</td></tr>";
                                tab = tab + "<table><tr><td colspan='2'>Refund Amount:"+stramount[0]+"</td></tr>";
                                tab = tab + "<tr><td colspan='2'>Thanks</td></tr></table>";

                                htmlBody = tab.ToString();
                                SendMail("support@gopalancinemas.com", strmailid[0], "nagarajan.manimaran@influx.co.in,senthilkumar.ayyasamy@influx.co.in", htmlBody, "Your refund.", "");

                                if (bFromDate.Text != "" && bToDate.Text != "")
                                {
                                    string strDates = "";
                                    string[] strarrFDate = bFromDate.Text.Split('/');
                                    string strFDate = strarrFDate[2] + "/" + strarrFDate[1] + "/" + strarrFDate[0];
                                    string[] strarrTDate = bToDate.Text.Split('/');
                                    string strTDate = strarrTDate[2] + "/" + strarrTDate[1] + "/" + strarrTDate[0];
                                    dtRefund = objRefund.getRefundDetails(strFDate, strTDate);
                                    gdRefund.DataSource = dtRefund;
                                    gdRefund.DataBind();
                                }
                                else
                                {
                                    dtRefund = objRefund.getRefundDetailsAll();
                                    gdRefund.DataSource = dtRefund;
                                    gdRefund.DataBind();
                                }

                                this.lblmsg.Visible = true;
                                this.lblmsg.Text = "Refunded Successfully...";
                                
                            }
                            else
                            {
                                this.lblmsg.Visible = true;
                                this.lblmsg.Text ="Refund Failure:"+ strresult[0].ToString();
                            }
                            
                        }
                    }
            catch (Exception ex)
            {
                
                InfluxRepository.ErrorLog.ErrorLog.LogErrorToLogFile(ex, "Gopalan Cinemas");
            }
            
        }
        public string SendMail(string mailFrom, string mailTo, string copyTo, string htmlBody, string subject, string sFile)
        {

            if (MailComponent == "web")
            {
                return SendWebMail(mailFrom, mailTo, copyTo, htmlBody, subject, sFile);
            }
            if (MailComponent == "net")
            {
                return SendNetMail(mailFrom, mailTo, copyTo, htmlBody, subject, sFile);
            }
            return "not sent";
        }
        public string SendNetMail(string mailFrom, string mailTo, string copyTo, string htmlBody, string subject, string sFile)
        {
            try
            {
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress(mailFrom, "Gopalan Cinemas");

                smtpClient.Host = MailServer;
                smtpClient.Port = int.Parse(SmtpMailPort);
                System.Net.NetworkCredential SMTPUserInfo = new System.Net.NetworkCredential(SmtpMailId, SmtpMailPwd);
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = SMTPUserInfo;

                message.From = fromAddress;
                message.To.Add(mailTo);

                if (copyTo.Trim() != "")
                {
                    string[] list_of_email = copyTo.Split(',');
                    System.Net.Mail.MailAddress bcc = new System.Net.Mail.MailAddress(list_of_email[0].ToString());
                    message.Bcc.Add(bcc);

                    for (int y = 1; y < list_of_email.Length; y++)
                    {
                        bcc = new System.Net.Mail.MailAddress(list_of_email[y].ToString());
                        message.Bcc.Add(bcc);
                    }
                }
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = htmlBody;
                if (sFile != "")
                {
                    System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(sFile, System.Net.Mime.MediaTypeNames.Application.Octet);
                    System.Net.Mime.ContentDisposition disposition = data.ContentDisposition;
                    message.Attachments.Add(data);
                }
                smtpClient.Send(message);
                message.Dispose();
                return "success";
            }
            catch (Exception e)
            {
                return e.Message.ToString();
            }
        }

        public string SendWebMail(string mailFrom, string mailTo, string copyTo, string htmlBody, string subject, string sFile)
        {
            try
            {
                System.Web.Mail.SmtpMail.SmtpServer = MailServer;
                System.Web.Mail.MailMessage msg = new System.Web.Mail.MailMessage();
                msg.From = mailFrom;
                msg.To = mailTo;
                msg.Subject = subject;
                msg.Body = htmlBody;
                if (copyTo.Trim() != "")
                {
                    msg.Bcc = copyTo;
                }
                if (sFile != "")
                {
                    System.Web.Mail.MailAttachment oAttch = new System.Web.Mail.MailAttachment(sFile, System.Web.Mail.MailEncoding.Base64);
                    msg.Attachments.Add(oAttch);
                }

                System.Web.Mail.SmtpMail.Send(msg);
                return "success";
            }
            catch (Exception e)
            {

                return e.Message.ToString();
            }
        }
        protected void lnkRefund_Click(object sender, EventArgs e)
        {
            
            string PxPayUserId = ConfigurationManager.AppSettings["PxPayUserId"];
            string PxPayKey = ConfigurationManager.AppSettings["PxPayKey"];
            RefundRequest WS = new RefundRequest(PxPayUserId, PxPayKey);
            RequestInput input = new RequestInput();
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            int rowInd = (int)clickedRow.RowIndex;
            string totalAmount = gdRefund.Rows[rowInd].Cells[6].Text;
            Label transId=(Label)clickedRow.FindControl("lblPaymentId");
            TransactionID = transId.Text;
            Label paymentId = (Label)clickedRow.FindControl("lblTransactionId");
            BookingInfoId = paymentId.Text;
            Label firstName = (Label)clickedRow.FindControl("lblFirstName");
            FirstName = Convert.ToString(firstName.Text)??"test";
            if (FirstName == null)
            {
                FirstName = "test";
            }
           
             //TODO:GUID representing unique identifier for the transaction within the shopping cart (normally would be an order ID or similar)
            //Response.Write(totalAmount + TransactionID + PaymentId + FirstName);
            
            string xmlId = "<id>" + strTransportalID + "</id>";
            string xmlPassword="<password>"+strGCTransportalPwd+"</password>";
            string xmlAction="<action>2</action>";
            string xmlAmount = "<amt>" + totalAmount + "</amt>";
            string xmlTrans="<transid>"+TransactionID+"</transid>";
            string xmlMember="<member>"+FirstName+"</member>";
            string xmlCurrency = "<currencycode>356</currencycode>";
            string INQRequest = xmlId + xmlPassword + xmlCurrency + xmlAction + xmlAmount + xmlTrans + xmlMember;
            //WS.GenerateRequest(INQRequest);
            //DUAL VERIFIACTION URL, this is string INQUrl = strGCDualURL;
            try
            {
                //create a SSL connection xmlhttp formated object server-to-server
                System.IO.StreamWriter myWriter = null;
                //it will open a http connection with provided url
                System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("https://securepg.fssnet.co.in/pgway/servlet/TranPortalXMLServlet");//send data using objxmlhttp object
                objRequest.Method = "POST";
                objRequest.ContentLength = INQRequest.Length;
                objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
                myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(INQRequest);//send data
                myWriter.Close();//closed the myWriter object

                System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();
                //receive the responce from objxmlhttp object 
                using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
                {
                    TranInqResponse = sr.ReadToEnd();
                    string[] INQCheck = GetStringInBetween(TranInqResponse, "<result>", "</result>", false, false);//This line will check if any error in TranInqResponse 		
                    Response.Write(INQCheck[0]);
                    Response.End();
                    if (INQCheck[0] == "CAPTURED")
                    {
                        //XML response received for DUAL VERIFICATION.
                        /* 
                        NOTE - MERCHANT MUST LOG THE RESPONSE RECEIVED IN LOGS AS PER BEST PRACTICE
                        */
                        //Collect DUAL VERIFICATION RESULT
                        string[] INQResResult = GetStringInBetween(TranInqResponse, "<result>", "</result>", false, false);//It will give DualInquiry Result 
                        string[] INQResAuth = GetStringInBetween(TranInqResponse, "<auth>", "</auth>", false, false);//It will give TrackID ENROLLED
                        string[] INQResRef = GetStringInBetween(TranInqResponse, "<ref>", "</ref>", false, false);//It will give Ref.NO.
                        string[] INQResAvrg= GetStringInBetween(TranInqResponse, "<avr>", "</avr>", false, false);//It will give tranid
                        string[] INQResPost = GetStringInBetween(TranInqResponse, "<postdate>", "</postdate>", false, false);//It will give Auth 
                        string[] INQResTrans = GetStringInBetween(TranInqResponse, "<tranid>", "</tranid>", false, false);//It will give Auth 
                        string[] INQResPayid = GetStringInBetween(TranInqResponse, "<payid>", "</payid>", false, false);//It will give payid
                        string[] INQResUdf2 = GetStringInBetween(TranInqResponse, "<udf2>", "</udf2>", false, false);//It will give Amount
                        string[] INQResUdf5 = GetStringInBetween(TranInqResponse, "<udf5>", "</udf5>", false, false);//It will give Amount
                        string[] INQResAmount = GetStringInBetween(TranInqResponse, "<amt>", "</amt>", false, false);//It will give Amount

                        //MERCHANT CAN GET ALL VERIFICATION RESULT PARAMETERS USING BELOW CODE 
                        /*
                        string[] INQResAutht=GetStringInBetween(TranInqResponse,"<auth>","</auth>",false,false);//It will give Auth 
                        string[] INQResRef=GetStringInBetween(TranInqResponse,"<ref>","</ref>",false,false);//It will give Ref.NO.
                        string[] INQResAvr=GetStringInBetween(TranInqResponse,"<avr>","</avr>",false,false);//It will give AVR 
                        string[] INQResPostdate=GetStringInBetween(TranInqResponse,"<postdate>","</postdate>",false,false);//It will give  postdate
                        string[] INQResTranid=GetStringInBetween(TranInqResponse,"<tranid>","</tranid>",false,false);//It will give tranid
                        string[] INQResPayid=GetStringInBetween(TranInqResponse,"<payid>","</payid>",false,false);//It will give payid
                        string[] INQResUdf1=GetStringInBetween(TranInqResponse,"<udf1>","</udf1>",false,false);//It will give udf1
                        string[] INQResUdf2=GetStringInBetween(TranInqResponse,"<udf2>","</udf2>",false,false);//It will give udf2
                        string[] INQResUdf3=GetStringInBetween(TranInqResponse,"<udf3>","</udf3>",false,false);//It will give udf3
                        string[] INQResUdf4=GetStringInBetween(TranInqResponse,"<udf4>","</udf4>",false,false);//It will give udf4
                        string[] INQResUdf5=GetStringInBetween(TranInqResponse,"<udf5>","</udf5>",false,false);//It will give udf5
                        */

                        /*
                        IMPORTANT NOTE - MERCHANT DOES RESPONSE HANDLING AND VALIDATIONS OF 
                        TRACK ID, AMOUNT AT THIS PLACE. THEN ONLY MERCHANT SHOULD UPDATE 
                        TRANACTION PAYMENT STATUS IN MERCHANT DATABASE AT THIS POSITION 
                        AND THEN REDIRECT CUSTOMER ON RESULT PAGE
                        */

                        /* !!IMPORTANT INFORMATION!!
                        During redirection, ME can pass the values as per ME requirement.
                        NOTE: NO PROCESSING should be done on the RESULT PAGE basis of values passed in the RESULT PAGE from this page. 
                        ME does all validations on the responseURL page and then redirects the customer to RESULT 
                        PAGE ONLY FOR RECEIPT PRESENTATION/TRANSACTION STATUS CONFIRMATION
                        For demonstration purpose the result and track id are passed to Result page
                        */
                        //UpdatePGStatus();
                        //Session["sessSuccess"] = "Success";
                        //Response.Write("REDIRECT=" + strGCUrl + "sendresponse.aspx?ResRef=" + INQResRef[0] + "&ResAuth=" + INQResAutht[0] + "&ResTranId=" + INQResTranid[0] + "&PayID=" + INQResPayid[0] + "&ResResult=Transaction Success [" + INQResResult[0] + "]&ResTrackId=" + INQResTrackId[0] + "&ResAmount=" + INQResAmount[0] + "&Caption=" + INQCheck[0]);
                        commonfunctions common = new commonfunctions();
                        INQResRef[0] = common.EncryptString(INQResRef[0], "gop");
                        INQResAuth[0] = common.EncryptString(INQResAuth[0], "gop");
                        INQResTrans[0] = common.EncryptString(INQResTrans[0], "gop");
                        INQResPayid[0] = common.EncryptString(INQResPayid[0], "gop");
                        INQResResult[0] = common.EncryptString(INQResResult[0], "gop");
                        INQResAmount[0] = common.EncryptString(INQResAmount[0], "gop");
                        INQResUdf2[0] = common.EncryptString(INQResUdf2[0], "gop");
                        INQResUdf5[0] = common.EncryptString(INQResUdf5[0], "gop");
                        INQResPost[0] = common.EncryptString(INQResPost[0], "gop");
                        INQResAvrg[0] = common.EncryptString(INQResAvrg[0], "gop");
                        INQCheck[0] = common.EncryptString(INQCheck[0], "gop");
                        strResponceIP = common.EncryptString(strResponceIP, "gop");
                        Response.Write("REDIRECT=" + strGCUrl + "RefundResponse.aspx?ResRef=" + INQResRef[0] + "&ResAuth=" + INQResAuth[0] + "&ResAuth=" + INQResTrans[0] + "&PayID=" + INQResPayid[0] + "&ResResult=" + INQResResult[0] + "&ResAmount=" + INQResAmount[0] + "&Caption=" + INQCheck[0] + "&udf1=" + strResponceIP + "&udf2=" + INQResUdf2 + "&udf1=" + INQResUdf5 + "&udf1=" + INQResPost + "&udf1=" + INQResAvrg);

                        //Server.Transfer("sendresponse.aspx");
                        //Response.Redirect(strGCUrl + "sendresponse.aspx?ResRef=" + INQResRef[0] + "&ResAuth=" + INQResAutht[0] + "&ResTranId=" + INQResTranid[0] + "&PayID=" + INQResPayid[0] + "&ResResult=Transaction Success [" + INQResResult[0] + "]&ResTrackId=" + INQResTrackId[0] + "&ResAmount=" + INQResAmount[0] + "&Caption=" + INQCheck[0]);
                        //Response.Write("REDIRECT=" + strGCUrl + "confirmation.aspx");
                    }
                    else
                    {
                        /*
                        ERROR IN TRANSACTION PROCESSING
                        IMPORTANT NOTE - MERCHANT SHOULD UPDATE 
                        TRANACTION PAYMENT STATUS IN MERCHANT DATABASE AT THIS POSITION 
                        AND THEN REDIRECT CUSTOMER ON RESULT PAGE
                        */
                        Response.Write("REDIRECT=" + strGCUrl + "RefundFailure.aspx?ResError=" + INQCheck[0]);
                    }
                }
            }
            catch (Exception Ex)
            {
                Response.Write(Ex.Message);// any excpetion occurred for above code exception throws here
            }
            //WS.GenerateRequest(xmlString);
            //Redirect user to payment page
            //Response.Redirect("refund.aspx");
       
        }
        protected void Imagebtn_Click(object sender, EventArgs e)
        {
            btnGo.Enabled = true;
            btnAll.Enabled = false;

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
        protected void gdRefund_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdRefund.PageIndex = e.NewPageIndex;
            LoadGrid("Go");
        }
        protected void gdRefund_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label lblBDID = (Label)gdRefund.Rows[e.RowIndex].FindControl("lblBookingDetailID");
            if (lblBDID.Text != "")
            {
                updateRefundStatusbybookingdetailid(true, int.Parse(lblBDID.Text));
                LoadGrid("Go");
            }
        }
        
        
        private void updateRefundStatusbybookingdetailid(bool refundstatus, int bdid)
        {
            try
            {
                objRefund.updateRefundStatusbybookingdetailid(refundstatus, bdid);
            }
            catch (Exception ex)
            {
                InfluxRepository.ErrorLog.ErrorLog.LogErrorToLogFile(ex, "Gopalan Cinemas");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (bFromDate.Text != "" && bToDate.Text != "")
            {
                LoadGrid("Export");
            }
            else 
            {
                LoadGrid("Export");
            }
        }
        protected void btnAll_Click(object sender, EventArgs e)
        {
           
                LoadGrid("All");
           
        }
    }
}