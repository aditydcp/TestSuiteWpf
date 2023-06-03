using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Threading;
using TestSuiteWpf.Models;
using TestSuiteWpf.Pages;
using TestSuiteWpf.Repositories;

namespace TestSuiteWpf.ViewModels
{
    internal class BlockViewController
    {
        private readonly BlockView view;
        private readonly QuestionSetRepository questionSetRepository;
        private readonly IEnumerable<QuestionSet> questionSets;

        private readonly DispatcherTimer blockTimer;
        private long blockTimerInSeconds;
        private readonly DispatcherTimer questionTimer;
        private long questionTimerInSeconds;
        private readonly DispatcherTimer feedbackTimer;
        private long feedbackTimerInMilliseconds;

        // local references
        private int score;
        private Question question;
        private int level;
        private QuestionSet activeQuestionSet;

        public void SubmitAnswer(int answer)
        {
            if (question.CheckAnswer(answer)) score++;
            else
            {
                score--;
                // Enter feedback state if wrong
                EnterFeedbackState();
                return;
            }
        }

        private void StartNewQuestion()
        {
            SetLevel();
            SetActiveQuestionSet();
            question = GetRandomQuestion();
            ResetQuestionTimer();
            ControlView();
        }

        private int SetLevel()
        {
            switch (score)
            {
                case <= 35:
                    level = 1;
                    break;
                case <= 40:
                    level = 2;
                    break;
                case <= 45:
                    level = 3;
                    break;
                case <= 50:
                    level = 4;
                    break;
                case > 50:
                    level = 5;
                    break;
            }
            return level;
        }

        private void SetActiveQuestionSet()
        {
            QuestionSet questionSet = questionSets.First();
            foreach (QuestionSet set in questionSets)
            {
                if (set.Level == level) questionSet = set;
            }
            activeQuestionSet = questionSet;
        }

        public Question GetRandomQuestion() { return activeQuestionSet.GetRandomQuestion(); }

        public void ControlView()
        {
            //view.SetLevelText(level);
            view.SetScoreText(score);
            view.SetQuestionText(question.QuestionText);
            view.SetTimerText(GetTimeString(questionTimerInSeconds));
        }

        private void EnterFeedbackState()
        {
            // stops all other timer
            StopBlockTimer();
            StopQuestionTimer();
            // start feedback timer
            view.ShowFeedback(true);
            StartFeedbackTimer();
        }

        public void StartBlockTimer() { blockTimer.Start(); }
        public void StopBlockTimer() { blockTimer.Stop(); }

        public void StartQuestionTimer() { questionTimer.Start(); }
        public void StopQuestionTimer() { if (questionTimer.IsEnabled) questionTimer.Stop(); }
        public void ResetQuestionTimer()
        {
            StopQuestionTimer();
            questionTimerInSeconds = 5;
            StartQuestionTimer();
        }

        public void StartFeedbackTimer() { feedbackTimer.Start(); }
        public void StopFeedbackTimer() { feedbackTimer.Stop(); }

        private void BlockTimerTicks(object? sender, EventArgs e)
        {
            // TODO: Handle when block ends
            blockTimerInSeconds++;
        }

        private void QuestionTimerTicks(object? sender, EventArgs e)
        {
            questionTimerInSeconds--;
            if (questionTimerInSeconds <= 0) { SubmitAnswer(-1); }
            view.SetTimerText(GetTimeString(questionTimerInSeconds));
        }

        private void FeedbackTimerTicks(object? sender, EventArgs e)
        {
            feedbackTimerInMilliseconds--;
            // when timeout (after 500 ms)
            if (feedbackTimerInMilliseconds <= 0)
            {
                // goes out of feedback state and to next question
                StopFeedbackTimer();
                view.ShowFeedback(false);
                // set the timer back to 500 ms
                feedbackTimerInMilliseconds = 500;
            }
        }

        public string GetTimeString(long timeInSeconds)
        {
            return ((timeInSeconds + 1) / 60 / 10).ToString() + ((timeInSeconds + 1) / 60 % 10).ToString() +
                ":" + ((timeInSeconds + 1) % 60 / 10).ToString() + ((timeInSeconds + 1) % 60 % 10).ToString(); 
        }

        public BlockViewController(BlockView view)
        {
            this.view = view;
            questionSetRepository = new QuestionSetRepository();
            questionSets = questionSetRepository.GetAllQuestionSets();

            blockTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            blockTimer.Tick += BlockTimerTicks;
            blockTimerInSeconds = 0;

            questionTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            questionTimer.Tick += QuestionTimerTicks;
            questionTimerInSeconds = 5;

            feedbackTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1)
            };
            feedbackTimer.Tick += FeedbackTimerTicks;
            feedbackTimerInMilliseconds = 500;

            score = 30;
            level = 1;
            activeQuestionSet = questionSets.First();
            question = GetRandomQuestion();
        }
    }
}
