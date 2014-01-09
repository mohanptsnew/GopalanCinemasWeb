using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using GopalanCinemasBL;
using GopalanCinemasEntities;

namespace GopalanCinemasWeb
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

        string TotalAmount, TransactionID, BookingInfoId, FirstName = null, BookId, EmailId, Mobile;
        string strResponceIP, TranInqResponse, ResPaymentId, ResResult, ResErrorText, ResPosdate, ResTranId, ResAuth, ResAmount, ResErrorNo, ResTrackID, ResRef, Resudf1, Resudf2, Resudf3, Resudf4, Resudf5;
        public string strTransportalID, strGCTransportalPwd, strGCUrl, strGCPaymentURL, strGCDualURL;
        DataTable dt;
        booking_bal book = new booking_bal();
        hdf_response objHdfcResponse = new hdf_response();
        protected void Page_Load(object sender, EventArgs e)
        {
            strTransportalID = System.Configuration.ConfigurationManager.AppSettings["GCTransportalID"].ToString();
            strGCTransportalPwd = System.Configuration.ConfigurationManager.AppSettings["GCTransportalPwd"].ToString();
            strGCUrl = System.Configuration.ConfigurationManager.AppSettings["GCUrl"].ToString();
            strGCPaymentURL = System.Configuration.ConfigurationManager.AppSettings["GCPaymentURL"].ToString();
            strGCDualURL = System.Configuration.ConfigurationManager.AppSettings["GCDualURL"].ToString();
            LoadData();
            //Load1();
        }

        private void Load1()
        {
            string xmlId = "<id>" + strTransportalID + "</id>";
            string xmlPassword = "<password>" + strGCTransportalPwd + "</password>";
            string xmlAction = "<action>8</action>";
            string xmlCurrency = "<udf5>PaymentID</udf5>";
            string xmlAmount = "<amt>560.5500</amt>";
            string xmlMember = "<trackid>GCAM1002621051</trackid>";
            string xmlTrans = "<transid>9063460231322650</transid>";
            string currencycode = "<currencycode>356</currencycode>";  // Mandatory
            string INQRequest = xmlId + xmlPassword + xmlAction + xmlAmount + xmlMember + xmlTrans + xmlCurrency + currencycode;
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
                            Response.Write(" ----- ");
                            Response.Write(INQCheck[1]);
                            Response.Write("<br/>");
                            Response.Write("<br/>");
                            //Response.End();
                            if (INQCheck[0] == "CAPTURED")
                            {
                                //string[] INQResResult = GetStringInBetween(TranInqResponse, "<result>", "</result>", false, false);//It will give DualInquiry Result 
                                //string[] INQResAuth = GetStringInBetween(TranInqResponse, "<auth>", "</auth>", false, false);//It will give TrackID ENROLLED
                                //string[] INQResRef = GetStringInBetween(TranInqResponse, "<ref>", "</ref>", false, false);//It will give Ref.NO.
                                //string[] INQResAvrg = GetStringInBetween(TranInqResponse, "<avr>", "</avr>", false, false);//It will give tranid
                                //string[] INQResPost = GetStringInBetween(TranInqResponse, "<postdate>", "</postdate>", false, false);//It will give Auth 
                                //string[] INQResTrans = GetStringInBetween(TranInqResponse, "<tranid>", "</tranid>", false, false);//It will give Auth 
                                //string[] INQResPayid = GetStringInBetween(TranInqResponse, "<payid>", "</payid>", false, false);//It will give payid
                                //string[] INQResUdf2 = GetStringInBetween(TranInqResponse, "<udf2>", "</udf2>", false, false);//It will give Amount
                                //string[] INQResUdf5 = GetStringInBetween(TranInqResponse, "<udf5>", "</udf5>", false, false);//It will give Amount
                                //string[] INQResAmount = GetStringInBetween(TranInqResponse, "<amt>", "</amt>", false, false);//It will give Amount

                                //commonfunctions common = new commonfunctions();
                                //INQResRef[0] = common.EncryptString(INQResRef[0], "gop");
                                //INQResAuth[0] = common.EncryptString(INQResAuth[0], "gop");
                                //INQResTrans[0] = common.EncryptString(INQResTrans[0], "gop");
                                //INQResPayid[0] = common.EncryptString(INQResPayid[0], "gop");
                                //INQResResult[0] = common.EncryptString(INQResResult[0], "gop");
                                //INQResAmount[0] = common.EncryptString(INQResAmount[0], "gop");
                                //INQResUdf2[0] = common.EncryptString(INQResUdf2[0], "gop");
                                //INQResUdf5[0] = common.EncryptString(INQResUdf5[0], "gop");
                                //INQResPost[0] = common.EncryptString(INQResPost[0], "gop");
                                //INQResAvrg[0] = common.EncryptString(INQResAvrg[0], "gop");
                                //INQCheck[0] = common.EncryptString(INQCheck[0], "gop");
                                //strResponceIP = common.EncryptString(strResponceIP, "gop");
                                //Response.Write("REDIRECT=" + strGCUrl + "RefundResponse.aspx?ResRef=" + INQResRef[0] + "&ResAuth=" + INQResAuth[0] + "&ResAuth=" + INQResTrans[0] + "&PayID=" + INQResPayid[0] + "&ResResult=" + INQResResult[0] + "&ResAmount=" + INQResAmount[0] + "&Caption=" + INQCheck[0] + "&udf1=" + strResponceIP + "&udf2=" + INQResUdf2 + "&udf1=" + INQResUdf5 + "&udf1=" + INQResPost + "&udf1=" + INQResAvrg);
                            }
                            else
                            {
                               // Response.Write("REDIRECT=" + strGCUrl + "RefundFailure.aspx?ResError=" + INQCheck[0]);
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        Response.Write(Ex.Message);// any excpetion occurred for above code exception throws here
                    }
            
        }

        private void LoadData()
        {
            booking_bal objRefund = new booking_bal();
            dt = objRefund.getRecheckPaymentDetails();
            if (dt.Rows.Count > 0)
            {
                string xmlId = "<id>" + strTransportalID + "</id>";
                string xmlPassword = "<password>" + strGCTransportalPwd + "</password>";
                string xmlAction = "<action>8</action>";
                string xmlCurrency = "<udf5>PaymentID</udf5>";
                string currencycode = "<currencycode>356</currencycode>";  // Mandatory
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string xmlAmount = "<amt>" + dt.Rows[i]["OverAllAmount"].ToString() + "</amt>";
                    string xmlMember = "<trackid>" + dt.Rows[i]["BookingID"].ToString() + "</trackid>";
                    string xmlTrans = "<transid>" + dt.Rows[i]["PaymentID"].ToString() + "</transid>";
                    //string member = "<member>" + dt.Rows[i]["FirstName"].ToString() + "</member>";	 //Mandatory
                    string INQRequest = xmlId + xmlPassword + currencycode + xmlAction + xmlAmount + xmlMember + xmlTrans + xmlCurrency;
                    //Response.Write(INQRequest);
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
                            Response.Write(" ----- ");
                            Response.Write(INQCheck[1]);
                            Response.Write("<br/>");
                            book.updatePGRecheckStatusbybookingdetailid(1, Convert.ToInt32(dt.Rows[i]["BookingDetailID"].ToString()), INQCheck[0]);
                            //Response.End();
                            if (INQCheck[0] == "SUCCESS")
                            {
                                //string[] INQResResult = GetStringInBetween(TranInqResponse, "<result>", "</result>", false, false);//It will give DualInquiry Result 
                                string[] INQResAuth = GetStringInBetween(TranInqResponse, "<auth>", "</auth>", false, false);//It will give TrackID ENROLLED
                                string[] INQResRef = GetStringInBetween(TranInqResponse, "<ref>", "</ref>", false, false);//It will give Ref.NO.
                                //string[] INQResAvrg = GetStringInBetween(TranInqResponse, "<avr>", "</avr>", false, false);//It will give tranid
                                //string[] INQResPost = GetStringInBetween(TranInqResponse, "<postdate>", "</postdate>", false, false);//It will give Auth 
                                string[] INQResTrans = GetStringInBetween(TranInqResponse, "<tranid>", "</tranid>", false, false);//It will give Auth 
                                string[] INQResTrackid = GetStringInBetween(TranInqResponse, "<trackid>", "</trackid>", false, false);
                                //It will
                                string[] INQResPayid = GetStringInBetween(TranInqResponse, "<payid>", "</payid>", false, false);//It will give payid
                                //string[] INQResUdf2 = GetStringInBetween(TranInqResponse, "<udf2>", "</udf2>", false, false);//It will give Amount
                                //string[] INQResUdf5 = GetStringInBetween(TranInqResponse, "<udf5>", "</udf5>", false, false);//It will give Amount
                                //string[] INQResAmount = GetStringInBetween(TranInqResponse, "<amt>", "</amt>", false, false);//It will give Amount

                                //commonfunctions common = new commonfunctions();
                                //INQResRef[0] = common.EncryptString(INQResRef[0], "gop");
                                //INQResAuth[0] = common.EncryptString(INQResAuth[0], "gop");
                                //INQResTrans[0] = common.EncryptString(INQResTrans[0], "gop");
                                //INQResPayid[0] = common.EncryptString(INQResPayid[0], "gop");
                                //INQResResult[0] = common.EncryptString(INQResResult[0], "gop");
                                //INQResAmount[0] = common.EncryptString(INQResAmount[0], "gop");
                                //INQResUdf2[0] = common.EncryptString(INQResUdf2[0], "gop");
                                //INQResUdf5[0] = common.EncryptString(INQResUdf5[0], "gop");
                                //INQResPost[0] = common.EncryptString(INQResPost[0], "gop");
                                //INQResAvrg[0] = common.EncryptString(INQResAvrg[0], "gop");
                                //INQCheck[0] = common.EncryptString(INQCheck[0], "gop");
                                //strResponceIP = common.EncryptString(strResponceIP, "gop");
                                //Response.Write("REDIRECT=" + strGCUrl + "RefundResponse.aspx?ResRef=" + INQResRef[0] + "&ResAuth=" + INQResAuth[0] + "&ResAuth=" + INQResTrans[0] + "&PayID=" + INQResPayid[0] + "&ResResult=" + INQResResult[0] + "&ResAmount=" + INQResAmount[0] + "&Caption=" + INQCheck[0] + "&udf1=" + strResponceIP + "&udf2=" + INQResUdf2 + "&udf1=" + INQResUdf5 + "&udf1=" + INQResPost + "&udf1=" + INQResAvrg);
                                book.updatePGStatusbybookingdetailid(true, int.Parse(dt.Rows[i]["BookingDetailID"].ToString()));
                                objHdfcResponse.BookingInfoID = Convert.ToInt32(dt.Rows[i]["BookingDetailID"].ToString());
                                objHdfcResponse.PaymentId = INQResPayid[0];
                                objHdfcResponse.TrackId = INQResTrackid[0];
                                objHdfcResponse.Result = INQCheck[0] + "[" + INQCheck[0] + "]";
                                objHdfcResponse.TransactionID = INQResTrans[0];
                                objHdfcResponse.AuthCode = INQResAuth[0];
                                objHdfcResponse.Reference = INQResRef[0];
                                objHdfcResponse.TransactionStatus = true;
                                objHdfcResponse.DualVerification = true;
                                book.insertResponseDetails(objHdfcResponse);
                            }
                            else
                            {
                               // Response.Write("REDIRECT=" + strGCUrl + "RefundFailure.aspx?ResError=" + INQCheck[0]);
                            }
                        }
                    }
                    catch (Exception Ex)
                    {
                        Response.Write(Ex.Message);// any excpetion occurred for above code exception throws here
                    }
                }
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