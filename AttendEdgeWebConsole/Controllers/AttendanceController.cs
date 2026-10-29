using AttendEdgeWebConsole.Filter;
using AttendEdgeWebConsole.Helper;
using AttendEdgeWebConsole.Infrastructure;
using AttendEdgeWebConsole.Infrastructure.IService;
using System;
using System.Web.Mvc;

namespace AttendEdgeWebConsole.Controllers
{
    [Authentication]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _service;
        private readonly IDropdownService _dropdownService;

        public AttendanceController(IAttendanceService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            return View(new Domain.AttendanceLister());
        }

        public ActionResult List(Domain.AttendanceLister mLister)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                ViewBag.ShowCompany = false;

                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;
                else
                    mLister.SearchCriteria.CompanyId = HttpContextHelper.LoginUser.CompanyId;

                Infrastructure.Helper.DefaultSetter.ApplyDefaults(mLister.SearchCriteria);
                mLister = _service.GetAllAsync(mLister).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                ViewBag.NotificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
                ViewBag.NotificationMsg = ex.Message;
            }
            finally
            {
                mLister.Pagination.PaginationSummary = PaginationHelper.GetPaginationSummary(mLister.Pagination);
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
                ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Users = _dropdownService.GetActiveUsersOptionsAsync(HttpContextHelper.LoginUser.CompanyId).GetAwaiter().GetResult();
            }

            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Create()
        {
            System.Threading.Thread.Sleep(1000);
            ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
            ViewBag.ShowCompany = false;


            ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();

            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            ViewBag.Users = _dropdownService.GetActiveUsersOptionsAsync(HttpContextHelper.LoginUser.CompanyId).GetAwaiter();
            return PartialView("_Create", new Domain.Attendance());
        }

        [HttpPost]
        public ActionResult Create(Domain.Attendance mAttendance)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                Infrastructure.Helper.DefaultSetter.ApplyDefaults(mAttendance);
                Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mAttendance);

                mAttendance = _service.CreateAsync(mAttendance).GetAwaiter().GetResult();
                bool success = mAttendance != null && mAttendance.Id > 0;
                ViewBag.NotificationType = success
                    ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                    : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                    ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Attendance).StrToLower())
                    : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Attendance).StrToLower());

                return base.PartialView("_Create", (object)mAttendance);
            }
            catch (Exception ex)
            {
                ViewBag.NotificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
                ViewBag.NotificationMsg = ex.Message;
            }
            finally
            {
                ViewBag.Users = _dropdownService.GetActiveUsersOptionsAsync(HttpContextHelper.LoginUser.CompanyId).GetAwaiter().GetResult();
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;
                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_Create", mAttendance);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            var notificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
            var notificationMsg = string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Attendance).StrToLower());

            try
            {
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                notificationType = EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower();
                notificationMsg = string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Attendance).StrToLower());
            }
            catch (Exception ex)
            {
                ViewBag.NotificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
                ViewBag.NotificationMsg = ex.Message;
            }

            var resultData = new
            {
                NotificationType = notificationType,
                NotificationMsg = notificationMsg
            };

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
    }
}