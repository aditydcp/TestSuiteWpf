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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Page = System.Windows.Controls.Page;

namespace TestSuiteWpf.Pages
{
    /// <summary>
    /// Interaction logic for ClosingPage.xaml
    /// </summary>
    public partial class ClosingPage : Page
    {
        public ClosingPage()
        {
            InitializeComponent();
        }

        private void OnSaveButtonClicked(object sender, RoutedEventArgs e)
        {
            App.SaveRunData("file_on_save");
        }

        private void OnNextButtonClicked(object sender, RoutedEventArgs e)
        {
            // save this run's data
            App.Subject.CollectionEndTime = DateTime.Now;
            App.SaveSubjectData();
            App.SaveRunData();
            
            // close the window
            MainWindow? window = App.FindParentOfType<MainWindow>(this) as MainWindow;
            window?.Close();

            // shut down the app
            Application.Current.Shutdown();
        }
    }
}
