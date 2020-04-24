using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeanHub.ApplicationCore.Services;
using LeanHub.DAL.Repositories;
using LeanHub.Console.Controllers;
using Moq;


namespace LeanHub.Tests.DAL.Repositories
{
    [TestClass]
    public class GitHubRepositoryTests
    {
        private UserService _service;
        private Mock<IGitHubRepository> _mockRepo;
        private Random _randy;


        [TestInitialize]
        public void Init()
        {
            _randy = new Random();
            _service = new UserService();
            _mockRepo = new Mock<IGitHubRepository>();


        }
        [TestMethod]
        public void MakeApiCall_GivenNameandMethod_ReturnsContent()
        {
            var name = "myName";
            var method = "myMethod";
            

        }
    }
}