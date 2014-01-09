<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="right-panel.ascx.cs" Inherits="GopalanCinemasWeb.right_panel1" %>
<script language="javascript">
    function valid() {
        if (document.getElementById("<%=ddlChinema.ClientID %>").value == "0") {
            alert("Select Cinema");
            document.getElementById("<%=ddlChinema.ClientID %>").focus();
            return false;
        }
        if (document.getElementById("<%=ddlMovie.ClientID %>").value == "0") {
            alert("Select Movie");
            document.getElementById("<%=ddlMovie.ClientID %>").focus();
            return false;
        }
        if (document.getElementById("<%=ddlDate.ClientID %>").value == "0") {
            alert("Select Date");
            document.getElementById("<%=ddlDate.ClientID %>").focus();
            return false;
        }
        if (document.getElementById("<%=ddlShowTime.ClientID %>").value == "0") {
            alert("Select Show Time");
            document.getElementById("<%=ddlShowTime.ClientID %>").focus();
            return false;
        }
        if (document.getElementById("<%=ddlSeats.ClientID %>").value == "0") {
            alert("Select Seat");
            document.getElementById("<%=ddlSeats.ClientID %>").focus();
            return false;
        }
    }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
<div class="book-tkt">
    <h1>
        <img src="../images/book-tickets.gif" /></h1>
    <asp:DropDownList ID="ddlChinema" runat="server" AutoPostBack="true" 
        DataTextField="Cinema_strName" DataValueField="Cinema_strID" 
        onselectedindexchanged="ddlChinema_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlMovie" runat="server" AutoPostBack="true" 
        DataTextField="Film_strTitle" DataValueField="Film_strCode" 
        onselectedindexchanged="ddlMovie_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlDate" runat="server" AutoPostBack="true" 
        DataTextField="Session_dtmRealShow" DataValueField="Session_dtmRealShow" 
        onselectedindexchanged="ddlDate_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlShowTime" runat="server" AutoPostBack="true" 
        DataTextField="Session_dtmRealShow" DataValueField="Session_lngSessionId" 
        onselectedindexchanged="ddlShowTime_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlSeats" runat="server">
    </asp:DropDownList>
      <%--<select name=""><option>Select Cinema</option></select>
      <select name=""><option>Select Movie</option><option>Select Date</option><option>Select Date</option></select>
      <select name=""><option>Select Date</option></select>
      <select name=""><option>Select Show Time</option></select>
      <select name=""><option>No of Seats</option></select>--%>
      <dfn>
    <asp:ImageButton ID="imgSubmit" runat="server" 
        ImageUrl="images/btn_submit.gif" onclick="imgSubmit_Click" 
        OnClientClick="return valid();" />
    </dfn>
</div>
    </ContentTemplate></asp:UpdatePanel>

