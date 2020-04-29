using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeanHub.DAL.Repositories;
using Moq;
using Bogus.DataSets;
using LeanHub.ApplicationCore.Services;
using System.Net.Http.Headers;

namespace LeanHub.Tests.ApplicationCore.Services
{
    [TestClass]
    public class CompareServiceTests
    {
     
        private CompareService _service;
        private Mock<IGitHubRepository> _mockHubRepo;
        private Mock<ICsvRepository> _mockCsvRepo;
        private Random _randy;
        private Lorem _lorem;


        [TestInitialize]
        public void Init()
        {
            _randy = new Random();
            _lorem = new Lorem(locale: "en");
            _mockHubRepo = new Mock<IGitHubRepository>();
            _mockCsvRepo = new Mock<ICsvRepository>();
            _service = new CompareService(_mockHubRepo.Object, _mockCsvRepo.Object);
        }

        [TestMethod]
        public void CompareUsers_CallsGetUsers_OnCsvRepository()
        {
            var actual = _service.CompareUsers();

            _mockCsvRepo.Verify(r => r.GetUsers(), Times.Once);
        }

        [TestMethod]
        public void CompareUsers_CallsGetCredentials_OnGithubRepository()
        {            
            var actual = _service.CompareUsers();

            _mockHubRepo.Verify(r => r.GetCredentials(), Times.Once);
        }

        [TestMethod]
        public void CompareUsers_CallsGetListOfUsers_OnGithubRepository()
        {
            var expectedAuth = new AuthenticationHeaderValue(_lorem.Word());
            _mockHubRepo.Setup(r => r.GetCredentials()).Returns(expectedAuth);
            
            var actual = _service.CompareUsers();

            _mockHubRepo.Verify(r => r.GetListOfUsers(expectedAuth), Times.Once);
        }
    }
}
