using System;
using System.Threading;

namespace Sprint.Models
{
    /// <summary>
    /// Модель секундомера.
    /// </summary>
    public class StopwatchModel : IDisposable
    {
        #region Properties

        /// <summary>
        /// Задать или получить время для отсчета секундомера.
        /// </summary>
        private DateTime TimeStart { get; set; }

        /// <summary>
        /// Задать или получить поток для секундомера.
        /// </summary>
        private Thread Thread { get; set; }

        /// <summary>
        /// Получить текущее время на секундомере (задавать можно только внутри модели).
        /// </summary>
        public TimeModel Time { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        /// Запустить секундомер.
        /// </summary>
        public void Start()
        {
            Dispose();

            TimeStart = DateTime.Now;
            Time = new TimeModel();

            Thread = new Thread(() => StopwatchProc(Time, TimeStart));
            Thread.Start();
        }

        /// <summary>
        /// Остановить секундомер.
        /// </summary>
        public void Stop()
        {
            Thread.Suspend();
        }

        /// <summary>
        /// Возобновить секундомер.
        /// </summary>
        public void Continue()
        {
            Thread.Resume();
        }
        
        /// <summary>
        /// Освободить все занимаемые объектом ресурсы.
        /// </summary>
        public void Dispose()
        {
            if (Thread != null)
            {
                try
                {
                    Thread.Abort();
                }
                catch (ThreadStateException)
                {
                    Thread.Resume();
                    Thread.Abort();
                }

                Thread = null;

                Time = null;
            }
        }

        /// <summary>
        /// Процесс секундомера.
        /// </summary>
        private void StopwatchProc(TimeModel time, DateTime timeStart)
        {
            while (true)
            {
                var StopwatchTime = (DateTime.Now - timeStart);

                time.Min    = StopwatchTime.Minutes;
                time.Sec    = StopwatchTime.Seconds;
                time.Mlsec  = StopwatchTime.Milliseconds;

                time.TimeSpan = StopwatchTime;
            }
        }

        #endregion
    }
}
