using System.Threading.Tasks;
using XMEN.Core.DTOs;

namespace XMEN.Core.Interfaces
{
    public interface IMutantService
    {
        bool IsMutant(MutantRequest mutantRequest);

        Task<StatisticsResponse> GetStatistics();
    }
}
