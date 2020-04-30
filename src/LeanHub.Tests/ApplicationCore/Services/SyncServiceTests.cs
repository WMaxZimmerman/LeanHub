using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Bogus.DataSets;
using LeanHub.ApplicationCore.Services;
using LeanHub.Shared.Models;
using System.Collections.Generic;

namespace LeanHub.Tests.ApplicationCore.Services
{
    [TestClass]
    public class SyncServiceTests
    {
     
        private SyncService _service;
        private Mock<ICompareService> _mockCompare;
        private Mock<IUserService> _mockUser;
        private Random _randy;
        private Lorem _lorem;


        [TestInitialize]
        public void Init()
        {
            _randy = new Random();
            _lorem = new Lorem(locale: "en");
            _mockCompare = new Mock<ICompareService>();
            _mockUser = new Mock<IUserService>();
            _service = new SyncService(_mockCompare.Object, _mockUser.Object);
        }

        [TestMethod]
        public void SyncUsers_CallsCompareUsers()
        {
            var users = GetFakeUsers(0);
            _mockCompare.Setup(c => c.CompareUsers()).Returns(users);
            
            _service.SyncUsers();

            _mockCompare.Verify(s => s.CompareUsers(), Times.Once);
        }

        [TestMethod]
        public void SyncUsers_CallsRemoveUserFromOrg_ForEachUserWithActionRemove()
        {
            var users = GetFakeUsers(3, LeanHub.Shared.Enums.Action.Remove);
            _mockCompare.Setup(c => c.CompareUsers()).Returns(users);
            
            _service.SyncUsers();

            foreach (var user in users)
            {
                _mockUser.Verify(u => u.RemoveUserFromOrg(user.Login), Times.Once);
            }
        }

        [TestMethod]
        public void SyncUsers_CallsAddUserToOrg_ForEachUserWithActionAdd()
        {
            var users = GetFakeUsers(3, LeanHub.Shared.Enums.Action.Add);
            _mockCompare.Setup(c => c.CompareUsers()).Returns(users);
            
            _service.SyncUsers();

            foreach (var user in users)
            {
                _mockUser.Verify(u => u.AddUserToOrg(user.Login), Times.Once);
            }
        }

        private ComparedUser GetFakeUser(Nullable<LeanHub.Shared.Enums.Action> action = null)
        {
            return new ComparedUser
            {
                Login = _lorem.Word(),
                Name = _lorem.Word(),
                Action = action ?? LeanHub.Shared.Enums.Action.Nothing
            };
        }

        private List<ComparedUser> GetFakeUsers(int count, Nullable<LeanHub.Shared.Enums.Action> action = null)
        {
            var users = new List<ComparedUser>();
            for (var i = 0; i < count; i++)
            {
                users.Add(GetFakeUser(action));
            }
            return users;
        }
    }
}
