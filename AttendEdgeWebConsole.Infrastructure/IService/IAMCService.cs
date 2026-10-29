using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Infrastructure.IService
{
    public interface IAMCService
    {
        Task<Domain.AMCLister> GetAllAsync(Domain.AMCLister mLister);

        Task<Domain.AMC> CreateAsync(Domain.AMC mAMC);

        Task<Domain.AMC> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
