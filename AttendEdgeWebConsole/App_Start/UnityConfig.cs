using AttendEdgeWebConsole.Infrastructure.IService;
using AttendEdgeWebConsole.Service;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace AttendEdgeWebConsole
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IAMCService, AMCService>();
            container.RegisterType<IAttendanceService, AttendanceService>();
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IChangePasswordService, ChangePasswordService>();
            container.RegisterType<ICompanyService, CompanyService>();
            container.RegisterType<IContactService, ContactService>();
            container.RegisterType<IDropdownService, DropdownService>();
            container.RegisterType<IForgotPasswordService, ForgotPasswordService>();
            container.RegisterType<IHolidayService, HolidayService>();
            container.RegisterType<IHttpClientService, HttpClientService>();
            container.RegisterType<ILeaveRequestService, LeaveRequestService>();
            container.RegisterType<IUserDocumentService, UserDocumentService>();
            container.RegisterType<IUserService, UserService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}