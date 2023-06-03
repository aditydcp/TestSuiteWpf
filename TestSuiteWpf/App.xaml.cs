using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestSuiteWpf.ViewModels;

namespace TestSuiteWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Global score reference
        public static int Score { get; set; } = 0;

        public enum Stages { Trial, First, Second, Third }
        // Global state reference
        public static Stages Stage { get; set; } = Stages.Trial;

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
