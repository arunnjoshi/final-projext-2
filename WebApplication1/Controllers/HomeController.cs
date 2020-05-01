using OneCasa.BusinessAccess;
using OneCasa.Models.ViewModels;
using OneCasa.Models.UserRoles;
using System;
using System.Web.Mvc;

namespace OneCasa.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        EmployeeService  _objEmployeeService;
        public HomeController()
        {
            _objEmployeeService = new EmployeeService();
        }
        public ActionResult Index()
        {
          
            return View();
        }




        public ActionResult GetEvents()
        {
            return View("_Events");
        }


        [HttpGet]
        public JsonResult GetUpcomingEvents()
        {
            var emp = _objEmployeeService.GetUpcomingEvents();
            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPastEvents()
        {
            var emp = _objEmployeeService.GetPastEvents();
            return Json(emp, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetEmployees()
        {
            var emp = _objEmployeeService.GetEmployeeData();
            if (User.IsInRole(RoleName.Admin) || User.IsInRole(RoleName.HR))
            {
                return View(emp);
            }
            return View("GetEmployeesUser", emp);
        }

        [HttpGet]
        public JsonResult GetSearchEmployees()
        {
            var emp = _objEmployeeService.GetEmployeeData();
            return Json(emp, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        [Authorize(Roles = RoleName.Admin+","+RoleName.HR)]
        public ActionResult AddEmployee()
        {
            var dep = _objEmployeeService.GetDepartments();
            SelectList departments = new SelectList(dep, "depid", "DepartmentName");
            ViewBag.Departments = departments;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.HR)]
        public ActionResult AddEmployee(EmployeeAddress emp)
        {
            try
            {
                _objEmployeeService.AddEmployee(emp);
                return RedirectToAction("GetEmployees", "Home");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [Authorize(Roles = RoleName.Admin + "," + RoleName.HR)]
        public ActionResult EditEmployee(int empid)
        {
            var dep = _objEmployeeService.GetDepartments();
            SelectList departments = new SelectList(dep, "depid", "DepartmentName");
            ViewBag.Departments = departments;

            EmployeeAddress emp = _objEmployeeService.GetEmployee(empid);
            return View("EditEmployee", emp);
        }

        [HttpPost]
        [Authorize(Roles = RoleName.Admin + "," + RoleName.HR)]
        public ActionResult EditEmployee(EmployeeAddress emp)
        {
            try
            {
                _objEmployeeService.EditEmployee(emp);
                return RedirectToAction("GetEmployees", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("EditEmployee", "Home", new { emp.EmpId });
            }
        }
    }
}