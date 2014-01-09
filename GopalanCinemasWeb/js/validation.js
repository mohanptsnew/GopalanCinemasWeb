function ValidProceed() {
    alert('d');
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
function displaycity() {

    document.getElementById('pop_div').style.display = "block";
    document.getElementById("html_pop").style.display = "block";
    document.getElementById('pop_div').style.height = docht() + "px";
}
