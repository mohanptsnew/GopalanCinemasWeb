using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GopalanCinemasWeb
{
    public partial class Common
    {
        public string connectionStr = System.Configuration.ConfigurationManager.AppSettings["GCCon"];
        string strTransID;
        string strTransPwd;

        public string[] GetTransID(string strCinema)
        {
            strTransID = System.Configuration.ConfigurationManager.AppSettings["GCTransportalID" + strCinema];
            strTransPwd = System.Configuration.ConfigurationManager.AppSettings["GCTransportalPwd" + strCinema];
            string sdd = strTransID + "~" + strTransPwd;
            return sdd.Split('~');
        }
    }
}