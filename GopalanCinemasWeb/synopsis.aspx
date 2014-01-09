<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/GopalanWeb.Master" CodeBehind="synopsis.aspx.cs" Inherits="GopalanCinemasWeb.synopsis" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <div id="maincontent" runat="server">
    <link href="Styles/style1.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="js/validation.js"></script>
    <script src="js/docheight.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        document.getElementById("aMovie").className = "active";
        function fndisplaydata(cls) {
            document.getElementById("dvSynopis").className = cls;
        }
        function ValidProceed() {
            if (document.getElementById("<%=ddlMoviesChinema.ClientID %>").value == "0") {
                alert("Select Cinema");
                document.getElementById("<%=ddlMoviesChinema.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlMoviesDate.ClientID %>").value == "0") {
                alert("Select Date");
                document.getElementById("<%=ddlMoviesDate.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlMoviesShowTime.ClientID %>").value == "0") {
                alert("Select Show Time");
                document.getElementById("<%=ddlMoviesShowTime.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlMoviesSeats.ClientID %>").value == "0") {
                alert("Select Seat");
                document.getElementById("<%=ddlMoviesSeats.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=hddGetFilmCode.ClientID %>").value == "") {
                alert("Select Movies");
                return false;
            }
        }
        function displaycity() {

            document.getElementById('pop_div').style.display = "block";
            document.getElementById("html_pop").style.display = "block";
            document.getElementById('pop_div').style.height = docht() + "px";
        }
    </script>
    <script type="text/JavaScript">
<!--
        var curr = "";
        var currplus = "";
        function toggle(val, plus) {

            if (document.getElementById(val).style.display == 'none') {

                document.getElementById(val).style.display = 'block';
                document.getElementById(plus).src = "../images/movies/dwn_arrow.gif";
                if (curr != "") {
                    document.getElementById(curr).style.display = 'none';
                    document.getElementById(currplus).src = "../images/movies/dwn_arrow.gif";
                }
                document.getElementById(plus).src = "../images/movies/dwn_arrow.gif";

                curr = val;
                currplus = plus;
            } else {

                document.getElementById(val).style.display = 'none';
                document.getElementById(plus).src = '../images/movies/up_arrow.gif';
                curr = "";
                currplus = "";
            }
        }

        function MM_openBrWindow(theURL, winName, features) { //v2.0
            window.open(theURL, winName, features);
        }
//-->
</script>
<asp:ScriptManager runat="server"></asp:ScriptManager>
<div id="pop_div" style="z-index:100;margin-top:-25px;"></div> 
<div id="html_pop" style="display:none">
      <asp:UpdatePanel runat="server" id="upDisplay">
      <ContentTemplate>
<div class="book-now">
  <div class="book-now_blk">
  <h1 id="hFilmName" runat="server"></h1>
    <div class="btn"><a href="javascript:void(0)"><img style="cursor:pointer;" onclick="document.getElementById('html_pop').style.display='none';document.getElementById('pop_div').style.display='none';" type='image' src="../images/btn_close_bk.gif" title="Close" width="16" height="17" /></a></div>
  </div>
  <div class="book-now_blk2">
    <div class="select_blk">
      <h2>Cinema</h2>
     <%-- <select name="" class="sel-bx">
        <option>Select</option>
      </select>--%>
      <asp:DropDownList ID="ddlMoviesChinema" runat="server" AutoPostBack="true" 
        DataTextField="Cinema_strName" DataValueField="Cinema_strID" 
        onselectedindexchanged="ddlMoviesChinema_SelectedIndexChanged">
    </asp:DropDownList>
    </div>
    <div class="select_blk">
      <h2>Date</h2>
     <%-- <select name="" class="sel-bx">
        <option>Select</option>
      </select>--%>
      <asp:DropDownList ID="ddlMoviesDate" runat="server" AutoPostBack="true" 
        DataTextField="Session_dtmRealShow" DataValueField="Session_dtmRealShow" 
        onselectedindexchanged="ddlMoviesDate_SelectedIndexChanged">
    </asp:DropDownList>
    </div>
    <div class="select_blk">
      <h2>Time</h2>
      <%--<select name="" class="sel-bx">
        <option>Select</option>
      </select>--%>
      <asp:DropDownList ID="ddlMoviesShowTime" runat="server" AutoPostBack="true" 
        DataTextField="Session_dtmRealShow" DataValueField="Session_lngSessionId" 
        onselectedindexchanged="ddlMoviesShowTime_SelectedIndexChanged">
    </asp:DropDownList>
    </div>
    <div class="select_blk">
      <h2>No. of Seats</h2>
      <%--<select name="" class="sel-bx">
        <option>Select</option>
      </select>--%>
       <asp:DropDownList ID="ddlMoviesSeats" runat="server">
    </asp:DropDownList>
    </div>
  </div>
  <div class="book-now_blk2"><asp:ImageButton ID="btn_proceedSeat" 
          CssClass="IProceedSeat" runat="server" 
          ImageUrl="../images/btn_seat-selection.gif" OnClientClick="return ValidProceed()" onclick="btn_proceedSeat_Click" /></div>
</div>
</ContentTemplate>
      </asp:UpdatePanel>

</div>
    <div class="content_in_sp">
    <div class="sp-menu">
    <div class="coming-soon" id="dvSynopis"><a href="movies.aspx" class="active"></a></div>
    </div>
    <div  class="content_in_sp2">
       <cite><a href="movies.aspx">Back</a><asp:HiddenField ID="hddGetFilmCode" runat="server" /> </cite>
       <div class="movie_blk1">
    <div class="tmb"><img id="iSynImageName" runat="server"  /></div>
    <div class="txt">
    <h1 id="hSynFilmName" runat="server"></h1>
    <h4>Starcast:</h4>
    <p>Natalie Portman, Mila Kunis, Vincent Cassel</p>
    <h4>Director:</h4>
    <p>Darren Aronofsky</p>
    <span><asp:ImageButton ID="imgSynBookNow" runat="server" 
            ImageUrl="images/movies/btn_book-now.gif" onclick="imgSynBookNow_Click" /></span>    </div>
    </div>
    <div class="cont_blk">
    <h2  style="cursor:pointer;" onClick="toggle('synopsisInfo','plus1');">Synopsis <img src="images/movies/up_arrow.gif" id="plus1" /></h2>
    <p id="synopsisInfo" runat="server" style="display:none">Nina (Portman) is a ballerina in a New York City ballet company whose life, like all those in her profession, is completely consumed with dance. She lives with her obsessive former ballerina mother Erica (Hershey) who exerts a suffocating control over her.</p>
    </div>
    
    <div class="cont_blk">
    <h2  style="cursor:pointer;" onClick="toggle('trailer','plus2');">Trailer <img src="images/movies/up_arrow.gif" id="plus2" /></h2>
    <p id="trailer" runat="server" style="display:none"><img src="../images/video-img.jpg" /></p> 
    </div>
    
    </div>
    </div>
    </div>
    <div id="errorcontent" style="display:none;" runat="server">
    <iframe src="error.aspx" width="100" height="100"></iframe>
    </div>
</asp:Content>