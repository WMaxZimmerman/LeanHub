using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeanHub.Shared.Models;
using LeanHub.Shared.Helpers;
using Moq;
using Bogus.DataSets;
using LeanHub.ApplicationCore.Services;
using LeanHub.Console.Controllers;


namespace LeanHub.Tests.Console.Controller
{
    [TestClass]
    public class UserControllerTests
    {
        private Mock<IUserService> _mockService;
        private Mock<IConsoleHelper> _mockConsole;
        private UserController _controller;
        private Random _randy;
        private Lorem _lorem;
            
        [TestInitialize]
        public void Init()
        {
            _randy = new Random();
            _lorem = new Lorem(locale: "en");
            _mockService = new Mock<IUserService>();
            _mockConsole = new Mock<IConsoleHelper>();
            _controller = new UserController(_mockService.Object, _mockConsole.Object);
        }

        [TestMethod]
        public void AddUser_CallsAddUserToOrgService_WithNameUserNameandPassword()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var user = GetFakeMember();
            _mockService.Setup(s => s.AddUserToOrg(name)).Returns(user);

            _controller.AddUser(name);
           
            _mockService.Verify(s => s.AddUserToOrg(name), Times.Once);
         
        } 

        [TestMethod]
        public void AddUser_CallsConsoleHelper_WithCorrectMessage()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var user = GetFakeMember();
            var messageActual = $"{user.User.Login} ({user.State})";
            _mockService.Setup(s => s.AddUserToOrg(name)).Returns(user);
           
            _controller.AddUser(name);
           
            _mockConsole.Verify(c => c.WriteLine(messageActual), Times.Once);
        } 

        [TestMethod]
        public void RemoveUser_CallsRemoveUserFromOrgService_WithNameUserNameandPassword()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var expectedResult = false;
            var user = GetFakeMember();
            _mockService.Setup(s => s.RemoveUserFromOrg(name)).Returns(expectedResult);

            _controller.RemoveUser(name);
           
            _mockService.Verify(s => s.RemoveUserFromOrg(name), Times.Once);
         
        } 

        [TestMethod]
        public void RemoveUser_CallsConsoleHelper_WithCorrectMessage_WhenTrue()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var expectedResult = true;
            var messageActual = $"{name} was successfully removed";
            _mockService.Setup(s => s.RemoveUserFromOrg(name)).Returns(expectedResult);
           
            _controller.RemoveUser(name);
           
            _mockConsole.Verify(c => c.WriteLine(messageActual), Times.Once);
        } 

        [TestMethod]
        public void RemoveUser_CallsConsoleHelper_WithCorrectMessage_WhenFalse()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var expectedResult = false;
            var messageActual = $"something went wrong trying to remove {name}";
            _mockService.Setup(s => s.RemoveUserFromOrg(name)).Returns(expectedResult);
           
            _controller.RemoveUser(name);
           
            _mockConsole.Verify(c => c.WriteLine(messageActual), Times.Once);
        } 

        [TestMethod]
        public void GetUsers_CallsGetUsersService_WithUserNameandPassword()
        {
            var username = _lorem.Word();
            var password = _lorem.Word();
            var expectedResult = new List<User>();
            var user = GetFakeMember();
            _mockService.Setup(s => s.GetUsers()).Returns(expectedResult);

            _controller.GetUsers();
           
            _mockService.Verify(s => s.GetUsers(), Times.Once);
         
        } 

        [TestMethod]
        public void GetUsers_CallsConsoleHelper_WhenUsersIsEmpty_ReturnsEmptyList()
        {
            var username = _lorem.Word();
            var password = _lorem.Word();
            var expectedResult = new List<User>();
            _mockService.Setup(s => s.GetUsers()).Returns(expectedResult);
           
            _controller.GetUsers();
           
            _mockConsole.Verify(c => c.WriteLine(It.IsAny<string>()), Times.Never);
        } 

        [TestMethod]
        public void GetUsers_CallsConsoleHelper_WhenUsersIsNotEmpty_ReturnsUserList()
        {
            var username = _lorem.Word();
            var password = _lorem.Word();
            var expectedResult = new List<User>();
            expectedResult.Add(GetFakeUser());
            expectedResult.Add(GetFakeUser());
            expectedResult.Add(GetFakeUser());
            var expectedMessages = expectedResult.Select(u => u.Login);         
            _mockService.Setup(s => s.GetUsers()).Returns(expectedResult);
           
            _controller.GetUsers();
           
           foreach(var message in expectedMessages)
           {
                _mockConsole.Verify(c => c.WriteLine(message), Times.Once);
           }
        }

        private Member GetFakeMember()
        {
            return new Member
            {
                State = _lorem.Word(),
                User = GetFakeUser()
            };
        }

        private User GetFakeUser()
        {
            return new User { Login = _lorem.Word() };
        }
    }
}
