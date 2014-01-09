using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GopalanCinemasEntities;
using GopalanCinemasBL;

namespace GopalanCinemasWeb
{
    public partial class confirmation : System.Web.UI.Page
    {
        int BookingInfoID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["BookingDetailID"] != null)
            {
                DisplayInfo();
                Session["sessionPayID"] = null;
                Session["sessionTrackID"] = null;
                Session["BookingDetailID"] = null;
            }
            else
            {
                Response.Redirect("index.aspx");
            }
        }
        private void DisplayInfo()
        {
            BookingInfoID = Convert.ToInt32(Session["BookingDetailID"].ToString());
            booking_bal booking = new booking_bal();
            DataTable dtBookingInfo = booking.getBookingDetailByBDID(int.Parse(Session["BookingDetailID"].ToString()));
            if (dtBookingInfo.Rows.Count > 0)
            {
                span_uname.InnerHtml = dtBookingInfo.Rows[0]["LastName"].ToString();
                bid.InnerHtml = Session["sessionTrackID"].ToString();
                mname.InnerHtml = dtBookingInfo.Rows[0]["Film_strTitle"].ToString();
                cname.InnerHtml = dtBookingInfo.Rows[0]["Cinema_strName"].ToString();
                DateTime sddt = DateTime.Parse(dtBookingInfo.Rows[0]["ShowDate"].ToString());
                showdate.InnerHtml = String.Format("{0:dd MMMM yyyy}", sddt) + " (24-hour format)";
                DateTime shdt = DateTime.Parse(dtBookingInfo.Rows[0]["ShowTime"].ToString());
                showtime.InnerHtml = String.Format("{0:HH:mm}", shdt);
                totalseat.InnerHtml = dtBookingInfo.Rows[0]["NoofSeat"].ToString();
                seatinfo.InnerHtml = dtBookingInfo.Rows[0]["SeatInfo"].ToString();
                kioskid.InnerHtml = dtBookingInfo.Rows[0]["KioskID"].ToString();
                decimal dclTotalAmount = Convert.ToDecimal(dtBookingInfo.Rows[0]["OverAllAmount"].ToString());
                dclTotalAmount = Math.Round(dclTotalAmount, 2);
                totalamount.InnerHtml = dclTotalAmount.ToString();
                totalamount1.InnerHtml = dclTotalAmount.ToString();
                //if (int.Parse(dtBookingInfo.Rows[0]["NoofSeat"].ToString()) == 2)
                //{
                //    dvOffer.InnerHtml = "You are eligible for small pop corn free of cost.";
                //}
                //if (int.Parse(dtBookingInfo.Rows[0]["NoofSeat"].ToString()) >= 3 && int.Parse(dtBookingInfo.Rows[0]["NoofSeat"].ToString()) <= 5)
                //{
                //    dvOffer.InnerHtml = "You are eligible for Small pepsi combo(small pepsi + Small popcorn) free of cost.";
                //}
                //if (int.Parse(dtBookingInfo.Rows[0]["NoofSeat"].ToString()) >= 6)
                //{
                //    dvOffer.InnerHtml = "You are eligible for combo coupon (Tub popcorn & 2 small pepsi) free of cost.";
                //}
                dvOffer.InnerHtml = "";
                //spMsg.InnerHtml = "You are entitled for " + dtBookingInfo.Rows[0]["NoofSeat"].ToString() + " free small popcorn(s). Please take printout of this booking confirmation page to receive this offer.";
            }
        }
    }
}