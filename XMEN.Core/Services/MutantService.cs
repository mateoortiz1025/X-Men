using System.Linq;
using System.Threading.Tasks;
using XMEN.Core.DTOs;
using XMEN.Core.Entities;
using XMEN.Core.Exceptions;
using XMEN.Core.Interfaces;

namespace XMEN.Core.Services
{
    public class MutantService : IMutantService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MutantService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<StatisticsResponse> GetStatistics()
        {
            var ListVerifiedDNA = _unitOfWork.VerifiedDNAHistoryRepository.GetAll();

            StatisticsResponse stats = new StatisticsResponse()
            {
                CountHumanDNA = 0,
                CountMutantDNA = 0,
                Ratio = 0
            };

            if (ListVerifiedDNA.Count() != 0)
            {
                stats.CountHumanDNA = ListVerifiedDNA.Where(x => x.IsMutant == false).Count();
                stats.CountMutantDNA = ListVerifiedDNA.Count() - stats.CountHumanDNA;
                stats.Ratio = stats.CountMutantDNA / stats.CountHumanDNA;
            }

            return Task.FromResult(stats);
        }

        public async Task<bool> IsMutant(MutantRequest mutantRequest)
        {
            string sDNA = FormatDNA(mutantRequest.DNA.ToArray());
            var DNARepsonse = await _unitOfWork.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA);

            // Validate if exists
            if (DNARepsonse != null)
            {
                return DNARepsonse.IsMutant;
            }

            VerifiedDNAHistory verifiedDNAHistory = new VerifiedDNAHistory()
            {
                DNA = sDNA,
                IsMutant = false
            };

            int minimumLengthSeq = 4;
            int limitSeq = 2;
            int lengthDna = LengthDna(mutantRequest.DNA.ToArray(), minimumLengthSeq);

            if (lengthDna == 0)
            {
                throw new BusinessException("Wrong DNA chain length");
            }

            string[,] Data = PrepareData(mutantRequest.DNA.ToArray(), lengthDna);
            int k = 0, sequenceCount = 0;
            for (int i = 0; i < lengthDna; i++)
            {
                for (int j = 0; j < lengthDna; j++)
                {
                    string currentValue = Data[i, j];

                    //right-horizontal validation
                    bool isNavigateHorizontallyRight = false;
                    int l;
                    if (lengthDna - j + 1 > minimumLengthSeq)
                    {
                        isNavigateHorizontallyRight = true;
                        l = 1;
                        while (l < minimumLengthSeq && currentValue.Equals(Data[i, k + 1]))
                        {
                            l++;
                        }
                        if (l == minimumLengthSeq)
                        {
                            sequenceCount++;
                            if (sequenceCount == limitSeq)
                            {
                                verifiedDNAHistory.IsMutant = true;
                                await SaveHistory(verifiedDNAHistory);
                                return true;
                            }
                        }
                    }

                    //left-horizontal validation
                    bool isNavigateHorizontallyLeft = false;
                    if (j + 1 >= minimumLengthSeq) { isNavigateHorizontallyLeft = true; }

                    //vertical validation
                    bool isNavigateVertically = false;
                    if (lengthDna - i + 1 > minimumLengthSeq)
                    {
                        isNavigateVertically = true;
                        l = 1;
                        while (l < minimumLengthSeq && currentValue.Equals(Data[k + 1, j]))
                        {
                            l++;
                        }
                        if (l == minimumLengthSeq)
                        {
                            sequenceCount++;
                            if (sequenceCount == limitSeq)
                            {
                                verifiedDNAHistory.IsMutant = true;
                                await SaveHistory(verifiedDNAHistory);
                                return true;
                            }
                        }
                    }

                    //diagonal validation
                    if (isNavigateVertically)
                    {
                        if (isNavigateHorizontallyRight)
                        {
                            l = 1;
                            while (l < minimumLengthSeq && currentValue.Equals(Data[i + l, j + l]))
                            {
                                l++;
                            }
                            if (l == minimumLengthSeq)
                            {
                                sequenceCount++;
                                if (sequenceCount == limitSeq)
                                {
                                    verifiedDNAHistory.IsMutant = true;
                                    await SaveHistory(verifiedDNAHistory);
                                    return true;
                                }
                            }
                        }

                        if (isNavigateHorizontallyLeft)
                        {
                            l = 1;
                            while (l < minimumLengthSeq && currentValue.Equals(Data[i + l, j - l]))
                            {
                                l++;
                            }
                            if (l == minimumLengthSeq)
                            {
                                sequenceCount++;
                                if (sequenceCount == limitSeq)
                                {
                                    verifiedDNAHistory.IsMutant = true;
                                    await SaveHistory(verifiedDNAHistory);
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            await SaveHistory(verifiedDNAHistory);
            return false;
        }

        private int LengthDna(string[] dna, int minimumLength)
        {
            int y = dna.Length;
            int finalLength = 0;

            if (y >= minimumLength)
            {
                finalLength = y;
                foreach (string code in dna)
                    if (code.Length != y)
                        return 0;
            }
            return finalLength;
        }

        private string[,] PrepareData(string[] dna, int lengthDna)
        {
            string[,] dnaMat = new string[lengthDna, lengthDna];
            for (int i = 0; i < lengthDna; i++)
            {
                char[] codes = dna[i].ToCharArray();
                for (int j = 0; j < lengthDna; j++)
                {
                    dnaMat[i, j] = codes[j].ToString();
                }
            }

            return dnaMat;
        }

        private string FormatDNA(string[] DNA)
        {
            return string.Join("-", DNA);
        }

        private async Task<bool> SaveHistory(VerifiedDNAHistory verifiedDNAHistory)
        {
            await _unitOfWork.VerifiedDNAHistoryRepository.Add(verifiedDNAHistory);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
