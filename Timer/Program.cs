using System;
using System.Windows.Forms;

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

            var splashScreen = new SplashScreenView();
            splashScreen.Show();

            Application.Run(new MainView(splashScreen));
        }
    }
}
