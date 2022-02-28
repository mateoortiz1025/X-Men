using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XMEN.Core.DTOs;
using XMEN.Core.Entities;
using XMEN.Core.Interfaces;
using XMEN.Core.Services;

namespace XMEN.Tests
{
    [TestClass]
    public class MutantServiceStaticsTest
    {

        private Mock<IUnitOfWork> _mockUnitOfWork { get; set; }


        [TestInitialize]
        public void Initialize()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
        }


        [TestMethod]
        public void getStatsOneMutantTwoHumansTest()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            IEnumerable<VerifiedDNAHistory> ListVerifiedDNAHistory = new List<VerifiedDNAHistory>()
            {
                new VerifiedDNAHistory(){  Id = 1, IsMutant = true},
                new VerifiedDNAHistory(){  Id = 2, IsMutant = false},
                new VerifiedDNAHistory(){  Id = 3, IsMutant = false},
            };

            int mutantCount = ListVerifiedDNAHistory.Where(x => x.IsMutant).Count();
            int humanCount = ListVerifiedDNAHistory.Count() - mutantCount;
            double ratio = (double)mutantCount / humanCount;

            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetAll()).Returns(ListVerifiedDNAHistory);

            Task<StatisticsResponse> Statistics = service.GetStatistics();

            Assert.IsNotNull(Statistics.Result);
            Assert.AreEqual(ratio, Statistics.Result.Ratio);
            Assert.AreEqual(mutantCount, Statistics.Result.CountMutantDNA);
            Assert.AreEqual(humanCount, Statistics.Result.CountHumanDNA);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetAll());
        }

        [TestMethod]
        public void getStatsZeroMutantZeroHumansTest()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            IEnumerable<VerifiedDNAHistory> ListVerifiedDNAHistory = new List<VerifiedDNAHistory>();

            int mutantCount = ListVerifiedDNAHistory.Where(x => x.IsMutant).Count();
            int humanCount = ListVerifiedDNAHistory.Count() - mutantCount;
            double ratio = 0;

            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetAll()).Returns(ListVerifiedDNAHistory);

            Task<StatisticsResponse> Statistics = service.GetStatistics();

            Assert.IsNotNull(Statistics.Result);
            Assert.AreEqual(ratio, Statistics.Result.Ratio);
            Assert.AreEqual(mutantCount, Statistics.Result.CountMutantDNA);
            Assert.AreEqual(humanCount, Statistics.Result.CountHumanDNA);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetAll());
        }

        [TestMethod]
        public void getStatsTwoMutantOneHumansTest()
        {
            MutantService service = new MutantService(_mockUnitOfWork.Object);

            IEnumerable<VerifiedDNAHistory> ListVerifiedDNAHistory = new List<VerifiedDNAHistory>()
            {
                new VerifiedDNAHistory(){  Id = 1, IsMutant = true},
                new VerifiedDNAHistory(){  Id = 2, IsMutant = true},
                new VerifiedDNAHistory(){  Id = 3, IsMutant = false},
            };

            int mutantCount = ListVerifiedDNAHistory.Where(x => x.IsMutant).Count();
            int humanCount = ListVerifiedDNAHistory.Count() - mutantCount;
            double ratio = (double)mutantCount / humanCount;

            _mockUnitOfWork.Setup(x => x.VerifiedDNAHistoryRepository.GetAll()).Returns(ListVerifiedDNAHistory);

            Task<StatisticsResponse> Statistics = service.GetStatistics();

            Assert.IsNotNull(Statistics.Result);
            Assert.AreEqual(ratio, Statistics.Result.Ratio);
            Assert.AreEqual(mutantCount, Statistics.Result.CountMutantDNA);
            Assert.AreEqual(humanCount, Statistics.Result.CountHumanDNA);
            _mockUnitOfWork.Verify(x => x.VerifiedDNAHistoryRepository.GetAll());
        }
    }
}
