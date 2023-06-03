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

namespace TestSuiteWpf.Pages
{
    /// <summary>
    /// Interaction logic for LandingPage.xaml
    /// </summary>
    public partial class LandingPage : Page
    {
        public LandingPage()
        {
            InitializeComponent();
            LoadContent();
        }

        private void LoadContent()
        {
            // hard-coded text values
            TitleLabel.Content = "Math Task";
            DescriptionTextBlock.Text = 
                "You will be shown arithmetic problems to solve.\n" +
                "You have a max of 5 seconds for each problem.\n" +
                "All problems have solutions ranging from 0 - 9.\n\n" +
                "A response dial with buttons of 0 - 9 will appear on the screen.\n" +
                "Press the button that corresponds to the solution.\n\n" +
                "The test will be scored.\n" +
                "Each correct answer will increase the score.\n" +
                "Incorrect answers and timeouts will deduct the score.\n" +
                "If you make a mistake, a visual feedback will be presented to remind you to be more careful.\n\n" +
                "Press the Continue button to start a demo trial.";
            NextButton.Content = "Continue";
        }

        private void OnNextButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BlockIntro());
        }
    }
}
