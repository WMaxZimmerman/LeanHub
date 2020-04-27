using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeanHub.DAL.Repositories;
using LeanHub.Shared.Models;
using Moq;
using Bogus.DataSets;
using System.Net.Http.Headers;
using LeanHub.ApplicationCore.Services;

namespace LeanHub.Tests.ApplicationCore.Services
{
    [TestClass]
    public class UserServiceTests
    {
     
        private IUserService _service;
        private Mock<IGitHubRepository> _mockRepo;
        private Random _randy;
        private Lorem _lorem;


        [TestInitialize]
        public void Init()
        {
            _randy = new Random();
            _lorem = new Lorem(locale: "en");
            _mockRepo = new Mock<IGitHubRepository>();
            _service = new UserService(_mockRepo.Object);
        }

        [TestMethod]
        public void AddUserToOrg_CallsGetCredentials_WithUserNameandPassword()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var expectedAuth = new AuthenticationHeaderValue(_lorem.Word());

            _mockRepo.Setup(s => s.GetCredentials(username, password)).Returns(expectedAuth);

            var actual = _service.AddUserToOrg(name,username,password);

            _mockRepo.Verify(s => s.GetCredentials(username, password), Times.Once);
        }      

        [TestMethod]
        public void AddUserToOrg_CallsAddUserToOrg_WithNameandAuth_AndReturnsValue()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var auth = new AuthenticationHeaderValue(_lorem.Word());
            var expectedResult = new Member();
            _mockRepo.Setup(s => s.GetCredentials(username, password)).Returns(auth);
            _mockRepo.Setup(s => s.AddUserToOrg(name, auth)).Returns(expectedResult);

            var actual = _service.AddUserToOrg(name, username, password);

            _mockRepo.Verify(s => s.AddUserToOrg(name, auth), Times.Once); 

            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod]
        public void RemoveUserFromOrg_CallsGetCredentials_WithUserNameandPassword()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var expectedAuth = new AuthenticationHeaderValue(_lorem.Word());

            _mockRepo.Setup(s => s.GetCredentials(username, password)).Returns(expectedAuth);

            var actual = _service.RemoveUserFromOrg(name,username,password);

            _mockRepo.Verify(s => s.GetCredentials(username, password), Times.Once);
        }      
          
        [TestMethod]
        public void RemoveUserFromOrg_CallsRemoveUserFromOrg_WithNameandAuth_AndReturnsValue()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var auth = new AuthenticationHeaderValue(_lorem.Word());
            var expectedResult = true;
            _mockRepo.Setup(s => s.GetCredentials(username, password)).Returns(auth);
            _mockRepo.Setup(s => s.RemoveUserFromOrg(name, auth)).Returns(expectedResult);

            var actual = _service.RemoveUserFromOrg(name, username, password);

            _mockRepo.Verify(s => s.RemoveUserFromOrg(name, auth), Times.Once); 

            Assert.AreEqual(expectedResult, actual);
        }

        [TestMethod]
        public void GetUsers_CallsGetCredentials_WithUserNameandPassword()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var expectedAuth = new AuthenticationHeaderValue(_lorem.Word());

            _mockRepo.Setup(s => s.GetCredentials(username, password)).Returns(expectedAuth);

            var actual = _service.GetUsers(username,password);

            _mockRepo.Verify(s => s.GetCredentials(username, password), Times.Once);
        }      
          
        [TestMethod]
        public void GetUsers_CallsGetUsers_WithAuth_AndReturnsListOfUsers()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var auth = new AuthenticationHeaderValue(_lorem.Word());
            var expectedResult = new List<User>();
            _mockRepo.Setup(s => s.GetCredentials(username, password)).Returns(auth);
            _mockRepo.Setup(s => s.GetListOfUsers(auth)).Returns(expectedResult);

            var actual = _service.GetUsers(username, password);

            _mockRepo.Verify(s => s.GetListOfUsers(auth), Times.Once); 

            Assert.AreEqual(expectedResult, actual);
        }
    }
}