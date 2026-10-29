using AttendEdgeWebConsole.Infrastructure;
using AttendEdgeWebConsole.Infrastructure.IService;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AttendEdgeWebConsole.Service
{
    public class DropdownService : IDropdownService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string baseUrl;

        public DropdownService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");
        }

        public dynamic GetStatusOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Active", Value = "Active" },
                new SelectListItem { Text = "Disabled", Value = "Disabled" }
            };
        }

        public dynamic GetDocumentOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Aadhar_Card", Value = "Aadhar_Card" },
                new SelectListItem { Text = "Pan_Card", Value = "Pan_Card" },
                new SelectListItem { Text = "Driving_license", Value = "Driving_license" }
            };
        }

        public dynamic GetPageSizeOptions(int selectedSize = 20)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "50", Value = "50" },
                new SelectListItem { Text = "100", Value = "100" },
                new SelectListItem { Text = "All", Value = "-1" }
            };

            list.ForEach(item => item.Selected = item.Value == selectedSize.ToString());
            return list;
        }

        //public async Task<dynamic> GetUserDocumentOptions(int userId)
        //{
        //    var mSelectListItems = new List<SelectListItem>();

        //    var mDocuments = await _httpClientService.GetAsync<List<Domain.UserDocument>>(
        //        url: $"{baseUrl}/UserDocument/UserId/{userId}",
        //        token: HttpContextHelper.LoginUser?.Token
        //    ).ConfigureAwait(false);

        //    foreach (var mDocument in mDocuments)
        //    {
        //        mSelectListItems.Add(new SelectListItem() { Text = mDocument.DocumentType, Value = (mDocument.DocumentType) });
        //    }

        //    return mSelectListItems;
        //}


        public async Task<dynamic> GetRolesOptionsAsync(int roleId = 0)
        {
            var mSelectListItems = new List<SelectListItem>();
            var systemOwnerRoleId = EnumsHelper.GetEnumIndex(Enums.Role.System_Owner);
            var isSystemOwner = roleId == systemOwnerRoleId;

            var mRoles = await _httpClientService.GetAsync<List<Domain.Role>>(
                url: $"{baseUrl}/Role",
                token: HttpContextHelper.LoginUser?.Token).ConfigureAwait(false);

            var filteredRoles = mRoles.Where(role => isSystemOwner || role.Id != systemOwnerRoleId).ToList();

            foreach (var filteredRole in filteredRoles)
            {
                mSelectListItems.Add(new SelectListItem() { Text = filteredRole.Name, Value = Convert.ToString(filteredRole.Id) });
            }
            return mSelectListItems;
        }

        public async Task<dynamic> GetCompanyOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mCompanies = await _httpClientService.GetAsync<List<Domain.Company>>(
                url: $"{baseUrl}/Company/GetActiveCompanies",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mCompany in mCompanies)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mCompany.Name, Value = Convert.ToString(mCompany.Id) });
            }
            return mSelectListItems;
        }

        public async Task<dynamic> GetLeaveTypesOptionAsync()
        {
            var leaveTypeItems = Enum.GetValues(typeof(Enums.LeaveType))
                                     .Cast<Enums.LeaveType>()
                                     .Select(l => new SelectListItem
                                     {
                                         Value = l.ToString().Replace("_", " "),
                                         Text = l.ToString().Replace("_", " ")
                                     }).ToList();

            return await Task.FromResult(leaveTypeItems);
        }

        public async Task<dynamic> GetStatusAsync(string status = null)
        {
            var statusTypeItems = Enum.GetValues(typeof(Enums.StatusType))
                                      .Cast<Enums.StatusType>()
                                      .Select(l => new SelectListItem
                                      {
                                          Value = l.ToString().Replace("_", " "),
                                          Text = l.ToString().Replace("_", " "),
                                          Selected = string.Equals(l.ToString().Replace("_", " "), status, StringComparison.OrdinalIgnoreCase)
                                      }).ToList();

            return await Task.FromResult(statusTypeItems);
        }

        public async Task<dynamic> GetHalfTypeAsync(string halfType = null)
        {
            var halfTypeItems = Enum.GetValues(typeof(Enums.HalfType))
                                    .Cast<Enums.HalfType>()
                                    .Select(l => new SelectListItem
                                    {
                                        Value = l.ToString().Replace("_", " "),
                                        Text = l.ToString().Replace("_", " "),
                                        Selected = string.Equals(l.ToString().Replace("_", " "), halfType, StringComparison.OrdinalIgnoreCase)
                                    }).ToList();

            return await Task.FromResult(halfTypeItems);
        }

        public async Task<dynamic> GetActiveUsersOptionsAsync(int companyId)
        {
            var mSelectListItems = new List<SelectListItem>();

            var mUsers = await _httpClientService.GetAsync<List<Domain.User>>(
                url: $"{baseUrl}/User/GetActiveUsers/CompanyId/{companyId}",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var user in mUsers)
            {
                mSelectListItems.Add(new SelectListItem()
                {
                    Text = $"{user.FirstName} {user.LastName}",
                    Value = Convert.ToString(user.Id)
                });
            }

            return mSelectListItems;
        }

    }
}
