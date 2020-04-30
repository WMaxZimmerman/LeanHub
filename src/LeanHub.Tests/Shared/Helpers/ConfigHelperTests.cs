using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeanHub.Shared.Helpers;
using Moq;
using Bogus.DataSets;

namespace LeanHub.Tests.Shared.Helpers
{
    [TestClass]
    public class ConfigHelperTests
    {
        private Mock<IEnvironmentHelper> _mockEnv;
        private ConfigHelper _config;
        private Lorem _lorem;

        [TestInitialize]
        public void Init()
        {
            _mockEnv = new Mock<IEnvironmentHelper>();
            _config = new ConfigHelper(_mockEnv.Object);
            _lorem = new Lorem(locale: "en");
        }

        [TestMethod]
        public void Username_CallsGetValue_WithExpectedName_AndReturnsValue()
        {
            var expectedName = "GITHUB_USERNAME";
            var expectedValue = _lorem.Word();
            _mockEnv.Setup(e => e.GetValue(expectedName)).Returns(expectedValue);

            var actual = _config.Username;

            _mockEnv.Verify(e => e.GetValue(expectedName), Times.Once);
            Assert.AreEqual(expectedValue, actual);
        }

        [TestMethod]
        public void Password_CallsGetValue_WithExpectedName_AndReturnsValue()
        {
            var expectedName = "GITHUB_PASSWORD";
            var expectedValue = _lorem.Word();
            _mockEnv.Setup(e => e.GetValue(expectedName)).Returns(expectedValue);

            var actual = _config.Password;

            _mockEnv.Verify(e => e.GetValue(expectedName), Times.Once);
            Assert.AreEqual(expectedValue, actual);
        }
    }
}
