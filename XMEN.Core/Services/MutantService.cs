using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XMEN.Core.DTOs;
using XMEN.Core.Interfaces;

namespace XMEN.Core.Services
{
    class MutantService : IMutantService
    {
        public Task<StatisticsResponse> GetStatistics()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsMutant(MutantRequest dna)
        {
            throw new NotImplementedException();
        }
    }
}
