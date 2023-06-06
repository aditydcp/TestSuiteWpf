using CsvHelper;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Media;
using TestSuiteWpf.Models;

namespace TestSuiteWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        // Global state reference
        public static Stages Stage { get; set; } = Stages.Trial;

        // Global subject data reference
        public static SubjectData Subject { get; set; } = new SubjectData();

        // Global block data reference
        public static BlockData BlockData { get; set; } = new BlockData(Stage);

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new SubjectForm();
            //MainWindow = new MainWindow()
            //{
            //    DataContext = new MainController()
            //};
            ;
            MainWindow.Show();

            base.OnStartup(e);
        }

        /// <summary>
        /// Refreshes <see cref="App.BlockData"/> to a new instance
        /// with the current value of <see cref="App.Stage"/>.
        /// </summary>
        public static void ResetBlockData()
        {
            BlockData = new BlockData(Stage);
        }

        public static void SaveDataAsCsv()
        {
            using var writer = new StreamWriter("C:\\Users\\123\\Desktop\\test\\file.csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(Subject.Blocks);
        }
        public static void SaveDataAsCsv(string filename)
        {
            using var writer = new StreamWriter("C:\\Users\\123\\Desktop\\test\\" + filename + ".csv");
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csv.WriteRecords(Subject.Blocks);
        }

        public static T? FindParentOfType<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentDepObj = child;
            do
            {
                parentDepObj = VisualTreeHelper.GetParent(parentDepObj);
                T parent = parentDepObj as T;
                if (parent != null) return parent;
            }
            while (parentDepObj != null);
            return null;
        }

        // Global static parameters
        /// <summary>
        /// Starting score for each block.
        /// </summary>
        public static int InitialScore { get; } = 30;

        /// <summary>
        /// Duration for each trial/question in seconds.
        /// </summary>
        public static int TrialDuration { get; } = 5;

        /// <summary>
        /// Duration for each block in seconds.
        /// </summary>
        public static int BlockDuration { get; } = 1 * 20;

        /// <summary>
        /// Duration for the visual feedback in ms.
        /// </summary>
        public static int FeedbackDuration { get; } = 500;
    }
}
