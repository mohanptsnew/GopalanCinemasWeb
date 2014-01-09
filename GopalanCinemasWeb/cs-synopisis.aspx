<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/GopalanWeb.Master" CodeBehind="cs-synopisis.aspx.cs" Inherits="GopalanCinemasWeb.cs_synopisis" %>

<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <script language="javascript">
        document.getElementById("aMovie").className = "active";
    </script>
    <script type="text/JavaScript">
<!--
        var curr = "";
        var currplus = "";
        function toggle(val, plus) {

            if (document.getElementById(val).style.display == 'none') {

                document.getElementById(val).style.display = 'block';
                document.getElementById(plus).src = "../images/movies/dwn_arrow.gif";
                if (curr != "") {
                    document.getElementById(curr).style.display = 'none';
                    document.getElementById(currplus).src = "../images/movies/dwn_arrow.gif";
                }
                document.getElementById(plus).src = "../images/movies/dwn_arrow.gif";

                curr = val;
                currplus = plus;
            } else {

                document.getElementById(val).style.display = 'none';
                document.getElementById(plus).src = '../images/movies/up_arrow.gif';
                curr = "";
                currplus = "";
            }
        }

        function MM_openBrWindow(theURL, winName, features) { //v2.0
            window.open(theURL, winName, features);
        }
//-->
</script>
    <div class="content_in_sp">
    <div class="sp-menu">
    <div class="now-showing"><a href="movies.aspx">Now Showing</a></div>
    <div class="coming-soon"><a href="movies.aspx" class="active">Coming Soon</a></div>
    </div>
    <div  class="content_in_sp2">
       <cite><a href="movies.aspx">Back</a></cite>
       <div class="movie_blk1">
    <div class="tmb"><img src="../images/coming-soon/tmb1.jpg" /></div>
    <div class="txt">
    <h1>Tanu Weds Manu (Hindi)</h1>
    <h4>Starcast:</h4>
    <p>R. Madhavan, Kangana Ranuat, Jimmy Shergil</p>
    <h4>Director:</h4>
    <p>Anand L. Rai</p>
    <span><a href="payment.aspx"><img src="../images/movies/btn_book-now.gif" /></a></span>
    </div>
    </div>
    <div class="cont_blk">
    <h2  style="cursor:pointer;" onClick="toggle('synopsis','plus1');">Synopsis <img src="../images/movies/up_arrow.gif" id="plus1" /></h2>
    <p id="synopsis" style="display:none">Marriages are made in heaven………though the journey be 'tedha'. Manoj Sharma (a.k.a) A seedha-saadha an NRI doctor from London. The epitome of the 'good boy', who comes to India under 'family pressure' to meet……….Tanuja trivedi (a.k.a Tanu) A smart, intelligent, rebellious girl who does everythinhg her parents disapprove. Dead against arranged marriage. And when the twain shall meet, sparks shall fly in the heartland of India.</p> 
    </div>
    
    <div class="cont_blk">
    <h2  style="cursor:pointer;" onClick="toggle('trailer','plus2');">Trailer <img src="../images/movies/up_arrow.gif" id="plus2" /></h2>
    <p id="trailer" style="display:none"><img src="../images/video-img.jpg" /></p> 
    </div>
    
    </div>
    </div>

</asp:Content>


