let empObj={};
$.ajax({
    url: "/Home/GetSearchEmployees",
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
    success: function (response) {
        empObj=response;
    },
});  


$("#search").keyup(function (e) {
    e.preventDefault();
    let search_str=$("#search").val().trim().toLowerCase();
    if(search_str.length!=0)
    {
      
        let html=``;
        $.each(empObj, function (indexInArray, emp) { 
           if(
                emp.EmpName.toLowerCase().search(search_str) >= 0 || 
                emp.EmpId.toString().search(search_str) >= 0 ||
                emp.Manager.toLowerCase().search(search_str) >= 0 ||
                emp.PhoneNumber.toString().search(search_str) >= 0 || 
                emp.Email.toLowerCase().search(search_str) >= 0 || 
                emp.Department.toLowerCase().search(search_str) >= 0
            )   
           {
               html+=`<tr>
                        <td>${emp.EmpId}</td>
                        <td>${emp.EmpName}</td>
                        <td>${emp.Department}</td>
                        <td>${emp.Email}</td>
                        <td>${emp.PhoneNumber}</td>
                        <td>${emp.Manager} | <a class="btn btn-link" href="/home/EditEmployee?empid=${emp.EmpId}">Edit</a></td>
                     </tr>`
        
           }
           $("#Emp-tbody").html(html);
        });

    }
    else if(search_str.length == 0){
        let html=``;
            $.each(empObj, function (indexInArray, emp) { 
                {
                    
                    html+=`<tr>
                             <td>${emp.EmpId}</td>
                             <td>${emp.EmpName}</td>
                             <td>${emp.Department}</td>
                             <td>${emp.Email}</td>
                             <td>${emp.PhoneNumber}</td>
                             <td>${emp.Manager} | <a class="btn btn-link" href="/home/EditEmployee?empid=${emp.EmpId}">Edit</a></td>
                          </tr>`
                    
                }
              });
         $("#Emp-tbody").html(html);
    }
});