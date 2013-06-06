using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sprint.Managers;
using Sprint.Exceptions;

namespace UnitTest.Managers
{
    [TestClass]
    public class UTExceptionsManager
    {
        [TestMethod]
        public void CreateExceptionMessageTest()
        {
            var message = ExceptionsManager.CreateExceptionMessage(new SprintBusinessLogicException("New exception.", "CreateExceptionMessageTest()"));
            var variable = "Method name: CreateExceptionMessageTest()\r\n\r\nMessage: New exception.\r\n\r\n--------------------------------------------------------------------------------------\r\nStackTrace: \r\n\r\n";

            Assert.AreEqual(message, variable, true);
        }
    }
}
