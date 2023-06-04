using System.Windows;
using System.Windows.Input;
using TestSuiteWpf.Pages;

namespace TestSuiteWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnMainFrameLoaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new LandingPage();
            ControlFontSize();
        }

        private void MainFrame_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ControlFontSize();
        }

        /// <summary>
        /// Get desired font size based on the Frame's height
        /// and set it to text font size variables
        /// </summary>
        private void ControlFontSize()
        {
            // Font size controller
            double sizeFactor = ((MainFrame.ActualHeight / 12) / 3 * 2) / 5;
            // size factor table
            // 1080 -> 12
            // 720 -> 8
            // 480 -> 5.333

            double titleFontSize = sizeFactor * 5.5;
            double bodyFontSize = sizeFactor * 2.5;
            double buttonFontSize = sizeFactor * 3;
            double subBodyFontSize = sizeFactor * 2;

            Application.Current.Resources.Remove("TitleFontSize");
            Application.Current.Resources.Add("TitleFontSize", titleFontSize);
            Application.Current.Resources.Remove("BodyFontSize");
            Application.Current.Resources.Add("BodyFontSize", bodyFontSize);
            Application.Current.Resources.Remove("ButtonFontSize");
            Application.Current.Resources.Add("ButtonFontSize", buttonFontSize);
            Application.Current.Resources.Remove("SubBodyFontSize");
            Application.Current.Resources.Add("SubBodyFontSize", subBodyFontSize);

            //// debug only
            //ViewportSizeLabel.Content = MainFrame.ActualHeight + "x" + MainFrame.ActualWidth;
            //viewportSize = MainFrame.ActualHeight + "x" + MainFrame.ActualWidth;
        }

        private void OnWindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ViewportSizeLabel.Content = MainFrame.ActualHeight + "x" + MainFrame.ActualWidth;
            //viewportSize = MainFrame.ActualHeight + "x" + MainFrame.ActualWidth;
        }
    }
}
