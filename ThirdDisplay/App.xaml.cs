using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using ThirdDisplay.ViewModels;
using ThirdDisplay.Views;

namespace ThirdDisplay
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var tm = new ThirdMonitorView
                        {
                            DataContext = new ThirdMonitorViewModel()
                        };

            tm.Show();
        }        
    }
}
