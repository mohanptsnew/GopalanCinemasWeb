<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="GopalanCinemasWeb.Index" %>
<%@ Register TagPrefix="ucl" TagName="RightBoxControl" Src="~/right-panel.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Gopalan Cinemas</title>

<link href="Styles/style.css" rel="stylesheet" type="text/css" />
<link href="Styles/skin.css" rel="stylesheet" type="text/css" >
<script type="text/javascript" src="js/jquery-1.js"></script>
<script type="text/javascript" src="js/jquery-new.js"></script>
<script type="text/javascript">
    function mycarousel_initCallback(carousel) {
        // Disable autoscrolling if the user clicks the prev or next button.
        carousel.buttonNext.bind('click', function () {
            carousel.startAuto(0);
        });

        carousel.buttonPrev.bind('click', function () {
            carousel.startAuto(0);
        });

        // Pause autoscrolling if the user moves with the cursor over the clip.
        carousel.clip.hover(function () {
            carousel.stopAuto();
        }, function () {
            carousel.startAuto();
        });
    };

    jQuery(document).ready(function () {
        jQuery('#mycarousel').jcarousel({
            auto: 2,
            wrap: 'last',
            initCallback: mycarousel_initCallback
        });
    });
</script>	<script type="text/javascript" src="js/easySlider1.5.js"></script>
	<script type="text/javascript">
	    $(document).ready(function () {

	        $("#slider2").easySlider({
	            controlsBefore: '<p id="controls2">',
	            controlsAfter: '</p>',
	            prevId: 'prevBtn2',
	            nextId: 'nextBtn2'
	        });
	    });
	    function show_movie() {
	        document.getElementById('pop_div').style.display = "block";
	        document.getElementById("div_movie").style.display = "block";
	        document.getElementById('pop_div').style.height = docht() + "px";
	        //count();
	    }
	    function hide_div() {
	        document.getElementById('pop_div').style.display = "none";
	        document.getElementById("div_movie").style.display = "none";
	    }
	    function docht() {
	        if (document.documentElement.clientHeight < document.documentElement.scrollHeight) {
	            return document.documentElement.scrollHeight;
	        } else {
	            return document.documentElement.clientHeight;
	        }

	    }
	</script>
</head>
<body>
<form id="form1" runat="server">
<style type="text/css">
#pop_div
{
position:absolute;
left:0px;
top:0px;
width:100%;
height:100%;
z-index:1000;
background-color:#000000;
opacity:0.75;
filter:alpha(opacity=75);

}
#div_movie{
position:absolute;
width:100%;
top:100px;
height:100%;
z-index:1003;
}
</style>
<asp:HiddenField ID="hddShowPopup" runat="server" />
<div id="pop_div" runat="server" style="display:none;"></div> 
<div style="display:none;"  id="div_movie" runat="server">
<table width="100%">
  <tbody>
    <tr>
      <td align="center"><table align="center">
          <tbody>
            <tr>

              <td><div class="top_blk2">
                  <div class="close"><a style="cursor:pointer; float:right" onclick="hide_div();"><font style='color:White'>Close</font></a></div>
                  <div class="select_city_blk">
                  <a href='tc.aspx'><img src="images/offer.jpg" alt="" style="cursor:pointer;" /></a>
                   </div>
                </div></td>
            </tr>

          </tbody>
        </table></td>
    </tr>
  </tbody>
</table>
</div>

<div class="container_out">
  <div class="container_in">
    <div class="bdy_content">
    <div class="follow_blk"> <img src="images/icon_blog.gif" /><img src="images/icon_face-book.gif" /><img src="images/icon_twitter.gif" />
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
      <div class="jcarousel-skin-tango" style="width:684px; height:auto; display:inline">
  <div style="position: relative; display: block;" class="jcarousel-container jcarousel-container-horizontal">
  <div style="position: relative; width:600px" class="jcarousel-clip jcarousel-clip-horizontal">
  <ul id="mycarousel" class="jcarousel-list jcarousel-list-horizontal">
<li><img src="images/main-banner/img1.jpg"></li>
<li><img src="images/main-banner/img2.jpg"></li>
<li><img src="images/main-banner/img3.jpg"></li>
<li><img src="images/main-banner/img4.jpg"></li>
<li><img src="images/main-banner/img5.jpg"></li>
<li><img src="images/main-banner/img6.jpg"></li>
<li><img src="images/main-banner/img7.jpg"></li>
<li><img src="images/main-banner/img8.jpg"></li>
<li><img src="images/main-banner/img9.jpg"></li>
<li><img src="images/main-banner/img10.jpg"></li>
<li><img src="images/main-banner/img11.jpg"></li>
<li><img src="images/main-banner/img12.jpg"></li>
<li><img src="images/main-banner/img13.jpg"></li>
<li><img src="images/main-banner/img14.jpg"></li>

<!--<li><img src="images/main-banner/img15.jpg"></li>
<li><img src="images/main-banner/img16.jpg"></li>
<li><img src="images/main-banner/img17.jpg"></li>
<li><img src="images/main-banner/img18.jpg"></li>
<li><img src="images/main-banner/img19.jpg"></li>
<li><img src="images/main-banner/img20.jpg"></li>
<li><img src="images/main-banner/img21.jpg"></li>
<li><img src="images/main-banner/img22.jpg"></li>-->
  </ul>
  </div>
  <div disabled="false" style="display: block;" class="jcarousel-prev jcarousel-prev-horizontal"></div>
  <div disabled="false" style="display: block;" class="jcarousel-next jcarousel-next-horizontal"></div>
  </div></div>
 
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
     <h1><a href="offers.aspx"><img src="../images/offers_tl.gif" /></a></h1><h3><a href="offers.aspx">View All</a></h3>
      <div class="coming-soon_blk_in">
      <div class="img">
       <a href="offers.aspx"><img src="../images/offer_tmb.gif" /></a>
      <p><a href="offers.aspx">Offer</a></p>
      </div>
            <div class="img">
      <a href="offers.aspx"><img src="../images/rewards_tmb.gif" /></a> 
      <p><a href="offers.aspx">Rewards</a></p>
      </div>
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
      <div class="link"><a href="index.aspx">Home</a> -  <a href="about-us.aspx">About Us</a>  -  <a href="movies.aspx">Movies</a>  -  <a href="schedules.aspx">Schedules</a>  -  <a href="#">Press Room</a>   <br /><a href="termsandconditions.aspx">Terms & Conditions</a> -  <a href="PrivacyPolicy.aspx">Privacy Policy</a> -  <a href="FAQS.aspx">FAQS</a> -  <a href="contact-us.aspx">Contact Us</a></div>
    </div>
    <div class="txt">
      <blockquote>
        <p>All rights reserved. Copyright Gopalan Cinemas 2012<br />
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

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})();
if (document.getElementById("<%=hddShowPopup.ClientID %>").value == "true") {
    show_movie();
}
</script>
</body>
</html>
