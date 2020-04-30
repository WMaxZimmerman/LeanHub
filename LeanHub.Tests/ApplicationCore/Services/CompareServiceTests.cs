using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeanHub.DAL.Repositories;
using Moq;
using Bogus.DataSets;
using LeanHub.ApplicationCore.Services;
using System.Net.Http.Headers;
using LeanHub.Shared.Models;
using System.Collections.Generic;
using System.Linq;

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
            var localUsers = GetFakeUsers(0);
            var orgUsers = GetFakeUsers(0);
            _mockCsvRepo.Setup(r => r.GetUsers()).Returns(localUsers);
            _mockHubRepo.Setup(r => r.GetListOfUsers(It.IsAny<AuthenticationHeaderValue>())).Returns(orgUsers);
            
            var actual = _service.CompareUsers();

            _mockCsvRepo.Verify(r => r.GetUsers(), Times.Once);
        }

        [TestMethod]
        public void CompareUsers_CallsGetCredentials_OnGithubRepository()
        {
            var localUsers = GetFakeUsers(0);
            var orgUsers = GetFakeUsers(0);
            _mockCsvRepo.Setup(r => r.GetUsers()).Returns(localUsers);
            _mockHubRepo.Setup(r => r.GetListOfUsers(It.IsAny<AuthenticationHeaderValue>())).Returns(orgUsers);
            
            var actual = _service.CompareUsers();

            _mockHubRepo.Verify(r => r.GetCredentials(), Times.Once);
        }

        [TestMethod]
        public void CompareUsers_CallsGetListOfUsers_OnGithubRepository()
        {
            var localUsers = GetFakeUsers(0);
            var orgUsers = GetFakeUsers(0);
            _mockCsvRepo.Setup(r => r.GetUsers()).Returns(localUsers);
            _mockHubRepo.Setup(r => r.GetListOfUsers(It.IsAny<AuthenticationHeaderValue>())).Returns(orgUsers);
            var expectedAuth = new AuthenticationHeaderValue(_lorem.Word());
            _mockHubRepo.Setup(r => r.GetCredentials()).Returns(expectedAuth);
            
            var actual = _service.CompareUsers();

            _mockHubRepo.Verify(r => r.GetListOfUsers(expectedAuth), Times.Once);
        }

        [TestMethod]
        public void CompareUsers_MarksUsersAsRemove_WhenUserExistsInOrgAndNotLocal()
        {
            var localUsers = GetFakeUsers(0);
            var orgUsers = GetFakeUsers(3);
            var expectedUsers = orgUsers.Select(u => new ComparedUser
            {
                Login = u.Login,
                Name = u.Name,
                Action = LeanHub.Shared.Enums.Action.Remove
            }).ToList();
            _mockCsvRepo.Setup(r => r.GetUsers()).Returns(localUsers);
            _mockHubRepo.Setup(r => r.GetListOfUsers(It.IsAny<AuthenticationHeaderValue>())).Returns(orgUsers);

            var actual = _service.CompareUsers().ToList();
            
            CollectionAssert.AreEqual(expectedUsers, actual);
        }

        [TestMethod]
        public void CompareUsers_MarksUsersAsAdd_WhenUserExistsInLocalAndNotOrg()
        {
            var localUsers = GetFakeUsers(3);
            var orgUsers = GetFakeUsers(0);
            var expectedUsers = localUsers.Select(u => new ComparedUser
            {
                Login = u.Login,
                Name = u.Name,
                Action = LeanHub.Shared.Enums.Action.Add
            }).ToList();
            _mockCsvRepo.Setup(r => r.GetUsers()).Returns(localUsers);
            _mockHubRepo.Setup(r => r.GetListOfUsers(It.IsAny<AuthenticationHeaderValue>())).Returns(orgUsers);

            var actual = _service.CompareUsers().ToList();
            
            CollectionAssert.AreEqual(expectedUsers, actual);
        }

        [TestMethod]
        public void CompareUsers_DoesNotReturnUsers_WhenUserExistsInBothLists()
        {
            var localUsers = GetFakeUsers(3);
            var orgUsers = localUsers;
            var expectedUsers = new List<User>();
            _mockCsvRepo.Setup(r => r.GetUsers()).Returns(localUsers);
            _mockHubRepo.Setup(r => r.GetListOfUsers(It.IsAny<AuthenticationHeaderValue>())).Returns(orgUsers);

            var actual = _service.CompareUsers().ToList();
            
            CollectionAssert.AreEqual(expectedUsers, actual);
        }

        private User GetFakeUser()
        {
            return new User
            {
                Login = _lorem.Word(),
                Name = _lorem.Word()
            };
        }

        private List<User> GetFakeUsers(int count)
        {
            var users = new List<User>();
            for (var i = 0; i < count; i++)
            {
                users.Add(GetFakeUser());
            }
            return users;
        }
    }
}
