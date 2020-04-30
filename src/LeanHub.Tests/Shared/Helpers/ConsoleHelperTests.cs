using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LeanHub.Shared.Helpers;

namespace LeanHub.Tests.Shared.Helpers
{
    [TestClass]
    public class ConsoleHelperTests
    {
        private StringWriter consoleMock;
        private StringBuilder mockConsole = new StringBuilder();
        private ConsoleHelper _console;

        [TestInitialize]
        public void Init()
        {
            consoleMock = new StringWriter(mockConsole);
            System.Console.SetOut(consoleMock);

            _console = new ConsoleHelper();
        }

        [TestMethod]
        public void WriteLine_OutputsLineToConsole()
        {
            var message = "hello world";
            var expectedMessage = message + Environment.NewLine;
            _console.WriteLine(message);

            Assert.AreEqual(expectedMessage, mockConsole.ToString());
        }
    }
}