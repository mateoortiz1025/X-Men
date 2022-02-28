using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using XMEN.Core.DTOs;
using XMEN.Core.Entities;
using XMEN.Core.Interfaces;
using XMEN.Core.Services;

namespace XMEN.Tests
{
    [TestClass]
    public class MutantServiceTest
    {

        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }


        [TestInitialize]
        public void Initialize()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }


        [TestMethod]
        public void isMutantWithHorizontalSeq()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            MutantRequest mutantRequest = new MutantRequest()
            {
                DNA = new List<string> {
                    "ATCCATG",
                    "TTTTAGC",
                    "TTCCGAG",
                    "TGCCCCA",
                    "GGGATCG",
                    "TAATCTC",
                    "TGCTAGC"
                }
            };

            string sDNA = string.Join("-", mutantRequest.DNA.ToArray());
            VerifiedDNAHistory verifiedDNAHistory = new VerifiedDNAHistory()
            {
                DNA = sDNA,
                IsMutant = true
            };
            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA)).ReturnsAsync(value: null);
            Task<bool> IsMutant = service.IsMutant(mutantRequest);

            Assert.IsNotNull(IsMutant.Result);
            Assert.AreEqual(verifiedDNAHistory.IsMutant, IsMutant.Result);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA));
        }

        [TestMethod]
        public void isMutantWithVerticalSeq()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            MutantRequest mutantRequest = new MutantRequest()
            {
                DNA = new List<string> {
                    "ATCCATG",
                    "TGCTAGC",
                    "TTCCGAG",
                    "TGCATCA",
                    "TGGATCG",
                    "TAATCTC",
                    "TGCTAGC"
                }
            };

            string sDNA = string.Join("-", mutantRequest.DNA.ToArray());
            VerifiedDNAHistory verifiedDNAHistory = new VerifiedDNAHistory()
            {
                DNA = sDNA,
                IsMutant = true
            };
            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA)).ReturnsAsync(value: null);
            Task<bool> IsMutant = service.IsMutant(mutantRequest);

            Assert.IsNotNull(IsMutant.Result);
            Assert.AreEqual(verifiedDNAHistory.IsMutant, IsMutant.Result);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA));
        }

        [TestMethod]
        public void isMutantWithRightDiagonalSeq()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            MutantRequest mutantRequest = new MutantRequest()
            {
                DNA = new List<string> {
                    "ATCAATG",
                    "TGTTAGC",
                    "TTTCGAG",
                    "AGCTTCA",
                    "TGGATCG",
                    "AAATCTC",
                    "TGCTAGT"
                }
            };

            string sDNA = string.Join("-", mutantRequest.DNA.ToArray());
            VerifiedDNAHistory verifiedDNAHistory = new VerifiedDNAHistory()
            {
                DNA = sDNA,
                IsMutant = true
            };
            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA)).ReturnsAsync(value: null);
            Task<bool> IsMutant = service.IsMutant(mutantRequest);

            Assert.IsNotNull(IsMutant.Result);
            Assert.AreEqual(verifiedDNAHistory.IsMutant, IsMutant.Result);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA));
        }

        [TestMethod]
        public void isMutantWithLeftDiagonalSeq()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            MutantRequest mutantRequest = new MutantRequest()
            {
                DNA = new List<string> {
                    "ATCAATG",
                    "TGTTAGC",
                    "TTTGGAC",
                    "AGGTTCA",
                    "TGGACCG",
                    "GAACCAC",
                    "TGCTAGC"
                }
            };

            string sDNA = string.Join("-", mutantRequest.DNA.ToArray());
            VerifiedDNAHistory verifiedDNAHistory = new VerifiedDNAHistory()
            {
                DNA = sDNA,
                IsMutant = true
            };
            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA)).ReturnsAsync(value: null);
            Task<bool> IsMutant = service.IsMutant(mutantRequest);

            Assert.IsNotNull(IsMutant.Result);
            Assert.AreEqual(verifiedDNAHistory.IsMutant, IsMutant.Result);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA));
        }

        [TestMethod]
        public void isMutantWithMultiplelSeq()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            MutantRequest mutantRequest = new MutantRequest()
            {
                DNA = new List<string> {
                    "ATCCATG",
                    "GGTTAGC",
                    "GTTCGAG",
                    "GGTTTTT",
                    "GGGATTG",
                    "TAATTTC",
                    "GGCTAGC"
                }
            };

            string sDNA = string.Join("-", mutantRequest.DNA.ToArray());
            VerifiedDNAHistory verifiedDNAHistory = new VerifiedDNAHistory()
            {
                DNA = sDNA,
                IsMutant = true
            };
            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA)).ReturnsAsync(value: null);
            Task<bool> IsMutant = service.IsMutant(mutantRequest);

            Assert.IsNotNull(IsMutant.Result);
            Assert.AreEqual(verifiedDNAHistory.IsMutant, IsMutant.Result);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA));
        }

        [TestMethod]
        public void isNotMutan()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            MutantRequest mutantRequest = new MutantRequest()
            {
                DNA = new List<string> {
                    "ATCC",
                    "GATA",
                    "CGAG",
                    "TCGC"
                }
            };

            string sDNA = string.Join("-", mutantRequest.DNA.ToArray());
            VerifiedDNAHistory verifiedDNAHistory = new VerifiedDNAHistory()
            {
                DNA = sDNA,
                IsMutant = false
            };
            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA)).ReturnsAsync(value: null);
            Task<bool> IsMutant = service.IsMutant(mutantRequest);

            Assert.IsNotNull(IsMutant.Result);
            Assert.AreEqual(verifiedDNAHistory.IsMutant, IsMutant.Result);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA));
        }

        [TestMethod]
        public void isNotMutanExistsSeq()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            MutantRequest mutantRequest = new MutantRequest()
            {
                DNA = new List<string> {
                    "ATCC",
                    "GATA",
                    "CGAG",
                    "TCGC"
                }
            };

            string sDNA = string.Join("-", mutantRequest.DNA.ToArray());
            VerifiedDNAHistory verifiedDNAHistory = new VerifiedDNAHistory()
            {
                DNA = sDNA,
                IsMutant = false
            };
            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA)).ReturnsAsync(verifiedDNAHistory);
            Task<bool> IsMutant = service.IsMutant(mutantRequest);

            Assert.IsNotNull(IsMutant.Result);
            Assert.AreEqual(verifiedDNAHistory.IsMutant, IsMutant.Result);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA));
        }

        [TestMethod]
        public void isMutanExistsSeq()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            MutantRequest mutantRequest = new MutantRequest()
            {
                DNA = new List<string> {
                    "ATGCGA",
                    "CAGTGC",
                    "TTATGT",
                    "AGAGTG",
                    "CCCTTA",
                    "TCTCTG"
                }
            };

            string sDNA = string.Join("-", mutantRequest.DNA.ToArray());
            VerifiedDNAHistory verifiedDNAHistory = new VerifiedDNAHistory()
            {
                DNA = sDNA,
                IsMutant = true
            };
            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA)).ReturnsAsync(verifiedDNAHistory);
            Task<bool> IsMutant = service.IsMutant(mutantRequest);

            Assert.IsNotNull(IsMutant.Result);
            Assert.AreEqual(verifiedDNAHistory.IsMutant, IsMutant.Result);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA));
        }

        [TestMethod]
        public void incorrectDNALength()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            MutantRequest mutantRequest = new MutantRequest()
            {
                DNA = new List<string> {
                    "ATGCGA",
                    "CAGTGC",
                    "TTATGT",
                    "AGAGTG",
                    "CCCTTA",
                    "TCTCT"
                }
            };

            string errorLengthMessage = "Wrong DNA chain length";
            string sDNA = string.Join("-", mutantRequest.DNA.ToArray());
            VerifiedDNAHistory verifiedDNAHistory = new VerifiedDNAHistory()
            {
                DNA = sDNA,
                IsMutant = true
            };
            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA)).ReturnsAsync(value: null);
            Task<bool> IsMutant = service.IsMutant(mutantRequest);


            Assert.AreEqual(errorLengthMessage, IsMutant.Exception.InnerException.Message);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA));
        }

        [TestMethod]
        public void incorrectLength()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            MutantRequest mutantRequest = new MutantRequest()
            {
                DNA = new List<string> {
                    "ATG"
                }
            };

            string errorLengthMessage = "Wrong DNA chain length";
            string sDNA = string.Join("-", mutantRequest.DNA.ToArray());
            VerifiedDNAHistory verifiedDNAHistory = new VerifiedDNAHistory()
            {
                DNA = sDNA,
                IsMutant = false
            };
            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA)).ReturnsAsync(value: null);
            Task<bool> IsMutant = service.IsMutant(mutantRequest);


            Assert.AreEqual(errorLengthMessage, IsMutant.Exception.InnerException.Message);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetVerifiedDNAHistoryByDNA(sDNA));
        }
    }
}
