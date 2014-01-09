<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/GopalanWeb.Master" CodeBehind="contact-us.aspx.cs" Inherits="GopalanCinemasWeb.contact_us" %>
<asp:Content ID="Content1" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
    <script language="javascript">
        document.getElementById("aContact").className = "active";
    </script>
    <script language="javascript" type="text/javascript">
        function fndisplaydata(id, cls) {
            document.getElementById("cont").style.display = "none";
            document.getElementById("up").style.display = "none";
            document.getElementById("co").className = "";
            document.getElementById("u").className = "";
            document.getElementById(id).style.display = "block";
            document.getElementById(cls).className = "active";
        }
</script>
        <div class="content_in_sp">
    <div class="sp-menu">
    <div class="contact-us"><a href="javascript:void(0)" id="co" class="active" onclick="fndisplaydata('cont','co')">Contact Us</a></div>
    <div class="upcoming"><a href="javascript:void(0)" id="u" onclick="fndisplaydata('up','u')">Upcoming Mall</a></div>
    </div>
    <div class="content_in_sp1" id="cont">
    <div class="contact_blk">
    <div class="contact_blk_in">
    <div class="img"><img src="../images/contact/innovation-mall_img.jpg" /></div>
    <div class="cnt_blk">
    <p><span>Gopalan innovation Mall</span><br />Bannerghatta Road,<br /> Bangalore</p>
    <p><span>TEL:</span> 080-2227 7121 / 2229 / 64</p>
    <p><span>E-MAIL:</span> <a href="mailto:marketing@gopalanenterprises.com">marketing@gopalanenterprises.com</a></p>
    <p><br /> <a href="http://maps.google.co.in/maps/place?q=Gopalan+innovation+Mall+Bannerghatta+Road,+Bangalore&hl=en&cid=5944645196217811528" target="_blank"><img src="../images/contact/innovation-mall.jpg" /></a></p>
    </div>
    </div>
    </div>
    <div class="contact_blk">
    <div class="contact_blk_in">
    <div class="img"><img src="../images/contact/mall_sirsi-circle_img.jpg" /></div>
    <div class="cnt_blk">
    <p><span>Gopalan Mall</span><br />Sirsi Circle, Mysore Road<br /> Bangalore</p>
    <p><span>TEL:</span> 080-2227 7121 / 2229 / 64</p>
    <p><span>E-MAIL:</span> <a href="mailto:marketing@gopalanenterprises.com">marketing@gopalanenterprises.com</a></p>
    <p><br /> <a href="http://maps.google.co.in/maps/place?q=Gopalan+Mall:+Sirsi+Circle,+Mysore+Road,+Bangalore&hl=en&cid=12935649779508464571" target="_blank"><img src="../images/contact/mall_sirsi-circle.jpg" /></a></p>
    </div>
    </div>
    </div>
    <div class="contact_blk" style="background:none">
    <div class="contact_blk_in">
    <div class="img"><img src="../images/contact/arch-mall_img.jpg" /></div>
    <div class="cnt_blk">
    <p><span>Arch Mall</span><br />RR Nagar, Mysore Road<br /> Bangalore</p>
    <p><span>TEL:</span> 080-2227 7121 / 2229 / 64</p>
    <p><span>E-MAIL:</span> <a href="mailto:marketing@gopalanenterprises.com">marketing@gopalanenterprises.com</a></p>
    <p><br /> <a href="http://maps.google.co.in/maps/place?q=Gopalan+Cinemas:+Arch+Mall,+RR+Nagar,+Mysore+Road,+Bangalore&hl=en&cid=6571840073366378877" target="_blank"><img src="../images/contact/arch-mall.jpg" /></a></p>
    </div>
    </div>
    </div>
    </div>
    
    <div  class="content_in_sp1" id="up" style="display:none">
    <div class="contact_blk"  style="background:none; display:none">
    <div class="contact_blk_in">
    <div class="img"><img src="../images/contact/arch-mall_img.jpg" /></div>
    <div class="cnt_blk">
    <p><span>Arch Mall</span><br />RR Nagar, Mysore Road<br /> Bangalore</p>
    <p><span>TEL:</span> 080-2227 7121 / 2229 / 64</p>
    <p><span>E-MAIL:</span> <a href="mailto:marketing@gopalanenterprises.com">marketing@gopalanenterprises.com</a></p>
    <p><br /> <a href="#"><img src="../images/contact/arch-mall.jpg" /></a></p>
    </div>
    </div>
    </div>
    </div>
    </div>
</asp:Content>


