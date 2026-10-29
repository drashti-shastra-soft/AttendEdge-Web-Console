using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Infrastructure.IService
{
    public interface IContactService
    {
        Task<Domain.ContactLister> GetAllAsync(Domain.ContactLister mLister);

        Task<Domain.Contact> CreateAsync(Domain.Contact mContactUs);

        Task<Domain.Contact> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
