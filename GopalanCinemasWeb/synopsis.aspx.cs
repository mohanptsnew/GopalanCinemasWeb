using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GopalanCinemasBL;

namespace GopalanCinemasWeb
{
    public partial class synopsis : System.Web.UI.Page
    {
        string[] strSynopsis;
        int intCount = 0;
        MovieBL mbl = new MovieBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Request.ServerVariables["HTTP_REFERER"].ToString().Contains("movies.aspx"))
                {
                    Session["SessSynopsis"] = null;
                }
                if (Session["SessSynopsis"] != null)
                {
                    strSynopsis = Session["SessSynopsis"].ToString().Split('#');
                    hddGetFilmCode.Value = strSynopsis[1].ToString();
                    if (strSynopsis[0].ToString() == "coming-soon")
                    {
                        imgSynBookNow.Visible = false;
                    }
                    else
                    {
                        imgSynBookNow.Visible = true;
                    }
                    string strScript1 = "<script language='javascript'>fndisplaydata('"+ strSynopsis[0] +"');</script>";
                    Page.RegisterStartupScript("myjava2", strScript1);
                    LoadFields(strSynopsis[1]);
                }
            }
        }

        private void LoadFields(string p)
        {
            DataTable dtFilmInfo = mbl.GetFilmDetailsByFilmID(p);
            if (dtFilmInfo.Rows.Count > 0)
            {
                iSynImageName.Src = "images/movies/common/"+ dtFilmInfo.Rows[0]["ImageName"].ToString();
                hSynFilmName.InnerHtml = dtFilmInfo.Rows[0]["Film_strTitle"].ToString();
                synopsisInfo.InnerHtml = dtFilmInfo.Rows[0]["Synopsis"].ToString();
                trailer.InnerHtml = dtFilmInfo.Rows[0]["Trailor"].ToString();
            }
        }

        protected void imgSynBookNow_Click(object sender, ImageClickEventArgs e)
        {
            if (hddGetFilmCode.Value.ToString() != "")
            {
                DataTable dtCinema = mbl.GetChinemaByFilmID(hddGetFilmCode.Value.ToString());
                if (dtCinema.Rows.Count > 0)
                {
                    ddlMoviesChinema.DataSource = dtCinema;
                    ddlMoviesChinema.DataBind();
                    ddlMoviesChinema.Items.Add(new ListItem("Select Cinema", "0"));
                    ddlMoviesChinema.SelectedValue = "0";
                    ddlMoviesDate.Items.Clear();
                    ddlMoviesShowTime.Items.Clear();
                    ddlMoviesSeats.Items.Clear();
                    EmptyDropDown(ddlMoviesDate, "Date");
                    EmptyDropDown(ddlMoviesShowTime, "Show Time");
                    EmptyDropDown(ddlMoviesSeats, "Seat(s)");
                }
                string strScript2 = "<script language='javascript'>displaycity();</script>";
                Page.RegisterStartupScript("myjava2", strScript2);
            }
        }
        private void EmptyDropDown(DropDownList ddlCommon, string s)
        {
            if (intCount == 0)
            {
                ddlCommon.Items.Add(new ListItem("Select " + s + "", "0"));
                ddlCommon.SelectedValue = "0";
            }
        }

        protected void ddlMoviesChinema_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMoviesChinema.SelectedValue != "0" && hddGetFilmCode.Value.ToString() != "")
            {
                DataTable dtShowDate = mbl.GetShowDateList(ddlMoviesChinema.SelectedValue, hddGetFilmCode.Value.ToString(), 0);
                if (dtShowDate.Rows.Count > 0)
                {
                    ddlMoviesDate.DataSource = dtShowDate;
                    ddlMoviesDate.DataBind();
                    ddlMoviesDate.Items.Add(new ListItem("Select Date", "0"));
                    ddlMoviesDate.SelectedValue = "0";
                    ddlMoviesShowTime.Items.Clear();
                    ddlMoviesSeats.Items.Clear();
                    EmptyDropDown(ddlMoviesShowTime, "Show Time");
                    EmptyDropDown(ddlMoviesSeats, "Seat(s)");
                }
                string strScript3 = "<script language='javascript'>displaycity();</script>";
                Page.RegisterStartupScript("myjava3", strScript3);
            }
        }
        protected void ddlMoviesDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMoviesChinema.SelectedValue != "0" && hddGetFilmCode.Value.ToString() != "" && ddlMoviesDate.SelectedValue != "0")
            {
                DataTable dtShowTime = mbl.GetShowTimeList(ddlMoviesChinema.SelectedValue, hddGetFilmCode.Value.ToString(), 0, Convert.ToDateTime(ddlMoviesDate.SelectedValue));
                if (dtShowTime.Rows.Count > 0)
                {
                    string strDay;
                    ddlMoviesShowTime.Items.Clear();
                    ddlMoviesShowTime.Items.Add(new ListItem("Select Show Time", "0"));
                    for (int i = 0; i < dtShowTime.Rows.Count; i++)
                    {
                        strDay = Convert.ToDateTime(dtShowTime.Rows[i]["Session_dtmRealShow"]).ToShortTimeString();
                        ddlMoviesShowTime.Items.Add(new ListItem(strDay, dtShowTime.Rows[i]["Session_lngSessionId"].ToString()));
                    }
                    ddlMoviesShowTime.SelectedValue = "0";
                    ddlMoviesSeats.Items.Clear();
                    EmptyDropDown(ddlMoviesSeats, "Seat(s)");
                }
                string strScript4 = "<script language='javascript'>displaycity();</script>";
                Page.RegisterStartupScript("myjava4", strScript4);
            }
        }

        protected void ddlMoviesShowTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMoviesChinema.SelectedValue != "0" && hddGetFilmCode.Value.ToString() != "" && ddlMoviesDate.SelectedValue != "0" && ddlMoviesShowTime.SelectedValue != "0")
            {
                ddlMoviesSeats.Items.Clear();
                ddlMoviesSeats.Items.Add(new ListItem("Select Seat(s)", "0"));
                for (int i = 1; i < 10; i++)
                {
                    ddlMoviesSeats.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddlMoviesSeats.SelectedValue = "0";
            }
            string strScript5 = "<script language='javascript'>displaycity();</script>";
            Page.RegisterStartupScript("myjava5", strScript5);
        }

        protected void btn_proceedSeat_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlMoviesChinema.SelectedValue != "0" && hddGetFilmCode.Value.ToString() != "" && ddlMoviesDate.SelectedValue != "0" && ddlMoviesShowTime.SelectedValue != "0" && ddlMoviesSeats.SelectedValue != "0")
            {
                string[] strBookingInfo = new string[] { ddlMoviesChinema.SelectedValue, hddGetFilmCode.Value.ToString(), ddlMoviesDate.SelectedValue, ddlMoviesShowTime.SelectedValue, ddlMoviesSeats.SelectedValue, ddlMoviesShowTime.SelectedItem.Text, ddlMoviesChinema.SelectedItem.Text, hSynFilmName.InnerHtml };
                Session["SessBookInfo"] = strBookingInfo;
                Response.Redirect("seat-selection.aspx");
            }
        }
    }
}