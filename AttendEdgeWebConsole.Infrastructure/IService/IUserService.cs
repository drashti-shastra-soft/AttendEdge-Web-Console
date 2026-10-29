using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Infrastructure.IService
{
    public interface IUserService
    {
        Task<Domain.UserLister> GetAllAsync(Domain.UserLister mLister);

        Task<Domain.User> CreateAsync(Domain.User mUser);

        Task<Domain.User> GetAsync(int id);

        Task<Domain.User> GetSaveAsync(string panCard, string aadhaarCard);

        Task DeleteAsync(int id);

        Task<Domain.User> SaveBasicInfo(Domain.User mUser);

        Task<bool> CheckEmailAddressExistAsync(Domain.User mUser);
    }
}
