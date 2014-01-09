using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GopalanCinemasEntities;
using GopalanCinemasBL;

namespace GopalanCinemasWeb
{
    public partial class sendfailure : System.Web.UI.Page
    {
        string strErrorMessage;
        booking_bal book = new booking_bal();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["BookingDetailID"] != null && (Request.UrlReferrer.ToString().Contains("response.aspx") || Request.UrlReferrer.ToString().Contains("sendresponse.aspx")))
            if (Session["BookingDetailID"] != null)
            {
                updatefailuredetails();
                Response.Redirect("failure.aspx");
            }
            else
            {
                if (Session["SessUpdateId"] != null)
                {
                    strErrorMessage = Request["ResError"];
                    //book.updatePGStatusbybookingdetailid(false, int.Parse(Session["BookingDetailID"].ToString()));
                    book.updateTxnStatusbybookingdetailid(false, int.Parse(Session["SessUpdateId"].ToString()));
                    book.updateErrorStatusbybookingdetailid(strErrorMessage, int.Parse(Session["SessUpdateId"].ToString()));
                }
                Response.Redirect("failure.aspx");
            }
        }

        private void updatefailuredetails()
        {
            strErrorMessage =  Request["ResError"];
            //book.updatePGStatusbybookingdetailid(false, int.Parse(Session["BookingDetailID"].ToString()));
            book.updateTxnStatusbybookingdetailid(false, int.Parse(Session["BookingDetailID"].ToString()));
            book.updateErrorStatusbybookingdetailid(strErrorMessage, int.Parse(Session["BookingDetailID"].ToString()));
        }
    }
}