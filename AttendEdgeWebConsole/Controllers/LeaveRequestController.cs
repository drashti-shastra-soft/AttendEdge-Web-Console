using AttendEdgeWebConsole.Infrastructure.IService;
using AttendEdgeWebConsole.Infrastructure;
using AttendEdgeWebConsole.Filter;
using AttendEdgeWebConsole.Helper;
using System.Web.Mvc;
using System;

namespace AttendEdgeWebConsole.Controllers
{
    [Authentication]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService _service;
        private readonly IDropdownService _dropdownService;

        public LeaveRequestController(ILeaveRequestService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            if (HttpContextHelper.LoginUser.RoleId == (int)Enums.Role.Employee)
            {
                ViewBag.ShowUser = false;
                ViewBag.ShowStatus = false;
                ViewBag.ShowAdminNote = false;
            }
            else
            {
                ViewBag.ShowUser = true;
                ViewBag.ShowStatus = true;
                ViewBag.ShowAdminNote = true;
            }
            return View(new Domain.LeaveRequestLister());
        }

        public ActionResult List(Domain.LeaveRequestLister mLister)
        {
            try
            {
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
                ViewBag.LeaveStatus = _dropdownService.GetStatusOptions();
                ViewBag.HalfType = _dropdownService.GetHalfTypeAsync().Result;
                ViewBag.LeaveType = _dropdownService.GetLeaveTypesOptionAsync().Result;
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
                ViewBag.IsEmployee = HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.Employee);
                if (HttpContextHelper.LoginUser.RoleId == (int)Enums.Role.Employee)
                {
                    ViewBag.ShowUser = false;
                    ViewBag.ShowStatus = false;
                    ViewBag.ShowAdminNote = false;
                }
                else
                {
                    ViewBag.ShowUser = true;
                    ViewBag.ShowStatus = true;
                    ViewBag.ShowAdminNote = true;
                }
            }
            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.IsEmployee = HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.Employee);
            if (HttpContextHelper.LoginUser.RoleId == (int)Enums.Role.Employee)
            {
                ViewBag.ShowUser = false;
                ViewBag.ShowStatus = false;
                ViewBag.ShowAdminNote = false;
            }
            else
            {
                ViewBag.ShowUser = true;
                ViewBag.ShowStatus = true;
                ViewBag.ShowAdminNote = true;
            }

            ViewBag.Users = _dropdownService.GetActiveUsersOptionsAsync(HttpContextHelper.LoginUser.CompanyId).GetAwaiter().GetResult();
            ViewBag.LeaveType = _dropdownService.GetLeaveTypesOptionAsync().GetAwaiter().GetResult();
            ViewBag.Status = _dropdownService.GetStatusAsync().GetAwaiter().GetResult();
            ViewBag.HalfType = _dropdownService.GetHalfTypeAsync().GetAwaiter().GetResult();

            var model = new Domain.LeaveRequest
            {
                UserId = HttpContextHelper.LoginUser.Id
            };

            return PartialView("_Create", model);
        }

        [HttpPost]
        public ActionResult Create(Domain.LeaveRequest mLeaveRequest, Domain.User mUser)
        {
            try
            {
                Infrastructure.Helper.DefaultSetter.ApplyDefaults(mLeaveRequest);
                Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mLeaveRequest);
                ViewBag.IsEmployee = HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.Employee);
                mLeaveRequest = _service.CreateAsync(mLeaveRequest, mUser).GetAwaiter().GetResult();
                bool success = mLeaveRequest != null && mLeaveRequest.Id > 0;
                ViewBag.NotificationType = success
                    ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                    : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                    ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.LeaveRequest).StrToLower())
                    : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.LeaveRequest).StrToLower());

                return base.PartialView("_Create", (object)mLeaveRequest);
            }
            catch (Exception ex)
            {
                ViewBag.NotificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
                ViewBag.NotificationMsg = ex.Message;
                return PartialView("_Create", mLeaveRequest);
            }
            finally
            {
                ViewBag.Users = _dropdownService.GetActiveUsersOptionsAsync(HttpContextHelper.LoginUser.CompanyId).GetAwaiter().GetResult();
                ViewBag.LeaveType = _dropdownService.GetLeaveTypesOptionAsync().Result;
                ViewBag.Status = _dropdownService.GetStatusAsync().Result;
                ViewBag.HalfType = _dropdownService.GetHalfTypeAsync().Result;
            }
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mLeaveRequest = new Domain.LeaveRequest();
            try
            {
                mLeaveRequest = _service.GetAsync(id).GetAwaiter().GetResult();
                if (mLeaveRequest == null || mLeaveRequest.Id == 0)
                    mLeaveRequest = new Domain.LeaveRequest();
            }
            catch (Exception ex)
            {
                ViewBag.NotificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
                ViewBag.NotificationMsg = ex.Message;
            }
            finally
            {
                if (HttpContextHelper.LoginUser.RoleId == (int)Enums.Role.Employee)
                {
                    ViewBag.ShowUser = false;
                    ViewBag.ShowStatus = false;
                    ViewBag.ShowAdminNote = false;
                }
                else
                {
                    ViewBag.ShowUser = true;
                    ViewBag.ShowStatus = true;
                    ViewBag.ShowAdminNote = true;
                }

                ViewBag.LeaveType = _dropdownService.GetLeaveTypesOptionAsync().Result;
                ViewBag.Users = _dropdownService.GetActiveUsersOptionsAsync(HttpContextHelper.LoginUser.CompanyId).GetAwaiter().GetResult();
                ViewBag.Status = _dropdownService.GetStatusAsync(mLeaveRequest.Status).Result;
                ViewBag.HalfType = _dropdownService.GetHalfTypeAsync(mLeaveRequest.HalfType).Result;
            }
            return PartialView("_Create", mLeaveRequest);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var notificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
            var notificationMsg = string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.LeaveRequest).StrToLower());

            try
            {
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                notificationType = EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower();
                notificationMsg = string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.LeaveRequest).StrToLower());
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