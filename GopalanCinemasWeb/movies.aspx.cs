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
    public partial class movies : System.Web.UI.Page
    {
        int intCount = 0;
        MovieBL mbl = new MovieBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadNowShowingMovies();
                LoadComingSoonMovies();
            }
        }

        private void LoadNowShowingMovies()
        {
            DataTable dtNowShowingMovies = mbl.GetNowShowingMoviesList();
            if (dtNowShowingMovies.Rows.Count > 0)
            {
                gvNowShowing.DataSource = dtNowShowingMovies;
                gvNowShowing.DataBind();
            }
            else
            {

            }
        }

        private void LoadComingSoonMovies()
        {
            DataTable dtComingSoonMovies = mbl.GetComingSoonMoviesList();
            if (dtComingSoonMovies.Rows.Count > 0)
            {
                gvComingSoon.DataSource = dtComingSoonMovies;
                gvComingSoon.DataBind();
            }
            else
            {

            }
        }

        protected void gvNowShowing_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvNowShowing.PageIndex = e.NewPageIndex;
            LoadNowShowingMovies();
            string strScript = "<script language='javascript'>fndisplaydata('now','n');</script>";
            Page.RegisterStartupScript("myjava1", strScript);
        }

        protected void gvComingSoon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvComingSoon.PageIndex = e.NewPageIndex;
            LoadComingSoonMovies();
            string strScript1 = "<script language='javascript'>fndisplaydata('coming','c');</script>";
            Page.RegisterStartupScript("myjava2", strScript1);
        }

        protected void gvNowShowing_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //HiddenField lblNSFilmCode = (HiddenField)gvNowShowing.Rows[e.RowIndex].FindControl("hddNSFilmCode");
            //if (lblNSFilmCode != null)
            //{
            //    Session["SessSynopsis"] = "now-showing#" + lblNSFilmCode.Value;
            //    Response.Redirect("synopsis.aspx");
            //}
        }
        protected void gvComingSoon_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //HiddenField lblCSFilmCode = (HiddenField)gvComingSoon.Rows[e.RowIndex].FindControl("hddCSFilmCode");
            //if (lblCSFilmCode != null)
            //{
            //    Session["SessSynopsis"] = "coming-soon#" + lblCSFilmCode.Value;
            //    Response.Redirect("synopsis.aspx");
            //}
        }

        protected void gvNowShowing_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "BookNow")
            {
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hddNSFilmCode = (HiddenField)row.FindControl("hddNSFilmCode");
                HiddenField hddNSFilmTitle = (HiddenField)row.FindControl("hddNSFilmTitle");
                if (hddNSFilmCode.Value != null)
                {
                    hFilmName.InnerHtml = hddNSFilmTitle.Value;
                    hddGetFilmCode.Value = hddNSFilmCode.Value;
                    DataTable dtCinema = mbl.GetChinemaByFilmID(hddNSFilmCode.Value);
                    if (dtCinema.Rows.Count > 0)
                    {
                        //ddlMoviesChinema.DataSource = dtCinema;
                        //ddlMoviesChinema.DataBind();
                        ddlMoviesChinema.Items.Clear();
                        ddlMoviesChinema.Items.Add(new ListItem("Select Cinema", "0"));
                        BindData(ddlMoviesChinema, dtCinema, "Cinema_strName", "Cinema_strID");
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
        }
        private void BindData(DropDownList ddlValue, DataTable dtValue, string p, string p_2)
        {
            for (int z = 0; z < dtValue.Rows.Count; z++)
            {
                ddlValue.Items.Add(new ListItem(dtValue.Rows[z][p].ToString(), dtValue.Rows[z][p_2].ToString()));
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
            if (ddlMoviesChinema.SelectedValue != "0" && hddGetFilmCode.Value != "")
            {
                DataTable dtShowDate = mbl.GetShowDateList(ddlMoviesChinema.SelectedValue, hddGetFilmCode.Value, 0);
                if (dtShowDate.Rows.Count > 0)
                {
                    //ddlMoviesDate.DataSource = dtShowDate;
                    //ddlMoviesDate.DataBind();
                    ddlMoviesDate.Items.Clear();
                    ddlMoviesDate.Items.Add(new ListItem("Select Date", "0"));
                    BindData(ddlMoviesDate, dtShowDate, "Session_dtmRealShow2", "Session_dtmRealShow");
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
            if (ddlMoviesChinema.SelectedValue != "0" && hddGetFilmCode.Value != "" && ddlMoviesDate.SelectedValue != "0")
            {
                DataTable dtShowTime = mbl.GetShowTimeList(ddlMoviesChinema.SelectedValue, hddGetFilmCode.Value, 0, Convert.ToDateTime(ddlMoviesDate.SelectedValue));
                if (dtShowTime.Rows.Count > 0)
                {
                    string strDay;
                    ddlMoviesShowTime.Items.Clear();
                    ddlMoviesShowTime.Items.Add(new ListItem("Select Show Time", "0"));
                    string strDate = (Convert.ToDateTime(ddlMoviesDate.SelectedValue)).ToString("yyyy-MM-dd");
                    DateTime dtSecondDate;
                    dtSecondDate = DateTime.Now;
                    int intCompare;
                    for (int i = 0; i < dtShowTime.Rows.Count; i++)
                    {
                        DateTime dtFirstDate = Convert.ToDateTime(Convert.ToDateTime(dtShowTime.Rows[i]["Session_dtmRealShow"].ToString()).ToShortTimeString());
                        //Print out the date and time
                        intCompare = 1;
                        if(dtSecondDate.ToString("yyyy-MM-dd") == strDate)
                        {
                            intCompare = dtFirstDate.CompareTo(dtSecondDate);
                        }
                        if (intCompare > 0)
                        {
                            strDay = Convert.ToDateTime(dtShowTime.Rows[i]["Session_dtmRealShow"]).ToShortTimeString();
                            ddlMoviesShowTime.Items.Add(new ListItem(strDay, dtShowTime.Rows[i]["Session_lngSessionId"].ToString()));
                        }
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
            if (ddlMoviesChinema.SelectedValue != "0" && hddGetFilmCode.Value != "" && ddlMoviesDate.SelectedValue != "0" && ddlMoviesShowTime.SelectedValue != "0")
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
            if (ddlMoviesChinema.SelectedValue != "0" && hddGetFilmCode.Value != "" && ddlMoviesDate.SelectedValue != "0" && ddlMoviesShowTime.SelectedValue != "0" && ddlMoviesSeats.SelectedValue != "0")
            {
                string[] strBookingInfo = new string[] { ddlMoviesChinema.SelectedValue, hddGetFilmCode.Value, ddlMoviesDate.SelectedValue, ddlMoviesShowTime.SelectedValue, ddlMoviesSeats.SelectedValue, ddlMoviesShowTime.SelectedItem.Text, ddlMoviesChinema.SelectedItem.Text, hFilmName.InnerHtml };
                Session["SessBookInfo"] = strBookingInfo;
                Response.Redirect("seat-selection.aspx");
            }
        }
    }
}