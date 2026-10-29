using AttendEdgeWebConsole.Filter;
using AttendEdgeWebConsole.Helper;
using AttendEdgeWebConsole.Infrastructure;
using AttendEdgeWebConsole.Infrastructure.IService;
using System;
using System.Web.Mvc;

namespace AttendEdgeWebConsole.Controllers
{
    [Authentication]
    public class ContactController : Controller
    {
        private readonly IContactService _service;
        private readonly IDropdownService _dropdownService;

        public ContactController(IContactService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }
        
        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            return View(new Domain.ContactLister());
        }

        public ActionResult List(Domain.ContactLister mLister)
        {
            try
            {
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
                ViewBag.Status = _dropdownService.GetStatusOptions();
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
            }

            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_Create",new Domain.Contact());
        }

        [HttpPost]
        public ActionResult Create(Domain.Contact mContactUs)
        {
            try
            {
                mContactUs = Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mContactUs);
                mContactUs = _service.CreateAsync(mContactUs).GetAwaiter().GetResult();

                bool success = mContactUs != null && mContactUs.Id > 0;
                ViewBag.NotificationType = success
                    ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                    : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                    ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Contact).StrToLower())
                    : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Contact).StrToLower());

                return base.PartialView("_Create", (object)mContactUs);
            }
            catch (Exception ex)
            {
                ViewBag.NotificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
                ViewBag.NotificationMsg = ex.Message;
                return PartialView("_Create", mContactUs);
            }
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mContactUs = new Domain.Contact();
            try
            {
                mContactUs = _service.GetAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                ViewBag.NotificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
                ViewBag.NotificationMsg = ex.Message;
            }
            return PartialView("_Create", mContactUs);
        }


        [HttpGet]
        public ActionResult GetMessage(int id)
        {
            var mContactUs = new Domain.Contact();
            try
            {
                mContactUs = _service.GetAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                ViewBag.NotificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
                ViewBag.NotificationMsg = ex.Message;
            }
            return PartialView("_Info", mContactUs);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var notificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
            var notificationMsg = string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Company).StrToLower());

            try
            {
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                notificationType = EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower();
                notificationMsg = string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Company).StrToLower());
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