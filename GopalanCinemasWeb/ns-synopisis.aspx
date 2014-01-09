<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/GopalanWeb.Master" CodeBehind="ns-synopisis.aspx.cs" Inherits="GopalanCinemasWeb.ns_synopisis" %>

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
    <div class="now-showing"><a href="movies.aspx" class="active">Now Showing</a></div>
    <div class="coming-soon"><a href="movies.aspx">Coming Soon</a></div>
    </div>
    <div  class="content_in_sp2">
       <cite><a href="movies.aspx">Back</a></cite>
       <div class="movie_blk1">
    <div class="tmb"><img src="../images/now-showing/tmb2.jpg" /></div>
    <div class="txt">
    <h1>Black Swan (English) (A)</h1>
    <h4>Starcast:</h4>
    <p>Natalie Portman, Mila Kunis, Vincent Cassel</p>
    <h4>Director:</h4>
    <p>Darren Aronofsky</p>
    <span><a href="payment.aspx"><img src="../images/movies/btn_book-now.gif" /></a></span>    </div>
    </div>
    <div class="cont_blk">
    <h2  style="cursor:pointer;" onClick="toggle('synopsis','plus1');">Synopsis <img src="../images/movies/up_arrow.gif" id="plus1" /></h2>
    <p id="synopsis" style="display:none">Nina (Portman) is a ballerina in a New York City ballet company whose life, like all those in her profession, is completely consumed with dance. She lives with her obsessive former ballerina mother Erica (Hershey) who exerts a suffocating control over her.</p> 
    </div>
    
    <div class="cont_blk">
    <h2  style="cursor:pointer;" onClick="toggle('trailer','plus2');">Trailer <img src="../images/movies/up_arrow.gif" id="plus2" /></h2>
    <p id="trailer" style="display:none"><img src="../images/video-img.jpg" /></p> 
    </div>
    
    </div>
    </div>
   
</asp:Content>


