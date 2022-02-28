using System.Threading.Tasks;
using XMEN.Core.Interfaces;
using XMEN.Infrastructure.Configurations.Data;

namespace XMEN.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MutantContext _context;
        private readonly IVerifiedDNAHistoryRepository _verifiedDNAHistoryRepository;


        public UnitOfWork(MutantContext contex)
        {
            _context = contex;
        }

        public IVerifiedDNAHistoryRepository VerifiedDNAHistoryRepository => _verifiedDNAHistoryRepository ?? new VerifiedDNAHistoryRepository(_context);


        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
