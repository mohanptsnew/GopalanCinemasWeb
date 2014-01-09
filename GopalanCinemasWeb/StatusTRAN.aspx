<%@ Page Language="C#"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
    <title></title>
    <table border="1" align="center"  width="100%" >
	<tr>
	<td align="left" width="90%"><font  size = 5 color = darkblue face = verdana ><b>Sample Page</td>
	<td align="right"width="10%"><IMG SRC="images/fssnet.JPG" WIDTH="169" HEIGHT="37" BORDER="0" ALT=""></td>
	</tr>
</table>
</head>
<BODY bgcolor="white">
	
 <script language="C#" runat="server">
            public string result;
         protected void Page_Load(object sender, EventArgs e)
         {
              
             try
             {           
  </script>   

  <br><br> <br><br> <br><br>
	<table border="1" align="center" >
	<tr>
		<th colspan="50" bgcolor="#9999FF" ><p style= "color:White">Final Response  
    </th>
	</tr>
		
	<tr>
		<td>
			<table align="center"  border="2">
			<tr>
				<td colspan="35">Transaction Status</td>				
				<td> <%= Request["ResResult"] %></td>				
			</tr>
			<tr>
				<td colspan="35">Merchant Reference No:[TRACK_ID]</td>				
				<td> <%= Request["ResTrackId"]  %></td>				
			</tr>
						<tr>
				<td colspan="35">Transaction Amount</td>				
				<td> <%= Request["ResAmount"]  %></td>				
			</tr>
			</table>
		</td>
	</tr>
	</table>
<br><br><br><br><br><br><br>
<table border="1" align="center"  width="100%" >
	<tr>
	<td align="Left" width="90%"><font  size = 5 color = darkblue face = verdana ><b>Sample Page</td>
	<td align="right"width="10%"><IMG SRC="images/fssnet.JPG" WIDTH="169" HEIGHT="37" BORDER="0" ALT=""></td>
	</tr>
	</table>
  
 <script language="C#" runat="server">         
                 
              }
             catch (Exception Ex)
             {
                 Response.Write(Ex.Message);
             }
         }

	


        </script>

</body>
</html>
<!-- Disclaimer:- Important Note in Sample Pages
- This is a sample demonstration page only ment for demonstration, this page should not be used in production
- Transaction data should only be accepted once from a browser at the point of input, and then kept in a way that does not allow others to modify it (example server session, database  etc.)
- Any transaction information displayed to a customer, such as amount, should be passed only as display information and the actual transactional data should be retrieved from the secure source last thing at the point of processing the transaction.
- Any information passed through the customer's browser can potentially be modified/edited/changed/deleted by the customer, or even by third parties to fraudulently alter the transaction data/information. Therefore, all transaction information should not be passed through the browser to Payment Gateway in a way that could potentially be modified (example hidden form fields). 
 -->