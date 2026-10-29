using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Infrastructure.IService
{
    public interface IChangePasswordService
    {
        Task Initiate(Domain.ChangePassword mChangePassword);
    }
}
