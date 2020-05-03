using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using OneCasa.BusinessAccess;
using OneCasa.Models.ViewModels;

namespace OneCasa.Controllers
{
    [Authorize]
    public class LeavesController : Controller
    {
        // GET
        private LeaveServices _leaveServices;

        public LeavesController()
        {
            _leaveServices = new LeaveServices();
        }
        public ActionResult Index()
        {

            List<PublicHolidays> publicHolidayses = new List<PublicHolidays>();
            publicHolidayses = _leaveServices.GetPublicHolidays();
            ViewBag.PubluicHolidays = publicHolidayses;
            List<Leave> leaves = _leaveServices.GetApplyedLeaves();
            return View("Index", leaves);
        }

        [HttpGet]
        public ActionResult ApplyLeave()
        {
            return View("_ApplyLeave");
        }

        [HttpPost]
        public ActionResult ApplyLeave(Leave leave)
        {

            try
            {
                leave.Email = User.Identity.GetUserName();
                _leaveServices.ApplyLeave(leave);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return RedirectToAction("index");
            }
        }

        public JsonResult GetPendingLeaves(string emailId)
        {
            var pendingLeaves = _leaveServices.GetLeaveStatus(User.Identity.GetUserName()).OrderBy(l=>l.FromDate).ThenBy(l=>l.ToDate);
            return Json(pendingLeaves, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLeaveHistory(string emailId)
        {
            var leaveHistory = _leaveServices.GetLeaveHistory(emailId).OrderByDescending(l=>l.FromDate).ThenByDescending(l=>l.ToDate);
            return Json(leaveHistory, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public JsonResult CancelLeave(Leave leave)
        {
            if (leave != null)
            {
                leave.Email = User.Identity.GetUserName();
                _leaveServices.CancelLeave(leave);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            return Json(false, JsonRequestBehavior.AllowGet);;
        }

        
        // [HandleError]
        // [HttpGet]
        // public ActionResult EditLeave(DateTime fromdate,DateTime todate)
        // {
        //     Leave leave =  _leaveServices.GetApplyedLeaves().FirstOrDefault(l => l.Email == User.Identity.GetUserName() && l.FromDate == fromdate && l.ToDate == todate);
        //     return View("_ApplyLeave", leave);
        // }
    }
}