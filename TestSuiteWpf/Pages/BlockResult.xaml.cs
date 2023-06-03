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
    /// Interaction logic for BlockResult.xaml
    /// </summary>
    public partial class BlockResult : Page
    {
        public BlockResult()
        {
            InitializeComponent();
            LoadContent();
        }

        private void LoadContent()
        {
            // hard-coded text values
            switch (App.Stage)
            {
                case App.Stages.Trial:
                    NavigateToNextSection();
                    break;
                case App.Stages.First:
                    TitleTextBlock.Text = "You have finished the first block!";
                    break;
                case App.Stages.Second:
                    TitleTextBlock.Text = "You have finished the second block!";
                    break;
                case App.Stages.Third:
                    TitleTextBlock.Text = "You have finished the third block!";
                    break;
            }
            DescriptionTextBlock.Text =
                        "You attempted a total of " + questionCount + " trials\n\n" +
                        "Accuracy (percentage of trials answered correctly) : " + accuracy + "%\n\n" +
                        "Mean reaction time of correct responses (in ms) : " + meanReactionTimeInMs + " ms";
            NextButton.Content = "Start";
        }

        private void NavigateToNextSection()
        {
            App.Stage++;
            NavigationService.Navigate(new BlockIntro());
        }

        private void OnNextButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigateToNextSection();
        }
    }
}
