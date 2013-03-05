using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sprint.Managers;
using Sprint.Exceptions;

namespace UnitTest
{
    [TestClass]
    public class UTExceptionsManager
    {
        [TestMethod]
        public void CreateExceptionMessageTest()
        {
            var message = ExceptionsManager.CreateExceptionMessage(new SprintBusinessLogicException("New exception.", "CreateExceptionMessageTest()"));
            var variable = 
@"Method name: CreateExceptionMessageTest()

Message: New exception.

StackTrace: ";

            Assert.AreEqual(message, variable, true);
        }
    }
}
