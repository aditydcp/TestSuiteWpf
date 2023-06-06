using System;
using System.Collections.Generic;
using System.Globalization;
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
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            LoadContent();
        }

        private void LoadContent()
        {
            // hard-coded text values
            switch (App.Stage)
            {
                case Models.Stages.Trial:
                    NavigateToNextSection();
                    break;
                case Models.Stages.First:
                    TitleTextBlock.Text = "You have finished the first block!";
                    break;
                case Models.Stages.Second:
                    TitleTextBlock.Text = "You have finished the second block!";
                    break;
                case Models.Stages.Third:
                    TitleTextBlock.Text = "You have finished the third block!";
                    break;
            }
            DescriptionTextBlock.Text =
                        "You attempted a total of " + App.BlockData.TrialsCount + " trials\n\n" +
                        "Accuracy (percentage of trials answered correctly) : " + App.BlockData.Accuracy.ToString("F", CultureInfo.InvariantCulture) + "%\n\n" +
                        "Mean reaction time of correct responses (in ms) : " + App.BlockData.MeanReactionTimeOnCorrectTrials.ToString("F", CultureInfo.InvariantCulture) + " ms";
            NextButton.Content = "Start";
        }

        private void NavigateToNextSection()
        {
            if (App.Stage == Models.Stages.Third)
            {
                NavigationService.Navigate(new ClosingPage());
                return;
            }
            App.Stage++;
            App.ResetBlockData();
            NavigationService.Navigate(new BlockIntro());
        }

        private void OnNextButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigateToNextSection();
        }
    }
}
