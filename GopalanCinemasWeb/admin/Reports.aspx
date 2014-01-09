<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Masters/GopalanWebAdmin.Master"  CodeBehind="Reports.aspx.cs" Inherits="GopalanCinemasWeb.admin.Reports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxt" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
       <asp:ScriptManager ID="ScriptManager1" runat="server"  />
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" style="margin-top:5px;" id="displaygrid" runat="server">
        <tr>
            <td align="center" colspan="3"><span style="font-size:20px;" >Transaction Reports</span><br /></td>         
        </tr>
        <tr><td></td></tr>
        <tr>
            <td colspan="3">
                From Date :
                <asp:TextBox ID="bFromDate" runat="server" Width="100pt"></asp:TextBox>

                <asp:ImageButton ID="btnpop1" runat="server" ImageUrl="~/admin/images/calendar.png"
                    Width="16" Height="16" CausesValidation="false" />
                <ajaxt:CalendarExtender ID="cal2" runat="server" PopupButtonID="btnpop1" TargetControlID="bFromDate"
                    Format="dd/MM/yyyy">
                </ajaxt:CalendarExtender>

                <asp:RequiredFieldValidator ID="rf5" runat="server" Display="Dynamic" ControlToValidate="bFromDate"
                    ErrorMessage="Select From Date" ForeColor="Red"></asp:RequiredFieldValidator>
                To date :
                <asp:TextBox ID="bToDate" runat="server" Width="100pt"></asp:TextBox>

                <asp:ImageButton ID="btnpop2" runat="server" ImageUrl="~/admin/images/calendar.png"
                    Width="16" Height="16" CausesValidation="false" />
                <ajaxt:CalendarExtender ID="cal3" runat="server" PopupButtonID="btnpop2" TargetControlID="bToDate"
                    Format="dd/MM/yyyy">
                </ajaxt:CalendarExtender>

                <asp:RequiredFieldValidator ID="rf6" runat="server" ControlToValidate="bToDate" ErrorMessage="Select To Date"
                    ForeColor="Red"></asp:RequiredFieldValidator>

                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="bToDate"
                    ControlToCompare="bFromDate" Type="Date" Display="Dynamic" ErrorMessage="*Startdate should be less than Enddate"
                    Operator="GreaterThanEqual" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator>
                <%--                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="gvlblOrderId" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                --%>&nbsp;&nbsp;<br />
                <br />
                <asp:Label ID="lblpayment" runat="server" Text="Payment"></asp:Label>
                &nbsp;
                <asp:DropDownList ID="ddlPayment" runat="server" Font-Bold="false" Width="60px">
                    <asp:ListItem Text="Select" Value="2" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Label ID="lblvista" runat="server" Text="Vista"></asp:Label>
                &nbsp;
                <asp:DropDownList ID="ddlVista" runat="server" Font-Bold="false" Width="60px">
                    <asp:ListItem Text="Select" Value="2" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Label ID="lblTransaction" runat="server" Text="Transactions"></asp:Label>
                &nbsp;
                <asp:DropDownList ID="ddltrans" runat="server" Font-Bold="false" Width="60px">
                    <asp:ListItem Text="Select" Value="2" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                    <asp:ListItem Text="No" Value="0"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                <br />
                <div style="float: left; margin-left: 400px;">
                    <asp:Button ID="btnSubmit" runat="server" Text="Go" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnGo" runat="server" Text="Export" OnClick="btnGo_Click" />
            </td>
        </tr>
        <tr><td colspan="3"></td></tr>
        <tr>
                <td class="lft">
                <asp:GridView ID="gdRefund" Width="100%" runat="server" AllowPaging="true" AllowSorting="true" 
                        CellSpacing="2" CellPadding="2" PageSize="10" Caption="Transactions List" 
                        AutoGenerateColumns="false" EnableSortingAndPagingCallbacks="false" 
                        AlternatingRowStyle-BackColor="#CCCCCC" HeaderStyle-BackColor="#666666" 
                        HeaderStyle-ForeColor="White" 
                        onpageindexchanging="gdRefund_PageIndexChanging" onrowupdating="gdRefund_RowUpdating" 
                        >                <Columns>
<%--                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="gvlblOrderId" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
--%>            
               <asp:BoundField DataField="BookingID" HeaderText="Booking ID" />
               <asp:BoundField DataField="Email" HeaderText="Email" />
               <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
               <asp:BoundField DataField="ShowDate" HeaderText="Show Date" />
               <asp:BoundField DataField="ShowTime" HeaderText="Show Time" />
               <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" />
               <asp:BoundField DataField="NoofSeat" HeaderText="No of Seats" />
               <asp:BoundField DataField="OverAllAmount" HeaderText="Total Amount" />
               <asp:BoundField DataField="PGStatus" HeaderText="Payment Status" />
               <asp:BoundField DataField="VistaStatus" HeaderText="Vista Status" />
               <asp:BoundField DataField="MailStatus" HeaderText="Transaction Status" />
               <%--<asp:TemplateField HeaderText="Refund">
               <ItemTemplate>
               <asp:Label ID="lblBookingDetailID" runat="server" Visible="false" Text='<%# Bind("BookingInfoID") %>'></asp:Label>
               <asp:LinkButton ID="lnkRefund" runat="server" Text="Refund" CommandName="update" ></asp:LinkButton>
               </ItemTemplate>
               </asp:TemplateField>--%>
                 </Columns>
                </asp:GridView>
                </td>
              </tr>
              <tr>
              <td style="text-align:center">
              <asp:Label ID="lblmsg" runat="server" Font-Bold="true" ></asp:Label>
              </td>
              </tr>
    </table>
</asp:Content>


