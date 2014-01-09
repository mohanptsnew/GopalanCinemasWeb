<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="confirmation.aspx.cs" MasterPageFile="~/Masters/GopalanWeb.Master" Inherits="GopalanCinemasWeb.confirmation" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <link href="../Styles/style.css" rel="stylesheet" type="text/css" />
<link href="../Styles/font.css" rel="stylesheet" type="text/css" />

        <div class="content_in_sp">
    <div class="sp-menu">
    <div class="tl-img"><img src="../images/book-tickets_tl.gif" /></div>
    </div>
    <div  class="content_in_sp1">
    
    <div class="bking-success_blk">
    <h1>Booking Successful</h1>
         <p>Dear</p> <p><span id="span_uname" runat="server"></span></p>  <p>, Your booking details as follows:</p>
    <div class="bk-id_blk">Booking ID: <span id="bid" runat="server"></span></div>
    <div class="bk-id_blk" style="font-size:14px;">Kiosk ID:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<span id="kioskid" runat="server" style="font-size:14px;"></span></div>
    <div class="print_blk"><a  href="javascript:window.print()"><img src="../images/icon_print.gif" /> PRINT THIS PAGE</a></div>
    <div class="film_blk">
                <div class="hdg">
                  <h2 id="mname" runat="server"></h2>
                  <div class="amt"><span class="WebRupee">Rs.</span> <span id="totalamount" style="font-weight:bold" runat="server"></span></div>
                </div>
                <div class="detail_blk"><font>Date</font>
                  <p><span id="showdate" runat="server"></span></p>
                </div>
                <div class="detail_blk"><font>Time</font>
                  <p><span id="showtime" runat="server"></span></p>
                </div>
                <div class="detail_blk"><font>Cinema</font>
                  <p><span id="cname" runat="server"></span></p>
                </div>
                <div class="detail_blk"><font>No. of Seats</font>
                  <p><span id="totalseat" runat="server"></span></p>
                </div>
                <div class="detail_blk" id="last"><font>Seat(s) Info</font>
                  <p><span id="seatinfo" runat="server"></span></p>
                </div>
              </div>
         <%--<div class="film_blk2"><div class="hdg">
                  <h2>Food and Beverages</h2>
                  <div class="amt"><span class="WebRupee">Rs.</span> 220</div>
                </div>
                <div class="fd-bv_blk">
                  <div class="fd-bv_in">
                <div class="item"><strong>Items</strong></div><div class="qty"><strong>Qty</strong></div>
                </div>
                
                <div class="fd-bv_in">
                <div class="item">Jumbo Combo</div><div class="qty">5</div>
                </div>
                <div class="fd-bv_in">
                <div class="item">Large Pepsi</div><div class="qty">5</div>
                </div>
                <div class="fd-bv_in">
                <div class="item">Large Tub Popcorn</div><div class="qty">5</div>
                </div>        
                </div>
                
                <div class="fd-bv_blk">
                  <div class="fd-bv_in">
                <div class="item"><strong>Items</strong></div><div class="qty"><strong>Qty</strong></div>
                </div>
                <div class="fd-bv_in">
                <div class="item">Large Pepsi</div><div class="qty">5</div>
                </div>
                <div class="fd-bv_in">
                <div class="item">Jumbo Combo</div><div class="qty">5</div>
                </div>        
                </div>
                
                  </div>--%>  
            <div class="film_blk3" style="padding-left:10px;" id="dvOffer" runat="server"></div>                    

          <div class="film_blk3"><div class="hdg">
                  <h2>TOTAL AMOUNT</h2>
                  <div class="amt"><span class="WebRupee">Rs.</span> <span id="totalamount1" style="font-weight:bold" runat="server"></span></div>
                </div></div> 
               <%-- <img src="images/grandmall.jpg" border="0" /><br /><br />
                <span>"Kindly produce a printed copy of this booking confirmation or the e-mail confirmation at Gopalan Box office counter and collect your coupons to avail your free small popcorn(s)*".
<br /><br />  
*T&C
Book tickets through www.gopalancinemas.com website and get small popcorn free for each tickets.
<br /><br />  --%>
                 <span id="spMsg" runat="server">Please take printout for your future reference.</span>
    </div>
    
    
    </div>
    </div>

    </span>

</asp:Content>


