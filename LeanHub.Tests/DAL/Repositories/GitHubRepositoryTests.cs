using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeanHub.DAL.Repositories;
using Moq;
using Bogus.DataSets;
using System.Net.Http.Headers;
using LeanHub.Shared.Models;
using System.Collections.Generic;

namespace LeanHub.Tests.DAL.Repositories
{
    [TestClass]
    public class GitHubRepositoryTests
    {
        private Mock<IApiRepository> _mockApi;
        private IGitHubRepository _repo;
        private Random _randy;
        private Lorem _lorem;


        [TestInitialize]
        public void Init()
        {
            _randy = new Random();
            _mockApi = new Mock<IApiRepository>();
            _repo = new GitHubRepository(_mockApi.Object);
            _lorem = new Lorem(locale: "en");
        }

        [TestMethod]
        public void AddUserToOrg_CallsMakeApiCall_WithExpectedParameters_AndReturnsValue()
        {
            var name = _lorem.Word();
            var expectedAddress = "https://api.github.com/orgs/TestyMcTestOrg/memberships/" + name;
            var expectedMethod = "PUT";
            var expectedResult = new Member();
            var expectedAuth = new AuthenticationHeaderValue(_lorem.Word());
            _mockApi.Setup(a => a.MakeApiCall<Member>(expectedAddress, expectedMethod, expectedAuth))
                .Returns(expectedResult);

            var actual = _repo.AddUserToOrg(name, expectedAuth);

            _mockApi.Verify(a => a.MakeApiCall<Member>(expectedAddress, expectedMethod, expectedAuth), Times.Once);
            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod]
        public void RemoveUserFromOrg_CallsMakeApiCall_WithExpectedParameters_AndReturnsTrue()
        {
            var name = _lorem.Word();
            var expectedAddress = "https://api.github.com/orgs/TestyMcTestOrg/members/" + name;
            var expectedMethod = "DELETE";
            var result = new Object();
            var expected = true;
            var expectedAuth = new AuthenticationHeaderValue(_lorem.Word());
            _mockApi.Setup(a => a.MakeApiCall<Object>(expectedAddress, expectedMethod, expectedAuth))
                .Returns(result);

            var actual = _repo.RemoveUserFromOrg(name, expectedAuth);

            _mockApi.Verify(a => a.MakeApiCall<Object>(expectedAddress, expectedMethod, expectedAuth), Times.Once);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveUserFromOrg_CallsMakeApiCall_WithExpectedParameters_AndReturnsFalse()
        {
            var name = _lorem.Word();
            var expectedAddress = "https://api.github.com/orgs/TestyMcTestOrg/members/" + name;
            var expectedMethod = "DELETE";
            Object result = null;
            var expected = false;
            var expectedAuth = new AuthenticationHeaderValue(_lorem.Word());
            _mockApi.Setup(a => a.MakeApiCall<Object>(expectedAddress, expectedMethod, expectedAuth))
                .Returns(result);

            var actual = _repo.RemoveUserFromOrg(name, expectedAuth);

            _mockApi.Verify(a => a.MakeApiCall<Object>(expectedAddress, expectedMethod, expectedAuth), Times.Once);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetListOfUsers_CallsMakeApiCall_WithExpectedParameters_AndReturnsValue()
        {
            var expectedAddress = "https://api.github.com/orgs/TestyMcTestOrg/members";
            var expectedMethod = "GET";
            var expectedResult = new List<User>();
            var expectedAuth = new AuthenticationHeaderValue(_lorem.Word());
            _mockApi.Setup(a => a.MakeApiCall<List<User>>(expectedAddress, expectedMethod, expectedAuth))
                .Returns(expectedResult);

            var actual = _repo.GetListOfUsers(expectedAuth);

            _mockApi.Verify(a => a.MakeApiCall<List<User>>(expectedAddress, expectedMethod, expectedAuth), Times.Once);
            Assert.AreEqual(expectedResult, actual);
        }
    }
}
