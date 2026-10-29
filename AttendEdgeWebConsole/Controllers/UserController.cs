using AttendEdgeWebConsole.Infrastructure.IService;
using AttendEdgeWebConsole.Filter;
using AttendEdgeWebConsole.Helper;
using AttendEdgeWebConsole.Infrastructure;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace AttendEdgeWebConsole.Controllers
{
    [Authentication]
    public class UserController : Controller
    {
        private readonly IUserService _service;
        private readonly IDropdownService _dropdownService;
        private readonly IUserDocumentService _userDocumentService;

        public UserController(IUserService service, IDropdownService dropdownService, IUserDocumentService userImageService)
        {
            _service = service;
            _dropdownService = dropdownService;
            _userDocumentService = userImageService;
        }

        #region User  
        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;
            return View(new Domain.UserLister());
        }

        public ActionResult List(Domain.UserLister mLister)
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
                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();

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

            return PartialView("_Create", new Domain.User());
        }

        [HttpPost]
        public ActionResult Create(Domain.User mUser)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);

                if (HttpContextHelper.LoginUser.RoleId != EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    Infrastructure.Helper.DefaultSetter.ApplyDefaults(mUser);

                Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mUser);

                mUser = _service.CreateAsync(mUser).GetAwaiter().GetResult();

                bool success = mUser != null && mUser.Id > 0;
                ViewBag.NotificationType = success
                    ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                    : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                    ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.User).StrToLower())
                    : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.User).StrToLower());

                return base.PartialView("_Create", (object)mUser);
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = Constant.InternalServerError;
            }
            finally
            {
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;

                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_Create", mUser);
        }

        [HttpGet]
        public ActionResult GetMetadata(int id)
        {
            string url = Url.Action("Metadata", "User", new { id = id });
            return Json(new { success = true, redirectUrl = url }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Metadata(int id)
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;
            ViewBag.UserId = id;
            return View();
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mUser = new Domain.User();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mUser = _service.GetAsync(id).GetAwaiter().GetResult();
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
            return View("_Get", mUser);
        }

        [HttpPost]
        public ActionResult Save(Domain.User mUser)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);

                if (HttpContextHelper.LoginUser.RoleId != EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    Infrastructure.Helper.DefaultSetter.ApplyDefaults(mUser);

                Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mUser);

                mUser = _service.CreateAsync(mUser).GetAwaiter().GetResult();

                bool success = mUser != null && mUser.Id > 0;
                ViewBag.NotificationType = success
                    ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                    : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                    ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.User).StrToLower())
                    : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.User).StrToLower());
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = Constant.InternalServerError;
            }
            finally
            {
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;

                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_Get", mUser);
        }

        [HttpGet, Route("BasicInfo")]
        public ActionResult BasicInfo(int id)
        {
            var mUser = new Domain.User();
            mUser.Id = id;
            mUser = _service.GetAsync(id).GetAwaiter().GetResult();

            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;
            ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
            return PartialView("_BasicInfo", mUser);
        }

        [HttpPost, Route("BasicInfo")]
        public ActionResult BasicInfo(Domain.User mUser)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mUser = _service.SaveBasicInfo(mUser).GetAwaiter().GetResult();

                bool success = mUser != null && mUser.Id > 0;
                ViewBag.NotificationType = success
                    ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                    : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = Constant.InternalServerError;
            }
            finally
            {
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;
                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
            }
            return View("Details", new Domain.User());
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var notificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
            var notificationMsg = string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.User).StrToLower());

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                notificationType = EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower();
                notificationMsg = string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.User).StrToLower());
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

        [HttpPost]
        public ActionResult CheckEmailAddressExist(Domain.User mUser)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                System.Threading.Thread.Sleep(1000);
                var isExist = _service.CheckEmailAddressExistAsync(mUser).GetAwaiter().GetResult();
                resultData = new { StatusCode = "Ok", IsExist = isExist };
            }
            catch (UnauthorizedAccessException)
            {
                resultData = new { StatusCode = "Unauthorized" };
            }
            catch (BadRequestException ex)
            {
                resultData = new { StatusCode = "BadRequest", NotificationType = "error", NotificationMsg = ex.Message };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting User!" };
            }
            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region User Images Document
        [HttpGet, Route("GetUserImages")]
        public ActionResult GetUserDocument(int id)
        {
            var mDocuments = new List<Domain.UserDocument>();
            try
            {
                mDocuments = _userDocumentService.GetUserDocuments(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return PartialView("_Documents", mDocuments);
        }

        [HttpPost, Route("SaveUserDocument")]
        public ActionResult SaveUserDocument(int userId, HttpPostedFileBase file)
        {
            var mDocuments = new Domain.UserDocument();
            dynamic resultData = new ExpandoObject();
            try
            {
                System.Threading.Thread.Sleep(1000);
                if (file != null && file.ContentLength > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        var fileBytes = ms.ToArray();

                        var mDocument = new Domain.UserDocument
                        {
                            UserId = userId,
                            FileName = file.FileName,
                            Base64String = Convert.ToBase64String(fileBytes),
                            MimeType = file.ContentType
                        };

                        Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mDocument);

                        // Save single document
                        mDocument = _userDocumentService.SaveDocument(mDocument).GetAwaiter().GetResult();
                        if (mDocument != null && mDocument.Id > 0)
                            resultData = new { type = "success", message = "Documents has been uploaded successfully." };
                        else
                            resultData = new { type = "error", message = "Something went wrong!" };
                    }
                }
                else
                {
                    resultData = new { type = "error", message = "No file uploaded or file is empty." };
                }
            }
            catch (UnauthorizedAccessException)
            {
                resultData = new { StatusCode = "Unauthorized" };
            }
            catch (BadRequestException ex)
            {
                resultData = new { type = "error", message = ex.Message };
            }
            catch
            {
                resultData = new { type = "error", message = "Internal server error!" };
            }
            return Json(new { success = true });
        }

        [HttpGet, Route("FetchUserDocument")]
        public ActionResult FetchUserDocument(int userId)
        {
            dynamic jsonResult = new ExpandoObject();
            try
            {
                var mDocuments = _userDocumentService.GetUserDocuments(userId).GetAwaiter().GetResult();
                jsonResult = Json(mDocuments.Select(doc => new
                {
                    Id = doc.Id,
                    Base64String = doc.Base64String
                }), JsonRequestBehavior.AllowGet);

                jsonResult.MaxJsonLength = int.MaxValue; // 🚀 This is the key line
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return jsonResult;
        }

        [HttpGet, Route("DeleteUserImage")]
        public ActionResult DeleteUserDocument(int id)
        {
            dynamic resultData = new ExpandoObject();
            try
            {
                System.Threading.Thread.Sleep(1000);
                _userDocumentService.DeleteUserImage(id).GetAwaiter().GetResult();
                resultData = new { NotificationType = "success", NotificationMsg = "User Image has been deleted!" };
            }
            catch (UnauthorizedAccessException)
            {
                resultData = new { StatusCode = "Unauthorized" };
            }
            catch (BadRequestException ex)
            {
                resultData = new { StatusCode = "BadRequest", NotificationType = "error", NotificationMsg = ex.Message };
            }
            catch (Exception)
            {
                resultData = new { NotificationType = "error", NotificationMsg = "Error occurred while deleting Product Image" };
            }

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}