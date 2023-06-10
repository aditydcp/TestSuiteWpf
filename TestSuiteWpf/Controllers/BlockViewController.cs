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

        public bool IsOnFeedbackState { get; private set; }

        // local references
        private TrialData trial;
        private int score;
        private Question question;
        private int level;
        private QuestionSet activeQuestionSet;
        /// <summary>
        /// Local reference to this current block's duration
        /// </summary>
        private readonly int blockDuration;

        public void SubmitAnswer(int answer)
        {
            Result result = question.CheckAnswer(answer);
            switch (result)
            {
                case Result.Correct:
                    score++;
                    SaveData(result, score, answer);
                    // start a new question
                    StartNewQuestion();
                    break;
                default:
                    score--;
                    SaveData(result, score, answer);
                    // Enter feedback state if wrong
                    EnterFeedbackState();
                    break;
            }
        }

        private void FinishBlock()
        {
            StopAllTimer();
            App.BlockData.EndTime = DateTime.Now;
            App.BlockData.CalculateBlockData();
            App.Subject.SaveBlockData(App.BlockData);
            view.FinishBlock();
        }

        private void SaveData(Result result, int score, int answer)
        {
            // complete trial data
            trial.EnterData(result, score, DateTime.Now, question, answer);
            // save the trial data to the block
            App.BlockData.SaveTrialData(trial);
            // quick calculate current stack of data
            App.BlockData.QuickCalculateBlockData();
        }

        #region property setter functions
        private void StartNewQuestion()
        {
            // setup new trial data
            trial = new TrialData(score);

            // setup new question environment
            SetLevel();
            SetActiveQuestionSet();
            question = GetRandomQuestion();
            ResetQuestionTimer();
            StartBlockTimer();
            StartQuestionTimer();
            ControlView();
        }

        private int SetLevel()
        {
            return level = score switch
            {
                <= 35 => 1,
                <= 40 => 2,
                <= 45 => 3,
                <= 50 => 4,
                > 50 => 5,
            };
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
        #endregion

        public Question GetRandomQuestion() { return activeQuestionSet.GetRandomQuestion(); }

        public void ControlView()
        {
            //view.SetLevelText(level);
            view.SetScoreText(score);
            view.SetQuestionText(question.QuestionText);
            view.SetTimerText(GetTimeString(App.TrialDuration - questionTimerInSeconds));

            view.SetBlockTimerText(GetTimeString(blockDuration - blockTimerInSeconds));
        }

        #region Feedback State
        private void EnterFeedbackState()
        {
            // stops all other timer
            //StopBlockTimer();
            StopQuestionTimer();

            // start feedback timer
            IsOnFeedbackState = true;
            view.ShowFeedback(true);
            StartFeedbackTimer();
        }

        private void ExitFeedbackState()
        {
            IsOnFeedbackState = false;
            StopFeedbackTimer();
            view.ShowFeedback(false);

            //start a new question
            StartNewQuestion();
        }
        #endregion

        #region Timers
        public void StartBlockTimer() { if (!blockTimer.IsEnabled) blockTimer.Start(); }
        public void StopBlockTimer() { if (blockTimer.IsEnabled) blockTimer.Stop(); }

        public void StartQuestionTimer() { if (!questionTimer.IsEnabled) questionTimer.Start(); }
        public void StopQuestionTimer() { if (questionTimer.IsEnabled) questionTimer.Stop(); }
        public void ResetQuestionTimer()
        {
            StopQuestionTimer();
            questionTimerInSeconds = 0;
            StartQuestionTimer();
        }

        public void StartFeedbackTimer() { if (!feedbackTimer.IsEnabled) feedbackTimer.Start(); }
        public void StopFeedbackTimer() { if (feedbackTimer.IsEnabled) feedbackTimer.Stop(); }

        public void StopAllTimer()
        {
            StopBlockTimer();
            StopFeedbackTimer();
            StopQuestionTimer();
        }

        #region Timer Ticks functions
        private void BlockTimerTicks(object? sender, EventArgs e)
        {
            blockTimerInSeconds++;
            view.SetBlockTimerText(GetTimeString(blockDuration - blockTimerInSeconds));
            if (blockTimerInSeconds >= blockDuration) { FinishBlock(); }
        }

        private void QuestionTimerTicks(object? sender, EventArgs e)
        {
            questionTimerInSeconds++;
            view.SetTimerText(GetTimeString((App.TrialDuration - questionTimerInSeconds)));
            if (questionTimerInSeconds >= App.TrialDuration) { SubmitAnswer(-1); }
        }

        private void FeedbackTimerTicks(object? sender, EventArgs e)
        {
            ExitFeedbackState();
        }
        #endregion
        #endregion

        public string GetTimeString(long timeInSeconds)
        {
            return ((timeInSeconds) / 60 / 10).ToString() + ((timeInSeconds) / 60 % 10).ToString() +
                ":" + ((timeInSeconds) % 60 / 10).ToString() + ((timeInSeconds) % 60 % 10).ToString(); 
        }

        public BlockViewController(BlockView view)
        {
            // setup reference to view and repository
            this.view = view;
            questionSetRepository = new QuestionSetRepository();
            questionSets = questionSetRepository.GetAllQuestionSets();

            // setup block timer
            blockTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            blockTimer.Tick += BlockTimerTicks;
            blockTimerInSeconds = 0;

            // setup the question timer
            questionTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            questionTimer.Tick += QuestionTimerTicks;
            questionTimerInSeconds = 0;

            // setup the feedback timer
            feedbackTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(App.FeedbackDuration)
            };
            feedbackTimer.Tick += FeedbackTimerTicks;

            // set initial properties
            App.BlockData.StartTime = DateTime.Now;
            if (App.Stage == Stages.Trial) { blockDuration = App.IntroDuration; }
            else { blockDuration = App.BlockDuration; }
            IsOnFeedbackState = false;
            score = App.InitialScore;
            level = 1;
            activeQuestionSet = questionSets.First();
            question = GetRandomQuestion();
            trial = new TrialData();
            StartNewQuestion();
        }
    }
}
