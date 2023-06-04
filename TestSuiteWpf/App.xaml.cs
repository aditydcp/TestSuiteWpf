using System.Windows;
using TestSuiteWpf.Models;

namespace TestSuiteWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //// Global score reference
        //public static int Score { get; set; } = 0;

        // Global state reference
        public static Stages Stage { get; set; } = Stages.Trial;

        // Global subject data reference
        public SubjectData Subject { get; set; } = new SubjectData();

        // Global block data reference
        public BlockData BlockData { get; set; } = new BlockData(Stage);

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            //{
            //    DataContext = new MainController()
            //};
            ;
            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}
