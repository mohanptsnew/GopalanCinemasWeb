using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using GopalanCinemasBL;

namespace GopalanCinemasWeb.Masters
{
    public partial class GopalanWeb : System.Web.UI.MasterPage
    {
        MovieBL mbl = new MovieBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DateTime dtfirst = Convert.ToDateTime("1:35 PM");
                DateTime dtsecond = DateTime.Now;
                int intss = dtfirst.CompareTo(dtsecond);
                LatestNews();
            }
        }
        private void LatestNews()
        {
            DataTable dtLatestNews = mbl.GetLatestNewsList("one");
            if (dtLatestNews.Rows.Count > 0)
            {
                StringBuilder sbNews = new StringBuilder();
                sbNews.Append("<h1><img src='../images/latestNews_tl-1.gif' /></h1>");
                sbNews.Append("<p>" + dtLatestNews.Rows[0]["ShortDesc"] + "</p><!--<span><a href='#'>View More</a></span>--><div class='clear'></div>");
                sbNews.Append("<dfn>" + Convert.ToDateTime(dtLatestNews.Rows[0]["NewsDate"]).ToString("dd, MMM yyyy") + "<dfn>");
                dvMasterNews.InnerHtml = sbNews.ToString();
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