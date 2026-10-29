using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Infrastructure.IService
{
    public interface IForgotPasswordService
    {
        Task Initiate(Domain.ForgotPassword mForgotPassword);
    }
}
