using AttendEdgeWebConsole.Infrastructure;
using System.Web.Mvc;

namespace AttendEdgeWebConsole.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            ViewBag.ShowEmployee = true;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.Employee))
                ViewBag.ShowEmployee = false;

            return View();
        }
    }
}