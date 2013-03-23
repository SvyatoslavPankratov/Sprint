using System;
using System.Windows.Forms;

using Sprint.Managers;
using Sprint.Views;

namespace Sprint
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var sm = new ScreenManager();

            var splashScreen = new SplashScreenView();
            splashScreen.SetDesktopLocation(sm.ScreenPoints[0].X, sm.ScreenPoints[0].Y);
            splashScreen.Show();

            SecondMonitorView secondMonitor = null;

            if (sm.MonitorCount > 1)
            {
                secondMonitor = new SecondMonitorView();
                secondMonitor.SetDesktopLocation(sm.ScreenPoints[1].X, sm.ScreenPoints[1].Y);
                secondMonitor.Show();
            }

            var mainView = new MainView(splashScreen, secondMonitor);
            mainView.SetDesktopLocation(sm.ScreenPoints[0].X, sm.ScreenPoints[0].Y);

            Application.Run(mainView);
        }
    }
}
