using AttendEdgeWebConsole.Infrastructure.IService;
using System.Configuration;
using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Service
{
    public class HolidayService : IHolidayService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public HolidayService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.HolidayLister> GetAllAsync(Domain.HolidayLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.HolidayLister, Domain.HolidayLister>(
                url: $"{_baseUrl}/Holiday/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.Holiday> CreateAsync(Domain.Holiday mHoliday)
        {
            return await _httpClientService.PostAsync<Domain.Holiday, Domain.Holiday>(
                url: $"{_baseUrl}/Holiday",
                obj: mHoliday,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Holiday> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Holiday>(
                url: $"{_baseUrl}/Holiday/{id}",
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/Holiday/{id}/LoginUser/{_loginUserId}",
                token: _token
            ).ConfigureAwait(false);
        }


    }
}
