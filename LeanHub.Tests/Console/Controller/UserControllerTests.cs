using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeanHub.DAL.Repositories;
using LeanHub.Shared.Models;
using Moq;
using Bogus.DataSets;
using System.Net.Http.Headers;
using LeanHub.ApplicationCore.Services;
using LeanHub.Console.Controllers;

namespace LeanHub.Tests.Console.Controller
{
    [TestClass]
    public class UserControllerTests
    {
    
        
        private Mock<IUserService> _mockService;
        private UserController _controller;
        private Random _randy;
        private Lorem _lorem;

    
        [TestInitialize]
        public void Init()
        {
            _randy = new Random();
            _lorem = new Lorem(locale: "en");
            _mockService = new Mock<IUserService>();
            _controller = new UserController(_mockService.Object);
        }

        [TestMethod]
        public void AddUser_CallsAddUserToOrgService_WithNameUserNameandPassword()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var user = new Member
            {
                User = new User()
            };

            _mockService.Setup(s => s.AddUserToOrg(name, username, password)).Returns(user);

            _controller.AddUser(name, username, password);
           
            _mockService.Verify(s => s.AddUserToOrg(name, username, password), Times.Once);
         
        } 

        [TestMethod]
        public void RemoveUser_CallsRemoveUserFromOrgService_WithNameUserNameandPassword()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var expectedResult = false;
            var user = new Member
            {
                User = new User()
            };

            _mockService.Setup(s => s.RemoveUserFromOrg(name, username, password)).Returns(expectedResult);

            _controller.RemoveUser(name, username, password);
           
            _mockService.Verify(s => s.RemoveUserFromOrg(name, username, password), Times.Once);
         
        } 

        [TestMethod]
        public void GetUsers_CallsGetUsersService_WithUserNameandPassword()
        {
            var name = _lorem.Word();
            var username = _lorem.Word();
            var password = _lorem.Word();
            var expectedResult = new List<User>();
            var user = new Member
            {
                User = new User()
            };

            _mockService.Setup(s => s.GetUsers(username, password)).Returns(expectedResult);

            _controller.GetUsers(username, password);
           
            _mockService.Verify(s => s.GetUsers(username, password), Times.Once);
         
        } 
    }
}