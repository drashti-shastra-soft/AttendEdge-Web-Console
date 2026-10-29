using AttendEdgeWebConsole.Infrastructure.IService;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Service
{
    public class UserDocumentService : IUserDocumentService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public UserDocumentService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<Domain.UserDocument> SaveDocument(Domain.UserDocument mDocuments)
        {
            var result = await _httpClientService.PostAsync<Domain.UserDocument>(
                url: $"{_baseUrl}/UserDocument",
                obj: mDocuments,
                token: _token
                ).ConfigureAwait(false);
            return result;
        }

        public async Task<List<Domain.UserDocument>> GetUserDocuments(int userId)
        {
            var result = await _httpClientService.GetAsync<List<Domain.UserDocument>>(
                    url: $"{_baseUrl}/UserDocument/UserId/{userId}",
                    token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteUserImage(int id)
        {
            await _httpClientService.DeleteAsync(
                    url: $"{_baseUrl}/UserDocument/{id}/LoginUser/{_loginUserId}",
                    token: _token
              ).ConfigureAwait(false);

        }
    }
}
