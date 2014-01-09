function changemov(s)
{
	for(i=1;i<=4;i++){
		if(s=="s"+i+""){
			document.getElementById("mov"+i+"").className="tab_select_in2";
			document.getElementById("block"+i+"").style.display='block';
		}
		else{
			document.getElementById("mov"+i+"").className="tab_unselect_in2";
			document.getElementById("block"+i+"").style.display='none';
		}		
	}	 
}
function changepress(p)
{
	for(i=1;i<=2;i++){
		if(p=="p"+i+""){
			document.getElementById("pres"+i+"").className="tab_press_in2";
			document.getElementById("block1"+i+"").style.display='block';
		}
		else{
			document.getElementById("pres"+i+"").className="tab_unpress_in2";
			document.getElementById("block1"+i+"").style.display='none';
		}		
	}	 
}

