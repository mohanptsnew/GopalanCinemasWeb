using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GopalanCinemasBL;
using GopalanCinemasEntities;
using System.Data;
using System.Reflection;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

namespace GopalanCinemasWeb.admin
{
    public partial class Reports : System.Web.UI.Page
    {
        booking_bal objRefund = new booking_bal();
       
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (bFromDate.Text != "" && bToDate.Text != "")
            {
                LoadGrid("Export");
            }
        }
        private void LoadGrid(string p)
        {
            string strDates = "";
            string[] strarrFDate = bFromDate.Text.Split('/');
            string strFDate = strarrFDate[2] + "/" + strarrFDate[1] + "/" + strarrFDate[0];
            string[] strarrTDate = bToDate.Text.Split('/');
            string strTDate = strarrTDate[2] + "/" + strarrTDate[1] + "/" + strarrTDate[0];


            int pgstatus = Convert.ToInt32(this.ddlPayment.SelectedValue);
            int vistastatus = Convert.ToInt32(this.ddlVista.SelectedValue);
            int transactionstatus = Convert.ToInt32(this.ddltrans.SelectedValue);

            //if (bFromDate.Text == bToDate.Text)
            //{
            //    //strDates = " and CONVERT(varchar(10),a.BookingDate, 103) = '" + bFromDate.Text + "'";
            //    strDates = " and a.BookingDate = '" + strFDate + "'";
            //}
            //else
            //{
            //    //strDates = " and CONVERT(varchar(10),a.BookingDate, 103) >= '" + bFromDate.Text + "' and CONVERT(varchar(10),a.BookingDate, 103) <= '" + bToDate.Text + "'";
            //    strDates = " and a.BookingDate >= '" + strFDate + "' and a.BookingDate <= '" + strTDate + "'";
            //}
            //strDates = " and a.BookingDate >= '" + bFromDate.Text + "' and a.BookingDate <= '" + bToDate.Text + "'";
            DataTable dtRefund = objRefund.getTransactionDetails(strFDate,strTDate,pgstatus,vistastatus,transactionstatus);
            //HttpContext context = HttpContext.Current;
            //context.Response.Clear();
            //foreach (DataColumn column in dtRefund.Columns)
            //{
            //    context.Response.Write(column.ColumnName + ";");
            //}
            //context.Response.Write(Environment.NewLine);
            //foreach (DataRow row in dtRefund.Rows)
            //{
            //    for (int i = 0; i < dtRefund.Columns.Count; i++)
            //    {
            //        context.Response.Write(row[i].ToString().Replace(";", string.Empty) + ";");
            //    }
            //    context.Response.Write(Environment.NewLine);
            //}
            if (dtRefund.Rows.Count > 0)
            {
                this.lblmsg.Visible = false;           
                this.gdRefund.Visible = true;

                if (p == "Export")
                {
                    StringBuilder sb = new StringBuilder();
                    if (dtRefund.Rows.Count > 0)
                    {
                        sb.Append("<table border='1'>");
                        string strStatus = "";
                        sb.Append("<tr><td><b>Booking ID</b></td><td><b>Email</b></td><td><b>Mobile</b></td><td><b>Show Date</b></td><td><b>Show Time</b></td><td><b>Booking Date</b></td><td><b>No of Seat(s)</b></td><td><b>Total Amount</b></td><td><b>Payment Status</b></td><td><b>Vista Status</b></td><td><b>Transaction Status</b></td></tr>");
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
                            sb.Append("<td>" + dtRefund.Rows[i]["NoofSeat"].ToString() + "</td>");
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
                else
                {
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
        protected void gdRefund_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gdRefund.PageIndex = e.NewPageIndex;
            LoadGrid("go");
        }
        protected void gdRefund_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Label lblBDID = (Label)gdRefund.Rows[e.RowIndex].FindControl("lblBookingDetailID");
            //if (lblBDID.Text != "")
            //{
            //    updateRefundStatusbybookingdetailid(true, int.Parse(lblBDID.Text));
            //    LoadGrid();
            //}
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (bFromDate.Text != "" && bToDate.Text != "")
            {
                LoadGrid("go");
            }
        }

    }
}