let months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
$.ajax({
        url: "/Home/GetUpcomingEvents",
        method:"GET",
        dataType: "Json",
        beforeSend:function(){
                $(".loader").show();
        },
        complete:function () {
            setInterval(() => {
                 $(".loader").hide();   
            }, 500);
        },
        success: function (response) 
        {
            let html=``;
            console.log("yo");
            
            if(response.length!=0)
            {
                $.each(response, function (indexInArray, valueOfElement) { 

                    if(valueOfElement.EventName == "UpcomingBirthdays")
                    {
                        let d=new Date(parseInt(  valueOfElement.Date.replace('/Date(', '')));
                            html+=`<li> <img src="../../Content/Images/birthday.png" alt="png" width=20px height=20px>
                            ${valueOfElement.Name} BirthDay on ${d.getDate()} 
                            ${months[d.getMonth()].substring(0,3)}
                            ${new Date().getFullYear()}
                            </li><br/>`
                    }
                    else if(valueOfElement.EventName == "UpcomingAnniversary")
                    {
                        let d=new Date(parseInt(  valueOfElement.Date.replace('/Date(', '')));
                            html+=`<li> <img src="../../Content/Images/anniversary.png" alt="png" width=20px height=20px>
                            ${valueOfElement.Name} Complete ${new Date().getFullYear() - d.getFullYear() } Year at ONEBCG  ${d.getDate()} on ${months[d.getMonth()].substring(0,3)}
                            ${new Date().getFullYear()}
                            </li><br/>`
                    }
                    else
                    {
                        let d=new Date(parseInt(  valueOfElement.EventDate.replace('/Date(', '')));
                            html+=`<li> <img src="../../Content/Images/holiday.png" alt="png" width=30px height=30px>
                            ${valueOfElement.Name} Holiday on  ${d.getDate()} ${months[d.getMonth()].substring(0,3)} 
                            ${new Date().getFullYear()}
                            </li><br/>`
                    }
                });
            }
             $("#upcoming-events ul").html(html);
        
}});




$.ajax({
    url: "/Home/GetPastEvents",
    method:"GET",
    dataType: "Json",
    beforeSend:function(){
        console.log("before send")
    },
    complete:function () {
        console.log('complete')
    },
    success: function (response) {
        let html=``;
        if(response.length!=0){
        
            $.each(response, function (indexInArray, valueOfElement) { 
                if(valueOfElement.EventName == "PastBirthdays")
                {
                    let d=new Date(parseInt(  valueOfElement.Date.replace('/Date(', '')));
                        html+=`<li> <img src="../../Content/Images/birthday.png" alt="png" width=20px height=20px>
                        ${valueOfElement.Name} BirthDay on ${d.getDate()} 
                        ${months[d.getMonth()].substring(0,3)}
                        ${new Date().getFullYear()}
                        </li><br/>`
                }
                else if(valueOfElement.EventName == "PastAnniversary")
                {
                    let d=new Date(parseInt(  valueOfElement.Date.replace('/Date(', '')));
                        html+=`<li> <img src="../../Content/Images/anniversary.png" alt="png" width=20px height=20px>
                        ${valueOfElement.Name} Complete ${new Date().getFullYear() - d.getFullYear() } Year at ONEBCG  ${d.getDate()} ${months[d.getMonth()].substring(0,3)}
                        ${new Date().getFullYear()}
                        </li><br/>`
                }
                else
                {
                    let d=new Date(parseInt(  valueOfElement.EventDate.replace('/Date(', '')));
                        html+=`<li> <img src="../../Content/Images/holiday.png" alt="png" width=30px height=30px>
                        ${valueOfElement.Name} Holiday on  ${d.getDate()} ${months[d.getMonth()].substring(0,3)} 
                        ${new Date().getFullYear()}
                        </li><br/>`
                }
            });
            $("#past-events ul").html(html);
        }
}});




