using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using XMEN.Core.Entities;
using XMEN.Core.Interfaces;
using XMEN.Infrastructure.Configurations.Data;

namespace XMEN.Infrastructure.Repositories
{
    public class VerifiedDNAHistoryRepository : BaseRepository<VerifiedDNAHistory>, IVerifiedDNAHistoryRepository
    {
        public VerifiedDNAHistoryRepository(MutantContext context) : base(context)
        {
        }

        public async Task<VerifiedDNAHistory> GetVerifiedDNAHistoryByDNA(string DNA)
        {
            return await _entities.FirstOrDefaultAsync(x => x.DNA.Equals(DNA));
        }

    }
}
