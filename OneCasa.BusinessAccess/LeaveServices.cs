using System.Collections.Generic;
using OneCasa.DataAccess;
using OneCasa.Models.ViewModels;

namespace OneCasa.BusinessAccess
{
    public class LeaveServices :BaseBusinessAccess
    {
        public List<PublicHolidays> GetPublicHolidays()
        {
            List<PublicHolidays> holidayses = new List<PublicHolidays>();
            this.operation = () =>
            {
                LeaveRepopsitory access = new LeaveRepopsitory(this.Transaction);
                holidayses = access.GetPublicHolidays();
            };
            this.Start(false);
            return holidayses;
        }


        public void ApplyLeave(Leave leave)
        {
            this.operation = () =>
            {
                LeaveRepopsitory access = new LeaveRepopsitory(this.Transaction);
                access.ApplyLeave(leave);
            };
            this.Start(false);
        }
        public List<Leave> GetApplyedLeaves()
        {
            List<Leave>  leave = new List<Leave>();
            this.operation = () =>
            {
                LeaveRepopsitory access = new LeaveRepopsitory(this.Transaction);
                 leave = access.GetApplyedLeaves();
            };
            this.Start(false);
            return leave;
        }

        public List<Leave> GetLeaveStatus(string emailId)
        {
            List<Leave>  leave = new List<Leave>();
            this.operation = () =>
            {
                LeaveRepopsitory access = new LeaveRepopsitory(this.Transaction);
                leave = access.GetLeaveStatus(emailId);
            };
            this.Start(false);
            return leave;
        }

        public List<Leave> GetLeaveHistory(string emailId)
        {
            List<Leave>  leave = new List<Leave>();
            this.operation = () =>
            {
                LeaveRepopsitory access = new LeaveRepopsitory(this.Transaction);
                leave = access.GetLeaveHistory(emailId);
            };
            this.Start(false);
            return leave;
        }
        

    }
}