using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sprint.Data;
using Sprint.Managers;
using Sprint.Models;

namespace UnitTest
{
    [TestClass]
    public class UTOptionsDbManager
    {
        [TestMethod]
        public void SetOptions()
        {
            var id = Guid.NewGuid();

            var carClasses = CarClassesMeneger.GetCarClasses();
            var carClass = carClasses.FirstOrDefault(cc => cc.Name == CarClassesEnum.FWD.ToString());
            var options = new RacesOption { Id = id, CarClass = carClass, LidersCount = 5, RaceCount = 2 };

            var res = OptionsDbManager.SetOptions(options);

            Assert.IsTrue(res.Result);
        }

        [TestMethod]
        public void DeleteOptions()
        {
            var res = OptionsDbManager.DeleteOptions(CarClassesEnum.FWD);

            Assert.IsTrue(res.Result);
        }
    }
}
