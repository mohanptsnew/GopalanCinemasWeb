<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/GopalanWeb.Master" CodeBehind="payment.aspx.cs" Inherits="GopalanCinemasWeb.payment" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <link href="../Styles/tab.css" rel="stylesheet" type="text/css" />
    <style>
        .chkbox {
            width:25px;
            float:left;
        }

    </style>
    
<script type="text/javascript" src="../js/jquery-1.2.6.min.js"></script>
<script language="javascript" type="text/javascript">
    function chkEmpty() {
        if (Page_ClientValidate()) {
            document.getElementById('<%=imgPayment.ClientID %>').style.visibility = "hidden";
        }
    }
    function ValidateModuleList(source, args)
    {
        if (document.getElementById("<%= chkTerms.ClientID %>").checked == true) {
            args.IsValid = true;
            return;
        }
        args.IsValid = false;
    }

</script>
<script type="text/javascript" src="../js/tabs.js"></script>
    <div class="content_in_sp">
    <div class="sp-menu">
    <div class="tl-img"><img src="../images/book-tickets_tl.gif" /></div>
    </div>
    <div  class="content_in_sp1">
    <div class="payment-page">
    <div class="user-details_blk" style="width:600px;">
    <h1>Booking Information</h1>
    <div class="user-details_in" style="width:600px;">
    <label style="width:60px;">Cinema</label><span id="spCinemaName" style="width:240px;" runat="server"></span>
    <label style="width:60px;">Movie</label><span id="spMovieName" style="width:240px;" runat="server"></span>
    </div>
    <div class="user-details_in" style="width:600px;">
    <label style="width:50px;">Date</label><span id="spDate" style="width:80px;" runat="server"></span>
    <label style="width:80px;">Show Time</label><span id="spShowTime" style="width:80px;" runat="server"></span>
    <label style="width:50px;">Seat(s)</label><span id="spSeat" style="width:50px;" runat="server"></span>
    <label style="width:80px;">Seat(s) Info</label><span id="spSeatInfo" style="width:120px;" runat="server"></span>
    </div>
    </div>

    <div class="user-details_blk">
    <h1>User Details</h1>
    <div class="user-details_in">
    <label>Enter Name:</label><span><asp:TextBox ID="txtUName" runat="server"></asp:TextBox><em style="float:left; height:15px; width:10px;"><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUName" Text="*" ForeColor="Red" Width="10" ErrorMessage="*" ></asp:RequiredFieldValidator></em>
    <em style="float:left; height:15px; width:100px;"> </em></span></div>
    <div class="user-details_in">
    <label>Enter Email Id:</label><span><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><em style="float:left; height:15px; width:10px;"><asp:RequiredFieldValidator ID="rfvtxtEmail" runat="server" ControlToValidate="txtEmail" Text="*" ForeColor="Red" Width="10" ErrorMessage="*" ></asp:RequiredFieldValidator></em>
    <em style="float:left; height:15px; width:100px;"><asp:RegularExpressionValidator ID="revtxtEmail" ControlToValidate="txtEmail" Text="Invalid Email" ValidationExpression="^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ForeColor="Red" Width="100" ErrorMessage="Invalid Email"  runat="server"></asp:RegularExpressionValidator> </em></span></div>
        <div class="user-details_in">
    <label>Enter Mobile:</label><span><asp:TextBox ID="txtMobile" runat="server" MaxLength="10" ></asp:TextBox><em style="float:left; height:15px; width:10px;"><asp:RequiredFieldValidator ID="rfvtxtMobile" runat="server" ControlToValidate="txtMobile" Text="*" ForeColor="Red" ErrorMessage="*" Width="10" ></asp:RequiredFieldValidator></em>
    <em style="float:left; height:15px; width:100px;"><asp:RegularExpressionValidator id="RegularExpressionValidator1" 
                     ControlToValidate="txtMobile"
                     ValidationExpression="\d{10}"
                     ForeColor="Red"
                     Width="100"
                     ErrorMessage="Invalid Mobile"
                     runat="server"/></em>
    </span></div>
        <div class="user-details_in">
    <label>Total Amount:</label><span id="spTotalAmountDebit" runat="server"></span></div>
        <div class="user-details_in">
  <asp:CheckBox ID="chkTerms" TextAlign="Right" runat="server" CssClass="chkbox" Width="25" />  <span id="Span1" style="width:500px;">I have read, understood and accept the <a style="text-decoration:underline; color:black; font-weight:bold" href="termsandconditions.aspx">Terms &amp; Conditions</a> of this transaction</span>
<asp:CustomValidator runat="server" ID="cvmodulelist"
  ClientValidationFunction="ValidateModuleList"
  ErrorMessage="Please accept terms and conditions" ForeColor="Red" ></asp:CustomValidator>
        </div>
    <div class="user-details_in">
    <label>&nbsp;</label>
      <span>
      <asp:ImageButton ID="imgPayment" runat="server" 
            ImageUrl="../images/payment/btn_make-py.gif" Width="100" Height="24" CausesValidation="true" OnClientClick="chkEmpty();" alt="Make Payment" 
            onclick="imgPayment_Click" />&nbsp;
      <asp:ImageButton ID="imgCancel" runat="server" 
            ImageUrl="../images/payment/btn_make-cancel.gif" Width="100" Height="24" 
            CausesValidation="false"  alt="Cancel" onclick="imgCancel_Click" />
            <%--<input type="hidden" name="MAmount" id="MAmount" size="25" />
            <asp:HiddenField ID="hdntotalamount" runat="server" />--%>
      </span>
    </div>
    </div>
    
   <%--<div class="pay-details_blk"><!-- start tab  -->
   <h1>Payment Details</h1>
   <div id="payment-blk">
  <ul class="tab">
    <li id="debit" class="active">Debit Card</li>
    <li id="credit">Credit Card</li>
    <li id="net">Net Banking</li>
  </ul>
  <span class="clear"></span>
  <div class="py-content debit">
    <div class="debit-blk">
      <label>Card Category </label>
      <cite>:</cite> <span>
      <select name="">
        <option>Citibank Debit Card</option>
      </select>
      </span></div>
    <div class="debit-blk">
      <label>Card Number</label>
      <cite>:</cite> <span>
      <input type="text" name="#" />
      </span><dfn>(Your card number without spaces)</dfn></div>
    <div class="debit-blk">
      <label>Name on Card</label>
      <cite>:</cite> <span>
      <input type="text" name="#" />
      </span></div>
    <div class="debit-blk">
      <label>Expiry date</label>
      <cite>:</cite><span>
      <select name="select" id="select" class="month">
        <option>Month</option>
      </select>
      <select name="select" id="select" class="year">
        <option>Year</option>
      </select>
      </span> </div>
    <div class="debit-blk">
      <label>CVV</label>
      <cite>:</cite> <span>
      <input type="text" name="#" class="cvv" />
      </span></div>
    <div class="debit-blk">
      <label>Total Amount</label>
      <cite>:</cite> <span id="spTotalAmountDebit" runat="server"></span></div>
    <div class="debit-blk">
      <label>&nbsp;</label>
      <cite>&nbsp;</cite>
      <input name="" type="checkbox" value="" />
      <font>I agree to the <a href="#">Terms and Conditions</a></font></div>
    <div class="debit-blk">
      <label>&nbsp;</label>
      <cite>&nbsp;</cite> <span>
      <asp:ImageButton ID="imgPayment" runat="server" 
            ImageUrl="../images/payment/btn_make-py.gif" alt="Make Payment" 
            onclick="imgPayment_Click" />
            <input type="hidden" name="MAmount" id="MAmount" size="25" value="1.00" />
            <asp:HiddenField ID="hdntotalamount" runat="server" />
      </span></div>
  </div>
  
  
  <div class="py-content credit" style="display:none">
    <div class="credit-blk">
      <label>Card Category </label>
      <cite>:</cite> 
<input name="" type="radio" value="visa" class="radio" /> <img src="../images/payment/icon-visa.gif" align="left" /> <input name="" type="radio" value="" class="radio" /> <img src="../images/payment/icon-masterCard.gif" align="left" />
      </div>
    <div class="credit-blk">
      <label>Card Number</label>
      <cite>:</cite> <span>
      <input type="text" name="#" />
      </span><dfn>(Your card number without spaces)</dfn>
       </div>
    <div class="credit-blk">
      <label>Name on Card</label>
      <cite>:</cite> <span>
      <input type="text" name="#" />
      </span></div>
    <div class="credit-blk">
      <label>Expiry date</label>
      <cite>:</cite><span>
      <select name="select" id="select" class="month">
        <option>Month</option>
      </select>
      <select name="select" id="select" class="year">
        <option>Year</option>
      </select>
      </span> </div>
    <div class="credit-blk">
      <label>CVV</label>
      <cite>:</cite> <span>
      <input type="text" name="#" class="cvv" />
      </span></div>
    <div class="credit-blk">
      <label>Total Amount</label>
      <cite>:</cite> <span id="spTotalAmountCredit" runat="server" ></span></div>
    <div class="credit-blk">
      <label>&nbsp;</label>
      <cite>&nbsp;</cite>
      <input name="" type="checkbox" value="" />
      <font>I agree to the <a href="#">Terms and Conditions</a></font></div>
    <div class="credit-blk">
      <label>&nbsp;</label>
      <cite>&nbsp;</cite> <span><a href="#"><img src="../images/payment/btn_make-py.gif" alt="Make Payment" /></a></span></div>
  </div>
  
   <div class="py-content net" style="display:none">
<div class="net-blk1">
<p>Online Bank Account :</p>
<select name="">
<option>==Online Bank Account ==</option>
</select>
</div>
   
    <div class="net-blk">
      <label>Total Amount</label>
      <cite>:</cite> <span id="spTotalAmountNet" runat="server" ></span></div>
    <div class="net-blk">
      <label>&nbsp;</label>
      <cite>&nbsp;</cite>
      <input name="" type="checkbox" value="" />
      <font>I agree to the <a href="#">Terms and Conditions</a></font></div>
    <div class="net-blk">
      <label>&nbsp;</label>
      <cite>&nbsp;</cite> <span><a href="#"><img src="../images/payment/btn_make-py.gif" alt="Make Payment" /></a></span></div>
  </div>
</div>  
   
   </div>--%><!-- end tab -->
    <%--<img src="images/hdfc.jpg" />--%>
    </div>
    
    </div>
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

