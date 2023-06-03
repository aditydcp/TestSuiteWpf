using System.Collections.Generic;
using TestSuiteWpf.Models;
using TestSuiteWpf.Repositories;

namespace TestSuiteWpf.ViewModels
{
    internal class BlockViewModel : ViewModel
    {
        private readonly QuestionSetRepository questionSetRepository;
        private readonly IEnumerable<QuestionSet> questionSets;

        private int _score;
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        private string _question = string.Empty;
        public string Question
        {
            get { return _question; }
            set
            {
                _question = value;
                OnPropertyChanged(nameof(Question));
            }
        }

        public void SubmitAnswer(int answer)
        {
            Question = answer.ToString();
        }

        public BlockViewModel()
        {
            questionSetRepository = new QuestionSetRepository();
            questionSets = questionSetRepository.GetAllQuestionSets();
            Score = 30;
            Question = "Question";
        }
    }
}
