using AttendEdgeWebConsole.Filter;
using AttendEdgeWebConsole.Helper;
using AttendEdgeWebConsole.Infrastructure;
using AttendEdgeWebConsole.Infrastructure.IService;
using System;
using System.Web.Mvc;

namespace AttendEdgeWebConsole.Controllers
{
    [Authentication]
    public class HolidayController : Controller
    {
        private readonly IHolidayService _service;
        private readonly IDropdownService _dropdownService;

        public HolidayController(IHolidayService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            return View(new Domain.HolidayLister());
        }

        public ActionResult List(Domain.HolidayLister mLister)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);

                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;
                else
                    mLister.SearchCriteria.CompanyId = HttpContextHelper.LoginUser.CompanyId;

                mLister = _service.GetAllAsync(mLister).GetAwaiter().GetResult();

            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = Constant.SomethingWentWrong;
            }
            finally
            {
                mLister.Pagination.PaginationSummary = PaginationHelper.GetPaginationSummary(mLister.Pagination);
                ViewBag.Status = _dropdownService.GetStatusOptions();
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync().GetAwaiter().GetResult();

                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
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

            return PartialView("_Create", new Domain.Holiday());
        }

        [HttpPost]
        public ActionResult Create(Domain.Holiday mHoliday)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);

                if (HttpContextHelper.LoginUser.RoleId != EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    mHoliday.CompanyId = HttpContextHelper.LoginUser.CompanyId;

                Infrastructure.Helper.DefaultSetter.ApplyDefaults(mHoliday);
                mHoliday = Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mHoliday);

                mHoliday = _service.CreateAsync(mHoliday).GetAwaiter().GetResult();

                bool success = mHoliday != null && mHoliday.Id > 0;
                ViewBag.NotificationType = success
                    ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                    : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                    ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Holiday).StrToLower())
                    : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Holiday).StrToLower());

                return base.PartialView("_Create", (object)mHoliday);
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = Constant.InternalServerError;
                return PartialView("_Create", mHoliday);
            }
            finally
            {
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;

                ViewBag.Users = _dropdownService.GetActiveUsersOptionsAsync(HttpContextHelper.LoginUser.CompanyId).GetAwaiter().GetResult();
                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
            }
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mHoliday = new Domain.Holiday();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mHoliday = _service.GetAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.ShowCompany = false;
                ViewBag.Companies = string.Empty;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                {
                    ViewBag.ShowCompany = true;
                    ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
                }

                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
            }
            return PartialView("_Create", mHoliday);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var notificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
            var notificationMsg = string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Holiday).StrToLower());

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                notificationType = EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower();
                notificationMsg = string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Holiday).StrToLower());
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