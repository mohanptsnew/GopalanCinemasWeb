<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="GopalanCinemasWeb.Index" %>
<%@ Register TagPrefix="ucl" TagName="RightBoxControl" Src="~/right-panel.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Gopalan Cinemas</title>
<link href="Styles/style.css" rel="stylesheet" type="text/css" />
<link href="Styles/style4.css" rel="stylesheet" type="text/css" />
	<script type="text/javascript" src="js/jquery.js"></script>
	<script type="text/javascript" src="js/easySlider1.5.js"></script>
	<script type="text/javascript">
	    $(document).ready(function () {

	        $("#slider2").easySlider({
	            controlsBefore: '<p id="controls2">',
	            controlsAfter: '</p>',
	            prevId: 'prevBtn2',
	            nextId: 'nextBtn2'
	        });
	    });	
	</script>

</head>
<body>
<form id="form1" runat="server">
<div class="container_out">
  <div class="container_in">
    <div class="bdy_content">
    <div class="follow_blk"> <img src="images/icon_blog.gif" /> <img src="images/icon_face-book.gif" /> <img src="images/icon_twitter.gif" />
    <img src="images/icon_u-tube.gif" /> <p>Follow Us</p>
    </div>
      <div class="menu">
        <div class="home"><a href="index.aspx" class="active">Home</a></div>
        <div class="about-us"><a href="about-us.aspx">About Us</a></div>
        <div class="schedules"><a href="schedules.aspx">Schedules</a></div>
        <div class="logo">&nbsp;</div>
        <div class="movies"><a href="movies.aspx">Movies</a></div>
        <div class="press-room"><a href="#">Press Room</a></div>
        <div class="contact-us"><a href="contact-us.aspx">Contact Us</a></div>
      </div>
      <div class="content_in"><!-- start content -->
      <div class="content_in_hm">
      <div class="mainBanner">
		<div id="slider2">
			<ul>				
				<li><img src="images/main-banner/img1.jpg" width="180" height="250" /><img src="images/main-banner/img2.jpg" width="180" height="250"  /><img src="images/main-banner/img3.jpg" width="180" height="250"  class="last"  /></li>
				<li><img src="images/main-banner/img4.jpg" width="180" height="250" /><img src="images/main-banner/img5.jpg" width="180" height="250" /><img src="images/main-banner/img6.jpg" width="180" height="250" class="last"  /></li>
<%--<img src="images/main-banner/img1.jpg" class="last" />--%>
            </ul>
		</div>		
</div>
 
      <div class="book-tkt">
       <ucl:RightBoxControl ID="rbcRightPanel" runat="server" />
         </div>
      <div class="add"><img src="images/quick-bites_banner.jpg" /></div>
      </div>
      <div class="content_in2">
      <div class="now-showing_blk">
     <h1><img src="images/now-showing/now-showing_tl.gif" /></h1><h3><a href="movies.aspx">View All</a></h3>
      <div class="now-showing_blk_in" id="dvNowShowing" runat="server">
<%--      <div class="img">
      <img src="images/now-showing/img1.jpg" />
      <p>127 Hours</p>
      </div>
      <div class="img">
      <img src="images/now-showing/img2.jpg" />
      <p>Break Ke Baad</p>
      </div>
      <div class="img-last">
      <img src="images/now-showing/img3.jpg" />
      <p>Manmadhan Ambu</p>
      </div>
--%>      </div>
      </div>

      <div class="coming-soon_blk">
     <h1><img src="images/coming-soon/coming-soon_tl.gif" /></h1><h3><a href="movies.aspx">View All</a></h3>
      <div class="coming-soon_blk_in" id="dvComingSoon" runat="server">
<%--      <div class="img">
       <img src="images/coming-soon/img1.jpg" />
      <p>7 Khoon Maaf</p>
      </div>
    <div class="img">
<img src="images/coming-soon/img2.jpg" /> 
<p>L.B.W</p>
</div>--%>
      </div>
      </div>
      </div>
      <div class="content_in2">
      <div class="latest-news_blk" id="dvLatestNews" runat="server">
<%--      <div class="tl-bx">
      <h1><img src="images/latestNews_tl.gif" /></h1>
      <a href="#">More News</a>      </div>
      <dfn><img src="images/latestNews_div.gif" /></dfn>
      <div class="txt-bx">
      <h2>04, March 2011</h2>
      <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. 
Morbi viverra blandit urna sit amet </p>
      <p><a href="#">View More</a></p>
      </div>
--%>      </div>
      <div class="news-letter_blk" style="width:326px;">
      <h1><img src="images/newsletter_tl.gif" /></h1>
      <p>Nowshowing, coming soon, contest and lots more in our weekly newsletter</p>
      <div class="mail">
      <asp:TextBox ID="txtEmailID" runat="server"></asp:TextBox><a href="#"><asp:ImageButton ID="imgNewsGo" ImageAlign="Left" BorderWidth="0" ImageUrl="images/btn_go.gif" Width="24" Height="19" runat="server" onclick="imgNewsGo_Click"  /></a>
</div>
      </div>
      </div>
      </div><!-- end content -->
    </div>
       <div class="ftr">
    <div class="link">
      <blockquote>
        <p><a href="index.aspx">Home</a> -  <a href="about-us.aspx">About Us</a>  -  <a href="movies.aspx">Movies</a>  -  <a href="schedules.aspx">Schedules</a>  -  <a href="#">Press Room</a>  -  <a href="contact-us.aspx">Contact Us</a></p>
      </blockquote>
    </div>
    <div class="txt">
      <blockquote>
        <p>All rights reserved. Copyright Gopalan Cinemas 2011<br />
          Site design by <span><a href="#">Influx</a></span></p>
      </blockquote>
    </div>
    </div>
  </div>
</div>
</form>
<script type="text/javascript">
    var _gaq = _gaq || [];
    _gaq.push(['_setAccount', 'UA-27204121-1']);
    _gaq.push(['_trackPageview']);

    (function () {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
    })();

</script>
</body>
</html>
