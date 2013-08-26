using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Sprint.Models;

namespace Sprint.Interfaces
{
    /// <summary>
    /// Интерфейс главного окна.
    /// </summary>
    public interface IMain
    {
        #region Делегаты

        /// <summary>
        /// Задать или получиь делегат на метод, который будет 
        /// отрабатывать после окончания перерисовки какой-либо 
        /// таблицы с результатами.
        /// </summary>
        EventHandler TablePainted { get; set; }

        #endregion

        #region Свойства

        /// <summary>
        /// Задать или получить таблицу с результатами переднеприводных автомобилей за первый заезд.
        /// </summary>
        DataTable FwdFirstRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами переднеприводных автомобилей за второй заезд.
        /// </summary>
        DataTable FwdSecondRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами заднеприводных автомобилей за первый заезд.
        /// </summary>
        DataTable RwdFirstRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами заднеприводных автомобилей за второй заезд.
        /// </summary>
        DataTable RwdSecondRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами полноприводныэх автомобилей за первый заезд.
        /// </summary>
        DataTable AwdFirstRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами полноприводных автомобилей за второй заезд.
        /// </summary>
        DataTable AwdSecondRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами спортивных автомобилей за первый заезд.
        /// </summary>
        DataTable SportFirstRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами спортивных автомобилей за второй заезд.
        /// </summary>
        DataTable SportSecondRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами автомобилей с мощностью до 100 л/с за первый заезд.
        /// </summary>
        DataTable K100FirstRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами автомобилей с мощностью до 100 л/с за второй заезд.
        /// </summary>
        DataTable K100SecondRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами автомобилей с мощностью от 100 л/с до 160 л/с за первый заезд.
        /// </summary>
        DataTable K160FirstRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами автомобилей  с мощностью от 100 л/с до 160 л/с за второй заезд.
        /// </summary>
        DataTable K160SecondRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами автомобилей с мощностью от 160 л/с за первый заезд.
        /// </summary>
        DataTable KAFirstRace { get; set; }

        /// <summary>
        /// Задать или получить таблицу с результатами автомобилей с мощностью от 160 л/с за второй заезд.
        /// </summary>
        DataTable KASecondRace { get; set; }

        /// <summary>
        /// Задать или получть количество минут.
        /// </summary>
        int Min { set; }

        /// <summary>
        /// Задать или получить количество секунд.
        /// </summary>
        int Sec { set; }

        /// <summary>
        /// Задать или получить количество милисекунд.
        /// </summary>
        int Mlsec { set; }

        /// <summary>
        /// Задать первого текущего участника на трассе.
        /// </summary>
        int FirstCurrentRacer { get;  set; }

        /// <summary>
        /// Задать второго текущего участника на трассе.
        /// </summary>
        int SecondCurrentRacer { get;  set; }

        /// <summary>
        /// Задать следующего текущего участника на трассе.
        /// </summary>
        int NextCurrentRacer { get; set; }

        /// <summary>
        /// Задать состояние готовности следующему участнику, который должен будет выходить на трек.
        /// </summary>
        NextRacerState NextRacerState { get; set; }

        /// <summary>
        /// Задать или получить состояние секундомера.
        /// </summary>
        StopwatchStatesEnum StopwatchState { get; set; }

        #endregion

        #region Методы

        /// <summary>
        /// Задать положение окна относительно положения рабочего стола.
        /// </summary>
        /// <param name="x">Координата по X.</param>
        /// <param name="y">Координата по Y.</param>
        void SetDesktopLocation(int x, int y);

        /// <summary>
        /// Задать цвет для заданной строки в таблице у заданного класса автомобилей для заданного заезда.
        /// </summary>
        /// <param name="car_class">Класс автомобилей.</param>
        /// <param name="race_number">Номер заезда (0 или 1).</param>
        /// <param name="row_number">Номер строчки в таблице, которую будем красить.</param>
        /// <param name="row_color">Задаваемый цвет.</param>
        void SetRowColor(CarClassesEnum car_class, int race_number, int row_number, Color row_color);

        #endregion
    }
}
