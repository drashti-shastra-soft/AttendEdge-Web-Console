using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Infrastructure.IService
{
    public interface IAuthenticationService
    {
        Task<Domain.User> AuthenticateAsync(Domain.Credential mCredential);
    }
}
