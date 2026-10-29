using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Infrastructure.IService
{
    public interface ILeaveRequestService
    {
        Task<Domain.LeaveRequestLister> GetAllAsync(Domain.LeaveRequestLister mLister);

        Task<Domain.LeaveRequest> CreateAsync(Domain.LeaveRequest mLeaveRequest, Domain.User mUser);

        Task<Domain.LeaveRequest> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
