using System;
using System.Threading.Tasks;

namespace XMEN.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IVerifiedDNAHistoryRepository VerifiedDNAHistoryRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
