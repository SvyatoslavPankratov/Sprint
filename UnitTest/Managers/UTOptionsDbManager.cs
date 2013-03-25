using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sprint.Data;
using Sprint.Managers;
using Sprint.Models;

namespace UnitTest.Managers
{
    [TestClass]
    public class UTOptionsDbManager
    {
        [TestMethod]
        public void SetOptions()
        {
            var id = Guid.NewGuid();

            var carClasses = CarClassesDbMeneger.GetCarClasses();
            var options = new RaceOptionsModel(CarClassesEnum.FWD) { LidersCount = 5, RaceCount = 2 };

            var res = OptionsDbManager.SetOptions(options);

            Assert.IsTrue(res.Result);
        }

        [TestMethod]
        public void DeleteOptions()
        {
            SetOptions();

            var res = OptionsDbManager.DeleteOptions(CarClassesEnum.FWD);

            Assert.IsTrue(res.Result);
        }
    }
}
