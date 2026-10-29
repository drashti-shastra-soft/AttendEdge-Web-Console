using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Infrastructure.IService
{
    public interface IHolidayService
    {
        Task<Domain.HolidayLister> GetAllAsync(Domain.HolidayLister mLister);

        Task<Domain.Holiday> CreateAsync(Domain.Holiday mHoliday);

        Task<Domain.Holiday> GetAsync(int id);

        Task DeleteAsync(int id);
    }
}
