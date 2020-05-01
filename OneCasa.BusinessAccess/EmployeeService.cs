using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OneCasa.DataAccess;
using OneCasa.Models.ViewModels;

namespace OneCasa.BusinessAccess
{
    public class EmployeeService : BaseBusinessAccess
    {
        // frequently used data
        private readonly List<Employee>  _employeesDate;
        private int day = 10;
        private LeaveServices _leaveServices;

        public   EmployeeService()
        {
            _leaveServices = new LeaveServices();
            _employeesDate = _GetEmployeeData();
        }

        private List<Employee> _GetEmployeeData()
        {
            List<Employee> listEmployeeData = new List<Employee>();
            this.operation = () =>
            {
                EmplyeeRepopsitory access = new EmplyeeRepopsitory(this.Transaction);
                listEmployeeData = access.GetEmployeeData();
            };
            this.Start(false);
            return listEmployeeData;
        }

        public List<Events> GetUpcomingEvents()
        {
            List<Events> employees = GetEmployeeData().Where(e=> new DateTime(DateTime.Today.Year,e.DateOfBirth.Month,e.DateOfBirth.Day) <= DateTime.Today.AddDays(day) && 
                         new DateTime(DateTime.Today.Year,e.DateOfBirth.Month,e.DateOfBirth.Day) >= DateTime.Today)
                .Select(e => new Events()
                {
                    Name = e.EmpName,
                    EventName = "UpcomingBirthdays",
                    Date = e.DateOfBirth,
                    EventDate = new DateTime(DateTime.Today.Year,e.DateOfBirth.Month,e.DateOfBirth.Day)
                }).ToList();
            List<Events> ani = GetEmployeeData().Where(e =>
                new DateTime(DateTime.Today.Year, e.JoinDate.Month, e.JoinDate.Day) <= DateTime.Today.AddDays(day) &&
                new DateTime(DateTime.Today.Year, e.JoinDate.Month, e.JoinDate.Day) >= DateTime.Today &&
                e.JoinDate.Year < DateTime.Today.Year)
                .Select(e => new Events() 
                {
                    Name = e.EmpName,
                    Date = e.JoinDate,
                    EventName = "UpcomingAnniversary",
                    EventDate = new DateTime(DateTime.Today.Year,e.JoinDate.Month,e.JoinDate.Day)   
                }).ToList();
            employees.AddRange(ani);
            
            List<Events> holidays = _leaveServices.GetPublicHolidays().
                Where(h=> h.Date >= DateTime.Today && h.Date <= DateTime.Today.AddDays(day)).Select(h => new Events()
            {
                Name = h.Name,
                EventName = "PublicHoliday",
                EventDate = h.Date,
            }).ToList();
            employees.AddRange(holidays);
            return employees.OrderBy(e=>e.EventDate).ThenBy(e=>e.Name).ToList();
        }

        public List<Events> GetPastEvents()
        {
            List<Events> employees = GetEmployeeData().Where(e =>
                new DateTime(DateTime.Today.Year, e.DateOfBirth.Month, e.DateOfBirth.Day) >=
                DateTime.Today.AddDays(-day) &&
                new DateTime(DateTime.Today.Year, e.DateOfBirth.Month, e.DateOfBirth.Day) < DateTime.Today)
                .Select(e => new Events()
                {
                    Name = e.EmpName,
                    EventName = "PastBirthdays",
                    Date = e.DateOfBirth,
                    EventDate = new DateTime(DateTime.Today.Year,e.DateOfBirth.Month,e.DateOfBirth.Day)
                })
                .ToList();
            
            
            
            List<Events> ani = GetEmployeeData().Where(e =>
                new DateTime(DateTime.Today.Year, e.JoinDate.Month, e.JoinDate.Day) > DateTime.Today.AddDays(-day) &&
                new DateTime(DateTime.Today.Year, e.JoinDate.Month, e.JoinDate.Day) < DateTime.Today &&
                e.JoinDate.Year < DateTime.Today.Year && e.JoinDate.Day != DateTime.Today.Day)
                .Select(e => new Events() 
                {
                    Name = e.EmpName,
                    Date = e.JoinDate,
                    EventName = "PastAnniversary",
                    EventDate = new DateTime(DateTime.Today.Year,e.JoinDate.Month,e.JoinDate.Day)
                })
                .ToList();
            employees.AddRange(ani);

            List<Events> holidays = _leaveServices.GetPublicHolidays().Where(h=> h.Date < DateTime.Today && h.Date >= DateTime.Today.AddDays(-day)).Select(h => new Events()
            {
                Name = h.Name,
                EventName = "PublicHoliday",
                EventDate = h.Date,
            }).ToList();
            employees.AddRange(holidays);
            return employees.OrderByDescending(e => e.EventDate).ThenBy(e => e.Name).ToList();
        }
        
        public List<Employee> GetEmployeeData()
        {
            return _employeesDate;
        }
        

        public void AddEmployee(EmployeeAddress emp)
        {
            this.operation = () =>
            {
                EmplyeeRepopsitory access = new EmplyeeRepopsitory(this.Transaction);
                access.AddEmployee(emp);
            };
            this.Start(false);
        }

        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            this.operation = () =>
            {
                EmplyeeRepopsitory access = new EmplyeeRepopsitory(this.Transaction);
                departments = access.GetDepartments();
            };
            this.Start(false);
            return departments;
        }


        public EmployeeAddress GetEmployee(int id)
        {
            EmployeeAddress emp=new EmployeeAddress();
            this.operation = () =>
            {
                EmplyeeRepopsitory access = new EmplyeeRepopsitory(this.Transaction);
                emp = access.GetEmployee(id).FirstOrDefault();

            };
            this.Start(false);
            return emp;
        }

        public void EditEmployee(EmployeeAddress emp)
        {
            this.operation = () =>
            {
                EmplyeeRepopsitory access = new EmplyeeRepopsitory(this.Transaction);
                access.EditEmployee(emp);
            };
            this.Start(false);
        }


    }
}