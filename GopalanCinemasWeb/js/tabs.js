/***************************/
//@Author: Adrian "yEnS" Mato Gondelle & Ivan Guardado Castro
//@website: www.yensdesign.com
//@email: yensamg@gmail.com
//@license: Feel free to use it, but keep this credits please!					
/***************************/

$(document).ready(function(){
	$(".tab > li").click(function(e){
		switch(e.target.id){
			case "debit":
				//change status & style menu
				$("#debit").addClass("active");
				$("#credit").removeClass("active");
				$("#net").removeClass("active");
				//display selected division, hide others
				$("div.debit").fadeIn();
				$("div.credit").css("display", "none");
				$("div.net").css("display", "none");
			break;
			case "credit":
				//change status & style menu
				$("#debit").removeClass("active");
				$("#credit").addClass("active");
				$("#net").removeClass("active");
				//display selected division, hide others
				$("div.credit").fadeIn();
				$("div.debit").css("display", "none");
				$("div.net").css("display", "none");
			break;
			case "net":
				//change status & style menu
				$("#debit").removeClass("active");
				$("#credit").removeClass("active");
				$("#net").addClass("active");
				//display selected division, hide others
				$("div.net").fadeIn();
				$("div.debit").css("display", "none");
				$("div.credit").css("display", "none");
			break;
		}
		//alert(e.target.id);
		return false;
	});
});

function ValidProceed() {
    if (document.getElementById("<%=ddlMoviesChinema.ClientID %>").value == "0") {
        alert("Select Cinema");
        document.getElementById("<%=ddlMoviesChinema.ClientID %>").focus();
        return false;
    }
    if (document.getElementById("<%=ddlMoviesDate.ClientID %>").value == "0") {
        alert("Select Date");
        document.getElementById("<%=ddlMoviesDate.ClientID %>").focus();
        return false;
    }
    if (document.getElementById("<%=ddlMoviesShowTime.ClientID %>").value == "0") {
        alert("Select Show Time");
        document.getElementById("<%=ddlMoviesShowTime.ClientID %>").focus();
        return false;
    }
    if (document.getElementById("<%=ddlMoviesSeats.ClientID %>").value == "0") {
        alert("Select Seat");
        document.getElementById("<%=ddlMoviesSeats.ClientID %>").focus();
        return false;
    }
    if (document.getElementById("<%=hddGetFilmCode.ClientID %>").value == "") {
        alert("Select Movies");
        return false;
    }
}
