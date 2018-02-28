using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow window = new WpfApp1.MainWindow();
            window.Show();

            //Open the file of filepath stated with default application
            if (e.Args.Length > 0)
            {
                System.Diagnostics.Process.Start(e.Args[0]);
            }
        }
    }
}
