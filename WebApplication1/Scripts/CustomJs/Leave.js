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
            let html = "<tr>";
            $.each(response, function (indexInArray, valueOfElement) {
                let fromDate = new Date(parseInt(valueOfElement.FromDate.replace('/Date(', '')));
                let todate = new Date(parseInt(valueOfElement.ToDate.replace('/Date(', '')));
                html += `
                 <td>${ fromDate.getDate()}/${fromDate.getMonth()}/${fromDate.getFullYear()}</td>
                 <td>${ todate.getDate()}/${todate.getMonth()}/${todate.getFullYear()}</td>
                 <td>${valueOfElement.LeaveType}</td>
                 <td>${valueOfElement.Comment}</td>
                 <td>${valueOfElement.LeaveStatus}</td>
                 <td><input type="button" value="Edit" class="btn btn-link"> | <input type="button" value="Cancel Leave" class="btn btn-link"></td>
                 </tr>
                 `;
            });
            $(".pending-leaves table tbody").html(html);
        }
    }
});



$.ajax({
    url:`/Leaves/GetLeaveHistory?emailId=${mail}`,
    method:"GET",
    dataType: "Json",
    success: function (response) {
         console.log(response);
        if (response.length != 0)
        {
            let html = "<tr>";
            $.each(response, function (indexInArray, valueOfElement) {
                let fromDate = new Date(parseInt(valueOfElement.FromDate.replace('/Date(', '')));
                let todate = new Date(parseInt(valueOfElement.ToDate.replace('/Date(', '')));
                html += `
             <td>${ fromDate.getDate()}/${fromDate.getMonth()}/${fromDate.getFullYear()}</td>
             <td>${ todate.getDate()}/${todate.getMonth()}/${todate.getFullYear()}</td>
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

