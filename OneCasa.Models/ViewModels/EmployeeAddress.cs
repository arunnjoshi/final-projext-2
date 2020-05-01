using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OneCasa.Models.ViewModels
{
    public class EmployeeAddress
    {
        [DisplayName("Employee Id")]
        [Required]
        public int EmpId  { set; get; }
        
        
        
        [DisplayName("Employee Name")]
        [Required]
        [MaxLength(20),MinLength(3)]
        public string EmpName  { set; get; }
        
        
        
        [DisplayName("Gender")]
        [Required]
        public string   Gender { get; set; }
        
        [DisplayName("Reporting Manager")]
        [Required]
        [MaxLength(20),MinLength(3)]

        public string Manager  { set; get; }
        
        
        
        [DisplayName("Department")]
        [Required]
        public int Department   { set; get; }
        
        
        
        
        [DisplayName("Email")]
        [Required]
        public string Email { get; set; }
        
        
        
        
        [Required]
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        
        
        
        
        [Required]
        [DisplayName("Date of Join")]
        public DateTime JoinDate { get; set; }
        
        
        
        [Required]
        [DisplayName("Mobile Number")]
        [MaxLength(10,ErrorMessage = "please enter valid Number"),MinLength(10,ErrorMessage = "please enter valid Number")]
        public string PhoneNumber { get; set; }
        
        
        
        
        [Required]
        [DisplayName("Address")]
        [MaxLength(100),MinLength(20)]

        public string Address { get; set; }
        
        
        
        [Required]
        [DisplayName("Pin code")]
        [MaxLength(6,ErrorMessage = "please enter valid pin code"),MinLength(6,ErrorMessage = "please enter valid pin code")]
        public string PinCode { get; set; }
        
        
        
        [Required]
        [DisplayName("State")]
        [MaxLength(20),MinLength(3)]
        public string State { get; set; }
        
        
        
        [Required]
        [DisplayName("Country")]
        [MaxLength(20),MinLength(3)]
        public string Country { get; set; }
       
    }
}