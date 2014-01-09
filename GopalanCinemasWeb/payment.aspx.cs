using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GopalanCinemasEntities;
using GopalanCinemasBL;
using System.Data;
using System.Data.SqlClient;

namespace GopalanCinemasWeb
{
    public partial class payment : System.Web.UI.Page
    {
        string connstr = System.Configuration.ConfigurationManager.AppSettings["GCCon"];
        string[] strBookArr = new string[] { };
        int intUserID;
        Bigtree.VistaRemote.clsBook objbook = new Bigtree.VistaRemote.clsBook();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["SessBookInfo"] == null)
            {
                Response.Redirect("index.aspx");
            }
            else
            {
                strBookArr = (string[])Session["SessBookInfo"];
            }
            if (!Page.IsPostBack)
            {
                spCinemaName.InnerHtml = strBookArr[6];
                spMovieName.InnerHtml = strBookArr[7];
                spDate.InnerHtml = Convert.ToDateTime(strBookArr[2]).ToString("dd/MM/yyyy");
                spShowTime.InnerHtml =  strBookArr[5];
                spSeat.InnerHtml = strBookArr[4];
                spSeatInfo.InnerHtml = Session["sessionSeatInfo"].ToString();
                //spTotalAmountCredit.InnerHtml = "<strong>Rs. " + Session["TotalAmount"].ToString() + ".00</strong>";
                spTotalAmountDebit.InnerHtml = "<strong>Rs. " + Session["TotalAmount"].ToString() + "</strong>";
                //spTotalAmountNet.InnerHtml = "<strong>Rs. " + Session["TotalAmount"].ToString() + ".00</strong>";
               // hdntotalamount.Value = Session["TotalAmount"].ToString();
            }
        }

        protected void imgPayment_Click(object sender, ImageClickEventArgs e)
        {
            //string strScript1 = "<script language='javascript'>submitform();</script>";
            //Page.RegisterStartupScript("myjava2", strScript1);
//            Response.Redirect("SendPerformREQuest.aspx");
            if (txtEmail.Text != "" && txtMobile.Text != "" && txtUName.Text != "")
            {
//                imgPayment.Enabled = false;
                users objentities = new users();
                booking objEntities = new booking();
                booking_bal objBooking = new booking_bal();
                users_bal objuserbal = new users_bal();

                objentities.LastName = txtUName.Text;
                objentities.Email = txtEmail.Text;
                objentities.MobPhone = txtMobile.Text;
                string id = objuserbal.InsertUsers(objentities);

                intUserID = Convert.ToInt32(id);
                if (intUserID != 0 && intUserID.ToString() != "")
                {
                    //Session["FullsessionBookingID"] = Session["sessioncinemaID"].ToString() + sb.ToString() + Session["sessionBookingID"].ToString();
                    objEntities.Booking_CinemaID = strBookArr[0];
                    Session["SessCinemaID"] = strBookArr[0];
                    objEntities.Booking_SessionID = long.Parse(strBookArr[3].ToString());
                    objEntities.UserID = intUserID.ToString();
                    objEntities.Booking_MovieID = strBookArr[1].ToString();
                    objEntities.TransID = Session["sessionTransID"].ToString();
                    string strcid = "";
                    if (strBookArr[0] == "1000")
                    {
                        strcid = "GCIM";
                    }
                    if (strBookArr[0] == "1001")
                    {
                        strcid = "GCLM";
                    }
                    if (strBookArr[0] == "1002")
                    {
                        strcid = "GCAM";
                    }
                    if (strBookArr[0] == "1004")
                    {
                        strcid = "GCGM";
                    }
                    objEntities.BookingID = strcid + strBookArr[0] + Session["sessionBookingID"].ToString();
                    objEntities.Booking_BookingDate = DateTime.Parse(DateTime.Now.ToString());
                    objEntities.Booking_ShowDate = DateTime.Parse(strBookArr[2].ToString());
                    objEntities.ShowTime = strBookArr[5].ToString();
                    objEntities.bookid = long.Parse(Session["sessionBookingID"].ToString());
                    objEntities.ShowClass = Session["sessionShowClass"].ToString();
                    objEntities.NoofSeat = int.Parse(strBookArr[4].ToString());
                    objEntities.SeatDetails = Session["sessionSeatInfo"].ToString();
                    objEntities.FoodTotal = "0";
                    objEntities.TicketAmount = Session["sessionTicketAmount"].ToString();
                    objEntities.UIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    objEntities.ScreenName = "";
                    //objEntities.intCityID = int.Parse(Session["bookingcityid"].ToString());
                    int lastindentity = objBooking.insertBookingDetails(objEntities);
                    Bigtree.VistaRemote.clsBook objBook = new Bigtree.VistaRemote.clsBook();
                    if (lastindentity != 0 && lastindentity.ToString() != "")
                    {
                        Session["BookingDetailID"] = lastindentity.ToString();
                        objBooking.UpdateOverAllAmountByBookingDetailID(decimal.Parse(Session["TotalAmount"].ToString()), int.Parse(Session["BookingDetailID"].ToString()));
                        sendRequest(lastindentity);
                    }
                }
            }
        }

        private void sendRequest(int lastid)
        {
            try
            {
                booking_bal objBooking = new booking_bal();
                string TranTrackid;
                string TranAmount;
                Session["SessBookInfo"] = null;
                string strUDFInfo = Session["sessionTransID"].ToString() + "_" + Session["sessionBookingID"].ToString() + "_" + Session["BookingDetailID"].ToString();
                string strcinemaid = "";
                if (Session["SessCinemaID"].ToString() == "1000")
                {
                    strcinemaid = "GCIM";
                }
                if (Session["SessCinemaID"].ToString() == "1001")
                {
                    strcinemaid = "GCLM";
                }
                if (Session["SessCinemaID"].ToString() == "1002")
                {
                    strcinemaid = "GCAM";
                }
                if (Session["SessCinemaID"].ToString() == "1004")
                {
                    strcinemaid = "GCGM";
                }
                TranTrackid = strcinemaid + Session["SessCinemaID"].ToString() + Session["sessionBookingID"].ToString();
                Session["sessionTrackID"] = TranTrackid;
                TranAmount = Session["TotalAmount"].ToString();
                string strTransportalID = System.Configuration.ConfigurationManager.AppSettings["GCTransportalID" + Session["SessCinemaID"].ToString()].ToString();
                string strGCTransportalPwd = System.Configuration.ConfigurationManager.AppSettings["GCTransportalPwd" + Session["SessCinemaID"].ToString()].ToString();
                string strGCUrl = System.Configuration.ConfigurationManager.AppSettings["GCUrl"].ToString();
                string strGCPaymentURL = System.Configuration.ConfigurationManager.AppSettings["GCPaymentURL"].ToString();

                string ReqTranportalId = "id=" + strTransportalID + "&";
                string ReqTranportalPassword = "password=" + strGCTransportalPwd + "&";
                string ReqAction = "action=" + "1" + "&";
                string ReqLangid = "langid=" + "USA" + "&";
                string ReqCurrency = "currencycode=" + "356" + "&";
                string ReqAmount = "amt=" + TranAmount + "&";
                string ReqResponseUrl = "responseURL=" + strGCUrl + "response.aspx" + "&";
                //string ReqResponseUrl = "responseURL=" + strGCUrl + "pg/GetHandleRESponse.aspx" + "&";
                string ReqErrorUrl = "errorURL=" + strGCUrl + "failure.aspx" + "&";
                string ReqTrackId = "trackid=" + TranTrackid + "&";
                string ReqUdf1 = "udf1=" + strUDFInfo + "&";		// UDF1 values                                                                
                string ReqUdf2 = "udf2=" + txtEmail.Text + "&";		// UDF2 values 	                                                               
                string ReqUdf3 = "udf3=" + txtMobile.Text + "&";	 	// UDF3 values                                                             
                string ReqUdf4 = "udf4=" + txtUName.Text + "&";	 	// UDF4 values                                                                
                string ReqUdf5 = "udf5=" + Session["SessCinemaID"].ToString() + "&";    	// UDF5 values 

                string TranResponse = "";//Declaration of variable 

                /* Now merchant sets all the inputs in one string for passing to the Payment Gateway URL */
                string TranRequest = ReqTranportalId + ReqTranportalPassword + ReqAction + ReqLangid + ReqCurrency + ReqAmount + ReqResponseUrl + ReqErrorUrl + ReqTrackId + ReqUdf1 + ReqUdf2 + ReqUdf3 + ReqUdf4 + ReqUdf5;

                string TranUrl = strGCPaymentURL;
                //create a SSL connection xmlhttp formated object server-to-server
                System.IO.StreamWriter myWriter = null;
                // it will open a http connection with provided url
                System.Net.HttpWebRequest objRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(TranUrl);//send data using objxmlhttp object
                objRequest.Method = "POST";
                objRequest.ContentLength = TranRequest.Length;
                objRequest.ContentType = "application/x-www-form-urlencoded";//to set content type
                myWriter = new System.IO.StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(TranRequest);//send data
                myWriter.Close();//closed the myWriter object

                System.Net.HttpWebResponse objResponse = (System.Net.HttpWebResponse)objRequest.GetResponse();
                //receive the responce from objxmlhttp object 
                using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream()))
                {
                    TranResponse = sr.ReadToEnd();

                    string[] ErrorCheck = GetStringInBetween(TranResponse, "!", "!-", false, false);
                    //This line will find Error Keyword in TranResponse	

                    if (ErrorCheck[0] != "ERROR")//This block will check for Error in TranResponce
                    {
                        // Merchant MUST map (update) the Payment ID received with the merchant Track Id in his database at this place.
                        string payid = TranResponse.Substring(0, TranResponse.IndexOf(":http"));
                        string payURL = TranResponse.Substring(TranResponse.IndexOf("http"));
                        // here redirecting the customer browser from ME site to Payment Gateway Page with the Payment ID
                        string TranRedirect = payURL + "?PaymentID=" + payid;
                        Session["sessionPayID"] = payid;
                        objBooking.updatePayIdbybookingdetailid(payid, lastid);
                        Response.Redirect(TranRedirect);
                    }
                    else
                    {
                        string TranErrorUrl = strGCUrl +"failure.aspx?Message=Transaction Failed&ResTrackId=" + TranTrackid + "&ResAmount=" + TranAmount + "&ResError=" + TranResponse;
                        // here redirecting the error page 
                        Response.Redirect(TranErrorUrl);
                    }

                }
            }
            catch (Exception ex)// any excpetion occurred for above code exception throws here
            {
                Response.Write(ex);// exception message
            }
        }
        public static string[] GetStringInBetween(string strSource, string strBegin, string strEnd, bool includeBegin, bool includeEnd)
        {
            string[] result = { "", "" };
            int iIndexOfBegin = strSource.IndexOf(strBegin);
            if (iIndexOfBegin != -1)
            {
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
                    { 
                        result[1] = strSource.Substring(iEnd + strEnd.Length); 
                    }
                }
            }
            else
            // stay where we are
            { result[1] = strSource; }
            return result;
        }

        protected void imgCancel_Click(object sender, ImageClickEventArgs e)
        {
            string strmsg = "";
            objbook.blnSetSettings("ConnectionString", connstr);
            objbook.blnSetSettings("LicenseType", "WWW");
            if (objbook.blnCancelTrans(strBookArr[0], Session["sessionTransID"].ToString().Trim()))
            {
                strmsg += "1";
            }
            else
            {
                strmsg += "0";
            }
            //if (objbook.blnCancelBook(strBookArr[0], long.Parse(Session["sessionBookingID"].ToString().Trim())))
            //{
            //    strmsg += "1";
            //}
            //else
            //{
            //    strmsg += "0";
            //}
            Response.Redirect("index.aspx");
        }//String function end
    }
}