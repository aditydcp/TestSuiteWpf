using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestSuiteWpf.Pages;

namespace TestSuiteWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public string viewportSize { get; set; } = "0x0";

        public MainWindow()
        {
            InitializeComponent();
            //DataContext = this;
        }

        private void OnMainFrameLoaded(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new LandingPage();
            ControlFontSize();
            //viewportSize = MainFrame.ActualHeight + "x" + MainFrame.ActualWidth;
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

            Application.Current.Resources.Remove("TitleFontSize");
            Application.Current.Resources.Add("TitleFontSize", titleFontSize);
            Application.Current.Resources.Remove("BodyFontSize");
            Application.Current.Resources.Add("BodyFontSize", bodyFontSize);
            Application.Current.Resources.Remove("ButtonFontSize");
            Application.Current.Resources.Add("ButtonFontSize", buttonFontSize);

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
