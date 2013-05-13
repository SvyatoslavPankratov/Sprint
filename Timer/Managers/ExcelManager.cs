using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

using NLog;
using OfficeOpenXml;
using OfficeOpenXml.Style;

using Sprint.Exceptions;
using Sprint.Models;

namespace Sprint.Managers
{
    /// <summary>
    /// Менеджер для работы с Excel.
    /// </summary>
    public static class ExcelManager
    {
        #region Поля только для чтения

        /// <summary>
        /// Логгер.
        /// </summary>
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Методы

        /// <summary>
        /// Создать Excel документ и выгрузить в него все текущие данные.
        /// </summary>
        /// <param name="all_tables">Таблицы с текущими результатами гонок.</param>
        /// <returns>Результат операции.</returns>
        public static OperationResult CreateResultExcelDocument(IEnumerable<TableWithResults> all_tables)
        {
            if (all_tables == null || all_tables.Count() == 0)
            {
                var exception = new SprintExcelException("Не удалось создать документ Ecxel, т.к. для его создания не были переданы таблицы с текущими результатами гонок.",
                                                            "Sprint.Managers.ExcelManager.CreateResultExcelDocument(IEnumerable<TableWithResults> all_tables)");
                logger.Trace(ExceptionsManager.CreateExceptionMessage(exception));
                return new OperationResult(false, exception.Message, exception);
            }

            try
            {
                using (var doc = new ExcelPackage())
                {
                    SetDocProperties(doc);

                    foreach (var carClass in Enum.GetNames(Type.GetType("Sprint.Models.CarClassesEnum")))
                    {
                        var tables = from tbl in all_tables
                                     where tbl.CarClass.ToString() == carClass
                                     select tbl;

                        if (tables != null)
                        {
                            var ws_1 = CreateSheet(doc, carClass + " (Заезд 1)");
                            var ws_2 = CreateSheet(doc, carClass + " (Заезд 2)");

                            foreach (var table in tables)
                            {
                                switch (table.RaceNumber)
                                {
                                   case 1:
                                        {                                            
                                            CreateHeader(ws_1, 1, table.Results);
                                            CreateData(ws_1, 1, table.Results);
                                        } break;
                                    case 2:
                                        {                                            
                                            CreateHeader(ws_2, 1, table.Results);
                                            CreateData(ws_2, 1, table.Results);
                                        } break;
                                }
                            }
                        }
                    }

                    var bin = doc.GetAsByteArray();
                    var file = Guid.NewGuid().ToString() + ".xlsx";

                    File.WriteAllBytes(file, bin);


                    // Открываем файл в Excel
                    var pi = new ProcessStartInfo(file);
                    Process.Start(pi);
                }

                return new OperationResult(true);
            }
            catch (Exception ex)
            {
                var exception = new SprintException("Не удалось создать Excel документ и выгрузить в него все текущие данныке.",
                                                    "Sprint.Managers.ExcelManager.CreateResultExcelDocument(IEnumerable<TableWithResults> all_tables)", ex);
                logger.Error(ExceptionsManager.CreateExceptionMessage(exception));
                throw exception;
            }
        }

        /// <summary>
        /// Задать свойства документа.
        /// </summary>
        /// <param name="p">Документ, над которым проводится действие.</param>
        private static void SetDocProperties(ExcelPackage p)
        {
            p.Workbook.Properties.Author = "Sprint";
            p.Workbook.Properties.Title = "Результаты заездов";
        }

        /// <summary>
        /// Создать базовые внутренности докукмента.
        /// </summary>
        /// <param name="p">Документ, над которым проводится действие.</param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        private static ExcelWorksheet CreateSheet(ExcelPackage p, string sheetName)
        {
            var ws = p.Workbook.Worksheets.Add(sheetName);
            
            ws.Name = sheetName;
            ws.Cells.Style.Font.Size = 11;
            ws.Cells.Style.Font.Name = "Calibri";

            return ws;
        }

        /// <summary>
        /// Задать заголовок для таблицы на заданной странице.
        /// </summary>
        /// <param name="ws">Страница Excel'а на которой будет задан заголовок..</param>
        /// <param name="rowIndex">Номер строки на странице Excel'а в которой будет задан заголовок.</param>
        /// <param name="dt">Таблица заголовок которой будет задан на заданной странице Excel'а.</param>
        private static void CreateHeader(ExcelWorksheet ws, int rowIndex, DataTable dt)
        {
            int colIndex = 1;

            foreach (DataColumn dc in dt.Columns)
            {
                switch(colIndex)
                {
                    case 1:
                        {
                            ws.Column(colIndex).Width = 5;
                        } break;
                    case 2:
                        {
                            ws.Column(colIndex).Width = 45;
                        } break;
                    case 3:
                        {
                            ws.Column(colIndex).Width = 40;
                        } break;
                    case 4:
                        {
                            ws.Column(colIndex).Width = 20;
                        } break;
                    case 5:
                        {
                            ws.Column(colIndex).Width = 20;
                        } break;
                    case 6:
                        {
                            ws.Column(colIndex).Width = 20;
                        } break;
                    case 7:
                        {
                            ws.Column(colIndex).Width = 20;
                        } break;
                }

                ws.Row(1).Height = 20;

                var cell = ws.Cells[rowIndex, colIndex];
                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                cell.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                cell.Style.Border.Bottom.Color.SetColor(Color.Black);

                var fill = cell.Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.LightSlateGray);

                var font = cell.Style.Font;
                font.Bold = true;
                font.Color.SetColor(Color.White);

                cell.Value = dc.ColumnName;

                colIndex++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="rowIndex"></param>
        /// <param name="dt"></param>
        private static void CreateData(ExcelWorksheet ws, int rowIndex, DataTable dt)
        {
            int colIndex = 0;

            foreach (DataRow dr in dt.Rows) 
            {
                colIndex = 1;
                rowIndex++;

                foreach (DataColumn dc in dt.Columns)
                {
                    var cell = ws.Cells[rowIndex, colIndex];
                    cell.Value = dr[dc.ColumnName];

                    switch (colIndex)
                    {
                        case 1:
                            {
                                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            } break;
                        case 2:
                            {
                                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            } break;
                        case 3:
                            {
                                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            } break;
                        case 4:
                            {
                                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            } break;
                        case 5:
                            {
                                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            } break;
                        case 6:
                            {
                                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            } break;
                        case 7:
                            {
                                cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            } break;
                    }

                    colIndex++;
                }

                var fill = ws.Cells[rowIndex, 1].Style.Fill;
                fill.PatternType = ExcelFillStyle.Solid;
                fill.BackgroundColor.SetColor(Color.Orange);
            }
        }

        #endregion
    }
}
