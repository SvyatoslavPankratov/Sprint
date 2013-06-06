using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NLog;

namespace UnitTest
{
    [TestClass]
    public class UTLogger
    {
        [TestMethod]
        public void LoggerTest()
        {
            var logger = LogManager.GetCurrentClassLogger();            

            logger.Debug("Test message.");
            logger.Trace("Test message.");
        }
    }
}
