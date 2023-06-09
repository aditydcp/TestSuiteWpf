﻿using System;
using System.Windows;
using System.Windows.Controls;
using TestSuiteWpf.ViewModels;

namespace TestSuiteWpf.Pages
{
    /// <summary>
    /// Interaction logic for BlockView.xaml
    /// </summary>
    public partial class BlockView : Page
    {
        private BlockViewController? controller;
        private MainWindow? window;

        public BlockView()
        {
            InitializeComponent();
        }

        private void OnPageSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ControlButtons();
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            controller = new BlockViewController(this);
            ControlButtons();
            controller.ControlView();
            controller.StartBlock();

            // debug only
            window = App.FindParentOfType<MainWindow>(this) as MainWindow;
            window?.SetConsoleText();
        }

        private void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            controller!.StopAllTimer();
            controller = null;
        }

        private void OnAnswerButtonClicked(object sender, RoutedEventArgs e)
        {
            if (!controller!.IsOnFeedbackState)
            {
                // get chosen answer
                var tag = ((Button)sender).Tag.ToString();
                if (tag == null) { return; }

                controller!.SubmitAnswer(int.Parse(tag));
            }
        }

        public void FinishBlock()
        {
            NavigationService.Navigate(new BlockResult());
        }

        #region UI Setters
        public void SetScoreText(string score) { ScoreLabel.Text = score; }
        public void SetScoreText(int score) { ScoreLabel.Text = score.ToString(); }

        //public void SetLevelText(string level) { Level.Text = level; }
        //public void SetLevelText(int level) { Level.Text = level.ToString(); }

        public void SetTimerText(string timeString) { TimerLabel.Text = timeString; }

        public void SetQuestionText(string question) { QuestionLabel.Text = question; }

        public void ShowFeedback(bool show)
        {
            if (show) { FeedbackContainer.Visibility = Visibility.Visible; }
            else { FeedbackContainer.Visibility = Visibility.Collapsed; }
        }

        // debug only
        public void SetBlockTimerText(string timeString) { BlockTimerLabel.Text = timeString; }

        /// <summary>
        /// Set up the sizes and spacings of the answer buttons
        /// </summary>
        private void ControlButtons()
        {
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
                TimerLabel.Padding = new Thickness(20);
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
                TimerLabel.Padding = new Thickness(10);
            }

            double verticalScaleFactor = ActualHeight / 3.9;
            double horizontalScaleFactor = ActualWidth / 8.6;

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
        #endregion
    }
}
