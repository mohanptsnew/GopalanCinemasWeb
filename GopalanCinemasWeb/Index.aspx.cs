using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;
using GopalanCinemasBL;


namespace GopalanCinemasWeb
{
    public partial class Index : System.Web.UI.Page
    {
        MovieBL mbl = new MovieBL();
        Common cd = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadNowShowingMovies();
                LatestNews();
                hddShowPopup.Value = "true";
            }
            else
            {
                hddShowPopup.Value = "false";
            }
        }

        private void LatestNews()
        {
            DataTable dtLatestNews = mbl.GetLatestNewsList("one");
            if (dtLatestNews.Rows.Count > 0)
            {
                StringBuilder sbNews = new StringBuilder();
                sbNews.Append("<div class='tl-bx'><h1><img src='../images/latestNews_tl.gif' /></h1><a href='#'></a></div>");
                sbNews.Append("<dfn><img src='../images/latestNews_div.gif' /></dfn>");
                sbNews.Append("<div class='txt-bx'><h2>"+ Convert.ToDateTime(dtLatestNews.Rows[0]["NewsDate"]).ToString("dd, MMM yyyy") +"</h2><p>"+ dtLatestNews.Rows[0]["ShortDesc"] +"</p><p><a href='#'></a></p></div>");
                dvLatestNews.InnerHtml = sbNews.ToString();
            }
        }
        private void LoadNowShowingMovies()
        {
            DataTable dtNowShowingMovies = mbl.GetNowShowingMoviesList();
            if (dtNowShowingMovies.Rows.Count > 0)
            {
                StringBuilder sbNowShowing = new StringBuilder();
                for (int i = 0; i < dtNowShowingMovies.Rows.Count; i++)
                {
                    if (i <= 2)
                    {
                        if (File.Exists(Server.MapPath("images/movies/" + dtNowShowingMovies.Rows[i]["Film_strCode"].ToString() + ".jpg")))
                        {
                            sbNowShowing.Append("<div class='img'><img src='images/movies/" + dtNowShowingMovies.Rows[i]["Film_strCode"].ToString() + ".jpg' /><p>" + dtNowShowingMovies.Rows[i]["Film_strTitle"].ToString() + "</p></div>");
                        }
                        else
                        {
                            sbNowShowing.Append("<div class='img'><img src='images/movies/noimage-home.jpg' /><p>" + dtNowShowingMovies.Rows[i]["Film_strTitle"].ToString() + "</p></div>");
                        }
                    }
                }
                dvNowShowing.InnerHtml = sbNowShowing.ToString();
            }
            else
            {

            }
        }

        protected void imgNewsGo_Click(object sender, ImageClickEventArgs e)
        {
            if (txtEmailID.Text != "")
            {
                mbl.InsertNewsletter(txtEmailID.Text);
            }
        }
    }
}