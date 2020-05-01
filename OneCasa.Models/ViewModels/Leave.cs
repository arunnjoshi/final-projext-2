using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OneCasa.Models.ViewModels
{
    public class Leave
    {
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        
        
        public int DayCount { get; set; }
        
        public string EmpName { get; set; }
        
        

        public string Department { get; set; }
        
        
        
        
        public string LeaveStatus { get; set; }
        
        
        
        
        [Required]
        [DisplayName("Leave Type")]

        public string LeaveType { get; set; }
        
        
        [Required]
        [DisplayName("From Date")]

        public DateTime FromDate { get; set; }
        
        
        [Required]
        [DisplayName("To Date")]
        public DateTime ToDate { get; set; }


        [Required]
        [DisplayName("Comment")]

        [MinLength(10 ,ErrorMessage = "Min Letters 10"),MaxLength(100,ErrorMessage = "Max Letters 100")]
        public string Comment { get; set; }
    }
}