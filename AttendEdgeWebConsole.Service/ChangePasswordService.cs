using AttendEdgeWebConsole.Infrastructure.IService;
using System.Configuration;
using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Service
{
    public class ChangePasswordService : IChangePasswordService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;

        public ChangePasswordService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
        }

        public async Task Initiate(Domain.ChangePassword mChangePassword)
        {
            await _httpClientService.PutAsync<Domain.ChangePassword>(
                url: $"{_baseUrl}/ChangePassword",
                obj: mChangePassword,
                token: _token
                ).ConfigureAwait(false);
        }
    }
}
