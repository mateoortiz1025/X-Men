using System.Threading.Tasks;
using XMEN.Core.Entities;

namespace XMEN.Core.Interfaces
{
    public interface IVerifiedDNAHistoryRepository : IRepository<VerifiedDNAHistory>
    {
        Task<VerifiedDNAHistory> GetVerifiedDNAHistoryByDNA(string DNA);
    }
}