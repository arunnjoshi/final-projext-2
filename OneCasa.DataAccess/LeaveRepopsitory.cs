using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using OneCasa.Models.ViewModels;

namespace OneCasa.DataAccess
{
    public class LeaveRepopsitory : BaseDataAccess
    {
        public LeaveRepopsitory(BaseDataAccess baseaccess)
            : base(baseaccess)
        {
        }

        public List<PublicHolidays> GetPublicHolidays()
        {
            DBParameters.Clear();
            
            DateTime yearStart = new DateTime(DateTime.Now.Year,1,1);
            DateTime yearEnd = new DateTime(DateTime.Now.Year,12,31);
            AddParameter("@yearStart",yearStart);
            AddParameter("@yearEnd",yearEnd);
            
            DataSet deplist = ExecuteDataSet("sp_GetPublicHolidays");
            List<PublicHolidays> holidayses = deplist.Tables[0].AsEnumerable().Select(h=>new PublicHolidays()
            {
                Date = h.Field<DateTime>("date"),
                Name = h.Field<string>("name")
            }).OrderBy(h=>h.Date).ToList();
            return holidayses;
        }


        public void ApplyLeave(Leave leave)
        {
            DBParameters.Clear();

            AddParameter("@emailId",leave.Email);
            AddParameter("@fromDate",leave.FromDate);
            AddParameter("@toDate",leave.ToDate);
            AddParameter("@dayCount",DayCount(leave.FromDate,leave.ToDate));
            AddParameter("@comment",leave.Comment);
            AddParameter("@leaveType",leave.LeaveType);
            AddParameter("@leaveStatus","pending");
            AddParameter("@StartTime",DateTime.Today.TimeOfDay);
            AddParameter("@EndTime",DateTime.Today.TimeOfDay);

            ExecuteNonQuery("sp_ApplyLeave");
        }
        
        
        public int DayCount(DateTime startDate,DateTime endDate)
        {
            int count = 1;
            while (startDate != endDate)
            {
                if (startDate.DayOfWeek.ToString() == "Saturday" || startDate.DayOfWeek.ToString() == "Sunday")
                {
                    startDate=startDate.AddDays(1);
                    continue;
                }
                count += 1;
                startDate=startDate.AddDays(1);
            }

            return count;
        }
        
        public List<Leave> GetApplyedLeaves()
        {
            DBParameters.Clear();

            AddParameter("@startDate",DateTime.Today.AddDays(-15));
            AddParameter("@endDate",DateTime.Today.AddDays(15));
            DataSet dataSet =  ExecuteDataSet("sp_GetLeaves");
            List <Leave> leaves= dataSet.Tables[0].AsEnumerable().Select(l=>new Leave()
            {
                EmpName = l.Field<string>("emp_Name"),
                Department = l.Field<string>("Department"),
                FromDate = l.Field<DateTime>("fromDate"),
                ToDate = l.Field<DateTime>("toDate"),
                DayCount = l.Field<int>("dayCount"),
                Comment = l.Field<string>("comment"),
                LeaveType = l.Field<string>("leaveType"),
                LeaveStatus = l.Field<string>("leaveStatus"),
            }).ToList();

            return leaves;
        }



        public List<Leave> GetLeaveStatus(string emailId)
        {
            DBParameters.Clear();
            
            
            AddParameter("@emailId",emailId);
            DataSet deplist = ExecuteDataSet("sp_GetPendingLeaves");
            List<Leave> leaves = deplist.Tables[0].AsEnumerable().Select(l=>new Leave()
            {
                
                Email = l.Field<string>("emailId"),
                FromDate = l.Field<DateTime>("fromDate"),
                ToDate = l.Field<DateTime>("toDate"),
                DayCount = l.Field<int>("dayCount"),
                Comment = l.Field<string>("comment"),
                LeaveType = l.Field<string>("leaveType"),
                LeaveStatus = l.Field<string>("leaveStatus"),
            }).ToList();
            return leaves;
        }
        
        
        public List<Leave> GetLeaveHistory(string emailId)
        {
            DBParameters.Clear();
            
            
            AddParameter("@emailId",emailId);
            DataSet deplist = ExecuteDataSet("sp_LeavesHistory");
            List<Leave> leaves = deplist.Tables[0].AsEnumerable().Select(l=>new Leave()
            {
                
                Email = l.Field<string>("emailId"),
                FromDate = l.Field<DateTime>("fromDate"),
                ToDate = l.Field<DateTime>("toDate"),
                DayCount = l.Field<int>("dayCount"),
                Comment = l.Field<string>("comment"),
                LeaveType = l.Field<string>("leaveType"),
                LeaveStatus = l.Field<string>("leaveStatus"),
            }).ToList();
            return leaves;
        }

        
    }
}