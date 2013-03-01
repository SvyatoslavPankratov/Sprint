using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Sprint.Models;
using Sprint.Views;

namespace Sprint.Presenters
{
    /// <summary>
    /// Презентер окна ввода гонщиков.
    /// </summary>
    public class NewRacerPresenter
    {
        #region Properties

        /// <summary>
        /// Задать или получить 
        /// </summary>
        private INewRacerView NewRacerView { get; set; }

        /// <summary>
        /// Задать или получить список гонщиков.
        /// </summary>
        public List<RacerModel> Racers { get; set; }

        /// <summary>
        /// Получить список слассов автомобилей.
        /// </summary>
        private List<string> CarClasses
        {
            get
            {
                return Enum.GetNames(Type.GetType("Sprint.Models.CarClassesEnum")).ToList();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="newRaserForm">Ссылка на форму.</param>
        public NewRacerPresenter(INewRacerView newRaserForm)
        {
            NewRacerView = newRaserForm;

            if (NewRacerView != null)
            {
                NewRacerView.CarClasses = CarClasses;                

                Racers = new List<RacerModel>();

                NewRacerView.RacersTable = CreateTable();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///  Добавление нового участника.
        /// </summary>
        public void AddNewRacer()
        {
            var num = 1;

            if (Racers.Count > 0)
            {
                num = Racers.Last().RacerNumber + 1;
            }

            // Добавлениее новго гонщика в структуру
            Racers.Add(new RacerModel(NewRacerView.FirstName,
                                        NewRacerView.LastName,
                                        NewRacerView.MiddleName,
                                        NewRacerView.CarName,
                                        NewRacerView.CarClass) { RacerNumber = num });

            // Добавление нового гонщика в таблицу
            var racer   = Racers.Last();
            var table   = NewRacerView.RacersTable;
            var row     = table.NewRow();

            row[0] = racer.RacerNumber;
            row[1] = racer.FirstName;
            row[2] = racer.LastName;
            row[3] = racer.MiddleName;
            row[4] = racer.Car.Name;
            row[5] = racer.Car.CarClass.ToString();

            table.Rows.Add(row);

            NewRacerView.FirstName  = string.Empty;
            NewRacerView.LastName   = string.Empty;
            NewRacerView.MiddleName = string.Empty;
            NewRacerView.CarName    = string.Empty;
        }

        /// <summary>
        /// Сгенерировть заголовок таблицы.
        /// </summary>
        /// <returns>Сгенерированная таблица.</returns>
        private DataTable CreateTable()
        {
            var table = new DataTable();

            var column = new DataColumn("№");
            table.Columns.Add(column);

            column = new DataColumn("Фамилия");
            table.Columns.Add(column);

            column = new DataColumn("Имя");
            table.Columns.Add(column);

            column = new DataColumn("Отчетсво");
            table.Columns.Add(column);

            column = new DataColumn("Автомобиль");
            table.Columns.Add(column);

            column = new DataColumn("Класс");
            table.Columns.Add(column);

            return table;
        }

        /// <summary>
        /// Удалить гонщика из списка участников.
        /// </summary>
        /// <param name="index"></param>
        public void DeleteRacer(int index)
        {
            Racers.RemoveAt(index);
        }

        #endregion
    }
}
