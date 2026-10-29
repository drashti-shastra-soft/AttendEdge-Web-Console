using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendEdgeWebConsole.Infrastructure.IService
{
    public interface IUserDocumentService
    {
        Task<Domain.UserDocument> SaveDocument(Domain.UserDocument mDocuments);

        Task<List<Domain.UserDocument>> GetUserDocuments(int userId);

        Task DeleteUserImage(int id);
    }
}
