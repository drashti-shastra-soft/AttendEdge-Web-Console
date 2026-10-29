using AttendEdgeWebConsole.Infrastructure.IService;
using System.Configuration;
using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Service
{
    public class AMCService : IAMCService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public AMCService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
            _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
        }

        public async Task<Domain.AMCLister> GetAllAsync(Domain.AMCLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.AMCLister, Domain.AMCLister>(
                url: $"{_baseUrl}/AMC/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.AMC> CreateAsync(Domain.AMC mAMC)
        {
            return await _httpClientService.PostAsync<Domain.AMC, Domain.AMC>(
                url: $"{_baseUrl}/AMC",
                obj: mAMC,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.AMC> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.AMC>(
                url: $"{_baseUrl}/AMC/{id}",
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/AMC/{id}/LoginUser/{_loginUserId}",
                token: _token
            ).ConfigureAwait(false);
        }
    }
}
