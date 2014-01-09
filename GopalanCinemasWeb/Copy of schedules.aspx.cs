using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace GopalanCinemasWeb
{
    public partial class schedules : System.Web.UI.Page
    {
        int intCount = 0;
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
            DataTable dtCinema = GopalanCinemasBL.CinemaBL.GetCinemaList();
            if (dtCinema.Rows.Count > 0)
            {
                ddlChinemaName.DataSource = dtCinema;
                ddlChinemaName.DataBind();
                ddlChinemaName.Items.Add(new ListItem("Select Cinema", "0"));
                ddlChinemaName.SelectedValue = "0";
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if(ddlChinemaName.SelectedValue != "" && ddlDates.SelectedValue != "0" && ddlSeatsNo.SelectedValue != "0")
            {
                DataTable dtMoviesList = GopalanCinemasBL.MovieBL.GetAllMoviesByCinemaIDDate(ddlChinemaName.SelectedValue, 1, Convert.ToDateTime(ddlDates.SelectedValue));
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
                                //sbFilms.Append("</div></div>");
                            }
                            //sbFilms.Append("<div class='sch_blk-in3'><font>" + dtMoviesList.Rows[i]["Film_strTitle"].ToString() + "</font><div class='time'>");
                            pnlFilms.Controls.Add(new LiteralControl("<div class='sch_blk-in3'><font>" + dtMoviesList.Rows[i]["Film_strTitle"].ToString() + "</font><div class='time'>"));
                        }
                        strDay = Convert.ToDateTime(dtMoviesList.Rows[i]["Session_dtmRealShow"]).ToShortTimeString();
                        if (dtMoviesList.Rows[i]["Session_intSeatsAvail"].ToString() == "0")
                        {
                            //sbFilms.Append("<dfn>");
                            //sbFilms.Append(strDay);
                            //sbFilms.Append("</dfn>");
                            pnlFilms.Controls.Add(new LiteralControl("<dfn>" + strDay + "</dfn>"));
                        }
                        else
                        {
//                            string[] strBookingInfo = new string[] { ddlChinemaName.SelectedValue, dtMoviesList.Rows[i]["Film_strCode"].ToString(), ddlDates.SelectedValue, dtMoviesList.Rows[i]["Session_lngSessionId"].ToString(), ddlSeatsNo.SelectedValue, strDay, ddlChinemaName.SelectedItem.Text, dtMoviesList.Rows[i]["Film_strTitle"].ToString() };
//                            Session["SessBookInfo"] = strBookingInfo;
                            //sbFilms.Append("<a href='seat-selection.aspx'>" + strDay + "</a>");
                            LinkButton lnkShowTime = new LinkButton();
                            lnkShowTime.ID = "lnkDynamicST#" + i.ToString() + "#" + ddlChinemaName.SelectedValue + "#" + dtMoviesList.Rows[i]["Film_strCode"].ToString() + "#" + ddlDates.SelectedValue + "#" + dtMoviesList.Rows[i]["Session_lngSessionId"].ToString() + "#" + ddlSeatsNo.SelectedValue + "#" + strDay + "#" + ddlChinemaName.SelectedItem.Text + "#" + dtMoviesList.Rows[i]["Film_strTitle"].ToString();
                            lnkShowTime.Text = strDay;
//                            lnkShowTime.Attributes.Add("onmouseover", "changeColor(this.id)");
//                            lnkShowTime.Attributes.Add("onmouseout", "changeOriginalColor(this.id)");
//                            lnkShowTime.Click += new EventHandler(this.btnShowTime_Click);
                            pnlFilms.Controls.Add(lnkShowTime);
//                            Button btnShowTime = new Button();
//                            btnShowTime.ID = "btnST" + i.ToString() + "#" + ddlChinemaName.SelectedValue + "#" + dtMoviesList.Rows[i]["Film_strCode"].ToString() + "#" + ddlDates.SelectedValue + "#" + dtMoviesList.Rows[i]["Session_lngSessionId"].ToString() + "#" + ddlSeatsNo.SelectedValue + "#" + strDay + "#" + ddlChinemaName.SelectedItem.Text + "#" + dtMoviesList.Rows[i]["Film_strTitle"].ToString();
//                            btnShowTime.Text = strDay;
//                            btnShowTime.CssClass = "time1";
//                            btnShowTime.Attributes.Add("onmouseover", "changeColor(this.id)");
//                            btnShowTime.Attributes.Add("onmouseout", "changeOriginalColor(this.id)");
//                            btnShowTime.Click += new EventHandler(this.btnShowTime_Click);
//                            pnlFilms.Controls.Add(btnShowTime);
                        }
                        //sbFilms.Append("&nbsp;&nbsp;&nbsp;");
                        pnlFilms.Controls.Add(new LiteralControl("<div style='width:10px; float:left;'>&nbsp;</div>"));
                        if (i == (dtMoviesList.Rows.Count - 1))
                        {
                            //sbFilms.Append("</div></div>");
                            pnlFilms.Controls.Add(new LiteralControl("</div></div>"));
                        }
                        strFilmCode = dtMoviesList.Rows[i]["Film_strCode"].ToString();
                    }
//                    spFilms.InnerHtml = sbFilms.ToString();
                }
            }
        }
        //protected void btnShowTime_Click(object sender, EventArgs e)
        //{
        //    Button btnNewST = (Button)sender;
        //    string[] strInfo = btnNewST.ID.ToString().Split('#');
        //    string[] strBookingInfo = new string[] { strInfo[2], strInfo[3], strInfo[4], strInfo[5], strInfo[6], strInfo[7], strInfo[8], strInfo[9] };
        //    Session["SessBookInfo"] = strBookingInfo;
        //    Response.Redirect("seat-selection.aspx");
        //}
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
                DataTable dtDates = GopalanCinemasBL.MovieBL.GetShowDateByCinemaList(ddlChinemaName.SelectedValue, 1);
                if (dtDates.Rows.Count > 0)
                {
                    ddlDates.DataSource = dtDates;
                    ddlDates.DataBind();
                    ddlDates.Items.Add(new ListItem("Select Date", "0"));
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