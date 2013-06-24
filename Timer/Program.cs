using System;
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
        /// Задать или получить интерфейс на сплеш скрин приложения.
        /// </summary>
        private static ISplashScreenView SplashScreenView { get; set; }

        /// <summary>
        /// Задать или получить интерфейс на главное окно приложения.
        /// </summary>
        private static IMainView MainView { get; set; }

        /// <summary>
        /// Задать или получить интерфейс на второй экран приложения.
        /// </summary>
        private static ISecondMonitorView SecondMonitorView { get; set; }

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

            SecondMonitorView secondMonitor = null;

            if (ScreenManager.MonitorCount > 1)
            {
                secondMonitor = new SecondMonitorView();
                secondMonitor.SetDesktopLocation(ScreenManager.ScreenPoints[1].X, ScreenManager.ScreenPoints[1].Y);
                secondMonitor.Show();
            }

            MainView = new MainView(secondMonitor);
            MainView.SetDesktopLocation(ScreenManager.ScreenPoints[0].X, ScreenManager.ScreenPoints[0].Y);

            SplashScreenView.CloseSplashScreen();

            Application.Run(MainView as Form);
        }

        #endregion
    }
}
