<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/GopalanWebAdmin.Master" CodeBehind="refund.aspx.cs" Inherits="GopalanCinemasWeb.admin.refund" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxt" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
   
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" style="margin-top:5px;" id="displaygrid" runat="server">
        <tr>
            <td align="center" colspan="3"><span style="font-size:20px;" >Refund Reports</span></td>
        </tr>
        <tr> <td> &nbsp; </td> </tr>
        <tr>
            <td colspan="3">From Date : <asp:TextBox ID="bFromDate" runat="server" Width="100pt"></asp:TextBox>
                <asp:ImageButton ID="btnpop1" runat="server" ImageUrl="~/admin/images/calendar.png" Width="16" Height="16" CausesValidation="false" OnClick="Imagebtn_Click" />
                <ajaxt:calendarextender ID="cal2" runat="server" PopupButtonID="btnpop1" 
                    TargetControlID="bFromDate" Format="dd/MM/yyyy"></ajaxt:calendarextender>
                 <asp:RequiredFieldValidator ID="rf5" runat="server" Display="Dynamic" ControlToValidate="bFromDate" ErrorMessage="Select From Date" ForeColor="Red"></asp:RequiredFieldValidator>
                 To Date :
                  <asp:TextBox ID="bToDate" runat="server" Width="100pt"></asp:TextBox>
                <asp:ImageButton ID="btnpop2" runat="server" ImageUrl="~/admin/images/calendar.png" Width="16" Height="16" CausesValidation="false" />
                <ajaxt:calendarextender ID="cal3" runat="server" PopupButtonID="btnpop2" 
                    TargetControlID="bToDate" Format="dd/MM/yyyy"></ajaxt:calendarextender>
                <asp:RequiredFieldValidator ID="rf6" runat="server" ControlToValidate="bToDate" ErrorMessage="Select To Date" ForeColor="Red"></asp:RequiredFieldValidator>
<%--                <asp:CompareValidator ID="rf7" runat="server" Display="Dynamic" SetFocusOnError="true" EnableClientScript="false" ControlToValidate="bToDate" ControlToCompare="bFromDate" Type="Date" Operator="GreaterThanEqual" ErrorMessage="To Date must be greater than From Date" ForeColor="Red"></asp:CompareValidator>
--%>                 
                 <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="bToDate"  
                    ControlToCompare="bFromDate" Type="Date" Display="Dynamic" ErrorMessage="* From date should be less than Enddate"
                    Operator="GreaterThanEqual" SetFocusOnError="True" ForeColor="Red"></asp:CompareValidator>
&nbsp;&nbsp;      <br/> <div style="float: left; margin-left: 400px;">
                 <asp:Button ID="btnGo" runat="server" Text="Go" onclick="btnGo_Click" /> &nbsp;&nbsp;
                 <asp:Button ID="btnAll" runat="server" Text="All" OnClick="btnAll_Click" CausesValidation="false"/>&nbsp;&nbsp; 
                  <asp:Button ID="Button1" runat="server" Text="Export" onclick="Button1_Click" CausesValidation="false" />
                 </div>
                 </td>
        </tr>

        <tr><td colspan="3"></td></tr>
        <tr>
                <td class="lft">
                <asp:GridView ID="gdRefund" Width="100%" runat="server" AllowPaging="true" AllowSorting="true" 
                        CellSpacing="2" CellPadding="2" PageSize="10" AutoGenerateColumns="false" EnableSortingAndPagingCallbacks="false" 
                        AlternatingRowStyle-BackColor="#CCCCCC" HeaderStyle-BackColor="#666666" 
                        HeaderStyle-ForeColor="White" onpageindexchanging="gdRefund_PageIndexChanging" onrowupdating="gdRefund_RowUpdating" >                
                        <Columns>
<%--                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Label ID="gvlblOrderId" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
--%>            
               <asp:BoundField DataField="Email" HeaderText="Email" />
               <asp:BoundField DataField="Mobile" HeaderText="Mobile" />
               <asp:BoundField DataField="BookingID" HeaderText="Booking ID" />
               <asp:BoundField DataField="ShowDate" HeaderText="Show Date" />
               <asp:BoundField DataField="ShowTime" HeaderText="Show Time" />
               <asp:BoundField DataField="BookingDate" HeaderText="Booking Date" />
               <asp:BoundField DataField="OverAllAmount" HeaderText="Total Amount" />
              
               <asp:TemplateField HeaderText="Refund">
               <ItemTemplate>
               <asp:Label ID="lblBookingDetailID" runat="server" Visible="false" Text='<%# Bind("BookingInfoID") %>'></asp:Label>
               <asp:Label ID="lblBookId" runat="server" Visible="false" Text='<%# Bind("bookid") %>'></asp:Label>
               <asp:Label ID="lblTransactionId" runat="server" Visible="false" Text='<%# Bind("TransactionID") %>'></asp:Label>
               <asp:Label ID="lblFirstName" runat="server" Visible="false" Text='<%# Bind("LastName") %>'></asp:Label>
               <asp:Label ID="lblEmail" runat="server" Visible="false" Text='<%# Bind("Email") %>'></asp:Label>
               <asp:Label ID="lblMobile" runat="server" Visible="false" Text='<%# Bind("Mobile") %>'></asp:Label>
               <asp:LinkButton ID="lnkRefund" runat="server" Text="Refund"  OnClick="lnkRefund_Click"></asp:LinkButton>
               <asp:CheckBox ID="checkRefund" runat="server" ></asp:CheckBox>
               </ItemTemplate>
               </asp:TemplateField>
                 </Columns>
                </asp:GridView>
                </td>
              </tr>
              <tr>
              <td style="text-align:center">
              <asp:Label ID="lblmsg" runat="server" Font-Bold="true" ></asp:Label>

              </td>
              </tr>
              <asp:Panel ID="Panel1" runat="server">
    </asp:Panel>
    <asp:Xml ID="Xml1" runat="server" />
    </table>
</asp:Content>


