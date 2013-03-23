using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sprint.Managers;

namespace UnitTest.Managers
{
    [TestClass]
    public class UTScreenManager
    {
        [TestMethod]
        public void TestClassFromFirstMonitor()
        {
            var scr_manager = new ScreenManager();

            Assert.AreEqual(scr_manager.MonitorCount, 1);
        }

        [TestMethod]
        public void TestClassFromSecondMonitor()
        {
            var scr_manager = new ScreenManager();

            Assert.AreEqual(scr_manager.MonitorCount, 2);
        }

        [TestMethod]
        public void TestClassFromThreeMonitor()
        {
            var scr_manager = new ScreenManager();

            Assert.AreEqual(scr_manager.MonitorCount, 3);
        }
    }
}
