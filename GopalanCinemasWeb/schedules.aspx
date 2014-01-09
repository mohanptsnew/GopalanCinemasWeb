<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/GopalanWeb.Master" CodeBehind="schedules.aspx.cs" Inherits="GopalanCinemasWeb.schedules" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <script language="javascript">
        document.getElementById("aSchedule").className = "active";
        function changeColor(id) {
            document.getElementById(id).style.color = "#ffffff";
            document.getElementById(id).style.background = "#000000";
        }
        function changeOriginalColor(id) {
            document.getElementById(id).style.color = "#0e7e26";
            document.getElementById(id).style.background = "#ffffff";
        }
        function checkValid() {
            if (document.getElementById("<%=ddlChinemaName.ClientID %>").value == "0") {
                alert("Select Cinema");
                document.getElementById("<%=ddlChinemaName.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlDates.ClientID %>").value == "0") {
                alert("Select Date");
                document.getElementById("<%=ddlDates.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlSeatsNo.ClientID %>").value == "0") {
                alert("Select Seat(s)");
                document.getElementById("<%=ddlSeatsNo.ClientID %>").focus();
                return false;
            }
        }
    </script>
    <div class="content_in_sp">
    <div class="sp-menu">
    <div class="tl-img"><img src="../images/schedules_tl.gif" /></div>
    </div>
    <div  class="content_in_sp1">
    <div class="sch_blk-in2">
    <div class="cinema">
    <h2>Cinema</h2>
<%--    <select name="">
      <option>---  Select  ---</option>
    </select>--%>
    <asp:DropDownList ID="ddlChinemaName" runat="server" AutoPostBack="true" 
        DataTextField="Cinema_strName" DataValueField="Cinema_strID" 
            onselectedindexchanged="ddlChinemaName_SelectedIndexChanged" >
    </asp:DropDownList>
    </div>
 <div class="date">
    <h2>Date</h2>
    <asp:DropDownList ID="ddlDates" runat="server" DataTextField="Session_dtmRealShow" DataValueField="Session_dtmRealShow" ></asp:DropDownList>
    </div>
    <div class="seat">
    <h2>No. of Seats</h2>
    <asp:DropDownList ID="ddlSeatsNo" runat="server">
        <asp:ListItem Value="0" Text="Select Seat(s)"></asp:ListItem>
        <asp:ListItem Value="1" Text="1"></asp:ListItem>
        <asp:ListItem Value="2" Text="2"></asp:ListItem>
        <asp:ListItem Value="3" Text="3"></asp:ListItem>
        <asp:ListItem Value="4" Text="4"></asp:ListItem>
        <asp:ListItem Value="5" Text="5"></asp:ListItem>
        <asp:ListItem Value="6" Text="6"></asp:ListItem>
        <asp:ListItem Value="7" Text="7"></asp:ListItem>
        <asp:ListItem Value="8" Text="8"></asp:ListItem>
        <asp:ListItem Value="9" Text="9"></asp:ListItem>
    </asp:DropDownList></div>   
     <div class="seat" style="width:100px; height:30px;">
     <div style="height:5px;"></div>
         <asp:Button ID="btnGo" Text="Go" runat="server" OnClientClick="return checkValid()" onclick="btnGo_Click" />
         </div>
    </div>
    <div class="sch_blk-in2"><img src="images/green-bx.gif" /><cite>Available</cite> <img src="images/ash-bx.gif" /><cite>Bookings Closed</cite> <img src="images/red-bx.gif" /><cite>Sold Out</cite></div>
    <%--<div id="spFilms" runat="server"></div>--%>
    <asp:Panel ID="pnlFilms" runat="server"></asp:Panel>
<%--<div class="sch_blk-in3"><font>Break Ke Baad (Hindi) (U/A)</font><div class="time"><dfn>12.30 PM , </dfn> 3.45 PM , 9.00 PM</div></div>
<div class="sch_blk-in3"><font>Black Swan (English) (A)</font><div class="time">6.00 PM</div></div>
<div class="sch_blk-in3"><font>127 Hours (English) (A)</font><div class="time">3.00 PM , 7.15 PM , <span>9.30 PM</span></div></div>
<div class="sch_blk-in3"><font>Sucker Punch (English) (U/A)</font><div class="time"><dfn>3.15 PM</dfn></div></div>
<div class="sch_blk-in3"><font>The Eagle (English) (U/A)</font><div class="time">4.30 PM</div></div>
<div class="sch_blk-in3"><font>Happy Husbands (Hindi) (A)</font><div class="time">6.30 PM , <span>9.30 PM</span></div></div>
--%>    </div>
    
    
    
    <div  class="content_in_sp1" id="coming" style="display:none">
    <div class="movie_blk">
    <div class="tmb"><img src="../images/coming-soon/tmb1.jpg" /></div>
    <div class="txt">
    <h1>Tanu Weds Manu (Hindi)</h1>
    <h4>Starcast:</h4>
    <p>R. Madhavan, Kangana Ranuat, Jimmy Shergil</p>
    <h4>Director:</h4>
    <p>Anand L. Rai</p>
    <span><a href="cs-synopisis.aspx">view Synopsis</a> &nbsp;|&nbsp; <a href="cs-synopisis.aspx">View Trailer</a> &nbsp;|&nbsp; <dfn><a href="#">Book Now</a></dfn></span>
    </div>
    </div>
    <div class="movie_blk">
    <div class="tmb"><img src="../images/coming-soon/tmb2.jpg" /></div>
    <div class="txt">
    <h1>Just Go With It (English)</h1>
    <h4>Starcast:</h4>
    <p>Natalie Portman, Mila Kunis, Vincent Cassel</p>
    <h4>Director:</h4>
    <p>Darren Aronofsky</p>
    <span><a href="cs-synopisis.aspx">view Synopsis</a> &nbsp;|&nbsp; <a href="cs-synopisis.aspx">View Trailer</a> &nbsp;|&nbsp; <dfn><a href="#">Book Now</a></dfn></span>
    </div>
    </div>
    <div class="movie_blk" style="background:none">
    <div class="tmb"><img src="../images/coming-soon/tmb3.jpg" /></div>
    <div class="txt">
    <h1>Thank You (Hindi)</h1>
    <h4>Starcast:</h4>
    <p>James Franco, Kate Mara, Amber Tamblyn</p>
    <h4>Director:</h4>
    <p>Danny Boyle</p>
    <span><a href="cs-synopisis.aspx">view Synopsis</a> &nbsp;|&nbsp; <a href="cs-synopisis.aspx">View Trailer</a> &nbsp;|&nbsp; <dfn><a href="#">Book Now</a></dfn></span>
    </div>
    </div>
    <cite><a href="#">Previous</a> &nbsp;|&nbsp; <a href="#">Next</a></cite>
    </div>
    </div>
</asp:Content>


