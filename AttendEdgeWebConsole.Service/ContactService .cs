using AttendEdgeWebConsole.Infrastructure.IService;
using System.Configuration;
using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Service
{
    public class ContactService : IContactService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public ContactService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.ContactLister> GetAllAsync(Domain.ContactLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.ContactLister, Domain.ContactLister>(
                url: $"{_baseUrl}/Contact/GetAll",
                obj: mLister,
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task<Domain.Contact> CreateAsync(Domain.Contact mContactUs)
        {
            return await _httpClientService.PostAsync<Domain.Contact, Domain.Contact>(
                url: $"{_baseUrl}/Contact",
                obj: mContactUs,
                token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Contact> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Contact>(
                url: $"{_baseUrl}/Contact/{id}",                
                token: _token
            ).ConfigureAwait(false);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                url: $"{_baseUrl}/Contact/{id}/LoginUser/{_loginUserId}",
                token: _token
            ).ConfigureAwait(false);
        }
    }
}
