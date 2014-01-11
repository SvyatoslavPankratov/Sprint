using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Windows.Forms;

using Sprint.Interfaces;
using Sprint.Managers;
using Sprint.Views;

namespace Sprint
{
    static class Program
    {
        #region Свойства

        /// <summary>
        /// Задать или получить менеджер по работе с экранами.
        /// </summary>
        private static ScreenManager ScreenManager { get; set; }

        /// <summary>
        /// Задать или получить фонового работника для выполнения операций в фоновом режиме.
        /// </summary>
        private static BackgroundWorker BackgroundWorker { get; set; }

        /// <summary>
        /// Задать или получить интерфейс на сплеш скрин приложения.
        /// </summary>
        private static ISplashScreen SplashScreenView { get; set; }

        /// <summary>
        /// Задать или получить интерфейс на главное окно приложения.
        /// </summary>
        private static IMain MainView { get; set; }

        /// <summary>
        /// Задать или получить интерфейс на второй экран приложения.
        /// </summary>
        private static ISecondMonitor SecondMonitorView { get; set; }

        #endregion

        #region Методы

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ScreenManager = new ScreenManager();

            SplashScreenView = new SplashScreenView();
            SplashScreenView.SetDesktopLocation(ScreenManager.ScreenPoints[0].X, ScreenManager.ScreenPoints[0].Y);
            SplashScreenView.Show();
            SplashScreenView.Refresh();

            //BackgroundWorker = new BackgroundWorker {WorkerSupportsCancellation = true};
            //BackgroundWorker.DoWork += (sender, args) =>
            //                            {
            //                                ((SplashScreenView)args.Argument).Show();
            //                                ((SplashScreenView)args.Argument).Refresh();
            //                            };
            //BackgroundWorker.RunWorkerAsync(SplashScreenView);

            CheckFreeSpace();

            StartWcfService();

            if (ScreenManager.MonitorCount > 1)
            {
                SecondMonitorView = new SecondMonitorView();
                SecondMonitorView.SetDesktopLocation(ScreenManager.ScreenPoints[1].X, ScreenManager.ScreenPoints[1].Y);
                SecondMonitorView.Show();
            }

            MainView = new MainView(SecondMonitorView);
            MainView.SetDesktopLocation(ScreenManager.ScreenPoints[0].X, ScreenManager.ScreenPoints[0].Y);

            //BackgroundWorker.Dispose();
            SplashScreenView.Close();

            Application.Run(MainView as Form);
        }

        /// <summary>
        /// Проверка количества свободного места на диске с приложением
        /// </summary>
        private static void CheckFreeSpace()
        {
            var dir_info = new DirectoryInfo(Application.StartupPath);

            if (dir_info != null)
            {
                var disc_name = dir_info.Root.FullName;
                var free_space = DiscSpaceManager.GetFreeSpaceValue(disc_name)/1024.0/1024.0;

                if (ConfigurationSettings.AppSettings != null)
                {
                    var min_free_space = long.Parse(ConfigurationSettings.AppSettings["min_free_space"]);

                    if (free_space < min_free_space)
                    {
                        var message = "Внимание!" + Environment.NewLine +
                                      "На диске не достаточно места для корректной работы приложения." + Environment.NewLine +
                                      "После запуска программы, удалите, пожалуйста, все данные, хранимые приложением.";

                        MessageBox.Show(message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static void StartWcfService()
        {

        }

        #endregion
    }
}
