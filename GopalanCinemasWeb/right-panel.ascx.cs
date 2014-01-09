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
    public partial class right_panel1 : System.Web.UI.UserControl
    {
        int intCount = 0;
        DataTable dtShowDate;
        CinemaBL cbl = new CinemaBL();
        MovieBL mbl = new MovieBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["SessBookInfo"] != null)
                {
                    string[] strBindDetails = (string[])Session["SessBookInfo"];
                    LoadCinema(false);
                    LoadMovies(strBindDetails[0], false);
                    LoadShowDate(strBindDetails[0], strBindDetails[1], false);
                    LoadShowTime(strBindDetails[0], strBindDetails[1], strBindDetails[2],false);
                    LoadSeats();
                    ddlChinema.SelectedValue = strBindDetails[0];
                    ddlMovie.SelectedValue = strBindDetails[1];
                    ddlDate.SelectedValue = strBindDetails[2];
                    ddlShowTime.SelectedValue = strBindDetails[3];
                    ddlSeats.SelectedValue = strBindDetails[4];
                }
                else
                {
                    LoadCinema(true);
                }
            }
        }
        private void LoadCinema(bool p)
        {
            DataTable dtCinema =  cbl.GetCinemaList();
            if (dtCinema.Rows.Count > 0)
            {
                //ddlChinema.DataSource = dtCinema;
                //ddlChinema.DataBind();
                ddlChinema.Items.Clear();
                ddlChinema.Items.Add(new ListItem("Select Cinema", "0"));
                BindData(ddlChinema, dtCinema, "Cinema_strName", "Cinema_strID");
                ddlChinema.SelectedValue = "0";
                ddlMovie.Items.Clear();
                ddlDate.Items.Clear();
                ddlShowTime.Items.Clear();
                ddlSeats.Items.Clear();
                if (p == true)
                {
                    EmptyDropDown(ddlMovie, "Movie");
                    EmptyDropDown(ddlDate, "Date");
                    EmptyDropDown(ddlShowTime, "Show Time");
                    EmptyDropDown(ddlSeats, "Seat(s)");
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

        protected void ddlChinema_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChinema.SelectedValue != "0")
            {
                if (ddlChinema.SelectedValue == "1004")
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "show_movie();", true);
                }
                LoadMovies(ddlChinema.SelectedValue, true);
            }
        }

        private void LoadMovies(string p, bool p1)
        {
            DataTable dtMovie = mbl.GetMovieList(p);
            if (dtMovie.Rows.Count > 0)
            {
                //ddlMovie.DataSource = dtMovie;
                //ddlMovie.DataBind();
                ddlMovie.Items.Clear(); 
                ddlMovie.Items.Add(new ListItem("Select Movie", "0"));
                BindData(ddlMovie, dtMovie, "Film_strTitle", "Film_strCode");
                ddlMovie.SelectedValue = "0";
                ddlDate.Items.Clear();
                ddlShowTime.Items.Clear();
                ddlSeats.Items.Clear();
                if (p1 == true)
                {
                    EmptyDropDown(ddlDate, "Date");
                    EmptyDropDown(ddlShowTime, "Show Time");
                    EmptyDropDown(ddlSeats, "Seat(s)");
                }
            }
        }

        protected void ddlMovie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChinema.SelectedValue != "0" && ddlMovie.SelectedValue != "0")
            {
                LoadShowDate(ddlChinema.SelectedValue, ddlMovie.SelectedValue, true);
            }
        }

        private void LoadShowDate(string p, string p2, bool p3)
        {
            dtShowDate = mbl.GetShowDateList(p, p2, 0);
            if (dtShowDate.Rows.Count > 0)
            {
                //ddlDate.DataSource = dtShowDate;
                //ddlDate.DataBind();
                ddlDate.Items.Clear();
                ddlDate.Items.Add(new ListItem("Select Date", "0"));
                BindData(ddlDate, dtShowDate,"Session_dtmRealShow2", "Session_dtmRealShow");
                ddlDate.SelectedValue = "0";
                ddlShowTime.Items.Clear();
                ddlSeats.Items.Clear();
                if (p3 == true)
                {
                    EmptyDropDown(ddlShowTime, "Show Time");
                    EmptyDropDown(ddlSeats, "Seat(s)");
                }
            }
        }

        protected void ddlDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChinema.SelectedValue != "0" && ddlMovie.SelectedValue != "0" && ddlDate.SelectedValue != "0")
            {
                LoadShowTime(ddlChinema.SelectedValue, ddlMovie.SelectedValue, ddlDate.SelectedValue, true);
            }
        }

        private void LoadShowTime(string p, string p2, string p3, bool p4)
        {
            DataTable dtShowTime = mbl.GetShowTimeList(p, p2, 0, Convert.ToDateTime(p3));

            if (dtShowTime.Rows.Count > 0)
            {
                string strDay;
                ddlShowTime.Items.Clear();
                ddlShowTime.Items.Add(new ListItem("Select Show Time", "0"));
                string strDate = (Convert.ToDateTime(p3)).ToString("yyyy-MM-dd");
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
                        ddlShowTime.Items.Add(new ListItem(strDay, dtShowTime.Rows[i]["Session_lngSessionId"].ToString()));
                    }
                }
                ddlShowTime.SelectedValue = "0";
                ddlSeats.Items.Clear();
                if (p4 == true)
                {
                    EmptyDropDown(ddlSeats, "Seat(s)");
                }
            }
        }

        protected void ddlShowTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChinema.SelectedValue != "0" && ddlMovie.SelectedValue != "0" && ddlDate.SelectedValue != "0" && ddlShowTime.SelectedValue != "0")
            {
                LoadSeats();
            }
        }

        private void LoadSeats()
        {
            ddlSeats.Items.Clear();
            ddlSeats.Items.Add(new ListItem("Select Seat(s)", "0"));
            for (int i = 1; i < 10; i++)
            {
                ddlSeats.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlSeats.SelectedValue = "0";
        }

        protected void imgSubmit_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlChinema.SelectedValue != "0" && ddlMovie.SelectedValue != "0" && ddlDate.SelectedValue != "0" && ddlShowTime.SelectedValue != "0" && ddlSeats.SelectedValue != "0")
            {
                string[] strBookingInfo = new string[] { ddlChinema.SelectedValue, ddlMovie.SelectedValue, ddlDate.SelectedValue, ddlShowTime.SelectedValue, ddlSeats.SelectedValue, ddlShowTime.SelectedItem.Text, ddlChinema.SelectedItem.Text, ddlMovie.SelectedItem.Text };
                Session["SessBookInfo"] = strBookingInfo;
                Response.Redirect("seat-selection.aspx");
            }
        }
    }
}