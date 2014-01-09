<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/GopalanWeb.Master" CodeBehind="seat-selection.aspx.cs" Inherits="GopalanCinemasWeb.seat_selection" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <script language="javascript">
    function getvalue(selseatval, id) {
        if (document.getElementById(id).className == "curnt") {
            deselectseat(id);
            var cur = document.getElementById("<%=hdnselectedseats.ClientID%>").value;
            document.getElementById("<%=hdnselectedseats.ClientID%>").value = cur.replace(selseatval, "");
        }
        else {
            if (document.getElementById("<%=hdntotalseat.ClientID%>").value >= document.getElementById("<%=hdnSeatQty.ClientID%>").value) {
                alert("You have already selected no of seat(s)");
                return false;
            }
            else {
                if (document.getElementById("<%=hdntotalseat.ClientID%>").value <= document.getElementById("<%=hdnSeatQty.ClientID%>").value) {
                    document.getElementById("<%=hdntotalseat.ClientID%>").value = eval(document.getElementById("<%=hdntotalseat.ClientID%>").value) + 1;
                    document.getElementById(id).className = "curnt";
                    document.getElementById(id).id = id;
                    if (document.getElementById("<%=hdnselectedseats.ClientID%>").value == "") {
                        document.getElementById("<%=hdnselectedseats.ClientID%>").value = selseatval;
                    }
                    else {
                        document.getElementById("<%=hdnselectedseats.ClientID%>").value = document.getElementById("<%=hdnselectedseats.ClientID%>").value + selseatval;
                    }
                }
            }
        }
    }
    function deselectseat(val) {
        if (document.getElementById("<%=hdntotalseat.ClientID%>").value >= 0) {
            document.getElementById(val).className = "ava";
            document.getElementById("<%=hdntotalseat.ClientID%>").value = eval(document.getElementById("<%=hdntotalseat.ClientID%>").value - 1);
        }
    }
    function validateseats() {
        if (document.getElementById("<%=hdnselectedseats.ClientID%>").value == "") {
            alert("Select Seat");
            return false;
        }
        if (document.getElementById("<%=hdntotalseat.ClientID%>").value != document.getElementById("<%=hdnSeatQty.ClientID%>").value) {
            alert("Choose Your Seat(s)");
            return false;
        }
        return true;
    }
    </script>
    <div class="content_in_sp">
    <div class="sp-menu">
    <div class="tl-img"><img src="../images/book-tickets_tl.gif" /></div>
    </div>
    <div  class="content_in_sp3">
    <div class="seat-layout_blk">
    <div class="amt_blk">
    <h1>
    <asp:DropDownList ID="ddlClass" runat="server" AutoPostBack="true" DataTextField="TType_strDescription" 
            DataValueField="CodePrice" 
            onselectedindexchanged="ddlClass_SelectedIndexChanged">
    </asp:DropDownList></h1>
    <h4 id="pTotalCon" runat="server"></h4>
     <asp:HiddenField ID="hdntransid" runat="server" />
            <asp:HiddenField ID="hdnselectedseats" runat="server" />
            <asp:HiddenField ID="hdntotalseat" runat="server" />
            <asp:HiddenField ID="hdnSeatQty" runat="server" />
            <asp:HiddenField ID="hdnntransid" runat="server" />
           <asp:HiddenField ID="hddAreaCode" runat="server" />
           <asp:HiddenField ID="hddTypeCode" runat="server" />
   </div>
    <div class="seat-layout_blk_in">
   <div class="screen_blk">
   <ul>
   <li><img src="../images/seat-select/bked.gif" align="absmiddle" />&nbsp; Booked Seats</li>
   <li><img src="../images/seat-select/curnt.gif" align="absmiddle" />&nbsp; Current Selection</li>
   <li><img src="../images/seat-select/ava.gif" align="absmiddle" />&nbsp; Available Seats</li>
   </ul>
   <img src="../images/seat-select/screen.gif" /></div>
    
    <div class="seat_blk">
    <span id="dvSeatLayout" runat="server"></span>
    <div  id="dvProceed" runat="server" visible="false">
    <div class="btn-proceed">
    <asp:LinkButton ID="lnlProceed" runat="server" Text="Proceed" OnClientClick="return validateseats();" 
            onclick="lnlProceed_Click"></asp:LinkButton>
            </div>
            <div class="btn-proceed">
            <asp:LinkButton ID="lnkCancel" runat="server" Text="Cancel" 
                    onclick="lnkCancel_Click"></asp:LinkButton>
            </div>
            </div>
    </div>
    </div>
    </div>
    </div>
        </div>
</asp:Content>


