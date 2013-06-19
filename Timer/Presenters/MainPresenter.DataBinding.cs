using System;
using System.Data;
using System.Linq;

using Sprint.Models;
using Sprint.Views;

namespace Sprint.Presenters
{
    /// <summary>
    /// Часть презентера, отвечающая на привязку данных на представление главного окна.
    /// </summary>
    partial class MainPresenter
    {
        #region Работа с таблицами результатов участников

        /// <summary>
        /// Инициализация всех таблиц.
        /// </summary>
        private void InitializeAllTables()
        {
            MainView.FwdFirstRace = InitializeTable();
            MainView.FwdSecondRace = InitializeTable();
            MainView.RwdFirstRace = InitializeTable();
            MainView.RwdSecondRace = InitializeTable();
            MainView.AwdFirstRace = InitializeTable();
            MainView.AwdSecondRace = InitializeTable();
            MainView.SportFirstRace = InitializeTable();
            MainView.SportSecondRace = InitializeTable();
        }

        /// <summary>
        /// Сгенерировать заголовки для таблицы результатов.
        /// </summary>
        private DataTable InitializeTable()
        {
            var table = new DataTable();

            var column = new DataColumn("№");
            table.Columns.Add(column);

            column = new DataColumn("Ф И О");
            table.Columns.Add(column);

            column = new DataColumn("Автомобиль");
            table.Columns.Add(column);

            column = new DataColumn("Прогревочный круг");
            table.Columns.Add(column);

            column = new DataColumn("Круг №1");
            table.Columns.Add(column);

            column = new DataColumn("Круг №2");
            table.Columns.Add(column);

            column = new DataColumn("Круг №3");
            table.Columns.Add(column);

            return table;
        }

        /// <summary>
        /// Генерация и заполнение таблицы участниками для первого заезда.
        /// </summary>
        public void SetRacersForTableForFirstRace()
        {
            MainView.FwdFirstRace = SetNamesToTable(CarClassesEnum.FWD, 0);
            MainView.RwdFirstRace = SetNamesToTable(CarClassesEnum.RWD, 0);
            MainView.AwdFirstRace = SetNamesToTable(CarClassesEnum.AWD, 0);
            MainView.SportFirstRace = SetNamesToTable(CarClassesEnum.Sport, 0);
        }

        /// <summary>
        /// Генерация и заполнение таблицы финалистами для второго заезда.
        /// </summary>
        public void SetRacersForTableForSecondRace()
        {
            MainView.FwdSecondRace = SetNamesToTable(CarClassesEnum.FWD, 1);
            MainView.RwdSecondRace = SetNamesToTable(CarClassesEnum.RWD, 1);
            MainView.AwdSecondRace = SetNamesToTable(CarClassesEnum.AWD, 1);
            MainView.SportSecondRace = SetNamesToTable(CarClassesEnum.Sport, 1);
        }

        /// <summary>
        /// Заполним таблицу именами из заданной группы участников в списке групп.
        /// </summary>
        /// <param name="carClass">Класс группы автомобилей.</param>
        /// <param name="raceNumber">Номер заезда из которого необходимо брать участников (начиная с 0).</param>
        /// <returns>Таблица, заполненная участниками.</returns>
        private DataTable SetNamesToTable(CarClassesEnum carClass, int raceNumber)
        {
            var table = InitializeTable();

            var group = RacerGroups.FirstOrDefault(gr => gr.CarClass == carClass && gr.RaceNumber == raceNumber);

            if (group == null)
            {
                return table;
            }

            foreach (var racer in group.Racers)
            {
                var row = table.NewRow();
                row[0] = racer.RacerNumber;
                row[1] = string.Format("{0} {1} {2}", racer.FirstName, racer.LastName, racer.MiddleName);
                row[2] = racer.Car.Name;
                table.Rows.Add(row);
            }

            return table;
        }

        /// <summary>
        /// Очистка таблицы с результатами.
        /// </summary>
        public void ClearResultsTable()
        {
            InitializeAllTables();
            SetRacersForTableForFirstRace();
        }

        #endregion

        #region Методы привязки данных [Есть недоработки]

        /// <summary>
        /// Привязка всех данных к таблицам на форме.
        /// </summary>
        public void DataBind()
        {
            foreach (var group in RacerGroups)
            {
                switch (group.CarClass)
                {
                    case CarClassesEnum.FWD:
                        {
                            OutFwdResultForGroup(group);
                        } break;

                    case CarClassesEnum.RWD:
                        {
                            OutRwdResultForGroup(group);
                        } break;

                    case CarClassesEnum.AWD:
                        {
                            OutAwdResultForGroup(group);
                        } break;

                    case CarClassesEnum.Sport:
                        {
                            OutSportResultForGroup(group);
                        } break;
                    default: break;
                }
            }
        }

        /// <summary>
        /// Вывести результаты на форму у переднеприводного класса автомобилей.
        /// </summary>
        /// <param name="group">Переднеприводная группа гонщиков.</param>
        private void OutFwdResultForGroup(RacersGroupModel group)
        {
            switch (group.RaceNumber)
            {
                case 0:
                    {
                        MainView.FwdFirstRace = GetTableWithResults(group, group.RaceNumber);
                    } break;
                case 1:
                    {
                        MainView.FwdSecondRace = GetTableWithResults(group, group.RaceNumber);
                    } break;
            }
        }

        /// <summary>
        /// Вывести результаты на форму у заднеприводного класса автомобилей.
        /// </summary>
        /// <param name="group">Заднеприводная группа гонщиков.</param>
        private void OutRwdResultForGroup(RacersGroupModel group)
        {
            switch (group.RaceNumber)
            {
                case 0:
                    {
                        MainView.RwdFirstRace = GetTableWithResults(group, group.RaceNumber);
                    } break;
                case 1:
                    {
                        MainView.RwdSecondRace = GetTableWithResults(group, group.RaceNumber);
                    } break;
            }
        }

        /// <summary>
        /// Вывести результаты на форму у полноприводного класса автомобилей.
        /// </summary>
        /// <param name="group">Полноприводная группа гонщиков.</param>
        private void OutAwdResultForGroup(RacersGroupModel group)
        {
            switch (group.RaceNumber)
            {
                case 0:
                    {
                        MainView.AwdFirstRace = GetTableWithResults(group, group.RaceNumber);
                    } break;
                case 1:
                    {
                        MainView.AwdSecondRace = GetTableWithResults(group, group.RaceNumber);
                    } break;
            }
        }

        /// <summary>
        /// Вывести результаты на форму у спортивного класса автомобилей.
        /// </summary>
        /// <param name="group">Спортивная группа гонщиков.</param>
        private void OutSportResultForGroup(RacersGroupModel group)
        {
            switch (group.RaceNumber)
            {
                case 0:
                    {
                        MainView.SportFirstRace = GetTableWithResults(group, group.RaceNumber);
                    } break;
                case 1:
                    {
                        MainView.SportSecondRace = GetTableWithResults(group, group.RaceNumber);
                    } break;
            }
        }

        /// <summary>
        /// Получить таблицу с результатами участников заданной группы заданного заезда.
        /// </summary>
        /// <param name="group">
        /// Класс автомобилей, для участников которых будет получена таблица с результатами заданной группы заездов.
        /// </param>
        /// <param name="raceNumber">Номер заезда, результаты которого требуется получить начиная с 0.</param>
        /// <returns>Таблица с результатами.</returns>
        private DataTable GetTableWithResults(RacersGroupModel group, int raceNumber)
        {
            var table = SetNamesToTable(group.CarClass, raceNumber);

            for (int row = 0; row < group.Racers.Count(); row++)
            {
                var racer = group.Racers.ElementAt(row);

                if (racer.Results.ResultsList.ElementAt(raceNumber) == null)
                {
                    return table;
                }

                for (int circle = 0; circle < ConstantsModel.MaxCircleCount; circle++)
                {
                    var time = racer.Results.ResultsList.ElementAt(raceNumber).ElementAt(circle);

                    if (time != null)
                    {
                        table.Rows[row][circle + 3] = time.ToString();
                    }
                }
            }

            return table;
        }

        #endregion
    }
}
