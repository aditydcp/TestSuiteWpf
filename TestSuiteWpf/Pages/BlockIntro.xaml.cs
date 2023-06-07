using System.Linq;
using System.Text;
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
                        "This block will go for " + ToDurationString(App.BlockDuration) + ".\n" +
                        "Your goal is to reach the highest score you can.\n\n" +
                        "Press Start button to start the test.";
                    break;
                case Models.Stages.Second:
                    DescriptionTextBlock.Text =
                        "Get ready to start the second block.\n\n" +
                        "This block will go for " + ToDurationString(App.BlockDuration) + ".\n" +
                        "Your goal is to reach the highest score you can.\n\n" +
                        "Press Start button to start the test.";
                    break;
                case Models.Stages.Third:
                    DescriptionTextBlock.Text =
                        "Get ready to start the third block.\n\n" +
                        "This block will go for " + ToDurationString(App.BlockDuration) + ".\n" +
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

        private static string ToDurationString(int seconds)
        {
            StringBuilder @string = new StringBuilder();
            if (seconds <= 60)
            {
                @string.Append(seconds.ToString() + " seconds");
            }
            else
            {
                int minutes = seconds / 60;
                int trailingSeconds = seconds % 60;

                @string.Append(minutes.ToString() + " minutes");

                if (trailingSeconds > 0)
                {
                    @string.Append(" and ");
                    @string.Append(trailingSeconds.ToString() + " seconds");
                }
            }

            return @string.ToString();
        }
    }
}
