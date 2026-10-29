using AttendEdgeWebConsole.Filter;
using AttendEdgeWebConsole.Infrastructure;
using System.Web.Mvc;

namespace AttendEdgeWebConsole.Controllers
{
    [Authentication]
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            HttpContextHelper.LoginUser = null;
            return RedirectToAction("Index", "Authentication");
        }
    }
}