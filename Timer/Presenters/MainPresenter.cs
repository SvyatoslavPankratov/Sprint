using System;
using System.Data;
using System.Threading;

using Sprint.Models;
using Sprint.Views;

namespace Sprint.Presenters
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
        }

        #endregion

        #region Methods
        
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
            
            MainView.Min = Stopwatch.Time.Min;
            MainView.Sec = Stopwatch.Time.Sec;
            MainView.Mlsec = Stopwatch.Time.Mlsec;
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

            MainView.Results.Rows.Add(row);
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
