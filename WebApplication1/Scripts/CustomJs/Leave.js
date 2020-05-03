var mail = $("#Email").val();
$(".LeaveMenu").click(function () {
    $("#viewLeaves").hide();
    $("#ApplyLeaves").hide();
    $("#PendingLeaves").hide();
    $("#LeavesStatus").hide();
    $("#PublicHolidays").hide();
    var id=$(this).val();
    $(`#${id}`).fadeIn(500);
    
    // console.log($(this).val());
});

$(".leave-cell").click(function (e) { 
    e.preventDefault();
    // console.log($(this).children()[0].innerHTML);
    $("#aboutLeaveModal .modal-body").html($(this).children()[0].innerHTML);
    $("#aboutLeaveModal").modal('show');
});


$.ajax({
    url:`/Leaves/getpendingLeaves?emailId=${mail}`,
    method:"GET",
    dataType: "Json",
    success: function (response) {

        // console.log(response);
        if (response.length != 0) {
            let leave = {};
            let html = "<tr>";
            $.each(response, function (indexInArray, valueOfElement) {
                let fromDate = new Date(parseInt(valueOfElement.FromDate.replace('/Date(', '')));
                let todate = new Date(parseInt(valueOfElement.ToDate.replace('/Date(', '')));
                html += `
                 <td>${fromDate.toDateString()}</td>
                 <td>${ todate.toDateString()}</td>
                 <td>${valueOfElement.LeaveType}</td>
                 <td>${valueOfElement.Comment}</td>
                 <td>${valueOfElement.LeaveStatus}</td>
                 <td>
                    <input type="button" value="Cancel Leave" class="btn btn-link cancel-btn" ></td>
                 </tr>
                 `;
            });
            $(".pending-leaves table tbody").html(html);
        }
    }
});


$('.pending-leaves table tbody').on('click', '.cancel-btn', function() 
{
    let tableCell= $(this).parent(0).parent(0).children();
    
    let leave={
        "FromDate" : tableCell[0].innerText,
        "ToDate" : tableCell[1].innerText,
        "LeaveType": tableCell[2].innerText,
        "Comment": tableCell[3].innerText,
        "LeaveStatus": tableCell[4].innerText,
    };

    $("#myModal").modal({                   
        "backdrop"  : "static",
        "keyboard"  : true,
        "show"      : true                     
    });
    
    
   
    $("#myModal").on("click",".ok-btn",function (e) {
        $.post("/leaves/CancelLeave",leave,function (e,res) {
            // console.log(res);
            // console.log(e);
            window.location.replace("");
        });
    });
    // console.log(tableCell);
    // console.log(leave);
    
});



$('.pending-leaves table tbody').on('click', '.edit-btn', function() {
   console.log("yo"); 
  
});





$.ajax({
    url:`/Leaves/GetLeaveHistory?emailId=${mail}`,
    method:"GET",
    dataType: "Json",
    success: function (response) {
         //console.log(response);
        if (response.length != 0)
        {
            let html = "<tr>";
            $.each(response, function (indexInArray, valueOfElement) {
                let fromDate = new Date(parseInt(valueOfElement.FromDate.replace('/Date(', '')));
                let todate = new Date(parseInt(valueOfElement.ToDate.replace('/Date(', '')));
                html += `
             <td>${fromDate.toDateString()}</td>
             <td>${todate.toDateString()}</td>
             <td>${valueOfElement.LeaveType}</td>
             <td>${valueOfElement.Comment}</td>
             <td>${valueOfElement.LeaveStatus}</td>
             </tr>
             `;
            });
            $(".leave-history table tbody").html(html);
        }
    }
});























