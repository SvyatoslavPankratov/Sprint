using System;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Timer.Models;

namespace UnitTest
{
    [TestClass]
    public class UTStopwatchModel
    {
        [TestMethod]
        public void StopwatchTest()
        {
            var stopwatch = new StopwatchModel();

            stopwatch.Start();

            Thread.Sleep(10000);

            var time = stopwatch.Time;

            stopwatch.Stop();
            time = stopwatch.Time;

            stopwatch.Continue();
            time = stopwatch.Time;

            stopwatch.Stop();
            stopwatch.Dispose();
        }
    }
}
