using System;
using System.Threading.Tasks;
using XMEN.Core.DTOs;
using XMEN.Core.Interfaces;

namespace XMEN.Core.Services
{
    public class MutantService : IMutantService
    {
        public Task<StatisticsResponse> GetStatistics()
        {
            throw new NotImplementedException();
        }

        public bool IsMutant(MutantRequest mutantRequest)
        {
            int minimumLengthSeq = 4;
            int limitSeq = 2;
            int lengthDna = LengthDna(mutantRequest.DNA.ToArray(), minimumLengthSeq);

            if (lengthDna != 0)
            {
                string[,] Data = PrepareData(mutantRequest.DNA.ToArray(), lengthDna);
                int k = 0, l = 0, sequenceCount = 0;
                for (int i = 0; i < lengthDna; i++)
                {
                    for (int j = 0; j < lengthDna; j++)
                    {
                        string currentValue = Data[i,j];
                        //right-horizontal validation
                        bool isNavigateHorizontallyRight = false;
                        if (lengthDna - j + 1 > minimumLengthSeq)
                        {
                            isNavigateHorizontallyRight = true;
                            l = 1;
                            while (l < minimumLengthSeq && currentValue.Equals(Data[i,k + 1]))
                            {
                                l++;
                            }
                            if (l == minimumLengthSeq)
                            {
                                sequenceCount++;
                                if (sequenceCount == limitSeq) return true;
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
                            while (l < minimumLengthSeq && currentValue.Equals(Data[k + 1,j]))
                            {
                                l++;
                            }
                            if (l == minimumLengthSeq)
                            {
                                sequenceCount++;
                                if (sequenceCount == limitSeq) return true;
                            }
                        }

                        //diagonal validation
                        if (isNavigateVertically)
                        {
                            if (isNavigateHorizontallyRight)
                            {
                                l = 1;
                                while (l < minimumLengthSeq && currentValue.Equals(Data[i + l,j + l]))
                                {
                                    l++;
                                }
                                if (l == minimumLengthSeq)
                                {
                                    sequenceCount++;
                                    if (sequenceCount == limitSeq) return true;
                                }
                            }

                            if (isNavigateHorizontallyLeft)
                            {
                                l = 1;
                                while (l < minimumLengthSeq && currentValue.Equals(Data[i + l,j - l]))
                                {
                                    l++;
                                }
                                if (l == minimumLengthSeq)
                                {
                                    sequenceCount++;
                                    if (sequenceCount == limitSeq) return true;
                                }
                            }
                        }
                    }
                }
            }

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

        public static string[,] PrepareData(string[] dna, int lengthDna)
        {
            string[,] dnaMat = new string[lengthDna, lengthDna];
            for (int i = 0; i < lengthDna; i++)
            {
                char[] codes = dna[i].ToCharArray();
                for (int j = 0; j < lengthDna; j++)
                {
                    dnaMat[i,j] = codes[j].ToString();
                }
            }

            return dnaMat;
        }
    }
}
