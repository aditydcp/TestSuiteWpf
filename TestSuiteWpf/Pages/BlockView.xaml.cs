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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestSuiteWpf.ViewModels;

namespace TestSuiteWpf.Pages
{
    /// <summary>
    /// Interaction logic for BlockView.xaml
    /// </summary>
    public partial class BlockView : Page
    {
        /// <summary>
        /// Local reference to Score.
        /// Initial score is 30.
        /// </summary>
        //private int score = 30;

        //private Question? currentQuestion = null;
        private readonly BlockViewModel viewModel;

        //private int hitCount = 0;
        //private readonly double verticalScaleFactor = 2.5;

        public BlockView()
        {
            InitializeComponent();
            viewModel = new BlockViewModel();
        }

        private void OnPageSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //hitCount++;
            //QuestionLabel.Content = "Page just changed!" + hitCount;
            ControlButtons();
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            ControlButtons();
        }

        private void OnAnswerButtonClicked(object sender, RoutedEventArgs e)
        {
            // get chosen answer
            var tag = ((Button)sender).Tag.ToString();
            if (tag == null) { return; }
            //QuestionLabel.Content = tag;
            
            viewModel.SubmitAnswer(int.Parse(tag));

            // check correct or not
        }

        //private bool CheckCorrectAnswer(int answer, Question question)
        //{
        //    return question.CheckAnswer(answer);
        //}

        /// <summary>
        /// Set up the sizes and spacings of the answer buttons
        /// </summary>
        private void ControlButtons()
        {
            //double topBorder = ActualHeight / 5;
            //double bottomBorder = ActualHeight / 5 * 4;
            //double verticalCenter = ActualHeight / 2;
            //double horizontalCenter = ActualWidth / 2;

            if (ActualHeight > 720)
            {
                Button1.Padding = new Thickness(50, 15, 50, 20);
                Button2.Padding = new Thickness(50, 15, 50, 20);
                Button3.Padding = new Thickness(50, 15, 50, 20);
                Button4.Padding = new Thickness(50, 15, 50, 20);
                Button5.Padding = new Thickness(50, 15, 50, 20);
                Button6.Padding = new Thickness(50, 15, 50, 20);
                Button7.Padding = new Thickness(50, 15, 50, 20);
                Button8.Padding = new Thickness(50, 15, 50, 20);
                Button9.Padding = new Thickness(50, 15, 50, 20);
                Button0.Padding = new Thickness(50, 15, 50, 20);
            }
            else
            {
                Button1.Padding = new Thickness(35, 10, 35, 15);
                Button2.Padding = new Thickness(35, 10, 35, 15);
                Button3.Padding = new Thickness(35, 10, 35, 15);
                Button4.Padding = new Thickness(35, 10, 35, 15);
                Button5.Padding = new Thickness(35, 10, 35, 15);
                Button6.Padding = new Thickness(35, 10, 35, 15);
                Button7.Padding = new Thickness(35, 10, 35, 15);
                Button8.Padding = new Thickness(35, 10, 35, 15);
                Button9.Padding = new Thickness(35, 10, 35, 15);
                Button0.Padding = new Thickness(35, 10, 35, 15);
            }

            double verticalScaleFactor = ActualHeight / 4;
            // 600 / 5 * 2 = 240
            // 1080 / 5 * 2 = 432
            // 720 -> 288
            // 480 -> 192

            //double buttonWidth = Button1.ActualWidth;
            double horizontalScaleFactor = ActualWidth / 8.6;
            // 800 / 10 = 80

            Button1.Margin = new Thickness(
                    horizontalScaleFactor - (horizontalScaleFactor / 10),
                    0,
                    0,
                    verticalScaleFactor * 2
                );
            Button2.Margin = new Thickness(
                    horizontalScaleFactor * 2.2 - (horizontalScaleFactor / 10),
                    0,
                    0,
                    verticalScaleFactor * 1.1
                );
            Button3.Margin = new Thickness(
                    horizontalScaleFactor * 3.1 - (horizontalScaleFactor / 10),
                    0,
                    0,
                    0
                );
            Button4.Margin = new Thickness(
                    horizontalScaleFactor * 2.2 - (horizontalScaleFactor / 10),
                    verticalScaleFactor * 1.1,
                    0,
                    0
                );
            Button5.Margin = new Thickness(
                    horizontalScaleFactor - (horizontalScaleFactor / 10),
                    verticalScaleFactor * 2,
                    0,
                    0
                );
            Button6.Margin = new Thickness(
                    0,
                    verticalScaleFactor * 2,
                    horizontalScaleFactor - (horizontalScaleFactor / 10),
                    0
                );
            Button7.Margin = new Thickness(
                    0,
                    verticalScaleFactor * 1.1,
                    horizontalScaleFactor * 2.2 - (horizontalScaleFactor / 10),
                    0
                );
            Button8.Margin = new Thickness(
                    0,
                    0,
                    horizontalScaleFactor * 3.1 - (horizontalScaleFactor / 10),
                    0
                );
            Button9.Margin = new Thickness(
                    0,
                    0,
                    horizontalScaleFactor * 2.2 - (horizontalScaleFactor / 10),
                    verticalScaleFactor * 1.1
                );
            Button0.Margin = new Thickness(
                    0,
                    0,
                    horizontalScaleFactor - (horizontalScaleFactor / 10),
                    verticalScaleFactor * 2
                );
        }
    }
}
