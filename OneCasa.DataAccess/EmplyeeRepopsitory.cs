using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using OneCasa.Models.ViewModels;
using System.Data.SqlClient;
using System.Linq;

namespace OneCasa.DataAccess
{
    public class EmplyeeRepopsitory : BaseDataAccess
    {
        public EmplyeeRepopsitory(BaseDataAccess baseaccess)
            : base(baseaccess)
        {

        }
        public List<Employee> GetEmployeeData()
        {
            DBParameters.Clear();
            DataSet empList = ExecuteDataSet("sp_GetAllEmlpoyees");
            List<Employee> allemp = empList.Tables[0].AsEnumerable().Select(emp=>new Employee()
            {
                EmpId = emp.Field<int>("emp_id"),
                EmpName = emp.Field<string>("emp_name"),
                Manager = emp.Field<string>("Manager"),
                Department = emp.Field<string>("department"),
                Email = emp.Field<string>("Emailid"),
                DateOfBirth = emp.Field<DateTime>("DateOfBirth"),
                JoinDate = emp.Field<DateTime>("dateofjoin"),
                PhoneNumber=emp.Field<Int64>("PhoneNumber")
            }).ToList();
            return (allemp);
        }
        public void AddEmployee(EmployeeAddress emp)
        {
            DBParameters.Clear();
            AddParameter("@emp_Name",emp.EmpName);
            AddParameter("@gender",emp.Gender);
            AddParameter("@EmailId",emp.Email);
            AddParameter("@DateOfBirth",emp.DateOfBirth);
            AddParameter("@DepId",1);
            AddParameter("@Manager",emp.Manager);
            AddParameter("@PhoneNumber",Int64.Parse(emp.PhoneNumber));
            AddParameter("@pincode",Int32.Parse(emp.PinCode));
            AddParameter("@Address",emp.Address);
            AddParameter("@state",emp.State);
            AddParameter("@country",emp.Country);
            ExecuteNonQuery("sp_AddEmployee");
        }

        public List<Department> GetDepartments()
        {
            DBParameters.Clear();
            DataSet deplist = ExecuteDataSet("sp_GetAllDepartments");
            List<Department> departments = deplist.Tables[0].AsEnumerable().Select(dep=>new Department()
            {
               DepId = dep.Field<int>("depId"),
               DepartmentName = dep.Field<string>("department")
            }).ToList();
            return departments;
        }

        public List<EmployeeAddress> GetEmployee(int id)
        {
            DBParameters.Clear();
            
            AddParameter("@empId" ,id);
            DataSet table = ExecuteDataSet("sp_getEmployee");
            List<EmployeeAddress> employee =  table.Tables[0].AsEnumerable().Select(emp=>new EmployeeAddress()
            {
                EmpId = emp.Field<int>("emp_id"),
                Gender = emp.Field<string>("gender"),
                EmpName = emp.Field<string>("emp_name"),
                Manager = emp.Field<string>("Manager"),
                Department = emp.Field<int>("depId"),
                Email = emp.Field<string>("Emailid"),
                DateOfBirth = emp.Field<DateTime>("DateOfBirth"),
                JoinDate = emp.Field<DateTime>("dateofjoin"),
                PhoneNumber=emp.Field<Int64>("PhoneNumber").ToString(),
                Address = emp.Field<string>("address"),
                PinCode = emp.Field<int>("pincode").ToString(),
                State = emp.Field<string>("state"),
                Country=emp.Field<string>("country")
            }).ToList();
           return employee;
        }

        public void EditEmployee(EmployeeAddress emp)
        {
            DBParameters.Clear();
            
            AddParameter("@emp_id",emp.EmpId);
            AddParameter("@gender",emp.Gender);
            AddParameter("@emp_Name ",emp.EmpName);
            AddParameter("@EmailId ",emp.Email);
            AddParameter("@DateOfBirth ",emp.DateOfBirth);
            AddParameter("@DepId ",emp.Department);
            AddParameter("@Manager ",emp.Manager);
            AddParameter("@PhoneNumber ",Int64.Parse(emp.PhoneNumber));
            AddParameter("@pincode ",int.Parse(emp.PinCode));
            AddParameter("@Address ",emp.Address);
            AddParameter("@state ",emp.State);
            AddParameter("@country ",emp.Country);
            AddParameter("@joindate ",emp.JoinDate);
            
            ExecuteDataSet("sp_EditEmployee");
        }
        
    }
}