using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sprint.Managers;

namespace UnitTest.Managers
{
    [TestClass]
    public class UTDatabaseBackupManager

    {
        [TestMethod]
        public void CreateNewDatabaseBackup()
        {
            var res = DatabaseBackupManager.CreateNewDatabaseBackup();

            Assert.IsTrue(res.Result);
        }
    }
}
