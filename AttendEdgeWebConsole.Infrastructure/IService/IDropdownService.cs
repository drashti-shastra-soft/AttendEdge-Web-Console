using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Infrastructure.IService
{
    public interface IDropdownService
    {
        dynamic GetStatusOptions();

        dynamic GetPageSizeOptions(int selectedSize = 20);

        Task<dynamic> GetRolesOptionsAsync(int roleId = 0);

        Task<dynamic> GetCompanyOptionsAsync();

        Task<dynamic> GetLeaveTypesOptionAsync();

        Task<dynamic> GetStatusAsync(string status = null);

        Task<dynamic> GetHalfTypeAsync(string halfType = null);

        Task<dynamic> GetActiveUsersOptionsAsync(int companyId);

        dynamic GetDocumentOptions();
        //Task<dynamic> GetUserDocumentOptions(int userId);
    }
}
