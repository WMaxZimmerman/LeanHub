using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeanHub.DAL.Repositories;
using Moq;
using Bogus.DataSets;
using System.Net.Http.Headers;

namespace LeanHub.Tests.DAL.Repositories
{
    [TestClass]
    public class GitHubRepositoryTests
    {
        private Mock<IGitHubApi> _mockApi;
        private IGitHubRepository _repo;
        private Random _randy;
        private Lorem _lorem;


        [TestInitialize]
        public void Init()
        {
            _randy = new Random();
            _mockApi = new Mock<IGitHubApi>();
            _repo = new GitHubRepository(_mockApi.Object);
            _lorem = new Lorem(locale: "en");
        }

        [TestMethod]
        public void AddUserToOrg_CallsMakeApiCall_WithExpectedParameters_AndReturnsValue()
        {
            var expectedName = _lorem.Word();
            var expectedMethod = "PUT";
            var expectedResult = _lorem.Word();
            var expectedAuth = new AuthenticationHeaderValue(_lorem.Word());
            _mockApi.Setup(a => a.MakeApiCall(expectedName, expectedMethod, expectedAuth)).Returns(expectedResult);

            var actual = _repo.AddUserToOrg(expectedName, expectedAuth);

            _mockApi.Verify(a => a.MakeApiCall(expectedName, expectedMethod, expectedAuth), Times.Once);
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod]
        public void RemoveUserFromOrg_CallsMakeApiCall_WithExpectedParameters_AndReturnsValue()
        {
            var expectedName = _lorem.Word();
            var expectedMethod = "DELETE";
            var expectedResult = _lorem.Word();
            var expectedAuth = new AuthenticationHeaderValue(_lorem.Word());
            _mockApi.Setup(a => a.MakeApiCall(expectedName, expectedMethod, expectedAuth)).Returns(expectedResult);

            var actual = _repo.RemoveUserFromOrg(expectedName, expectedAuth);

            _mockApi.Verify(a => a.MakeApiCall(expectedName, expectedMethod, expectedAuth), Times.Once);
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod]
        public void GetListOfUsers_CallsMakeApiCall_WithExpectedParameters_AndReturnsValue()
        {
            string expectedName = null;
            var expectedMethod = "GET";
            var expectedResult = _lorem.Word();
            var expectedAuth = new AuthenticationHeaderValue(_lorem.Word());
            _mockApi.Setup(a => a.MakeApiCall(expectedName, expectedMethod, expectedAuth)).Returns(expectedResult);

            var actual = _repo.GetListOfUsers(expectedAuth);

            _mockApi.Verify(a => a.MakeApiCall(expectedName, expectedMethod, expectedAuth), Times.Once);
            Assert.AreEqual(expectedResult, actual);
        }
    }
}
