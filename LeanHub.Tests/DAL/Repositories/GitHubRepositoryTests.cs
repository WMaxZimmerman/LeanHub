using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeanHub.DAL.Repositories;
using Moq;
using Bogus.DataSets;

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
            _lorem = new Bogus.DataSets.Lorem(locale: "en");
        }

        [TestMethod]
        public void AddUserToOrg_CallsMakeApiCall_WithExpectedParameters_AndReturnsValue()
        {
            var name = _lorem.Word();
            

        }
    }
}
