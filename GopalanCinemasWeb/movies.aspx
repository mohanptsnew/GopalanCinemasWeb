<%@ Page Language="C#" MasterPageFile="~/Masters/GopalanWeb.Master" AutoEventWireup="true" CodeBehind="movies.aspx.cs" Inherits="GopalanCinemasWeb.movies" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <link href="Styles/style1.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="js/validation.js"></script>
    <script language="javascript">
        document.getElementById("aMovie").className = "active";
    </script>
    <script>
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
        function fndisplaydata(id, cls) {
            document.getElementById("now").style.display = "none";
            document.getElementById("coming").style.display = "none";
            document.getElementById("n").className = "";
            document.getElementById("c").className = "";
            document.getElementById(id).style.display = "block";
            document.getElementById(cls).className = "active";
        }
</script>
<script src="js/docheight.js" type="text/javascript"></script>
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
        DataTextField="Session_dtmRealShow2" DataValueField="Session_dtmRealShow" 
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
    <div class="now-showing"><a href="javascript:void(0)" id="n" class="active" onclick="fndisplaydata('now','n')">Now Showing</a></div>
    <div class="coming-soon"><a href="javascript:void(0)" id="c" onclick="fndisplaydata('coming','c')">Coming Soon</a></div>
    </div>
    <div  class="content_in_sp1" id="now"><asp:HiddenField ID="hddGetFilmCode" runat="server" />
    <asp:GridView ID="gvNowShowing" runat="server" DataKeyNames="Film_strCode" 
            PagerStyle-Font-Underline="false"  PagerSettings-NextPageText="&nbsp;Next" 
            PagerSettings-Position="Bottom" PagerSettings-Mode="NextPrevious" 
            PagerSettings-PreviousPageText="Previous&nbsp;"  PageSize="3" BorderStyle="None" 
            GridLines="None" BorderWidth="0" AllowPaging="true" AutoGenerateColumns="false" 
            onpageindexchanging="gvNowShowing_PageIndexChanging" 
            onrowupdating="gvNowShowing_RowUpdating" 
            onrowcommand="gvNowShowing_RowCommand">
    <PagerStyle HorizontalAlign="Right" CssClass="pagernext"/>
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="movie_blk">
                    <div class="tmb"><img width="96" height="137" src='<%# "images/movies/common/" + Eval("Film_strCode") + ".jpg" %>'  /></div>
                <%--  <div class="tmb"><img width="96" height="137" src='<%# "images/movies/noimage-inner.jpg" %>'  /></div>--%>  
                    <div class="txt">
                    <h1><%# Eval("Film_strTitle") %></h1>
                    <h4>Starcast:</h4>
                    <p><%# Eval("Stars") %></p>
                    <h4>Director:</h4>
                    <p><%# Eval("Director") %></p>
                    <span><asp:HiddenField ID="hddNSFilmCode" runat="server" Value='<%# Eval("Film_strCode") %>' /><asp:HiddenField ID="hddNSFilmTitle" runat="server" Value='<%# Eval("Film_strTitle") %>' /><asp:LinkButton ID="lnkNSSynopsis" runat="server" Text="View Synopsis" CommandName="update"></asp:LinkButton> &nbsp;|&nbsp; <asp:LinkButton ID="lnkNSTrailer" runat="server" CommandName="update" Text="View Trailer"></asp:LinkButton> &nbsp;|&nbsp; <dfn>
                    <asp:LinkButton ID="lnkBookNow" runat="server" Text="Book Now" CommandName="BookNow"></asp:LinkButton>
                    <%--<a href="javascript:void(0)" onclick="displaycity()">Book Now</a>--%></dfn></span>
                    </div>
                    </div>
                    
                </ItemTemplate>
            </asp:TemplateField>
            
        </Columns>
    </asp:GridView>
<%--    <cite><a href="#">Previous</a> &nbsp;|&nbsp; <a href="#">Next</a></cite>--%>
    </div>
    
        
    <div  class="content_in_sp1" id="coming" style="display:none">
    <asp:GridView ID="gvComingSoon" DataKeyNames="Film_strCode" runat="server" PagerStyle-Font-Underline="false" 
            BorderStyle="None" PageSize="3" 
            PagerSettings-NextPageText="&nbsp;Next" PagerSettings-Position="Bottom" 
            PagerSettings-Mode="NextPrevious" PagerSettings-PreviousPageText="Previous&nbsp;"  
            GridLines="None" BorderWidth="0" AllowPaging="true" AutoGenerateColumns="false" 
            onpageindexchanging="gvComingSoon_PageIndexChanging" 
            onrowupdating="gvComingSoon_RowUpdating">
             <PagerStyle HorizontalAlign="Right" CssClass="pagernext"/>
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="movie_blk">
                    <div class="tmb"><img width="96" height="137" src='<%# "images/movies/common/" + Eval("Film_strCode") + ".jpg" %>'  /></div>
<%--                    <div class="tmb"><img width="96" height="137" src='<%# "images/movies/noimage-inner.jpg" %>'  /></div>--%>
                    <div class="txt">
                    <h1><%# Eval("Film_strTitle") %><asp:Label ID="gvlblCSFilmCode" runat="server" Visible="false" Text='<%# Eval("Film_strCode") %>'></asp:Label></h1>
                    <h4>Starcast:</h4>
                    <p><%# Eval("Stars") %></p>
                    <h4>Director:</h4>
                    <p><%# Eval("Director") %></p>
                    <span><asp:HiddenField ID="hddCSFilmCode" runat="server" Value='<%# Eval("Film_strCode") %>' /><asp:LinkButton ID="lnkCSSynopsis" runat="server" CommandName="update" Text="View Synopsis"></asp:LinkButton>&nbsp;|&nbsp; <asp:LinkButton ID="lnkCSTrailer" runat="server" CommandName="update" Text="View Trailer"></asp:LinkButton> &nbsp;|&nbsp; <dfn style="color:InactiveBorder;">Book Now</dfn></span>
                    </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
  <%--  <cite><a href="#">Previous</a> &nbsp;|&nbsp; <a href="#">Next</a></cite>--%>
    </div>
    </div>
</asp:Content>


