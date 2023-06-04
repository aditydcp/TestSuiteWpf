using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace TestSuiteWpf.Pages
{
    /// <summary>
    /// Interaction logic for BlockIntro.xaml
    /// </summary>
    public partial class BlockIntro : Page
    {
        public BlockIntro()
        {
            InitializeComponent();
            LoadContent();
        }

        private void LoadContent()
        {
            // hard-coded text values
            switch (App.Stage)
            {
                case Models.Stages.Trial:
                    DescriptionTextBlock.Text =
                        "This is only a trial block.\n" +
                        "A number of questions will be presented " +
                        "to get you accustomed with the test environment.\n\n" +
                        "Don't worry, this part will not be recorded.\n\n" +
                        "Press Start button to start trial.";
                    break;
                case Models.Stages.First:
                    DescriptionTextBlock.Text =
                        "Get ready to start the first block.\n\n" +
                        "This block will go for 3 minutes.\n" +
                        "Your goal is to reach the highest score you can.\n\n" +
                        "Press Start button to start the test.";
                    break;
                case Models.Stages.Second:
                    DescriptionTextBlock.Text =
                        "Get ready to start the second block.\n\n" +
                        "This block will go for 3 minutes.\n" +
                        "Your goal is to reach the highest score you can.\n\n" +
                        "Press Start button to start the test.";
                    break;
                case Models.Stages.Third:
                    DescriptionTextBlock.Text =
                        "Get ready to start the third block.\n\n" +
                        "This block will go for 3 minutes.\n" +
                        "Your goal is to reach the highest score you can.\n\n" +
                        "Press Start button to start the test.";
                    break;
            }

            TitleLabel.Content = "Math Task";
            NextButton.Content = "Start";
        }

        private void OnNextButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new BlockView());
        }
    }
}
