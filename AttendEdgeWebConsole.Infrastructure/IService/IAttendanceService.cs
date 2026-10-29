using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Infrastructure.IService
{
    public interface IAttendanceService
    {
        Task<Domain.AttendanceLister> GetAllAsync(Domain.AttendanceLister mLister);

        Task<Domain.Attendance> CreateAsync(Domain.Attendance mAttendance);

        Task DeleteAsync(int id);
    }
}
