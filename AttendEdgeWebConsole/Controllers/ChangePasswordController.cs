using AttendEdgeWebConsole.Infrastructure.IService;
using AttendEdgeWebConsole.Infrastructure;
using AttendEdgeWebConsole.Filter;
using System.Web.Mvc;
using System;

namespace AttendEdgeWebConsole.Controllers
{
    [Authentication]
    public class ChangePasswordController : Controller
    {
        private readonly IChangePasswordService _service;

        public ChangePasswordController(IChangePasswordService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;
            return View(new Domain.ChangePassword());
        }

        [HttpPost]
        public ActionResult Index(Domain.ChangePassword mChangePassword)
        {
            try
            {
                mChangePassword.UserId = HttpContextHelper.LoginUser.Id;

                _service.Initiate(mChangePassword).GetAwaiter().GetResult();
                bool success = mChangePassword != null && mChangePassword.UserId > 0;

                ViewBag.NotificationType = success
                   ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                   : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                    ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.ChangePassword).StrToLower())
                    : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.ChangePassword).StrToLower());

            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Index", "Authentication");
            }
            catch (BadRequestException ex)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = ex.Message;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            finally
            {
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;
            }
            return View(mChangePassword);
        }
    }
}


