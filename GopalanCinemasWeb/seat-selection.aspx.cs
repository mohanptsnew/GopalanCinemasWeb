using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using GopalanCinemasEntities;
using GopalanCinemasBL;
using InfluxRepository;

namespace GopalanCinemasWeb
{
    public partial class seat_selection : System.Web.UI.Page
    {
        string connstr = System.Configuration.ConfigurationManager.AppSettings["GCCon"];

        public string transid, _strShowDate, _strCinema, _strMovie, _strLicense, _strTType, _strBFeeCode, _strTypeCode;
        public bool init, testremote, _blnseatlayout;
        public int _intQty;
        StringBuilder _General;
        public string showtime;
        string classname;
        public DateTime shdt;
        public string strCinemaId = string.Empty;
        int tempcount = 1;
        double intConvenience = 16.85;
        string tab = string.Empty;

        string strTransId = string.Empty;
        int seatsavail = 0;
        string msg = string.Empty;
        string[] session_typecode = null;
        string strSessionId = string.Empty;
        string strAreaCode = string.Empty;
        string strTypeCode = string.Empty;
        string strConvFee = string.Empty;
        string strTicketAmt = string.Empty;
        string strnoofseats = string.Empty;
        double tamt = 0;
        double totaltamt = 0;
        double convfee = 0;
        double totalconvfee = 0;
        double totalamount = 0;
        int cellid = 0;
        string selectedseatvalue = "";
        string ass = "";
        string selseats = "";
        string ass1 = "";
        string selseats1 = "";
        string[] strBookArr = new string[]{};
        Bigtree.VistaRemote.colAreas colA = new Bigtree.VistaRemote.colAreas();
        MovieBL mbl = new MovieBL();

        public DataTable dt;
        // public string transid, _strShowDate, _strCinema, _strMovie, _strLicense, _strTType, _strBFeeCode, _strTypeCode;

        Bigtree.VistaRemote.clsBook objbook = new Bigtree.VistaRemote.clsBook();
        string strSelseats = "", strSessId = "";

        string strLicenseType = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
//            Session["SessionLngID"] = long.Parse(Request["sessionid"].ToString());
//            Session["sessionCinemaCode"] = Request["cinema"];
            if (Session["SessBookInfo"] == null)
            {
                Response.Redirect("index.aspx");
            }
            else
            {
                strBookArr = (string[])Session["SessBookInfo"];
                hdntotalseat.Value = strBookArr[4].ToString();
                hdnSeatQty.Value = strBookArr[4].ToString();
            }
            if (!IsPostBack)
            {
                //Session["strrareacode"] = Request["areacode"];
                //Session["sessionMovieID"] = Request["movie"];
                //DateTime showdt = DateTime.Parse(Request["ShowDate"].ToString());
                //if (Request["sessionid"] != null)
                //{
                //    dt = objBooking.getShowTimeBySessionIDAndCinemaCode(Request["sessionid"], Request["cinema"]);
                //    if (dt.Rows.Count > 0)
                //    {
                //        shdt = DateTime.Parse(dt.Rows[0]["Session_dtmRealShow"].ToString());
                //        showtime = String.Format("{0:t}", shdt);
                //        Session["sessionShowTime"] = showtime;
                //        Session["showscreen"] = dt.Rows[0]["Screen_strName"].ToString();
                //    }
                //}
//                hdnselectedseats.Value = strBookArr[4].ToString();
                LoadClassAmount();
                if (Request["book"] == null)
                {
                    // showclass();
                    //listClass();
                }
            }
        }

        private void LoadClassAmount()
        {
            DataTable dtClass = mbl.GetClassList(strBookArr[0], strBookArr[1], strBookArr[3]);
            if (dtClass.Rows.Count > 0)
            {
                ddlClass.Items.Clear();
                //ddlClass.DataSource = dtClass;
                //ddlClass.DataBind();
                ddlClass.Items.Add(new ListItem("Select Class", "0"));
                BindData(ddlClass, dtClass, "TType_strDescription", "CodePrice");
                ddlClass.SelectedValue = "0";
            }
        }
        private void BindData(DropDownList ddlValue, DataTable dtValue, string p, string p_2)
        {
            for (int z = 0; z < dtValue.Rows.Count; z++)
            {
                ddlValue.Items.Add(new ListItem(dtValue.Rows[z][p].ToString(), dtValue.Rows[z][p_2].ToString()));
            }
        }
        private void listClass()
        {

            try
            {
                bookticket();
                _General = new StringBuilder();

                //hdnshowclass.Value = Session["showclass"].ToString();
                //hdnshowclass.Value = "SOFA";

                string strType = hddTypeCode.Value;
//                string strType = Request["strtype"];
                
                if (objbook.blnAddSeats(strBookArr[0].ToString(), Session["sessionTransID"].ToString(), long.Parse(strBookArr[3].ToString()), strType, short.Parse(strBookArr[4]), false))
                {
                    Session["sessionSeatInfo"] = objbook.strSeatInfo.ToString();
                    Session["sessionBookingID"] = long.Parse(objbook.intBookId.ToString());
                    decimal tff = decimal.Parse(objbook.curTicketsTotal.ToString());
                    tff = Math.Round(tff, 2);
                    Session["sessionTicketAmount"] = tff.ToString();
                    // Session["sessionBookingFee"] = objbook.curBookingFee.ToString();
                    Session["sessionFoodTotal"] = objbook.curFoodTotal.ToString();
                    // Session["sessionTotalAmount"] = objbook.curTotal.ToString();
                }
                if (objbook.blnGetSeatLayout(strBookArr[0].ToString(), Session["sessionTransID"].ToString(), long.Parse(strBookArr[3].ToString()), false, "", ""))
                {
                    colA = objbook.colA;
                    display_seatlayout();
                    //seatlayout();
                }
                else
                {
                    Response.Write("Error*" + objbook.intException + "*" + objbook.strException);
                }
            }
            catch (Exception ex)
            {
                InfluxRepository.ErrorLog.ErrorLog.LogErrorToLogFile(ex, "Gopalan Cinemas");
            }

        }

        private void bookticket()
        {
            try
            {
                //Response.Write("<BR>In Book ticket function<BR>");
                _intQty = 1;
                dt = mbl.GetLicenseType();
                if (dt.Rows.Count > 0)
                {
                    strLicenseType = dt.Rows[0]["License_strType"].ToString();
                }
                bool conbol = objbook.blnSetSettings("ConnectionString", connstr);
                bool conlin = objbook.blnSetSettings("LicenseType", strLicenseType);
                if (conbol && conlin)
                {
                    string version = objbook.strGetVersion();
                }
                else
                {
                    Response.Write(objbook.strException.ToString() + "<br>" + objbook.intException.ToString() + "<br>" + objbook.intExceptionEx.ToString());
                }
                testremote = objbook.blnTestRemote(strBookArr[0]); // To Test Remote Connection
                if (testremote)
                {
                    init = objbook.blnInitBook(strBookArr[0]); // To start booking ticket
                    if (init)
                    {
                        transid = objbook.strTransId.ToString();
                        Session["sessionTransID"] = transid;
                        hdntransid.Value = strBookArr[3].ToString();

                    }
                    else
                    {
                        Response.Write(objbook.strException.ToString() + "<br>" + objbook.intException.ToString() + "<br>" + objbook.intExceptionEx.ToString());
                    }
                }
                else
                {
                    Response.Write(objbook.strException.ToString() + "<br>" + objbook.intException.ToString() + "<br>" + objbook.intExceptionEx.ToString());
                }
            }
            catch (Exception ex)
            {
                InfluxRepository.ErrorLog.ErrorLog.LogErrorToLogFile(ex, "Gopalan Cinemas");
            }
        }

//        private void display_seatlayout()
//        {
//            int cells;
//            Bigtree.VistaRemote.clsBook objbook = new Bigtree.VistaRemote.clsBook();
//            Bigtree.VistaRemote.objArea objA = new Bigtree.VistaRemote.objArea();
//            Bigtree.VistaRemote.objRow objR = new Bigtree.VistaRemote.objRow();
//            Bigtree.VistaRemote.objSeat objS = new Bigtree.VistaRemote.objSeat();
//            Boolean blnShowAvailInArea;

//            ArrayList al = new ArrayList();
//            ArrayList cl = new ArrayList();
//            foreach (Bigtree.VistaRemote.objArea x in colA)
//            {
//                al.Add(x);
//            }
//            al.Reverse();
//            foreach (Bigtree.VistaRemote.objArea x in al)
//            {
//                objA = x;
////                if (Request["hddAreaCode"] == objA.strAreaCode.ToString())
//                if (hddAreaCode.Value == objA.strAreaCode.ToString())
//                    {
//                    //if (objA.strAreaDesc.ToString() != "NORMAL")
//                    //{
//                    tab = tab + "<tr><td class='' colspan='" + colA.intMaxSeatId().ToString() + "' style='color:#fff; font-family:arial;font-size:13px;'>" + msg + "</td></tr>";

//                    tab = tab + "<tr><td colspan='" + colA.intMaxSeatId().ToString() + "'><div class='screen'><b>" + objA.strAreaDesc.ToString().Trim().Substring(0, 1) + objA.strAreaDesc.ToString().Trim().Substring(1, objA.strAreaDesc.Length - 1).Trim().ToString().ToLower() + "</b></div></td>";
//                    //}
//                    foreach (Bigtree.VistaRemote.objRow row in objA.colRows)
//                    {
//                        cl.Add(row);
//                    }
//                    cl.Reverse();

//                    foreach (Bigtree.VistaRemote.objRow r in cl)
//                    {
//                        objR = r;
//                        string classs = "";

//                        blnShowAvailInArea = objA.blnHasCurrentOrder();


//                        tab = tab + "<tr><td class='seat_a'>" + objR.strRowPhyID.ToString() + "</td>";
//                        for (cells = short.Parse(colA.intMaxSeatId().ToString()); cells >= short.Parse(colA.intMinSeatId().ToString()); cells--)
//                        {

//                            cellid++;
//                            objS = objR.colSeats.GetSeatById(cells.ToString());
//                            if (objS.strGridSeatNum.ToString() == "0")
//                            {
//                                tab = tab + "<td  class='dark' id='td_" + cellid + "'></td>";
//                            }
//                            else
//                            {
//                                switch (objS.strSeatStatus.ToString())
//                                {
//                                    case "0": // 0-Available
//                                        if (blnShowAvailInArea)
//                                        {
//                                            classs = "gray";
//                                            selectedseatvalue = objA.strAreaCode.ToString() + "|" + objA.strAreaNum.ToString() + "|" + objR.intGridRowID.ToString() + "|" + objS.strGridSeatNum.ToString() + "|";
//                                            string selectedclass = "gray";
//                                            tab = tab + "<td align='center' class='" + selectedclass + "'  id='td_" + selectedseatvalue + "' style='cursor:pointer;' onclick=getvalue('" + selectedseatvalue + "',this.id)><font style='size:9px;'>&nbsp;</a></td>";
//                                        }
//                                        else
//                                        {
//                                            tab = tab + "<td  class='dark' id='td_" + cellid + "'></td>";
//                                        }
//                                        break;
//                                    case "3": //House /Special
//                                        tab = tab + "<td class='red' id='td_" + cellid + "'></td>";
//                                        break;
//                                    case "5": // Current Order
//                                        classs = "green";
//                                        selectedseatvalue = objA.strAreaCode.ToString() + "|" + objA.strAreaNum.ToString() + "|" + objR.intGridRowID.ToString() + "|" + objS.strGridSeatNum.ToString() + "|";
//                                        tab = tab + "<td align='center' class='" + classs + "'  id='td_" + selectedseatvalue + "' style='cursor:pointer;' onclick=getvalue('" + selectedseatvalue + "',this.id)><font style='size:9px;'></a></td>";
//                                        selseats = ass + selectedseatvalue;
//                                        ass = selseats;
//                                        selseats1 = ass1 + selectedseatvalue + "$";
//                                        ass1 = selseats1;
//                                        break;
//                                    case "1": //1-Boooked
//                                        tab = tab + "<td class='red' id='td_" + cellid + "'></td>";
//                                        break;
//                                }
//                            }


//                        }
//                        tempcount = 1;

//                        cells = 0;
//                        tab = tab + "</tr>";
//                    }


//                }

//            }

//            if (Request["seats"] == "1")
//            {
//                selectedseatvalue = selectedseatvalue;
//            }
//            else
//            {
//                selectedseatvalue = selectedseatvalue + "|";
//            }
//            string layoutht;
//            layoutht = "<table align='center'>" + tab + "</table>";
//            Session["selectedseats"] = selseats;
//            dvSeatLayout.InnerHtml = layoutht.ToString();
//            //objbook.blnSetSettings("ConnectionString", connstr);
//            //objbook.blnSetSettings("LicenseType", "WWW");
//            //strCinemaId = Session["sessionCinemaCode"].ToString();
//            //Session["sessioncinemaID"] = strCinemaId;

//            //strSelseats = "|" + Session["SesNoofSeat"].ToString() + "|" + selseats;
//            //strTransId = Session["sessionTransID"].ToString();
//            //strSessId = Session["sessionid"].ToString().Trim();
//            //Session["sessionShowDate"] = Request["showdate"];
//            //Session["sessionShowClass"] = Request["showclass"];
//            //Session["SesNoofSeat"] = int.Parse(Request["seats"].ToString());
//            //Session["lngsessionid"] = Session["sessionid"].ToString().Trim();
//            //if (objbook.blnSetSeats(strCinemaId, Session["sessionTransID"].ToString().Trim(), long.Parse(Session["sessionid"].ToString().Trim()), strSelseats))
//            //{
//            //    booking objEntities = new booking();
//            //    booking_bal objBooking = new booking_bal();
//            //    Session["sessionSeatDetails"] = objbook.strSeatInfo.ToString();

//            //    decimal bff;
//            //    decimal bfee = 15;
//            //    DataTable bt = objBooking.getBookingFeeByCinema(strCinemaId);
//            //    if (bt.Rows.Count > 0)
//            //    {
//            //        bfee = decimal.Parse(bt.Rows[0]["BookingFee"].ToString());
//            //    }
//            //    bff = decimal.Multiply(decimal.Parse(Session["SesNoofSeat"].ToString()), decimal.Parse(bfee.ToString()));
//            //    Session["sessionBookingFee"] = bff.ToString();

//            //    Session["Taxfee"] = "0.00";
//            //    decimal ovt = decimal.Parse(Session["sessionTicketAmount"].ToString()) + decimal.Parse(Session["sessionBookingFee"].ToString());
//            //    Session["sessionTotalAmount"] = Math.Round(ovt, 2);
//            //    int bookinglen = int.Parse(Session["sessionBookingID"].ToString().Length.ToString());
//            //    StringBuilder sb = new StringBuilder();
//            //    for (int k = 0; k < (10 - (bookinglen)); k++)
//            //    {
//            //        sb.Append("0");
//            //    }
//            //    Session["FullsessionBookingID"] = Session["sessionCinemaCode"].ToString() + sb.ToString() + Session["sessionBookingID"].ToString();
//            //    objEntities.Booking_CinemaID = strCinemaId;
//            //    objEntities.Booking_SessionID = long.Parse(strSessId.ToString());
//            //    objEntities.UserID = Session["userid"].ToString();
//            //    objEntities.Booking_MovieID = Session["sessionMovieID"].ToString();
//            //    objEntities.TransID = strTransId;
//            //    objEntities.BookingID = Session["FullsessionBookingID"].ToString();
//            //    objEntities.Booking_BookingDate = DateTime.Parse(DateTime.Now.ToString());
//            //    objEntities.Booking_ShowDate = DateTime.Parse(Session["sessionShowDate"].ToString());
//            //    objEntities.ShowTime = Session["sessionShowTime"].ToString();
//            //    //objEntities.ShowTime = "9 pm";
//            //    objEntities.ShowClass = Session["sessionShowClass"].ToString();
//            //    objEntities.NoofSeat = int.Parse(Request["seats"].ToString());
//            //    objEntities.SeatDetails = Session["sessionSeatDetails"].ToString();
//            //    objEntities.BookingFee = Session["sessionBookingFee"].ToString();
//            //    objEntities.FoodTotal = Session["sessionFoodTotal"].ToString();
//            //    objEntities.TicketAmount = Session["sessionTicketAmount"].ToString();
//            //    objEntities.ServiceTax = decimal.Parse(Session["Taxfee"].ToString());
//            //    objEntities.UIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
//            //    objEntities.layout = layoutht.ToString();
//            //    objEntities.ScreenName = Session["showscreen"].ToString();
//            //    DataTable dt = objBooking.getCityNameByCinemaId(strCinemaId);
//            //    if (dt.Rows.Count > 0)
//            //    {
//            //        Session["bookingcityid"] = dt.Rows[0]["intCityId"].ToString();
//            //        Session["bookingcityname"] = dt.Rows[0]["strCityName"].ToString();
//            //    }
//            //    objEntities.intCityID = int.Parse(Session["bookingcityid"].ToString());
//            //    int lastindentity = objBooking.insertBookingDetails(objEntities);
//            //    Session["BookingDetailID"] = lastindentity.ToString();

//            //    // Insert into booking.bookingdetails table
//            //    decimal dis = 0.00M;
//            //    dis = Math.Round(dis, 2);
//            //    Session["discount"] = dis;

//            //    string rss2 = objBooking.InsertAreacode(Session["strrareacode"].ToString(), int.Parse(Session["BookingDetailID"].ToString()));
//            //    //Response.Redirect("book-tickets-services.aspx?bookingid=" + Session["BookingDetailID"] + "&cid=" + strCinemaId + "&transid=" + Session["sessionTransID"].ToString());

//            //    Session["synergyuid"] = "";
//            //    Session["fbstatus"] = "";
//            //    Server.Transfer("book-tickets-services.aspx?bookingid=" + Session["BookingDetailID"] + "&cid=" + strCinemaId + "&transid=" + Session["sessionTransID"].ToString());

//            //}
//            //else
//            //{
//            //    Response.Write("Error@" + objbook.intException + "@" + objbook.strException);
//         //   }
//        }

        private void display_seatlayout()
        {
            int cells;
            Bigtree.VistaRemote.clsBook objbook = new Bigtree.VistaRemote.clsBook();
            Bigtree.VistaRemote.objArea objA = new Bigtree.VistaRemote.objArea();
            Bigtree.VistaRemote.objRow objR = new Bigtree.VistaRemote.objRow();
            Bigtree.VistaRemote.objSeat objS = new Bigtree.VistaRemote.objSeat();
            Boolean blnShowAvailInArea;

            ArrayList al = new ArrayList();
            ArrayList cl = new ArrayList();
            foreach (Bigtree.VistaRemote.objArea x in colA)
            {
                al.Add(x);
            }
            al.Reverse();
            foreach (Bigtree.VistaRemote.objArea x in al)
            {
                objA = x;
                if (hddAreaCode.Value == objA.strAreaCode.ToString())
                {
                    //if (objA.strAreaDesc.ToString() != "NORMAL")
                    //{
                    tab = tab + "<tr><td class='' colspan='" + colA.intMaxSeatId().ToString() + "' style='color:#fff; font-family:arial;font-size:13px;'>" + msg + "</td></tr>";

                    tab = tab + "<tr><td colspan='" + colA.intMaxSeatId().ToString() + "' align='center' height='30'><div class='screen' style='font:18px; font-weight:bold;'><b>" + objA.strAreaDesc.ToString().Trim().Substring(0, 1) + objA.strAreaDesc.ToString().Trim().Substring(1, objA.strAreaDesc.Length - 1).Trim().ToString().ToLower() + "</b></div></td>";
                    //}
                    foreach (Bigtree.VistaRemote.objRow row in objA.colRows)
                    {
                        cl.Add(row);
                    }
                   // cl.Reverse();

                    foreach (Bigtree.VistaRemote.objRow r in cl)
                    {
                        objR = r;
                        string classs = "";

                        blnShowAvailInArea = objA.blnHasCurrentOrder();


                        tab = tab + "<tr><td class='a'>" + objR.strRowPhyID.ToString() + "</td>";
                        for (cells = short.Parse(colA.intMaxSeatId().ToString()); cells >= short.Parse(colA.intMinSeatId().ToString()); cells--)
                        {

                            cellid++;
                            objS = objR.colSeats.GetSeatById(cells.ToString());
                            if (objS.strGridSeatNum.ToString() == "0")
                            {
                                tab = tab + "<td  class='dark' id='td_" + cellid + "'></td>";
                            }
                            else
                            {
                                switch (objS.strSeatStatus.ToString())
                                {
                                    case "0": // 0-Available
                                        if (blnShowAvailInArea)
                                        {
                                            classs = "ava";
                                            selectedseatvalue = objA.strAreaCode.ToString() + "|" + objA.strAreaNum.ToString() + "|" + objR.intGridRowID.ToString() + "|" + objS.strGridSeatNum.ToString() + "|";
                                            string selectedclass = "ava";
                                            tab = tab + "<td align='center' class='" + selectedclass + "'  id='td_" + selectedseatvalue + "' style='cursor:pointer;' onclick=getvalue('" + selectedseatvalue + "',this.id)><font style='size:9px;'>&nbsp;</a></td>";
                                        }
                                        else
                                        {
                                            tab = tab + "<td  class='dark' id='td_" + cellid + "'></td>";
                                        }
                                        break;
                                    case "3": //House /Special
                                        tab = tab + "<td class='bkd' id='td_" + cellid + "'></td>";
                                        break;
                                    case "5": // Current Order
                                        classs = "curnt";
                                        selectedseatvalue = objA.strAreaCode.ToString() + "|" + objA.strAreaNum.ToString() + "|" + objR.intGridRowID.ToString() + "|" + objS.strGridSeatNum.ToString() + "|";
                                        tab = tab + "<td align='center' class='" + classs + "'  id='td_" + selectedseatvalue + "' style='cursor:pointer;' onclick=getvalue('" + selectedseatvalue + "',this.id)><font style='size:9px;'></a></td>";
                                        selseats = ass + selectedseatvalue;
                                        ass = selseats;
                                        selseats1 = ass1 + selectedseatvalue + "$";
                                        ass1 = selseats1;
                                        break;
                                    case "1": //1-Boooked
                                        tab = tab + "<td class='bkd' id='td_" + cellid + "'></td>";
                                        break;
                                }
                            }


                        }
                        tempcount = 1;

                        cells = 0;
                        tab = tab + "</tr>";
                    }


                }

            }

            if (Request["seats"] == "1")
            {
                selectedseatvalue = selectedseatvalue;
            }
            else
            {
                selectedseatvalue = selectedseatvalue + "|";
            }
            string layoutht;
            layoutht = "<table cellpadding='0' cellspacing='0' border='0' width='627px'>" + tab + "</table>";
            dvProceed.Visible = true;
            hdnselectedseats.Value = selseats;
//            Session["selectedseats"] = selseats;

            dvSeatLayout.InnerHtml = layoutht.ToString();
            //objbook.blnSetSettings("ConnectionString", connstr);
            //objbook.blnSetSettings("LicenseType", "WWW");
            //strCinemaId = Session["sessionCinemaCode"].ToString();
            //Session["sessioncinemaID"] = strCinemaId;

            //strSelseats = "|" + Session["SesNoofSeat"].ToString() + "|" + selseats;
            //strTransId = Session["sessionTransID"].ToString();
            //strSessId = Session["sessionid"].ToString().Trim();
            //Session["sessionShowDate"] = Request["showdate"];
            //Session["sessionShowClass"] = Request["showclass"];
            //Session["SesNoofSeat"] = int.Parse(Request["seats"].ToString());
            //Session["lngsessionid"] = Session["sessionid"].ToString().Trim();
            //if (objbook.blnSetSeats(strCinemaId, Session["sessionTransID"].ToString().Trim(), long.Parse(Session["sessionid"].ToString().Trim()), strSelseats))
            //{
            //    booking objEntities = new booking();
            //    booking_bal objBooking = new booking_bal();
            //    Session["sessionSeatDetails"] = objbook.strSeatInfo.ToString();

            //    decimal bff;
            //    decimal bfee = 15;
            //    DataTable bt = objBooking.getBookingFeeByCinema(strCinemaId);
            //    if (bt.Rows.Count > 0)
            //    {
            //        bfee = decimal.Parse(bt.Rows[0]["BookingFee"].ToString());
            //    }
            //    bff = decimal.Multiply(decimal.Parse(Session["SesNoofSeat"].ToString()), decimal.Parse(bfee.ToString()));
            //    Session["sessionBookingFee"] = bff.ToString();

            //    Session["Taxfee"] = "0.00";
            //    decimal ovt = decimal.Parse(Session["sessionTicketAmount"].ToString()) + decimal.Parse(Session["sessionBookingFee"].ToString());
            //    Session["sessionTotalAmount"] = Math.Round(ovt, 2);
            //    int bookinglen = int.Parse(Session["sessionBookingID"].ToString().Length.ToString());
            //    StringBuilder sb = new StringBuilder();
            //    for (int k = 0; k < (10 - (bookinglen)); k++)
            //    {
            //        sb.Append("0");
            //    }
            //    Session["FullsessionBookingID"] = Session["sessionCinemaCode"].ToString() + sb.ToString() + Session["sessionBookingID"].ToString();
            //    objEntities.Booking_CinemaID = strCinemaId;
            //    objEntities.Booking_SessionID = long.Parse(strSessId.ToString());
            //    objEntities.UserID = Session["userid"].ToString();
            //    objEntities.Booking_MovieID = Session["sessionMovieID"].ToString();
            //    objEntities.TransID = strTransId;
            //    objEntities.BookingID = Session["FullsessionBookingID"].ToString();
            //    objEntities.Booking_BookingDate = DateTime.Parse(DateTime.Now.ToString());
            //    objEntities.Booking_ShowDate = DateTime.Parse(Session["sessionShowDate"].ToString());
            //    objEntities.ShowTime = Session["sessionShowTime"].ToString();
            //    //objEntities.ShowTime = "9 pm";
            //    objEntities.ShowClass = Session["sessionShowClass"].ToString();
            //    objEntities.NoofSeat = int.Parse(Request["seats"].ToString());
            //    objEntities.SeatDetails = Session["sessionSeatDetails"].ToString();
            //    objEntities.BookingFee = Session["sessionBookingFee"].ToString();
            //    objEntities.FoodTotal = Session["sessionFoodTotal"].ToString();
            //    objEntities.TicketAmount = Session["sessionTicketAmount"].ToString();
            //    objEntities.ServiceTax = decimal.Parse(Session["Taxfee"].ToString());
            //    objEntities.UIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            //    objEntities.layout = layoutht.ToString();
            //    objEntities.ScreenName = Session["showscreen"].ToString();
            //    DataTable dt = objBooking.getCityNameByCinemaId(strCinemaId);
            //    if (dt.Rows.Count > 0)
            //    {
            //        Session["bookingcityid"] = dt.Rows[0]["intCityId"].ToString();
            //        Session["bookingcityname"] = dt.Rows[0]["strCityName"].ToString();
            //    }
            //    objEntities.intCityID = int.Parse(Session["bookingcityid"].ToString());
            //    int lastindentity = objBooking.insertBookingDetails(objEntities);
            //    Session["BookingDetailID"] = lastindentity.ToString();

            //    // Insert into booking.bookingdetails table
            //    decimal dis = 0.00M;
            //    dis = Math.Round(dis, 2);
            //    Session["discount"] = dis;

            //    string rss2 = objBooking.InsertAreacode(Session["strrareacode"].ToString(), int.Parse(Session["BookingDetailID"].ToString()));
            //    //Response.Redirect("book-tickets-services.aspx?bookingid=" + Session["BookingDetailID"] + "&cid=" + strCinemaId + "&transid=" + Session["sessionTransID"].ToString());

            //    Session["synergyuid"] = "";
            //    Session["fbstatus"] = "";
            //    Server.Transfer("book-tickets-services.aspx?bookingid=" + Session["BookingDetailID"] + "&cid=" + strCinemaId + "&transid=" + Session["sessionTransID"].ToString());

            //}
            //else
            //{
            //    Response.Write("Error@" + objbook.intException + "@" + objbook.strException);
            //}


        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlClass.SelectedValue != "0")
            {
                string[] strClassArr = ddlClass.SelectedValue.Split('#');
                decimal dblTicketCost = Convert.ToDecimal(strClassArr[1]);
                decimal dblTotalTicketCost = dblTicketCost * Convert.ToInt32(strBookArr[4]);
                decimal dblTotalAmount = dblTotalTicketCost + (Convert.ToDecimal(intConvenience) * Convert.ToInt32(strBookArr[4]));
                Session["sessionShowClass"] = ddlClass.SelectedItem.Text;
                Session["TotalAmount"] = Math.Round(dblTotalAmount, 2);
                decimal intConvenienceDisplay = Convert.ToDecimal(Convert.ToDecimal(intConvenience) * Convert.ToInt32(strBookArr[4]));
                pTotalCon.InnerHtml = "<span>Total: Rs. " + Math.Round(dblTotalAmount, 2) + "</span> ( " + Math.Round(dblTicketCost, 2) + " x " + strBookArr[4] + " =  Rs. " + Math.Round(dblTotalTicketCost, 2) + " + Rs." + Math.Round(intConvenienceDisplay, 2) + " Convenience Charge)";
                Session["sessionTicketAmount"] = Math.Round(dblTotalTicketCost, 2);
                hddAreaCode.Value = strClassArr[0];
                hddTypeCode.Value = strClassArr[2];
                listClass();
            }
            else
            {
            }
        }

        protected void lnlProceed_Click(object sender, EventArgs e)
        {
            objbook.blnSetSettings("ConnectionString", connstr);
            objbook.blnSetSettings("LicenseType", "WWW");
            string strSelseats = string.Empty;
            strSelseats = "|" + strBookArr[4] + "|" + hdnselectedseats.Value;
            if (objbook.blnSetSeats(strBookArr[0], Session["sessionTransID"].ToString().Trim(), long.Parse(strBookArr[3].ToString().Trim()), strSelseats))
            {
                Session["sessionSeatInfo"] = objbook.strSeatInfo.ToString();
                Response.Redirect("payment.aspx");
            }
        }

        protected void lnkCancel_Click(object sender, EventArgs e)
        {
            string strmsg = "";
            objbook.blnSetSettings("ConnectionString", connstr);
            objbook.blnSetSettings("LicenseType", "WWW");
            if (objbook.blnCancelTrans(strBookArr[0], Session["sessionTransID"].ToString().Trim()))
            {
                strmsg += "1";
            }
            else
            {
                strmsg += "0"+objbook.strException;
            }
            //if (objbook.blnCancelBook(strBookArr[0], long.Parse(Session["sessionBookingID"].ToString().Trim())))
            //{
            //    strmsg += "1";
            //}
            //else
            //{
            //    strmsg += "0";
            //}
            Response.Redirect("index.aspx");
        }
    }
}