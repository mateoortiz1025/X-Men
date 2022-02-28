using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using XMEN.Api.Controllers;
using XMEN.Api.Responses;
using XMEN.Core.DTOs;
using XMEN.Core.Interfaces;

namespace XMEN.Tests
{
    [TestClass]
    public class XMenControllerTest
    {

        private Mock<IMutantService> _mutantService { get; set; }


        [TestInitialize]
        public void Initialize()
        {
            _mutantService = new Mock<IMutantService>();
        }


        [TestMethod]
        public void isMutantResponseOk()
        {
            var XMenController = new XMenController(_mutantService.Object);

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

            _mutantService.Setup(x => x.IsMutant(mutantRequest)).ReturnsAsync(true);
            Task<IActionResult> IsMutant = XMenController.Mutant(mutantRequest);

            var IsMutantResponse = new ApiResponse<bool>(true);
            var okResult = IsMutant.Result as OkObjectResult;
            var okObjectResult = okResult.Value as ApiResponse<bool>;

            Assert.IsNotNull(IsMutant.Result);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(IsMutantResponse.Data, okObjectResult.Data);
            _mutantService.Verify(x => x.IsMutant(mutantRequest));
        }

        [TestMethod]
        public void isNotMutantResponseForbidden()
        {
            var XMenController = new XMenController(_mutantService.Object);

            MutantRequest mutantRequest = new MutantRequest()
            {
                DNA = new List<string> {
                    "ATCC",
                    "GATA",
                    "CGAG",
                    "TCGC"
                }
            };

            _mutantService.Setup(x => x.IsMutant(mutantRequest)).ReturnsAsync(false);
            Task<IActionResult> IsMutant = XMenController.Mutant(mutantRequest);

            var IsMutantResponse = new ApiResponse<bool>(false);
            var Result = IsMutant.Result as ObjectResult;
            var forbiddenObjectResult = Result.Value as ApiResponse<bool>;

            Assert.IsNotNull(IsMutant.Result);
            Assert.AreEqual(403, Result.StatusCode);
            Assert.AreEqual(IsMutantResponse.Data, forbiddenObjectResult.Data);
            _mutantService.Verify(x => x.IsMutant(mutantRequest));
        }

        [TestMethod]
        public void statsOkZeroResults()
        {
            var XMenController = new XMenController(_mutantService.Object);

            var responseStats = new StatisticsResponse()
            {
                CountHumanDNA = 0,
                CountMutantDNA = 0,
                Ratio = 0
            };

            _mutantService.Setup(x => x.GetStatistics()).ReturnsAsync(responseStats);
            Task<IActionResult> statics = XMenController.Stats();

            var statsApiResponse = new ApiResponse<StatisticsResponse>(responseStats);
            var Result = statics.Result as OkObjectResult;
            var okObjectResult = Result.Value as ApiResponse<StatisticsResponse>;

            Assert.IsNotNull(statics.Result);
            Assert.AreEqual(200, Result.StatusCode);
            Assert.AreEqual(statsApiResponse.Data, okObjectResult.Data);
            _mutantService.Verify(x => x.GetStatistics());
        }

        [TestMethod]
        public void statsOkResults()
        {
            var XMenController = new XMenController(_mutantService.Object);

            var responseStats = new StatisticsResponse()
            {
                CountHumanDNA = 2,
                CountMutantDNA = 1,
                Ratio = 0.5
            };

            _mutantService.Setup(x => x.GetStatistics()).ReturnsAsync(responseStats);
            Task<IActionResult> statics = XMenController.Stats();

            var statsApiResponse = new ApiResponse<StatisticsResponse>(responseStats);
            var Result = statics.Result as OkObjectResult;
            var okObjectResult = Result.Value as ApiResponse<StatisticsResponse>;

            Assert.IsNotNull(statics.Result);
            Assert.AreEqual(200, Result.StatusCode);
            Assert.AreEqual(statsApiResponse.Data, okObjectResult.Data);
            _mutantService.Verify(x => x.GetStatistics());
        }

    }
}
