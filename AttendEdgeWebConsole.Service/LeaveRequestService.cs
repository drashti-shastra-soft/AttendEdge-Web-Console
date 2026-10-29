using AttendEdgeWebConsole.Infrastructure.IService;
using System.Configuration;
using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Service
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public LeaveRequestService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.LeaveRequestLister> GetAllAsync(Domain.LeaveRequestLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.LeaveRequestLister, Domain.LeaveRequestLister>(
                url: $"{_baseUrl}/LeaveRequest/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.LeaveRequest> CreateAsync(Domain.LeaveRequest mLeaveRequest, Domain.User mUser)
        {
            return await _httpClientService.PostAsync<Domain.LeaveRequest, Domain.LeaveRequest>(
                url: $"{_baseUrl}/LeaveRequest",
                obj: mLeaveRequest,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.LeaveRequest> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.LeaveRequest>(
                url: $"{_baseUrl}/LeaveRequest/{id}",
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/LeaveRequest/{id}/LoginUser/{_loginUserId}",
                token: _token
            ).ConfigureAwait(false);
        }

    }
}
