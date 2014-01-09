using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GopalanCinemasEntities;
using GopalanCinemasBL;
using System.Data;
using System.Text;
using System.Net.Mail;
using System.IO;
using System.Net;

namespace GopalanCinemasWeb
{
    public partial class sendresponse : System.Web.UI.Page
    {
        string strResponceIP, strResponseURL, TranInqResponse, ResPaymentId, ResResult, ResErrorText, ResPosdate, ResTranId, ResAuth, ResAmount, ResErrorNo, ResTrackID, ResRef, Resudf1, Resudf2, Resudf3, Resudf4, Resudf5;
        int BookingInfoID;
        string strCinemaID, strSessionTransID, strCardNo, StrCardType, strCVV, strName, strEmail, strMobile, strBookID, strCaption;
        long lngSessionLngID;
        short shrtExpirtyYear, shrtExpiryMonth;
        commonfunctions common = new commonfunctions();
        hdf_response objHdfcResponse = new hdf_response();
        booking_bal book = new booking_bal();
        Bigtree.VistaRemote.clsBook objBook = new Bigtree.VistaRemote.clsBook();
        string connstr = System.Configuration.ConfigurationManager.AppSettings["GCCon"];
        double intConvenience = 16.85;
        string strKioskID, strVistaError= "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["PayID"] == null || Request["ResTrackID"] == null || Request["ResResult"] == null || Request["ResTranId"] == null || Request["ResAuth"] == null || Request["ResRef"] == null || Request["Caption"] == null)
            {
                Response.Redirect("index.aspx");
            }
            ResPaymentId = Request["PayID"].ToString();
            ResTrackID = Request["ResTrackID"].ToString();
            ResResult = Request["ResResult"].ToString();
            ResTranId = Request["ResTranId"].ToString();
            ResAuth = Request["ResAuth"].ToString();
            ResRef = Request["ResRef"].ToString();
            strCaption = Request["Caption"].ToString();
           // strResponceIP = Request["udf1"].ToString();
           
            ResPaymentId = common.DecryptString(ResPaymentId.ToString(), "gop");
            ResTrackID = common.DecryptString(ResTrackID.ToString(), "gop");
            ResResult = common.DecryptString(ResResult.ToString(), "gop");
            ResTranId = common.DecryptString(ResTranId.ToString(), "gop");
            ResAuth = common.DecryptString(ResAuth.ToString(), "gop");
            ResRef = common.DecryptString(ResRef.ToString(), "gop");
            strCaption = common.DecryptString(strCaption.ToString(), "gop");
            //strResponceIP = common.DecryptString(strResponceIP.ToString(), "gop");

            DataTable dtDetails =  book.getBDIDbypaytrackid(ResPaymentId, ResTrackID);
            if (dtDetails.Rows.Count > 0)
            {
                Session["BookingDetailID"] = dtDetails.Rows[0][BookingInfoID].ToString();
            }
            else
            {
                Response.Redirect("sendfailure.aspx?ResError=RecordsNotFound");
            }
            if (Session["BookingDetailID"] != null)
            {
//                if (ResPaymentId == Session["sessionPayID"].ToString() && ResTrackID == Session["sessionTrackID"].ToString())
//                {
                    bool blPayID = checkPayId(ResPaymentId);
                    if (blPayID)
                    {
                        UpdatePGStatus();
                        UpdateResponseDetails();
                        updatevistastatus();
                        Response.Redirect("confirmation.aspx");
                    }
                    else
                    {
                        Response.Redirect("failure.aspx");
                    }
//                }
//                else
//                {
//                    Response.Redirect("failure.aspx");
//                }
            }
            else
            {
                //ResTrackID = Request["ResTrackID"].ToString();
                //ResPaymentId = Request["PayID"].ToString();
                //ResTranId = Request["ResTranId"].ToString();
                if (ResTrackID != "" && ResPaymentId != "" && ResTranId !="")
                {
                    DataTable dtBooking = book.getBDIDbyfullbookingid(ResTrackID);
                    if (dtBooking.Rows.Count > 0)
                    {
                        strCinemaID = dtBooking.Rows[0]["Cinema_str"].ToString();
                        strSessionTransID = dtBooking.Rows[0]["Trans_id"].ToString();
                        objBook.blnCancelTrans(strCinemaID, strSessionTransID);
                        book.updatePGStatusbybookingdetailid(true, int.Parse(dtBooking.Rows[0]["BookingInfoID"].ToString()));
                        updateVistaStatusbybookingdetailid(false, int.Parse(dtBooking.Rows[0]["BookingInfoID"].ToString()));
                        updateRefundStatusbybookingdetailid(false, int.Parse(dtBooking.Rows[0]["BookingInfoID"].ToString()));
                        sendBadMAIL(dtBooking);
                        Session["SessUpdateId"] = dtBooking.Rows[0]["BookingInfoID"].ToString();
                        Response.Redirect("sendfailure.aspx?ResError=SessionExpired");
                    }
                    else
                    {
                        Response.Redirect("index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("index.aspx");
                }
            }
        }

        private bool checkPayId(string ResPaymentId)
        {
            DataTable dtPay = book.getPaymentData(ResPaymentId);
            if (dtPay.Rows.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UpdateResponseDetails()
        {
            objHdfcResponse.BookingInfoID = Convert.ToInt32(Session["BookingDetailID"].ToString());
            objHdfcResponse.PaymentId = ResPaymentId.ToString();
            objHdfcResponse.TrackId = ResTrackID.ToString();
            //objHdfcResponse.Result = ResResult.ToString() + "[" + strCaption + "]" + "[" + strResponceIP + "#" +strResponseURL + "]";
            objHdfcResponse.Result = ResResult.ToString() + "[" + strCaption + "]";
            objHdfcResponse.TransactionID = ResTranId.ToString();
            objHdfcResponse.AuthCode = ResAuth.ToString();
            objHdfcResponse.Reference = ResRef.ToString();
            objHdfcResponse.TransactionStatus = true;
            objHdfcResponse.DualVerification = true;
            book.insertResponseDetails(objHdfcResponse);
        }

        private void updatevistastatus()
        {
            try
            {
                bool conbol = objBook.blnSetSettings("ConnectionString", connstr);
                bool conlin = objBook.blnSetSettings("LicenseType", "WWW");
                BookingInfoID = Convert.ToInt32(Session["BookingDetailID"].ToString());
                booking_bal booking = new booking_bal();
                if (conbol && conlin)
                {
                    DataTable dtBookingInfo = booking.getBookingDetailByBDID(int.Parse(Session["BookingDetailID"].ToString()));
                    if (dtBookingInfo.Rows.Count > 0)
                    {
                        strCinemaID = dtBookingInfo.Rows[0]["Cinema_str"].ToString();
                        lngSessionLngID = long.Parse(dtBookingInfo.Rows[0]["Session_lngSessionId"].ToString());
                        strSessionTransID = dtBookingInfo.Rows[0]["Trans_id"].ToString();
                        strBookID = dtBookingInfo.Rows[0]["bookid"].ToString();
                        strMobile = dtBookingInfo.Rows[0]["Mobile"].ToString();
                        strEmail = dtBookingInfo.Rows[0]["Email"].ToString();
                        strName = "Guest";
                        strCardNo = "1234123412341234";
                        StrCardType = "VISA";
                        shrtExpirtyYear = short.Parse("12");
                        shrtExpiryMonth = short.Parse("12");
                        strCVV = "123";
                        //bool blcn = false;
                        //blcn = objBook.blnCommitBook(strCinemaID, strSessionTransID, lngSessionLngID, true, strCardNo, StrCardType, shrtExpiryMonth, shrtExpirtyYear, strCVV, strName, strMobile, "Ticket Booking", strName);
                        //Response.Write(blcn);
                        //Response.End();
                        if (objBook.blnCommitBook(strCinemaID, strSessionTransID, lngSessionLngID, true, strCardNo, StrCardType, shrtExpiryMonth, shrtExpirtyYear, strCVV, strName, strMobile, "Ticket Booking", strName))
                        {
                            updateVistaStatusbybookingdetailid(true, int.Parse(Session["BookingDetailID"].ToString()));
                            strKioskID = objBook.strBookId;
                            updatekioskbybookingdetailid(objBook.strBookId, int.Parse(Session["BookingDetailID"].ToString()));
                            sendMAIL(strEmail, dtBookingInfo);
                            //sendSMS(strMobile, Session["FullsessionBookingID"].ToString());
                            book.updateTxnStatusbybookingdetailid(true, int.Parse(Session["BookingDetailID"].ToString()));
                        }
                        else
                        {
                            strVistaError = objBook.strException;
                            string strErrorInfo = "Code = " + objBook.intException + "<br/>Message = "+ objBook.intExceptionEx + " " + objBook.strException;
                            common.SendNetMail("support@gopalancinema.com", "mohan.pitchai@influx.co.in", "", strErrorInfo, "Gopalan Cinemas - Vista Error", "");
                            string BookingStatusMessage = objBook.strBookingStatus(strCinemaID, long.Parse(strBookID.ToString()));
                            if (BookingStatusMessage == "P")
                            {
                                objBook.blnContinueTrans(strCinemaID, strSessionTransID);
                                updateVistaStatusbybookingdetailid(true, int.Parse(Session["BookingDetailID"].ToString()));
                                //sendSMS(strMobile, Session["FullsessionBookingID"].ToString());
                                sendMAIL(strEmail, dtBookingInfo);
                                book.updateTxnStatusbybookingdetailid(true, int.Parse(Session["BookingDetailID"].ToString()));
                            }
                            else if (BookingStatusMessage == "I")
                            {
                                objBook.blnContinueTrans(strCinemaID, strSessionTransID);
                                BookingStatusMessage = objBook.strBookingStatus(strCinemaID, long.Parse(strBookID));
                                if (BookingStatusMessage == "P")
                                {
                                    updateVistaStatusbybookingdetailid(true, int.Parse(Session["BookingDetailID"].ToString()));
                                    //sendSMS(strMobile, Session["FullsessionBookingID"].ToString());
                                    sendMAIL(strEmail, dtBookingInfo);
                                    book.updateTxnStatusbybookingdetailid(true, int.Parse(Session["BookingDetailID"].ToString()));
                                }
                                else
                                {
                                    objBook.blnCancelTrans(strCinemaID, strSessionTransID);
                                    updateVistaStatusbybookingdetailid(false, int.Parse(Session["BookingDetailID"].ToString()));
                                    updateRefundStatusbybookingdetailid(false, int.Parse(Session["BookingDetailID"].ToString()));
                                    sendBadMAIL(dtBookingInfo);
                                    book.updateErrorStatusbybookingdetailid(strVistaError, int.Parse(Session["BookingDetailID"].ToString()));
                                    Response.Redirect("sendfailure.aspx");
                                }
                            }
                            else
                            {
                                objBook.blnCancelTrans(strCinemaID, strSessionTransID);
                                updateVistaStatusbybookingdetailid(false, int.Parse(Session["BookingDetailID"].ToString()));
                                updateRefundStatusbybookingdetailid(false, int.Parse(Session["BookingDetailID"].ToString()));
                                sendBadMAIL(dtBookingInfo);
                                //sendMAIL(strEmail, dtBookingInfo);
                                book.updateErrorStatusbybookingdetailid(strVistaError, int.Parse(Session["BookingDetailID"].ToString()));
                                Response.Redirect("sendfailure.aspx");
                                //Response.Redirect("confirmation.aspx");
                           }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());
            }
        }

        private void updateVistaStatusbybookingdetailid(bool vistastatus, int bdid)
        {
            try
            {
                book.updateVistaStatusbybookingdetailid(vistastatus, bdid);
            }
            catch (Exception ex)
            {
                InfluxRepository.ErrorLog.ErrorLog.LogErrorToLogFile(ex, "Gopalan Cinemas");
            }
        }

        private void UpdatePGStatus()
        {
            book.updatePGStatusbybookingdetailid(true, int.Parse(Session["BookingDetailID"].ToString()));
        }
        private void sendMAIL(string email, DataTable dtBookingInfo)
        {
            try
            {

                commonfunctions common = new commonfunctions();
                string mailFrom = "support@gopalancinemas.com";
                string mailTo = email;
                //string mailCC = citymailid;
                string mailCC = "support@gopalancinemas.com";

                string Subject = "Ticket Booking Confirmation";

                StringBuilder sb = new StringBuilder();
                decimal dclTotalAmount = Convert.ToDecimal(dtBookingInfo.Rows[0]["OverAllAmount"].ToString());
                dclTotalAmount = Math.Round(dclTotalAmount, 2);
                decimal dclTicketAmount = Convert.ToDecimal(dtBookingInfo.Rows[0]["TicketAmount"].ToString());
                dclTicketAmount = Math.Round(dclTicketAmount, 2);
                sb.Append("<html><head><title>Booking Confirmation</title>");
                sb.Append("<style type='text/css'>body {margin: 0px;}.bdy-txt {font: normal 12px Arial, Helvetica, sans-serif; color:#000; text-align:left}.bdy-txt2 {font: bold 12px Arial, Helvetica, sans-serif; color:#000; text-align:left; height:27px}.yellow-txt {font: bold 14px Arial, Helvetica, sans-serif; color:#faf115; text-align:left}.white-txt {font: normal 12px Arial, Helvetica, sans-serif; color:#fff; text-align:left;}.white-txt a {font: normal 12px Arial, Helvetica, sans-serif; color:#fff; text-align:left; text-decoration:underline}.white-txt a:hover {text-decoration:none}.white-txt2 {font: bold 12px Arial, Helvetica, sans-serif; color:#fff; text-align:left}</style>");
                sb.Append("</head>");
                sb.Append("<body>");
                sb.Append("<table width='635' border='0' cellspacing='0' cellpadding='0'>");
                sb.Append("<tr><td height='133' valign='top'>");
                sb.Append("<table width='635' border='0' cellspacing='0' cellpadding='0'>");
                sb.Append("<tr><td><img src='http://www.gopalancinemas.com/images/mailer-top_01.jpg' width='222' height='130' /></td><td><img src='http://www.gopalancinemas.com/images/mailer-top_02.jpg' width='192' height='130' /></td><td><img src='http://www.gopalancinemas.com/images/mailer-top_03.jpg' width='221' height='130' /></td></tr>");
                sb.Append("</table></td></tr>");
                sb.Append("<tr><td valign='top' bgcolor='#e2e2e2' style='padding:15px'>");
                sb.Append("<table width='605' border='0' cellspacing='0' cellpadding='0'>");
                sb.Append("<tr><td colspan='3' class='bdy-txt2'> Dear " + dtBookingInfo.Rows[0]["LastName"].ToString() + ", your booking details as follows <br/></td></tr>");
                sb.Append("<tr><td width='182' class='bdy-txt2'>Booking Id</td><td width='18' class='bdy-txt2'>:</td><td width='405' class='bdy-txt'>"+ dtBookingInfo.Rows[0]["BookingID"].ToString() +"</td></tr>");
                sb.Append("<tr><td width='182' class='bdy-txt2'>Kiosk Id</td><td width='18' class='bdy-txt2'>:</td><td width='405' class='bdy-txt'>" + strKioskID + "</td></tr>");
                sb.Append("<tr><td class='bdy-txt2'>Movie</td><td class='bdy-txt2'>:</td><td class='bdy-txt'>" + dtBookingInfo.Rows[0]["Film_strTitle"].ToString() + "</td></tr>");
                sb.Append("<tr><td class='bdy-txt2'>Cinema</td><td class='bdy-txt2'>:</td><td class='bdy-txt'>" + dtBookingInfo.Rows[0]["Cinema_strName"].ToString() + "</td></tr>");
                DateTime sddt = DateTime.Parse(dtBookingInfo.Rows[0]["ShowDate"].ToString());
                DateTime shdt = DateTime.Parse(dtBookingInfo.Rows[0]["ShowTime"].ToString());
                sb.Append("<tr><td class='bdy-txt2'>Show</td><td class='bdy-txt2'>:</td><td class='bdy-txt'>"+ String.Format("{0:dd MMMM yyyy}", sddt) +"@"+ String.Format("{0:HH:mm}", shdt)+" (24-hour format)</td></tr>");
                sb.Append("<tr><td class='bdy-txt2'>Seats Info</td><td class='bdy-txt2'>:</td><td class='bdy-txt'>"+dtBookingInfo.Rows[0]["SeatInfo"].ToString() +"</td></tr>");
                sb.Append("<tr><td class='bdy-txt2'>No of Ticket(s)</td><td class='bdy-txt2'>:</td><td class='bdy-txt'>"+dtBookingInfo.Rows[0]["NoofSeat"].ToString()+"</td></tr>");
                sb.Append("<tr><td class='bdy-txt2'>Total Amount Paid</td><td class='bdy-txt2'>:</td><td class='bdy-txt'>" + dclTotalAmount.ToString() + "</td></tr>");
                sb.Append("<tr><td class='bdy-txt2'>Ticketing Amount</td><td class='bdy-txt2'>:</td><td class='bdy-txt'>"+dclTicketAmount.ToString()+"</td></tr>");
                decimal intConvenienceDisplay = Convert.ToDecimal(Convert.ToDecimal(intConvenience) * Convert.ToInt32(dtBookingInfo.Rows[0]["NoofSeat"].ToString()));
                sb.Append("<tr><td class='bdy-txt2'>Convenience Fee</td><td class='bdy-txt2'>:</td><td class='bdy-txt'>" + Math.Round(intConvenienceDisplay, 2) + "</td></tr>");
                sb.Append("<tr><td class='bdy-txt2'>Total Amount</td><td class='bdy-txt2'>:</td><td class='bdy-txt'>" + dclTotalAmount.ToString() + "</td></tr>");
                sb.Append("<tr><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt'>&nbsp;</td></tr>");

                //if (int.Parse(dtBookingInfo.Rows[0]["NoofSeat"].ToString()) == 2)
                //{
                //    sb.Append("<tr><td colspan='3' class='bdy-txt2' align='left'>You are eligible for small pop corn free of cost.</td></tr>");
                //}
                //if (int.Parse(dtBookingInfo.Rows[0]["NoofSeat"].ToString()) >= 3 && int.Parse(dtBookingInfo.Rows[0]["NoofSeat"].ToString()) <= 5)
                //{
                //    sb.Append("<tr><td colspan='3' class='bdy-txt2' align='left'>You are eligible for Small pepsi combo(small pepsi + Small popcorn) free of cost.</td></tr>");
                //}
                //if (int.Parse(dtBookingInfo.Rows[0]["NoofSeat"].ToString()) >= 6)
                //{
                //    sb.Append("<tr><td colspan='3' class='bdy-txt2' align='left'>You are eligible for combo coupon (Tub popcorn & 2 small pepsi) free of cost.</td></tr>");
                //}

                //sb.Append("<tr><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt'>&nbsp;</td></tr>");
                sb.Append("<tr><td colspan='3' class='bdy-txt2' align='left'>Tickets once booked cannot be exchanged, cancelled or refunded.</td></tr>");
                sb.Append("<tr><td colspan='3' class='bdy-txt2' align='left'>The Credit Card and Credit Card Holder must be present at the ticket counter while collecting the Tickets.</td></tr>");
                //sb.Append("<tr><td colspan='3' class='bdy-txt2' align='left'><img src='http://www.gopalancinemas.com/images/grandmall.jpg' border='0' /></td></tr>");
                //sb.Append("<tr><td colspan='3' class='bdy-txt2' align='left'>"+(char)34 + "Kindly produce a printed copy of this booking confirmation or the e-mail confirmation at Gopalan Box office counter and collect your coupons to avail your free small popcorn(s)*"+(char)34+".<br /><br />*T&C Book tickets through www.gopalancinemas.com website and get small popcorn free for each tickets.</td></tr>");
                sb.Append("<tr><td colspan='3' class='bdy-txt2' align='left'>Please take printout for your future reference.</td></tr>");
                //sb.Append("<tr><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt'>&nbsp;</td></tr>");
                //sb.Append("<tr><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt'>&nbsp;</td></tr>");
                //sb.Append("<tr><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt'>&nbsp;</td></tr>");
                //sb.Append("<tr><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt'>&nbsp;</td></tr>");
                //sb.Append("<tr><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt'>&nbsp;</td></tr>");
                //sb.Append("<tr><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt'>&nbsp;</td></tr>");
                //sb.Append("<tr><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt'>&nbsp;</td></tr>");
                //sb.Append("<tr><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt2'>&nbsp;</td><td class='bdy-txt'>&nbsp;</td></tr>");
                sb.Append("</table>");
                sb.Append("</td></tr>");
                //sb.Append("<tr><td height='3'></td></tr>");
                sb.Append("<tr><td height='79' valign='top'  bgcolor='#850c15' style='padding:12px 0 0 15px'>");
                sb.Append("<table width='620' border='0' cellspacing='0' cellpadding='0'>");
                sb.Append("<tr>");
                sb.Append("<td width='432' valign='top'>");
                sb.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                sb.Append("<tr><td height='22' valign='top' class='yellow-txt' style='color:#ffffff;'>Thank you and have a nice day.</td></tr>");
                sb.Append("<tr><td class='white-txt2' style='color:#ffffff;'>Gopalan Cinemas Web Team.</td></tr>");
                sb.Append("<tr><td class='white-txt'><span class='white-txt2' style='color:#ffffff;'>visit us</span> <a href='http://www.gopalancinemas.com' class='white-txt' style='color:#ffffff;'>www.gopalancinemas.com</a></td></tr>");
                sb.Append("</table></td>");
                sb.Append("<td width='188' valign='top'>");
                sb.Append("<table width='100%' border='0' cellspacing='0' cellpadding='0'>");
                sb.Append("<tr><td height='18' valign='top' class='yellow-txt' style='color:#ffffff;'>Customer Support</td></tr>");
                sb.Append("<tr><td class='white-txt'><a href='mailto:support@gopalancinemas.com' class='white-txt' style='color:#ffffff;'>support@gopalancinemas.com</a></td></tr>");
                sb.Append("</table></td>");
                sb.Append("</tr></table></td></tr></table>");        
                sb.Append("</body></html>");        

                string mailStatus = common.SendNetMail(mailFrom, mailTo, mailCC, sb.ToString(), Subject, "");
                book.updateMAILStatusbybookingdetailid(true, int.Parse(Session["BookingDetailID"].ToString()));
                string result = "";
                WebRequest request = null;
                HttpWebResponse response = null;
                try
                {
                    String sendToPhoneNumber = "91" + dtBookingInfo.Rows[0]["Mobile"].ToString();
                    String userid = "2000051858";
                    String passwd = "gopalan123";
                    string strcname = "";
                    if (dtBookingInfo.Rows[0]["Cinema_str"].ToString() == "1000")
                    {
                        strcname = "Innovation Mall";
                    }
                    if (dtBookingInfo.Rows[0]["Cinema_str"].ToString() == "1001")
                    {
                        strcname = "Legacy Mall";
                    }
                    if (dtBookingInfo.Rows[0]["Cinema_str"].ToString() == "1002")
                    {
                        strcname = "Arcade Mall";
                    }
                    string filmnamesms = dtBookingInfo.Rows[0]["Film_strTitle"].ToString();
                    if (filmnamesms.Length > 20)
                    {
                        filmnamesms = filmnamesms.Substring(0, 19);
                    }
                    string seats = dtBookingInfo.Rows[0]["SeatInfo"].ToString();
                    seats = seats.Replace("-", " -");
                    string msg = "Hi " + dtBookingInfo.Rows[0]["LastName"].ToString() + ", Booking ID: " + dtBookingInfo.Rows[0]["BookingID"].ToString() + ". Seats: " + seats + " for " + filmnamesms + ": on " + String.Format("{0:dd MMMM yyyy}", sddt) + "@" + String.Format("{0:HH:mm tt}", shdt) + " at Gopalan Cinemas " + strcname + ". Please carry your CC/DC Card which was used for booking tickets.";
                    String url = "http://enterprise.smsgupshup.com/GatewayAPI/rest?method=sendMessage&send_to=" + sendToPhoneNumber + "&msg=" + msg + "&userid=" + userid + "&password=" + passwd + "&v=1.1&msg_type=TEXT&auth_scheme=PLAIN";
                    request = WebRequest.Create(url);
                    //in case u work behind proxy, uncomment the commented code and provide correct details
                    /*WebProxy proxy = new WebProxy("http://proxy:80/",true);
                    proxy.Credentials = new 
                    NetworkCredential("userId","password", "Domain");
                    request.Proxy = proxy;*/
                    // Send the 'HttpWebRequest' and wait for response.
                    response = (HttpWebResponse)request.GetResponse();
                    Stream stream = response.GetResponseStream();
                    Encoding ec = System.Text.Encoding.GetEncoding("utf-8");
                    StreamReader reader = new System.IO.StreamReader(stream, ec);
                    result = reader.ReadToEnd();
                    string[] strArrErr = result.Split('|');
                    if (strArrErr[0].ToString().Trim() == "success")
                    {
                        book.updateSMSStatusbybookingdetailid(true, int.Parse(Session["BookingDetailID"].ToString()));
                    }
                    else
                    {
                        Exception err = new Exception(result);
                        InfluxRepository.ErrorLog.ErrorLog.LogErrorToLogFile(err, "Gopalan Cinemas");
                    }
                    reader.Close();
                    stream.Close();
                }
                catch (Exception exp)
                {
                    InfluxRepository.ErrorLog.ErrorLog.LogErrorToLogFile(exp, "Gopalan Cinemas");
                }
                finally
                {
                    if (response != null)
                        response.Close();
                }
            }
            catch (Exception ex)
            {
                InfluxRepository.ErrorLog.ErrorLog.LogErrorToLogFile(ex, "Gopalan Cinemas");
            }
        }
        private void sendBadMAIL(DataTable dtBookingInfo)
        {
            try
            {
                commonfunctions common = new commonfunctions();
                string mailFrom = "support@gopalancinemas.com";
                string mailTo = "support@gopalancinemas.com";
                string mailCC = "mohan.pts@gmail.com";
                string htmlBody = "A bad transaction was identified where the customer's card was charged for " + dtBookingInfo.Rows[0]["OverAllAmount"].ToString() + " The Details are as below:<br/><br/>Email :" + dtBookingInfo.Rows[0]["Email"].ToString() + "<br/>Phone No :" + dtBookingInfo.Rows[0]["Mobile"].ToString() + "<br/>Cinema :" + dtBookingInfo.Rows[0]["Cinema_strName"].ToString() + "<br/>Booking ID :" + dtBookingInfo.Rows[0]["BookingID"].ToString() + "<br/> Film :" + dtBookingInfo.Rows[0]["Film_strTitle"].ToString() + "<br/>  Show Time :" + dtBookingInfo.Rows[0]["ShowDate"].ToString() + " @ " + dtBookingInfo.Rows[0]["ShowTime"].ToString() + " <br/>Seat Alloted :" + dtBookingInfo.Rows[0]["NoofSeat"].ToString() + "<br/>Total Amount :" + dtBookingInfo.Rows[0]["OverAllAmount"].ToString();
                string Subject = "Bad Transaction - " + Convert.ToDateTime(dtBookingInfo.Rows[0]["ShowDate"]).ToString("dd-MM-yyyy") + " @ " + Convert.ToDateTime(dtBookingInfo.Rows[0]["ShowDate"]).ToShortTimeString();
                string mailStatus = common.SendNetMail(mailFrom, mailTo, mailCC, htmlBody, Subject, "");
                string strBadMail = "<html><head><title></title></head><body><div style='font: normal 16px Calibri'>Dear " + dtBookingInfo.Rows[0]["LastName"].ToString() + ",<br /><br />Thank you for using Gopalancinemas  website for online ticket booking services. Refund instruction for amount of Rs: " + dtBookingInfo.Rows[0]["OverAllAmount"].ToString() + "/- towards the refund due for which the Unsuccessful transaction, Booking ID is : " + dtBookingInfo.Rows[0]["BookingID"].ToString() + "  been issued to electronic payment gateway on " + DateTime.Now.ToString("dd-MM-yyyy") + ". Same shall appear in your account within two to three working days. You are requested to please check with concerned bank regarding the same. <br /><br />We regret the delay if any, in crediting back the refunds.<br />We solicit your continued patronage to our services<br /><br />Regards,<br />Gopalan Web Team</div></body></html>";
                common.SendNetMail(mailFrom, dtBookingInfo.Rows[0]["Email"].ToString(), "support@gopalancinemas.com,muniraju@gopalancinemas.com,mohan.pitchai@influx.co.in", strBadMail, "Gopalan Cinemas - Transaction Error", "");
            }
            catch (Exception ex)
            {
                InfluxRepository.ErrorLog.ErrorLog.LogErrorToLogFile(ex, "Gopalan Cinemas");
            }
        }
        private void updateRefundStatusbybookingdetailid(bool refundstatus, int bdid)
        {
            try
            {
                book.updateRefundStatusbybookingdetailid(refundstatus, bdid);
            }
            catch (Exception ex)
            {
                InfluxRepository.ErrorLog.ErrorLog.LogErrorToLogFile(ex, "Gopalan Cinemas");
            }
        }
        private void updatekioskbybookingdetailid(string kioskid, int bdid)
        {
            try
            {
                book.updatekioskbybookingdetailid(kioskid, bdid);
            }
            catch (Exception ex)
            {
                InfluxRepository.ErrorLog.ErrorLog.LogErrorToLogFile(ex, "Gopalan Cinemas");
            }
        }
    }
}