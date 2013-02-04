using System;
using System.Data;
using System.Threading;

using Timer.Models;
using Timer.Views;

namespace Timer.Presenters
{
    class MainPresenter : IDisposable
    {
        #region Properties

        /// <summary>
        /// Задать или получить интерфейс на главную форму.
        /// </summary>
        private IMainView MainView { get; set; }

        /// <summary>
        /// Задать или получить секундомер.
        /// </summary>
        private StopwatchModel Stopwatch { get; set; }

        /// <summary>
        /// Задать или получить поток для осуществления моментального вывода данных секундомера на форму.
        /// </summary>
        private Thread ThreadSync { get; set; }

        /// <summary>
        /// Задать или получить флаг реверса выводимой отсечки.
        /// </summary>
        private bool Reverse { get; set; }

        /// <summary>
        /// Задать или получить первая-ли у секундомера отсечка после его остановки.
        /// </summary>
        private bool Stoped { get; set; }

        /// <summary>
        /// Задать или получить в какую колонку нужно добавлять новое значение, без учета флага реверса.
        /// </summary>
        private bool LeftRight { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="mainView">Интерфейс на главную форму.</param>
        public MainPresenter(IMainView mainView)
        {
            MainView = mainView;

            Stopwatch = new StopwatchModel();
            MainView.Results = CreateResTable();

            ThreadSync = new Thread(() => DataBindingProcess(MainView, Stopwatch));

            ResetFlags();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Сброс всех флагов в изначальное состояние.
        /// </summary>
        private void ResetFlags()
        {
            Reverse = false;
            LeftRight = true;
            Stoped = true;
        }

        /// <summary>
        /// Запустить секундомер.
        /// </summary>
        public void StartStopwatch()
        {
            Stopwatch.Start();
            
            try
            {
                ThreadSync.Start();
            }
            catch (ThreadStateException)
            {
                ThreadSync.Resume();
            }
        }

        /// <summary>
        /// Остановить секундомер.
        /// </summary>
        public void StopStopwatch()
        {
            Stopwatch.Stop();
            ThreadSync.Suspend();

            ResetFlags();
        }

        /// <summary>
        /// Произвести отсечку времени и добавить результат в таблицу.
        /// </summary>
        public void CutOffStopwatch()
        {
            var time = Stopwatch.Time;
            var row = MainView.Results.NewRow();

            row[0] = MainView.Results.Rows.Count + 1;
            row[1] = time.TimeSpan.ToString();

            var value = string.Empty;

            if (Stoped)
            {
                value = string.Format("{0} : {1} : {2}",    time.Min.ToString("00"),
                                                            time.Sec.ToString("00"),
                                                            time.Mlsec.ToString("000"));

                Stoped = false;
            }
            else
            {
                var previousRow = MainView.Results.Rows[MainView.Results.Rows.Count - 1];
                var timeSpan = time.TimeSpan - TimeSpan.Parse(previousRow[1].ToString());

                value = string.Format("{0} : {1} : {2}",    timeSpan.Minutes.ToString("00"),
                                                            timeSpan.Seconds.ToString("00"),
                                                            timeSpan.Milliseconds.ToString("000"));
            }

            if (LeftRight)
            {
                row[2] = value;
            }
            else
            {
                row[3] = value;
            }

            if (!Reverse)
            {
                LeftRight = !LeftRight;
            }

            MainView.Results.Rows.Add(row);
        }

        /// <summary>
        /// Инициировать флаг реверса выводимой отсечки.
        /// </summary>
        public void ReverseChange()
        {
            Reverse = !Reverse;
            LeftRight = !LeftRight;
        }

        /// <summary>
        /// Очистка таблицы с результатами.
        /// </summary>
        public void ClearResultsTable()
        {
            MainView.Results = CreateResTable();
        }

        /// <summary>
        /// Сгенерировать заголовки для таблицы результатов.
        /// </summary>
        private DataTable CreateResTable()
        {
            var table = new DataTable();

            var column = new DataColumn("№ отсечки");
            table.Columns.Add(column);

            column = new DataColumn("Отсечка");
            table.Columns.Add(column);

            column = new DataColumn("Водитель №1");
            table.Columns.Add(column);

            column = new DataColumn("Водитель №2");
            table.Columns.Add(column);

            return table;
        }

        /// <summary>
        /// Процесс, осуществляющий моментальный вывод данных секундомера на форму.
        /// </summary>
        /// <param name="mainView"></param>
        private void DataBindingProcess(IMainView mainView, StopwatchModel stopwatch)
        {
            while (true)
            {
                mainView.Min = stopwatch.Time.Min;
                mainView.Sec = stopwatch.Time.Sec;
                mainView.Mlsec = stopwatch.Time.Mlsec;
            }
        }

        /// <summary>
        /// Освободить все занимаемые объектом ресурсы.
        /// </summary>
        public void Dispose()
        {
            if (ThreadSync != null)
            {
                try
                {
                    ThreadSync.Abort();
                }
                catch (ThreadStateException)
                {
                    ThreadSync.Resume();
                    ThreadSync.Abort();
                }

                ThreadSync = null;
            }

            Stopwatch.Dispose();
        }

        #endregion
    }
}
