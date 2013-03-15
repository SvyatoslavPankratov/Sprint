using System;
using System.Collections.Generic;
using System.Data;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Sprint.Managers;
using Sprint.Models;

namespace UnitTest.Managers
{
    [TestClass]
    public class UTExcelManager
    {
        [TestMethod]
        public void CreateExcelDocument()
        {
            var tables = new List<TableWithResults>();

            var table = new DataTable();

            var column = new DataColumn("№");
            table.Columns.Add(column);

            column = new DataColumn("Ф И О");
            table.Columns.Add(column);

            column = new DataColumn("Автомобиль");
            table.Columns.Add(column);

            column = new DataColumn("Круг №1");
            table.Columns.Add(column);

            column = new DataColumn("Круг №2");
            table.Columns.Add(column);

            column = new DataColumn("Круг №3");
            table.Columns.Add(column);

            column = new DataColumn("Круг №4");
            table.Columns.Add(column);

            tables.Add(new TableWithResults(table, CarClassesEnum.FWD, 1));

            ExcelManager.CreateResultExcelDocument(tables);
        }
    }
}
