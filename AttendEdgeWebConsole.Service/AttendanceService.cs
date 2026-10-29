using AttendEdgeWebConsole.Infrastructure.IService;
using System.Configuration;
using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Service
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public AttendanceService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.AttendanceLister> GetAllAsync(Domain.AttendanceLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.AttendanceLister, Domain.AttendanceLister>(
                url: $"{_baseUrl}/Attendance/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.Attendance> CreateAsync(Domain.Attendance mAttendance)
        {
            return await _httpClientService.PostAsync<Domain.Attendance, Domain.Attendance>(
                url: $"{_baseUrl}/Attendance",
                obj: mAttendance,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/Attendance/{id}/LoginUser/{_loginUserId}",
                token: _token
            ).ConfigureAwait(false);
        }
    }
}
