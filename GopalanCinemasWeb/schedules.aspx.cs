using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using GopalanCinemasBL;
namespace GopalanCinemasWeb
{
    public partial class schedules : System.Web.UI.Page
    {
        int intCount = 0;
        CinemaBL cbl = new CinemaBL();
        MovieBL mbl = new MovieBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadCinema();
                ddlDates.Items.Clear();
                EmptyDropDown(ddlDates, "Date");
            }
            if (Page.IsPostBack)
            {
                string s = Request.Form["__EVENTTARGET"];
                if (s.Contains("lnkDynamicST"))
                {
                    StoreSession(s);
                }
            }
        }

        private void LoadCinema()
        {
            DataTable dtCinema = cbl.GetCinemaList();
            if (dtCinema.Rows.Count > 0)
            {
                //ddlChinemaName.DataSource = dtCinema;
                //ddlChinemaName.DataBind();
                ddlChinemaName.Items.Clear();
                ddlChinemaName.Items.Add(new ListItem("Select Cinema", "0"));
                BindData(ddlChinemaName, dtCinema, "Cinema_strName", "Cinema_strID");
                ddlChinemaName.SelectedValue = "0";
            }
        }
        private void BindData(DropDownList ddlValue, DataTable dtValue, string p, string p_2)
        {
            for (int z = 0; z < dtValue.Rows.Count; z++)
            {
                ddlValue.Items.Add(new ListItem(dtValue.Rows[z][p].ToString(), dtValue.Rows[z][p_2].ToString()));
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if(ddlChinemaName.SelectedValue != "" && ddlDates.SelectedValue != "0" && ddlSeatsNo.SelectedValue != "0")
            {
                DataTable dtMoviesList = mbl.GetAllMoviesByCinemaIDDate(ddlChinemaName.SelectedValue, 1, Convert.ToDateTime(ddlDates.SelectedValue));
                if(dtMoviesList.Rows.Count > 0)
                {
                    StringBuilder sbFilms = new StringBuilder();
                    string strFilmCode = "";
                    string strDay = "";
                    for(int i=0; i < dtMoviesList.Rows.Count; i++)
                    {
                        if (strFilmCode != dtMoviesList.Rows[i]["Film_strCode"].ToString())
                        {
                            if (i != 0)
                            {
                                pnlFilms.Controls.Add(new LiteralControl("</div></div>"));
                            }
                            pnlFilms.Controls.Add(new LiteralControl("<div class='sch_blk-in3'><font>" + dtMoviesList.Rows[i]["Film_strTitle"].ToString() + "</font><div class='time'>"));
                        }
                        strDay = Convert.ToDateTime(dtMoviesList.Rows[i]["Session_dtmRealShow"]).ToShortTimeString();
                        string strDate = (Convert.ToDateTime(ddlDates.SelectedValue)).ToString("yyyy-MM-dd");
                        DateTime dtFirstDate = Convert.ToDateTime(Convert.ToDateTime(dtMoviesList.Rows[i]["Session_dtmRealShow"].ToString()).ToShortTimeString());
                        DateTime dtSecondDate;
                        dtSecondDate = DateTime.Now;
                        //Print out the date and time
                        int intCompare=1;
                        if(dtSecondDate.ToString("yyyy-MM-dd") == strDate)
                        {
                            intCompare = dtFirstDate.CompareTo(dtSecondDate);
                        }
                        if (intCompare > 0)
                        {

                            if (dtMoviesList.Rows[i]["Session_intSeatsAvail"].ToString() == "0")
                            {
                                pnlFilms.Controls.Add(new LiteralControl("<span>" + strDay + "</span>"));
                            }
                            else
                            {
                                LinkButton lnkShowTime = new LinkButton();
                                lnkShowTime.ID = "lnkDynamicST#" + i.ToString() + "#" + ddlChinemaName.SelectedValue + "#" + dtMoviesList.Rows[i]["Film_strCode"].ToString() + "#" + ddlDates.SelectedValue + "#" + dtMoviesList.Rows[i]["Session_lngSessionId"].ToString() + "#" + ddlSeatsNo.SelectedValue + "#" + strDay + "#" + ddlChinemaName.SelectedItem.Text + "#" + dtMoviesList.Rows[i]["Film_strTitle"].ToString();
                                lnkShowTime.Text = strDay;
                                pnlFilms.Controls.Add(lnkShowTime);
                            }
                        }
                        else
                        {
                            pnlFilms.Controls.Add(new LiteralControl("<dfn>" + strDay + "</dfn>"));
                        }
                        Label lblShowTime = new Label();
                        lblShowTime.ID = lblShowTime + i.ToString();
                        lblShowTime.Text = "";
                        lblShowTime.Width = 10;
                        pnlFilms.Controls.Add(lblShowTime);
//                        pnlFilms.Controls.Add(new LiteralControl("<label style='width:10px; float:left;'>&nbsp;</label>"));
                        if (i == (dtMoviesList.Rows.Count - 1))
                        {
                            pnlFilms.Controls.Add(new LiteralControl("</div></div>"));
                        }
                        strFilmCode = dtMoviesList.Rows[i]["Film_strCode"].ToString();
                    }
                }
            }
        }
        private void StoreSession(string s)
        {
            string[] strInfo = s.Split('#');
            string[] strBookingInfo = new string[] { strInfo[2], strInfo[3], strInfo[4], strInfo[5], strInfo[6], strInfo[7], strInfo[8], strInfo[9] };
            Session["SessBookInfo"] = strBookingInfo;
            Response.Redirect("seat-selection.aspx");
        }
        protected void ddlChinemaName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlChinemaName.SelectedValue != "0")
            {
                DataTable dtDates = mbl.GetShowDateByCinemaList(ddlChinemaName.SelectedValue, 1);
                if (dtDates.Rows.Count > 0)
                {
                    //ddlDates.DataSource = dtDates;
                    //ddlDates.DataBind();
                    ddlDates.Items.Clear();
                    ddlDates.Items.Add(new ListItem("Select Date", "0"));
                    BindData(ddlDates, dtDates, "Session_dtmRealShow2", "Session_dtmRealShow");
                    ddlDates.SelectedValue = "0";
                }
                else
                {
                    ddlDates.Items.Clear();
                    EmptyDropDown(ddlDates, "Date");
                }
            }
            else
            {
                ddlDates.Items.Clear();
                EmptyDropDown(ddlDates, "Date");
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
    }
}