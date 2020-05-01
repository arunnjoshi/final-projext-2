using System;
using System.ComponentModel;

namespace OneCasa.Models.ViewModels
{
    public class Employee
    {
        [DisplayName("Employee Id")]
        public int EmpId  { set; get; }
        [DisplayName("Employee Name")]
        public string EmpName  { set; get; }
        [DisplayName("Reporting Manager")]
        public string Manager  { set; get; }
        [DisplayName("Department")]
        public string Department   { set; get; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        [DisplayName("Date of Join")]
        public DateTime JoinDate { get; set; }
        [DisplayName("Mobile Number")]
        public Int64 PhoneNumber { get; set; }


        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
    }
}